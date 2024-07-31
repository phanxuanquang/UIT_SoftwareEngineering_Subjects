/*
 * Copyright (c) 2019 Naman Dwivedi.
 *
 * Licensed under the GNU General Public License v3
 *
 * This is free software: you can redistribute it and/or modify it
 * under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 *
 * This software is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 *
 */
package com.uitk15.mugic.ui.fragments

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.DividerItemDecoration
import androidx.recyclerview.widget.DividerItemDecoration.VERTICAL
import androidx.recyclerview.widget.LinearLayoutManager
import com.uitk15.mugic.R
import com.uitk15.mugic.databinding.FragmentPlaylistsBinding
import com.uitk15.mugic.extensions.*
import com.uitk15.mugic.models.Playlist
import com.uitk15.mugic.ui.activities.MainActivity
import com.uitk15.mugic.ui.adapters.PlaylistAdapter
import com.uitk15.mugic.ui.dialogs.CreatePlaylistDialog
import com.uitk15.mugic.ui.fragments.base.MediaItemFragment
import com.uitk15.mugic.ui.fragments.songs.CategorySongsFragment
import com.uitk15.mugic.ui.widgets.RecyclerViewItemClickListener
import com.uitk15.mugic.util.AutoClearedValue
import kotlinx.coroutines.*
import kotlin.coroutines.CoroutineContext

class PlaylistFragment : MediaItemFragment(), CreatePlaylistDialog.PlaylistCreatedCallback,
    CoroutineScope {
    var binding by AutoClearedValue<FragmentPlaylistsBinding>(this)
    private lateinit var playlistAdapter: PlaylistAdapter

    override val coroutineContext: CoroutineContext = Dispatchers.Main

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = inflater.inflateWithBinding(R.layout.fragment_playlists, container)
        return binding.root
    }

    override fun onActivityCreated(savedInstanceState: Bundle?) {
        super.onActivityCreated(savedInstanceState)

        playlistAdapter = PlaylistAdapter()
        binding.recyclerView.apply {
            layoutManager = LinearLayoutManager(safeActivity)
            adapter = playlistAdapter
            addItemDecoration(DividerItemDecoration(safeActivity, VERTICAL).apply {
                setDrawable(safeActivity.drawable(R.drawable.divider)!!)
            })
            addOnItemClick(object : RecyclerViewItemClickListener {
                override fun invoke(position: Int, view: View) {
                    synchronized(MainActivity.blockGlobalScope) {
                        MainActivity.blockGlobalScope = true
                    }
                    mainViewModel.mediaItemClicked(playlistAdapter.playlists[position], null)
                }
            })
        }

        mediaItemFragmentViewModel.mediaItems
            .observe(this) { list ->
                @Suppress("UNCHECKED_CAST")
                playlistAdapter.updateData(list as List<Playlist>)
            }

        binding.btnNewPlaylist.setOnClickListener {
            CreatePlaylistDialog.show(this@PlaylistFragment)
        }
    }

    override fun onPlaylistCreated() = mediaItemFragmentViewModel.reloadMediaItems()

    override fun onDestroy() {
        super.onDestroy()
        coroutineContext.cancelChildren()
    }

    override fun setUserVisibleHint(isVisibleToUser: Boolean) {
        super.setUserVisibleHint(isVisibleToUser)
        if (isVisibleToUser && isResumed) {
            onResume()
        }
    }

    override fun onResume() {
        super.onResume()
        if (!userVisibleHint) return

        mediaItemFragmentViewModel.reloadMediaItems()

        if (!coroutineContext.isActive) {
            GlobalScope.launch(coroutineContext) {
                while (true) {
                    if (!MainActivity.blockGlobalScope) {
                        synchronized(MainActivity.hasUpdate) {
                            if (MainActivity.hasUpdate && this@PlaylistFragment.userVisibleHint) {
                                MainActivity.hasUpdate = false
                                mediaItemFragmentViewModel.reloadMediaItems()
                            }
                        }
                    }

                    delay(2000L)
                }
            }
        }
    }
}
