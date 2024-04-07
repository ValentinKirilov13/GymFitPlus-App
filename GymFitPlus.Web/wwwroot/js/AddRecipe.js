function readURL(input) {
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

const protein = document.getElementById('protein');
const carbs = document.getElementById('carbs');
const fats = document.getElementById('fats');
const calories = document.getElementById('calories');

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
    if (calories.value === '0') {
        calories.value = '';
    }
});


function showButton() {
    var button = document.getElementById('favoriteButnn');
    button.style.visibility = 'visible';

    document.addEventListener('click', function (event) {
        var target = event.target;
        var textarea = document.getElementById('myTextarea');

        if (target !== textarea && !textarea.contains(target)) {
            button.style.visibility = 'hidden';
        }
    });
}

