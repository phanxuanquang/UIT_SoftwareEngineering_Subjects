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
package com.uitk15.mugic.ui.activities

import android.content.Intent
import android.content.Intent.ACTION_VIEW
import android.net.Uri
import android.os.Bundle
import android.os.Handler
import android.os.Looper
import android.provider.MediaStore.EXTRA_MEDIA_TITLE
import android.provider.MediaStore.INTENT_ACTION_MEDIA_PLAY_FROM_SEARCH
import android.view.View
import android.widget.FrameLayout
import android.widget.Toast
import androidx.annotation.NonNull
import com.afollestad.rxkprefs.Pref
import com.google.android.material.bottomsheet.BottomSheetBehavior
import com.google.android.material.bottomsheet.BottomSheetBehavior.*
import com.uitk15.mugic.PREF_APP_THEME
import com.uitk15.mugic.R
import com.uitk15.mugic.constants.AppThemes
import com.uitk15.mugic.databinding.MainActivityBinding
import com.uitk15.mugic.extensions.*
import com.uitk15.mugic.models.MediaID
import com.uitk15.mugic.repository.SongsRepository
import com.uitk15.mugic.ui.activities.base.PermissionsActivity
import com.uitk15.mugic.ui.dialogs.DeleteSongDialog
import com.uitk15.mugic.ui.fragments.BottomControlsFragment
import com.uitk15.mugic.ui.fragments.MainFragment
import com.uitk15.mugic.ui.fragments.base.MediaItemFragment
import com.uitk15.mugic.ui.viewmodels.MainViewModel
import com.uitk15.mugic.ui.widgets.BottomSheetListener
import io.github.uditkarode.able.fragments.Home
import io.github.uditkarode.able.fragments.Search
import io.github.uditkarode.able.models.MusicMode
import io.github.uditkarode.able.models.Song
import io.github.uditkarode.able.services.DownloadService
import io.github.uditkarode.able.services.ServiceResultReceiver
import io.reactivex.functions.Consumer
import kotlinx.coroutines.*
import org.koin.android.ext.android.inject
import org.koin.androidx.viewmodel.ext.android.viewModel
import java.io.File
import kotlin.coroutines.CoroutineContext

