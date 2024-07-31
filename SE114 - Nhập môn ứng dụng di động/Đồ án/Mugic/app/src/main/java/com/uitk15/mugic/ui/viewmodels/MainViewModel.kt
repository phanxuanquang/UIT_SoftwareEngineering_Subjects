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
package com.uitk15.mugic.ui.viewmodels

import android.content.Context
import android.os.Bundle
import android.support.v4.media.MediaBrowserCompat
import android.support.v4.media.session.MediaControllerCompat
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import com.uitk15.mugic.constants.Constants.ACTION_PLAY_NEXT
import com.uitk15.mugic.constants.Constants.ACTION_REMOVED_FROM_PLAYLIST
import com.uitk15.mugic.constants.Constants.ACTION_SONG_DELETED
import com.uitk15.mugic.constants.Constants.SONG
import com.uitk15.mugic.extensions.*
import com.uitk15.mugic.models.MediaID
import com.uitk15.mugic.models.Song
import com.uitk15.mugic.playback.MediaSessionConnection
import com.uitk15.mugic.repository.AlbumRepository
import com.uitk15.mugic.repository.ArtistRepository
import com.uitk15.mugic.repository.PlaylistRepository
import com.uitk15.mugic.repository.SongsRepository
import com.uitk15.mugic.ui.activities.MainActivity
import com.uitk15.mugic.ui.dialogs.AddToPlaylistDialog
import com.uitk15.mugic.ui.dialogs.DeleteSongDialog
import com.uitk15.mugic.ui.listeners.PopupMenuListener
import com.uitk15.mugic.util.Event
import timber.log.Timber.d as log
import timber.log.Timber.w as warn

class MainViewModel(
    private val context: Context,
    private val mediaSessionConnection: MediaSessionConnection,
    private val songsRepository: SongsRepository,
    private val artistRepository: ArtistRepository,
    private val albumRepository: AlbumRepository,
    private val playlistsRepository: PlaylistRepository
) : ViewModel() {

    val rootMediaId: LiveData<MediaID> =
            mediaSessionConnection.isConnected.map { isConnected ->
                if (isConnected) {
                    MediaID().fromString(mediaSessionConnection.rootMediaId)
                } else {
                    null
                }
            }

    val mediaController: LiveData<MediaControllerCompat> =
            mediaSessionConnection.isConnected.map { isConnected ->
                if (isConnected) {
                    mediaSessionConnection.mediaController
                } else {
                    null
                }
            }

    val navigateToMediaItem: LiveData<Event<MediaID>> get() = _navigateToMediaItem
    private val _navigateToMediaItem = MutableLiveData<Event<MediaID>>()

    val customAction: LiveData<Event<String>> get() = _customAction
    private val _customAction = MutableLiveData<Event<String>>()

    fun mediaItemClicked(clickedItem: MediaBrowserCompat.MediaItem, extras: Bundle?) {
        log("mediaItemClicked(): $clickedItem")
        if (clickedItem.isBrowsable) {
            browseToItem(clickedItem)
        } else {
            playMedia(clickedItem, extras)
        }
    }

    private fun browseToItem(mediaItem: MediaBrowserCompat.MediaItem) {
        log("browseToItem(): $mediaItem")
        _navigateToMediaItem.value = Event(MediaID().fromString(mediaItem.mediaId!!).apply {
            this.mediaItem = mediaItem
        })
    }

    fun transportControls() = mediaSessionConnection.transportControls

    private fun playMedia(mediaItem: MediaBrowserCompat.MediaItem, extras: Bundle?) {
        log("playMedia(): $mediaItem")

        val nowPlaying = mediaSessionConnection.nowPlaying.value
        val transportControls = mediaSessionConnection.transportControls

        val isPrepared = mediaSessionConnection.playbackState.value?.isPrepared ?: false
        if (isPrepared && MediaID().fromString(mediaItem.mediaId!!).mediaId == nowPlaying?.id) {
            mediaSessionConnection.playbackState.value?.let { playbackState ->
                when {
                    playbackState.isPlaying -> transportControls.pause()
                    playbackState.isPlayEnabled -> transportControls.play()
                    else -> {
                        warn("Playable item clicked but neither play nor pause are enabled! (mediaId=${mediaItem.mediaId})")
                    }
                }
            }
        } else {
            transportControls.playFromMediaId(mediaItem.mediaId, extras)
        }
    }

    val popupMenuListener = object : PopupMenuListener {

        override fun play(song: Song) {
            playMedia(song, null)
        }

        override fun goToAlbum(song: Song) {
            browseToItem(albumRepository.getAlbum(song.albumId))
        }

        override fun goToArtist(song: Song) {
            browseToItem(artistRepository.getArtist(song.artistId))
        }

        override fun addToPlaylist(context: Context, song: Song) {
            AddToPlaylistDialog.show(context as AppCompatActivity, song)
        }

        override fun removeFromPlaylist(song: Song, playlistId: Long) {
            playlistsRepository.removeFromPlaylist(playlistId, song.id)
            _customAction.postValue(Event(ACTION_REMOVED_FROM_PLAYLIST))
        }

        override fun deleteSong(context: Context, song: Song) = DeleteSongDialog.show(context as MainActivity, song)

        override fun playNext(song: Song) {
            mediaSessionConnection.transportControls.sendCustomAction(ACTION_PLAY_NEXT,
                    Bundle().apply { putLong(SONG, song.id) }
            )
        }
    }

    fun onSongDeleted(id: Long) {
        _customAction.postValue(Event(ACTION_SONG_DELETED))
        // also need to remove the deleted song from the current playing queue
        mediaSessionConnection.transportControls.sendCustomAction(ACTION_SONG_DELETED,
                Bundle().apply {
                    // sending parceleable data through media session custom action bundle is not working currently
                    putLong(SONG, id)
                })
    }

    //cast helpers
}
