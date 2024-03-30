﻿function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imagePreview').css('background-image', 'url(' + e.target.result + ')');
            $('#imagePreview').hide();
            $('#imagePreview').fadeIn(650);
        }
        reader.readAsDataURL(input.files[0]);
    }
}
$("#imageUpload").change(function () {
    readURL(this);
});

var protein = document.getElementById('protein');
const carbs = document.getElementById('carbs');
const fats = document.getElementById('fats');

document.addEventListener('DOMContentLoaded', function () {
   
    if (protein.value === '0') {
        protein.value = '';
      
    }
    if (carbs.value === '0') {
        carbs.value = '';      
    }
    if (fats.value === '0') {
        fats.value = '';
    }
});
