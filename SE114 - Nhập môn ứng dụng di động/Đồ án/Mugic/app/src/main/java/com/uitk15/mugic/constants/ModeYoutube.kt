package com.uitk15.mugic.constants

import com.uitk15.mugic.R

enum class ModeYoutube(val rawValue: String, val mode_yt: Int) {
    DOWNLOAD("Download", 0),
    STREAM("Stream", 1);

    companion object {
        fun fromString(raw: String): ModeYoutube {
            return ModeYoutube.values().single { it.rawValue == raw }
        }

        fun toString(value: ModeYoutube): String = value.rawValue
    }
}