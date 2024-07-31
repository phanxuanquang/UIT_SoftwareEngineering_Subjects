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
import androidx.recyclerview.widget.LinearLayoutManager
import com.afollestad.rxkprefs.Pref
import com.google.android.material.snackbar.Snackbar
import com.uitk15.mugic.PREF_SONG_SORT_ORDER
import com.uitk15.mugic.R
import com.uitk15.mugic.constants.SongSortOrder
import com.uitk15.mugic.constants.SongSortOrder.SONG_A_Z
import com.uitk15.mugic.constants.SongSortOrder.SONG_DURATION
import com.uitk15.mugic.constants.SongSortOrder.SONG_YEAR
import com.uitk15.mugic.constants.SongSortOrder.SONG_Z_A
import com.uitk15.mugic.databinding.LayoutRecyclerviewBinding
import com.uitk15.mugic.extensions.*
import com.uitk15.mugic.models.Playlist
import com.uitk15.mugic.models.Song
import com.uitk15.mugic.ui.activities.MainActivity
import com.uitk15.mugic.ui.adapters.SongsAdapter
import com.uitk15.mugic.ui.fragments.base.MediaItemFragment
import com.uitk15.mugic.ui.listeners.SortMenuListener
import com.uitk15.mugic.util.AutoClearedValue
import kotlinx.android.synthetic.main.item_songs.*
import kotlinx.coroutines.*
import org.koin.android.ext.android.inject
import kotlin.coroutines.CoroutineContext
import kotlin.coroutines.EmptyCoroutineContext

class SongsFragment : MediaItemFragment(), CoroutineScope {
    private lateinit var songsAdapter: SongsAdapter
    private val sortOrderPref by inject<Pref<SongSortOrder>>(name = PREF_SONG_SORT_ORDER)

    var binding by AutoClearedValue<LayoutRecyclerviewBinding>(this)

    override val coroutineContext: CoroutineContext = Dispatchers.Default

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = inflater.inflateWithBinding(R.layout.layout_recyclerview, container)
        return binding.root
    }

    override fun onActivityCreated(savedInstanceState: Bundle?) {
        super.onActivityCreated(savedInstanceState)
        songsAdapter = SongsAdapter(this).apply {
            showHeader = true
            popupMenuListener = mainViewModel.popupMenuListener
            sortMenuListener = sortListener
        }

        binding.recyclerView.apply {
            layoutManager = LinearLayoutManager(safeActivity)
            adapter = songsAdapter
            addOnItemClick { position: Int, _: View ->
                songsAdapter.getSongForPosition(position)?.let { song ->
                    val extras = getExtraBundle(
                        songsAdapter.songs.toSongIds(),
                        getString(R.string.all_songs)
                    )
                    mainViewModel.mediaItemClicked(song, extras)
                }
            }
        }

        mediaItemFragmentViewModel.mediaItems
            .observe(this) { list ->
                @Suppress("UNCHECKED_CAST")
                songsAdapter.updateData(list as List<Song>)
            }
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        // Auto trigger a reload when the sort order pref changes
        sortOrderPref.observe()
            .ioToMain()
            .subscribe { mediaItemFragmentViewModel.reloadMediaItems() }
            .disposeOnDetach(view)
    }

    private val sortListener = object : SortMenuListener {
        override fun shuffleAll() {
            songsAdapter.songs.shuffled().apply {
                val extras = getExtraBundle(toSongIds(), getString(R.string.all_songs))

                if (this.isEmpty()) {
                    Snackbar.make(
                        binding.recyclerView,
                        R.string.shuffle_no_songs_error,
                        Snackbar.LENGTH_SHORT
                    )
                        .show()
                } else {
                    mainViewModel.mediaItemClicked(this[0], extras)
                }
            }
        }

        override fun sortAZ() = sortOrderPref.set(SONG_A_Z)

        override fun sortDuration() = sortOrderPref.set(SONG_DURATION)

        override fun sortYear() = sortOrderPref.set(SONG_YEAR)

        override fun numOfSongs() {}

        override fun sortZA() = sortOrderPref.set(SONG_Z_A)
    }

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
                    if(!MainActivity.blockGlobalScope){
                        synchronized(MainActivity.hasUpdate) {
                            if (MainActivity.hasUpdate && this@SongsFragment.userVisibleHint) {
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
