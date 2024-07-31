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
package com.uitk15.mugic.ui.fragments.songs

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.lifecycle.findViewTreeLifecycleOwner
import androidx.recyclerview.widget.LinearLayoutManager
import com.uitk15.mugic.R
import com.uitk15.mugic.playback.TimberMusicService.Companion.TYPE_PLAYLIST
import com.uitk15.mugic.constants.Constants.CATEGORY_SONG_DATA
import com.uitk15.mugic.databinding.FragmentCategorySongsBinding
import com.uitk15.mugic.extensions.addOnItemClick
import com.uitk15.mugic.extensions.argument
import com.uitk15.mugic.extensions.filter
import com.uitk15.mugic.extensions.getExtraBundle
import com.uitk15.mugic.extensions.inflateWithBinding
import com.uitk15.mugic.extensions.observe
import com.uitk15.mugic.extensions.safeActivity
import com.uitk15.mugic.extensions.toSongIds
import com.uitk15.mugic.models.CategorySongData
import com.uitk15.mugic.models.Song
import com.uitk15.mugic.repository.RealPlaylistRepository
import com.uitk15.mugic.ui.activities.MainActivity
import com.uitk15.mugic.ui.adapters.SongsAdapter
import com.uitk15.mugic.ui.fragments.base.CoroutineFragment
import com.uitk15.mugic.ui.fragments.base.MediaItemFragment
import com.uitk15.mugic.util.AutoClearedValue
import kotlinx.coroutines.*
import org.koin.android.ext.android.inject
import kotlin.coroutines.CoroutineContext
import kotlin.coroutines.EmptyCoroutineContext

class CategorySongsFragment : MediaItemFragment(), CoroutineScope {
    private lateinit var songsAdapter: SongsAdapter
    private lateinit var categorySongData: CategorySongData
    var binding by AutoClearedValue<FragmentCategorySongsBinding>(this)

    private val playlistRepository by inject<RealPlaylistRepository>()

    override val coroutineContext: CoroutineContext = Dispatchers.Default

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        categorySongData = argument(CATEGORY_SONG_DATA)
        binding = inflater.inflateWithBinding(R.layout.fragment_category_songs, container)
        return binding.root
    }

    override fun onActivityCreated(savedInstanceState: Bundle?) {
        super.onActivityCreated(savedInstanceState)

        binding.categorySongData = categorySongData

        songsAdapter = SongsAdapter(this).apply {
            popupMenuListener = mainViewModel.popupMenuListener
            if (categorySongData.type == TYPE_PLAYLIST) {
                playlistId = categorySongData.id
            }
        }

        binding.recyclerView.apply {
            layoutManager = LinearLayoutManager(safeActivity)
            adapter = songsAdapter
            addOnItemClick { position: Int, _: View ->
                val extras = getExtraBundle(songsAdapter.songs.toSongIds(), categorySongData.title)
                mainViewModel.mediaItemClicked(songsAdapter.songs[position], extras)
            }
        }

        mediaItemFragmentViewModel.mediaItems
            .observe(this) { list ->
                @Suppress("UNCHECKED_CAST")
                songsAdapter.updateData(list as List<Song>)
            }

        if (!coroutineContext.isActive) {
            launch(coroutineContext) {
                while (true) {
                    synchronized(MainActivity.hasUpdate) {
                        if (MainActivity.hasUpdate) {
                            MainActivity.hasUpdate = false
                            mediaItemFragmentViewModel.reloadMediaItems()

                            val songCount =
                                playlistRepository.getSongCountForPlaylist(categorySongData.id)
                            if (songCount != categorySongData.songCount) {
                                categorySongData = CategorySongData(
                                    categorySongData.title,
                                    songCount,
                                    categorySongData.type,
                                    categorySongData.id
                                )
                                binding.categorySongData = categorySongData
                            }
                        }
                    }

                    delay(2000L)
                }
            }
        }
    }

    override fun onDestroy() {
        super.onDestroy()
        coroutineContext.cancelChildren()
        synchronized(MainActivity.hasUpdate) {
            MainActivity.hasUpdate = true
            synchronized(MainActivity.blockGlobalScope){
                MainActivity.blockGlobalScope = false
            }
        }
    }
}
