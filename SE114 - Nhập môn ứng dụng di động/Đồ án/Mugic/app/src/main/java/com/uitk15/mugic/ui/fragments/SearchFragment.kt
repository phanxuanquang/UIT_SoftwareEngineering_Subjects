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

import android.content.Intent
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.GridLayoutManager
import androidx.recyclerview.widget.LinearLayoutManager
import com.afollestad.rxkprefs.Pref
import com.uitk15.mugic.PREF_LAST_FOLDER
import com.uitk15.mugic.PREF_MODE_YT
import com.uitk15.mugic.R
import com.uitk15.mugic.constants.ModeYoutube
import com.uitk15.mugic.databinding.FragmentSearchBinding
import com.uitk15.mugic.extensions.*
import com.uitk15.mugic.ui.adapters.AlbumAdapter
import com.uitk15.mugic.ui.adapters.ArtistAdapter
import com.uitk15.mugic.ui.adapters.SongsAdapter
import com.uitk15.mugic.ui.fragments.base.BaseNowPlayingFragment
import com.uitk15.mugic.ui.viewmodels.SearchViewModel
import com.uitk15.mugic.util.AutoClearedValue
import io.github.uditkarode.able.activities.MainActivity
import io.github.uditkarode.able.fragments.Search
import io.github.uditkarode.able.models.Song
import kotlinx.coroutines.ExperimentalCoroutinesApi
import org.koin.android.ext.android.inject
import org.koin.androidx.viewmodel.ext.android.sharedViewModel

class SearchFragment : BaseNowPlayingFragment(), Search.SongCallback {

    private val searchViewModel by sharedViewModel<SearchViewModel>()
    private val modeYoutube by inject<Pref<ModeYoutube>>(name = PREF_MODE_YT)

    private lateinit var songAdapter: SongsAdapter
    private lateinit var albumAdapter: AlbumAdapter
    private lateinit var artistAdapter: ArtistAdapter

    var binding by AutoClearedValue<FragmentSearchBinding>(this)

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = inflater.inflateWithBinding(R.layout.fragment_search, container)
        return binding.root
    }

    @OptIn(ExperimentalCoroutinesApi::class)
    override fun onActivityCreated(savedInstanceState: Bundle?) {
        super.onActivityCreated(savedInstanceState)

        songAdapter = SongsAdapter(this).apply {
            popupMenuListener = mainViewModel.popupMenuListener
        }
        binding.rvSongs.apply {
            layoutManager = LinearLayoutManager(context)
            adapter = songAdapter
        }

        albumAdapter = AlbumAdapter()
        binding.rvAlbums.apply {
            layoutManager = GridLayoutManager(safeActivity, 3)
            adapter = albumAdapter
            addOnItemClick { position: Int, _: View ->
                mainViewModel.mediaItemClicked(albumAdapter.albums[position], null)
            }
        }

        artistAdapter = ArtistAdapter()
        binding.rvArtist.apply {
            layoutManager = GridLayoutManager(safeActivity, 3)
            adapter = artistAdapter
            addOnItemClick { position: Int, _: View ->
                mainViewModel.mediaItemClicked(artistAdapter.artists[position], null)
            }
        }

        binding.rvSongs.addOnItemClick { position: Int, _: View ->
            songAdapter.getSongForPosition(position)?.let { song ->
                val extras = getExtraBundle(songAdapter.songs.toSongIds(), "All songs")
                mainViewModel.mediaItemClicked(song, extras)
            }
        }
        binding.etSearch.addTextChangedListener(object : TextWatcher {
            override fun onTextChanged(s: CharSequence, start: Int, before: Int, count: Int) {
                searchViewModel.search(s.toString())
            }

            override fun afterTextChanged(s: Editable?) {
                songAdapter.updateData(emptyList())
            }

            override fun beforeTextChanged(s: CharSequence?, start: Int, count: Int, after: Int) = Unit
        })
        binding.btnBack.setOnClickListener { safeActivity.onBackPressed() }

        searchViewModel.searchLiveData.observe(this) { searchData ->
            songAdapter.updateData(searchData.songs)
            albumAdapter.updateData(searchData.albums)
            artistAdapter.updateData(searchData.artists)
        }

        binding.let {
            it.viewModel = searchViewModel
            it.setLifecycleOwner(this)
        }

        binding.floatingActionButton.setOnClickListener {
            when(modeYoutube.get()){
                ModeYoutube.DOWNLOAD -> safeActivity.addFragment(fragment = Search())
                ModeYoutube.STREAM -> {
                    val intent = Intent(context, MainActivity::class.java)
                    startActivity(intent)
                }
            }
        }
    }

    override fun sendItem(song: Song, mode: String) {
        TODO("Not yet implemented")
    }

    override fun onPause() {
        super.onPause()
        com.uitk15.mugic.ui.activities.MainActivity.isShowSearching = false
    }
}
