﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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

document.addEventListener("DOMContentLoaded", function () {
    var scrollToTopBtn = document.getElementById("scrollToTopBtn");

    scrollToTopBtn.addEventListener("click", function () {
        scrollToTop();
    });

    function scrollToTop() {
        window.scrollTo({
            top: 0,
            behavior: 'smooth'
        });
    }

    // Show/hide the scroll-to-top button based on scroll position
    window.addEventListener("scroll", function () {
        if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
            scrollToTopBtn.style.display = "block";
        } else {
            scrollToTopBtn.style.display = "none";
        }
    });
});

window.addEventListener('beforeunload', function (e) {
    var loadingSpinner = document.getElementById('loadingSpinner');
    loadingSpinner.style.display = 'flex';

    setTimeout(function () {
        loadingSpinner.style.display = 'none';
    }, 3000); // 3000 milliseconds = 3 seconds
});

document.addEventListener("DOMContentLoaded", function () {
    // Get elements
    const cartTable = document.getElementById("cartTable");
    const cartTotal = document.getElementById("cartTotal");

    // Attach event listener to table for dynamic updates
    cartTable.addEventListener("input", function (event) {
        if (event.target.classList.contains("quantity")) {
            updateCartItem(event.target.closest("tr"));
        }
    });

    // Function to update cart item
    function updateCartItem(row) {
        const price = parseFloat(row.querySelector(".price").textContent.replace("$", ""));
        const quantity = parseInt(row.querySelector(".quantity").value);
        const total = price * quantity;
        row.querySelector(".total").textContent = `$${total.toFixed(2)}`;

        // Update total price
        updateTotalPrice();
    }

    // Function to update total price
    function updateTotalPrice() {
        const total = Array.from(cartTable.querySelectorAll(".total"))
            .map(total => parseFloat(total.textContent.replace("$", "")))
            .reduce((sum, value) => sum + value, 0);

        cartTotal.textContent = total.toFixed(2);
    }
});
