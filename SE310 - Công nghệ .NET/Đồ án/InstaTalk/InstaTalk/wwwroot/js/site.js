// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
const { Subject, ReplaySubject, Observable, BehaviorSubject, Subscription } = rxjs;
// Write your JavaScript code.
function CallToast(message) {
    let toast = document.getElementById("template-toast")
    let content = document.getElementById("content-toast")
    content.innerHTML = message
    new bootstrap.Toast(toast).show()
}