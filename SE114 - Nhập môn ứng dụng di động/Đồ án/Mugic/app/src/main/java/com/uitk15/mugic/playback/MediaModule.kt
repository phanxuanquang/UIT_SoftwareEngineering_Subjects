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
package com.uitk15.mugic.playback

import com.uitk15.mugic.playback.players.MusicPlayer
import com.uitk15.mugic.playback.players.Queue
import com.uitk15.mugic.playback.players.RealMusicPlayer
import com.uitk15.mugic.playback.players.RealQueue
import com.uitk15.mugic.playback.players.RealSongPlayer
import com.uitk15.mugic.playback.players.SongPlayer
import org.koin.dsl.module.module

val mediaModule = module {

    factory {
        RealMusicPlayer(get())
    } bind MusicPlayer::class

    factory {
        RealQueue(get(), get(), get())
    } bind Queue::class

    factory {
        RealSongPlayer(get(), get(), get(), get(), get())
    } bind SongPlayer::class
}
