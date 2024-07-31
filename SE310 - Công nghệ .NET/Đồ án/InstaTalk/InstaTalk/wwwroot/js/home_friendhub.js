

var isClicked = false;
var streamMedia;



function changeIcon() {
    var icon = document.getElementById("icon_pass_off");
    var input = document.getElementById("input-pass-mate-room");
    if (isClicked) {
        icon.innerHTML = "visibility_off";
        input.type = "password";
        isClicked = false;
    } else {
        icon.innerHTML = "visibility";
        input.type = "text";
        isClicked = true;
    }
}

function clearText() {
    var input = document.getElementById("input-room-mate-id");
    var label = document.getElementById("lb-input-room-mate-id");
    input.value = '';
    input.focus();
    input.blur();
}

$(document).ready(function () {
    let input_room_id = document.getElementById("input_room_id");
    if (input_room_id.value != "") {
        let btn_join_room = document.getElementById("btn_join_room");
        btn_join_room.click();
    }
});

async function getPermission() { 
    await navigator.mediaDevices.getUserMedia({ audio: true, video: true }).then(stream => {
        streamMedia = stream;
    }).catch(() => {
        streamMedia = null;
    });
}

async function checkPermission(id) {

    await getPermission();

    if (streamMedia) {
        await homeFormSubmit(id);
    }
    else {
        alert("Please allow access to your camera and microphone to continue!");
    }
}

async function homeFormSubmit(id) {
    if (streamMedia) {
        let form = document.getElementById(id);
        form.submit();
    }
    else {
        await checkPermission(id);
    }
}

function mediaHasChatFunc(x) {
    let btn_create_room = document.getElementById("btn_create_room");
    let btn_join_room = document.getElementById("btn_join_room");

    btn_create_room.style.margin = "0 0 2rem 0";
    btn_join_room.style.margin = "0 0 0 0";
}

// Create a MediaQueryList object
const mediaHasChat = window.matchMedia("(max-width: 730px)");

// Call the match function at run time
mediaHasChatFunc(mediaHasChat);

// Add the match function as a listener for state changes
mediaHasChat.addEventListener("change", function () {
    mediaHasChatFunc(mediaHasChat);
});
