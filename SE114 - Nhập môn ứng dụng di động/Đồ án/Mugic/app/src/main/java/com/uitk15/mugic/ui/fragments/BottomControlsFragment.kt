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
import android.os.Handler
import android.support.v4.media.session.PlaybackStateCompat.*
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.LinearLayout
import com.google.android.material.bottomsheet.BottomSheetBehavior.*
import com.uitk15.mugic.R
import com.uitk15.mugic.constants.Constants.NOW_PLAYING
import com.uitk15.mugic.databinding.LayoutBottomsheetControlsBinding
import com.uitk15.mugic.extensions.*
import com.uitk15.mugic.ui.activities.MainActivity
import com.uitk15.mugic.ui.fragments.base.BaseNowPlayingFragment
import com.uitk15.mugic.ui.widgets.BottomSheetListener
import com.uitk15.mugic.util.AutoClearedValue

class BottomControlsFragment : BaseNowPlayingFragment(), BottomSheetListener {
    var binding by AutoClearedValue<LayoutBottomsheetControlsBinding>(this)
    private var isCasting = false

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = inflater.inflateWithBinding(R.layout.layout_bottomsheet_controls, container)
        return binding.root
    }

    override fun onActivityCreated(savedInstanceState: Bundle?) {
        super.onActivityCreated(savedInstanceState)
        binding.rootView.setOnClickListener {
            if (!isCasting) {
                activity.addFragment(
                        fragment = NowPlayingFragment(),
                        tag = NOW_PLAYING
                )
            }
        }

        binding.viewModel = nowPlayingViewModel
        binding.lifecycleOwner = this

        setupUI()
    }

    private fun setupUI() {
        val layoutParams = binding.progressBar.layoutParams as LinearLayout.LayoutParams
        binding.progressBar.measure(0, 0)
        layoutParams.setMargins(0, -(binding.progressBar.measuredHeight / 2), 0, 0)
        binding.progressBar.layoutParams = layoutParams
        binding.songTitle.isSelected = true

        binding.btnTogglePlayPause.setOnClickListener {
            nowPlayingViewModel.currentData.value?.let { mediaData ->
                mainViewModel.mediaItemClicked(mediaData.toDummySong(), null)
            }
        }

        binding.btnPlayPause.setOnClickListener {
            nowPlayingViewModel.currentData.value?.let { mediaData ->
                mainViewModel.mediaItemClicked(mediaData.toDummySong(), null)
            }
        }

        binding.btnNext.setOnClickListener {
            mainViewModel.transportControls().skipToNext()
        }

        binding.btnPrevious.setOnClickListener {
            mainViewModel.transportControls().skipToPrevious()
        }

        binding.btnRepeat.setOnClickListener {
            when (nowPlayingViewModel.currentData.value?.repeatMode) {
                REPEAT_MODE_NONE -> mainViewModel.transportControls().setRepeatMode(REPEAT_MODE_ONE)
                REPEAT_MODE_ONE -> mainViewModel.transportControls().setRepeatMode(REPEAT_MODE_ALL)
                REPEAT_MODE_ALL -> mainViewModel.transportControls().setRepeatMode(REPEAT_MODE_NONE)
            }
        }

        binding.btnShuffle.setOnClickListener {
            when (nowPlayingViewModel.currentData.value?.shuffleMode) {
                SHUFFLE_MODE_NONE -> mainViewModel.transportControls().setShuffleMode(SHUFFLE_MODE_ALL)
                SHUFFLE_MODE_ALL -> mainViewModel.transportControls().setShuffleMode(SHUFFLE_MODE_NONE)
            }
        }

        (activity as? MainActivity)?.let { mainActivity ->
            binding.btnCollapse.setOnClickListener { mainActivity.collapseBottomSheet() }
            mainActivity.setBottomSheetListener(this)
        }

        buildUIControls()
    }

    private fun buildUIControls() {
        binding.btnLyrics.setOnClickListener {
            val currentSong = nowPlayingViewModel.currentData.value
            val artist = currentSong?.artist
            val title = currentSong?.title
            val mainActivity = activity as? MainActivity
            if (artist != null && title != null && mainActivity != null) {
                mainActivity.collapseBottomSheet()
                Handler().postDelayed({
                    mainActivity.addFragment(fragment = LyricsFragment.newInstance(artist, title))
                }, 200)
            }
        }
    }

    override fun onSlide(bottomSheet: View, slideOffset: Float) {
        if (slideOffset > 0) {
            binding.btnPlayPause.hide()
            binding.progressBar.hide()
            binding.btnCollapse.show()
        } else {
            binding.progressBar.show()
        }
    }

    override fun onStateChanged(bottomSheet: View, newState: Int) {
        if (newState == STATE_DRAGGING || newState == STATE_EXPANDED) {
            binding.btnPlayPause.hide()
            binding.btnCollapse.show()
            //disable expanded controls when casting as we don't support next/previous yet
            if (isCasting) {
                (activity as MainActivity).collapseBottomSheet()
            }
        } else if (newState == STATE_COLLAPSED) {
            binding.btnPlayPause.show()
            binding.btnCollapse.hide()
        }
    }

    override fun onResume() {
        super.onResume()
        mainViewModel.mediaController.observe(this) { mediaController ->
            binding.progressBar.setMediaController(mediaController)
            binding.progressText.setMediaController(mediaController)
            binding.seekBar.setMediaController(mediaController)
        }
    }

    override fun onStop() {
        binding.progressBar.disconnectController()
        binding.progressText.disconnectController()
        binding.seekBar.disconnectController()
        super.onStop()
    }
}
