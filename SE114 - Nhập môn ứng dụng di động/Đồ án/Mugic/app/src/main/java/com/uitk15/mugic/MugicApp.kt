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
@file:Suppress("unused")

package com.uitk15.mugic

import android.app.Application
import com.uitk15.mugic.BuildConfig.DEBUG
import com.uitk15.mugic.db.roomModule
import com.uitk15.mugic.logging.FabricTree
import com.uitk15.mugic.network.lastFmModule
import com.uitk15.mugic.network.lyricsModule
import com.uitk15.mugic.network.networkModule
import com.uitk15.mugic.notifications.notificationModule
import com.uitk15.mugic.permissions.permissionsModule
import com.uitk15.mugic.playback.mediaModule
import com.uitk15.mugic.repository.repositoriesModule
import com.uitk15.mugic.ui.viewmodels.viewModelsModule
import org.koin.android.ext.android.startKoin
import timber.log.Timber

class MugicApp : Application() {

    override fun onCreate() {
        super.onCreate()

        if (DEBUG) {
            Timber.plant(Timber.DebugTree())
        } else {
            Timber.plant(FabricTree())
        }

        val modules = listOf(
                mainModule,
                permissionsModule,
                mediaModule,
                prefsModule,
                networkModule,
                roomModule,
                notificationModule,
                repositoriesModule,
                viewModelsModule,
                lyricsModule,
                lastFmModule
        )
        startKoin(
                androidContext = this,
                modules = modules
        )
    }
}
