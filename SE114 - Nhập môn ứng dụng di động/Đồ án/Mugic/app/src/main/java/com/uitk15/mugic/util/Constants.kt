package com.uitk15.mugic.util

import android.os.Environment
import java.io.File

class Constants {
    companion object {
        /** a File object pointing to the folder where playlist JSONs will be stored */
        @Suppress("DEPRECATION")
        val playlistFolder = File(
            Environment.getExternalStorageDirectory(),
            "AbleMusic/playlists")

        /**
         * a File object pointing to the folder where all AbleMusic related files
         * will be stored.
         */
        @Suppress("DEPRECATION")
        val mugicSongDir = File(
            Environment.getExternalStorageDirectory(),
            "AbleMusic")

        /**
         * a File object pointing to the folder where all songs imported from Spotify
         * will be stored.
         */
        val playlistSongDir = File(mugicSongDir.absolutePath + "/playlist_songs")

        /** a File object pointing to the folder where album art JPGs will be stored */
        val albumArtDir = File(mugicSongDir.absolutePath + "/album_art")

        /** a File object pointing to the folder where temporary items will be stored */
        val cacheDir = File(mugicSongDir.absolutePath + "/cache")

        /**
         * API keys and version code names which *should* be replaced during compilation.
         */
        const val FLURRY_KEY = "INSERT_FLURRY_KEY"
        const val RAPID_API_KEY= "INSERT_RAPID_KEY"
        const val VERSION = "Debug"

        const val DEEZER_API = "https://deezerdevs-deezer.p.rapidapi.com/search?q="
        const val CHANNEL_ID = "AbleMusicDownload"
    }
}