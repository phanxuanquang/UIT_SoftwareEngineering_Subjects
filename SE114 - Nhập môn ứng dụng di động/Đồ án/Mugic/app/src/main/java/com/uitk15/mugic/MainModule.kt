package com.uitk15.mugic

import android.app.Application
import android.content.ComponentName
import android.content.ContentResolver
import com.uitk15.mugic.playback.MediaSessionConnection
import com.uitk15.mugic.playback.RealMediaSessionConnection
import com.uitk15.mugic.playback.TimberMusicService
import io.reactivex.android.schedulers.AndroidSchedulers
import org.koin.dsl.module.module

const val MAIN = "main"

val mainModule = module {

    factory<ContentResolver> {
        get<Application>().contentResolver
    }

    single {
        val component = ComponentName(get(), TimberMusicService::class.java)
        RealMediaSessionConnection(get(), component)
    } bind MediaSessionConnection::class

    factory(name = MAIN) {
        AndroidSchedulers.mainThread()
    }
}
