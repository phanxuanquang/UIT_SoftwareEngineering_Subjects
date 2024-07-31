package com.uitk15.mugic.ui.dialogs

import android.app.Dialog
import android.content.Intent
import android.media.MediaScannerConnection
import android.net.Uri
import android.os.Bundle
import android.provider.MediaStore
import androidx.annotation.NonNull
import androidx.fragment.app.DialogFragment
import androidx.fragment.app.FragmentActivity
import com.afollestad.materialdialogs.MaterialDialog
import com.afollestad.materialdialogs.callbacks.onDismiss
import com.afollestad.rxkprefs.Pref
import com.uitk15.mugic.PREF_LAST_FOLDER
import com.uitk15.mugic.R
import com.uitk15.mugic.repository.RealFoldersRepository
import com.uitk15.mugic.ui.activities.MainActivity
import org.koin.android.ext.android.inject
import java.io.File

class ReloadDialog : DialogFragment() {
    private val lastFolderPref by inject<Pref<String>>(name = PREF_LAST_FOLDER)
    private val foldersRepository by inject<RealFoldersRepository>()

    @NonNull
    override fun onCreateDialog(savedInstanceState: Bundle?): Dialog {
        return MaterialDialog(activity!!).show {
            title(R.string.reload_title)
            message(R.string.reload_message)
            positiveButton(R.string.accept) {
                if (reload) {
                    var filesExists =
                        foldersRepository.getMediaFiles(File(lastFolderPref.get()), true)
                    for (item in filesExists) {
                        loadMusicDownloaded(arrayOf(item.absolutePath), arrayOf(item.extension))
                    }
                }

                MainActivity.hasUpdate = true
            }
            negativeButton(android.R.string.cancel)
            onDismiss {
                // Make sure the DialogFragment dismisses as well
                this@ReloadDialog.dismiss()
            }
        }
    }

    private fun loadMusicDownloaded(pathFile: Array<String>, extension: Array<String>) {
        MediaScannerConnection.scanFile(activity!!, pathFile, extension, null)
    }

    companion object {
        private const val TAG = "ReloadDialog"
        private var reload: Boolean = false

        fun <T> show(activity: T, reload: Boolean = true) where T : FragmentActivity {
            val dialog = ReloadDialog()
            this.reload = reload
            dialog.show(activity.supportFragmentManager, TAG)
        }
    }
}