@OptIn(ExperimentalCoroutinesApi::class)
class MainActivity : PermissionsActivity(), DeleteSongDialog.OnSongDeleted, Search.SongCallback,
    ServiceResultReceiver.Receiver, CoroutineScope {

    private val viewModel by viewModel<MainViewModel>()
    private val songsRepository by inject<SongsRepository>()
    private val appThemePref by inject<Pref<AppThemes>>(name = PREF_APP_THEME)

    private var binding: MainActivityBinding? = null
    private var bottomSheetListener: BottomSheetListener? = null
    private var bottomSheetBehavior: BottomSheetBehavior<View>? = null
    private var mServiceResultReceiver: ServiceResultReceiver = ServiceResultReceiver(Handler(Looper.getMainLooper()))
    private lateinit var home: Home

    override val coroutineContext: CoroutineContext
        get() = Dispatchers.Default

    override fun onCreate(savedInstanceState: Bundle?) {
        setTheme(appThemePref.get().themeRes)
        super.onCreate(savedInstanceState)
        binding = setDataBindingContentView(R.layout.main_activity)
        supportActionBar?.setDisplayShowTitleEnabled(false)

        if (!permissionsManager.hasStoragePermission()) {
            permissionsManager.requestStoragePermission().subscribe(Consumer {
                setupUI()
            }).attachLifecycle(this)
            return
        }

        setupUI()

        /*mServiceResultReceiver = ServiceResultReceiver(Handler(Looper.getMainLooper()))*/
        mServiceResultReceiver.setReceiver(this@MainActivity)

        home = Home()
    }

    fun setBottomSheetListener(bottomSheetListener: BottomSheetListener) {
        this.bottomSheetListener = bottomSheetListener
    }

    fun collapseBottomSheet() {
        bottomSheetBehavior?.state = STATE_COLLAPSED
    }

    fun hideBottomSheet() {
        bottomSheetBehavior?.state = STATE_HIDDEN
    }

    fun showBottomSheet() {
        if (bottomSheetBehavior?.state == STATE_HIDDEN) {
            bottomSheetBehavior?.state = STATE_COLLAPSED
        }
    }

    override fun onBackPressed() {
        bottomSheetBehavior?.let {
            if (it.state == STATE_EXPANDED) {
                collapseBottomSheet()
            } else {
                super.onBackPressed()
            }
        }
    }

    override fun onSongDeleted(songId: Long) {
        viewModel.onSongDeleted(songId)
    }

    private fun setupUI() {
        viewModel.rootMediaId.observe(this) {
            replaceFragment(fragment = MainFragment())
            Handler().postDelayed({
                replaceFragment(
                    R.id.bottomControlsContainer,
                    BottomControlsFragment()
                )
            }, 150)

            //handle playback intents, (search intent or ACTION_VIEW intent)
            handlePlaybackIntent(intent)
        }

        viewModel.navigateToMediaItem
            .map { it.getContentIfNotHandled() }
            .filter { it != null }
            .observe(this) { navigateToMediaItem(it!!) }

        binding?.let {
            it.viewModel = viewModel
            it.lifecycleOwner = this
        }
        val parentThatHasBottomSheetBehavior = binding?.bottomSheetParent as FrameLayout? ?: return

        bottomSheetBehavior = BottomSheetBehavior.from(parentThatHasBottomSheetBehavior)
        bottomSheetBehavior?.isHideable = true
        bottomSheetBehavior?.setBottomSheetCallback(BottomSheetCallback())

        binding?.dimOverlay?.setOnClickListener { collapseBottomSheet() }
    }

    private fun navigateToMediaItem(mediaId: MediaID) {
        if (getBrowseFragment(mediaId) == null) {
            val fragment = MediaItemFragment.newInstance(mediaId)
            addFragment(
                fragment = fragment,
                tag = mediaId.type,
                addToBackStack = !isRootId(mediaId)
            )
        }
    }

    private fun handlePlaybackIntent(intent: Intent?) {
        if (intent == null || intent.action == null) return

        when (intent.action!!) {
            INTENT_ACTION_MEDIA_PLAY_FROM_SEARCH -> {
                val songTitle = intent.extras?.getString(EXTRA_MEDIA_TITLE, null)
                viewModel.transportControls().playFromSearch(songTitle, null)
            }
            ACTION_VIEW -> {
                val path = getIntent().data?.path ?: return
                val song = songsRepository.getSongFromPath(path)
                viewModel.mediaItemClicked(song, null)
            }
        }
    }

    private inner class BottomSheetCallback : BottomSheetBehavior.BottomSheetCallback() {
        override fun onStateChanged(@NonNull bottomSheet: View, newState: Int) {
            if (newState == STATE_DRAGGING || newState == STATE_EXPANDED) {
                binding?.dimOverlay?.show()
            } else if (newState == STATE_COLLAPSED) {
                binding?.dimOverlay?.hide()
            }
            bottomSheetListener?.onStateChanged(bottomSheet, newState)
        }

        override fun onSlide(@NonNull bottomSheet: View, slideOffset: Float) {
            if (slideOffset > 0) {
                binding?.dimOverlay?.alpha = slideOffset
            } else if (slideOffset == 0f) {
                binding?.dimOverlay?.hide()
            }
            bottomSheetListener?.onSlide(bottomSheet, slideOffset)
        }
    }

    private fun isRootId(mediaId: MediaID) = mediaId.type == viewModel.rootMediaId.value?.type

    private fun getBrowseFragment(mediaId: MediaID): MediaItemFragment? {
        return supportFragmentManager.findFragmentByTag(mediaId.type) as MediaItemFragment?
    }

    override fun sendItem(song: Song, mode: String) {
        var currentMode = MusicMode.download

        if (song.ytmThumbnail.contains("googleusercontent")) //set resolution for youtube music art
        {
            song.ytmThumbnail = song.ytmThumbnail.replace("w120", "w1500")
            song.ytmThumbnail = song.ytmThumbnail.replace("h120", "h1500")
        }

        when (currentMode) {
            MusicMode.download -> {
                val songL = ArrayList<String>()
                songL.add(song.name)
                songL.add(song.youtubeLink)
                songL.add(song.artist)
                songL.add(song.ytmThumbnail)
                val serviceIntentService = Intent(this@MainActivity, DownloadService::class.java)
                    .putStringArrayListExtra("song", songL)
                    .putExtra("receiver", mServiceResultReceiver)
                DownloadService.enqueueDownload(this, serviceIntentService)

                if (!coroutineContext.isActive) {
                    GlobalScope.launch(Dispatchers.Default) {
                        while (!DownloadService.isDownloaded) {
                            if (DownloadService.queueDownload != null) {
                                if (DownloadService.queueDownload!!.size > 0) {
                                    addMusic(DownloadService.queueDownload!!.poll())
                                }
                            }
                            delay(500L)
                        }

                        while (DownloadService.queueDownload!!.size > 0) {
                            addMusic(DownloadService.queueDownload!!.poll())
                        }

                        synchronized(hasUpdate) {
                            hasUpdate = true
                        }
                    }
                }

                Toast.makeText(
                    this@MainActivity,
                    "${song.name} ${getString(io.github.uditkarode.able.R.string.dl_added)}",
                    Toast.LENGTH_SHORT
                ).show()
            }
        }


    }

    private fun addMusic(pathFile: String) {
        sendBroadcast(Intent(Intent.ACTION_MEDIA_SCANNER_SCAN_FILE, Uri.fromFile(File(pathFile))))
    }

    override fun onDestroy() {
        super.onDestroy()
        coroutineContext.cancelChildren()
    }

    override fun onReceiveResult(resultCode: Int) {
        //TODO("Not yet implemented")
    }

    companion object {
        public var isShowSearching = false
        public var hasUpdate = false
        public var blockGlobalScope = false
    }
}