// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// script.js
document.addEventListener('DOMContentLoaded', function () {
    const image = document.querySelector('.landing-page .img img');

    // Function to toggle the zoomed class
    function toggleZoom() {
        image.classList.toggle('zoomed');
    }

    // Set interval to toggle zoom every 3 seconds (adjust as needed)
    setInterval(toggleZoom, 1000);
});