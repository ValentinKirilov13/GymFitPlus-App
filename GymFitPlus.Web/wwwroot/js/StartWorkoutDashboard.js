
document.addEventListener("DOMContentLoaded", function () {

    var table = document.getElementById('myTable');
    var rows = table.querySelectorAll('tbody > tr');
    var currentIndex = 0;
    var checkboxes = document.querySelectorAll('.checkbox');
    var progressBar = document.getElementById('progress-bar');
    var startButton = document.getElementById('startButton');
    var timerDiv = document.getElementById('divTimer');
    var upButton = document.getElementById('upButton');
    var downButton = document.getElementById('downButton');
    var labels = table.querySelectorAll('tbody > tr > td > label');
    var elements = document.querySelectorAll('.setsCol');
    var finishForm = document.getElementById('finishForm');

    function selectRow(row) {
        row.style.backgroundColor = '#ff206e';
        row.style.boxShadow = '39px 36px 42px -10px rgba(0,0,0,0.75)';
        row.style.zIndex = '5';
        row.style.position = 'relative';
        row.style.top = '10px';
        row.style.left = '-10px';
        row.style.textWeight = 'bold';
        row.style.fontSize = '20px';
        row.style.transition = '';
        row.style.opacity = '';
        row.style.transition = 'all 0.5s ease-out';
        elements[currentIndex].classList.add('hideAfter');
    }
    function deselectRow(row) {
        row.style.backgroundColor = '';
        row.style.boxShadow = '';
        row.style.zIndex = '';
        row.style.position = '';
        row.style.top = '';
        row.style.left = '';
        row.style.fontSize = '';
        row.style.transition = 'opacity 0.5s ease';
        row.style.opacity = '0.5';
        elements[currentIndex].classList.remove('hideAfter');
    }
    function areAllCheckboxesChecked() {

        for (var i = 0; i < checkboxes.length; i++) {
            if (!checkboxes[i].checked) {
                return false;
            }
        }
        return true;
    }
    function updateProgressBar() {
        var checkedCount = 0;
        checkboxes.forEach(function (checkbox) {
            if (checkbox.checked) {
                checkedCount++;
            }
        });
        var progress = (checkedCount / checkboxes.length) * 100;
        progressBar.style.width = progress + '%';
        progressBar.textContent = Math.ceil(progress) + '%';
    }

    updateProgressBar();

    startButton.addEventListener('click', function () {
        upButton.style.visibility = 'visible';
        downButton.style.visibility = 'visible';

        this.disabled = true;
        this.style.visibility = 'hidden';
        this.style.transition = 'opacity 0.1s ease';
        timerDiv.style.visibility = 'visible';

        labels.forEach(function (label) {
            label.style.visibility = 'visible';
        });

        rows.forEach(function (row) {
            row.style.transition = 'opacity 0.5s ease';
            row.style.opacity = '0.5';
        });

        selectRow(rows[currentIndex]);
    });
    upButton.addEventListener('click', function () {
        if (currentIndex > 0) {

            deselectRow(rows[currentIndex]);
            currentIndex--;
            selectRow(rows[currentIndex]);
        }
    });
    downButton.addEventListener('click', function () {
        if (currentIndex < rows.length - 1) {
            deselectRow(rows[currentIndex]);
            currentIndex++;
            selectRow(rows[currentIndex]);
        }
    });


    checkboxes.forEach(function (checkbox) {
        checkbox.addEventListener('change', function () {

            currentIndex = checkbox.closest('tr').rowIndex - 1;

            rows.forEach(function (row) {
                deselectRow(row);
            });

            upButton.style.visibility = 'hidden';
            upButton.disabled = true;
            downButton.style.visibility = 'hidden';
            downButton.disabled = true;

            finishForm.style.display = 'block';

            rows.forEach(function (row) {
                row.style.transition = '';
                row.style.opacity = '';
            });

            if (!areAllCheckboxesChecked()) {

                rows.forEach(function (row) {
                    row.style.transition = 'opacity 0.5s ease';
                    row.style.opacity = '0.5';
                });

                if (currentIndex < rows.length - 1 && checkbox.checked && !checkboxes[currentIndex + 1].checked) {
                    currentIndex++;
                    selectRow(rows[currentIndex]);

                } else {

                    var firstCheckbox = Array.from(checkboxes).find(function (checkbox) {
                        return !checkbox.checked;
                    });

                    currentIndex = firstCheckbox.closest('tr').rowIndex - 1;
                    selectRow(rows[currentIndex]);
                }

                upButton.style.visibility = 'visible';
                upButton.disabled = false;
                downButton.style.visibility = 'visible';
                downButton.disabled = false;

                finishForm.style.display = 'none';
            }
        });
        checkbox.addEventListener('change', updateProgressBar);
    });
});

document.addEventListener("DOMContentLoaded", function () {
    var timerInput = document.getElementById('timerInput');
    var timerPa = document.getElementById('timer');
    var startButton = document.getElementById('startButton');
    var stopButton = document.getElementById('stopButton');
    var startTime;
    var timerInterval;

    startButton.addEventListener('click', function () {
        startTime = new Date().getTime();

        timerInterval = setInterval(function () {
            var currentTime = new Date().getTime();
            var elapsedTime = currentTime - startTime;

            var minutes = Math.floor(elapsedTime / (1000 * 60));
            var seconds = Math.floor((elapsedTime % (1000 * 60)) / 1000);
            var milliseconds = elapsedTime % 1000;

            seconds = seconds < 10 ? '0' + seconds : seconds;
            milliseconds = milliseconds < 100 ? '0' + milliseconds : milliseconds < 10 ? '00' + milliseconds : milliseconds;


            timerInput.value = minutes;
            timerPa.textContent = timerInput.value + ':' + seconds;
        }, 10);
    });

    stopButton.addEventListener('click', function () {
        clearInterval(timerInterval);
    });
});

function displayValidation(duration) {
    var table = document.getElementById('myTable');
    var checkboxes = document.querySelectorAll('input[type="checkbox"]');
    var labels = table.querySelectorAll('tbody > tr > td > label');
    var finishForm = document.getElementById('finishForm');
    var timerPa = document.getElementById('timer');
    var startButton = document.getElementById('startButton');
    var timerDiv = document.getElementById('divTimer');

    checkboxes.forEach(function (checkbox) {
        checkbox.checked = true;
    });

    labels.forEach(function (label) {
        label.style.visibility = 'visible';
    });

    finishForm.style.display = 'block';

    startButton.disabled = true;
    startButton.style.visibility = 'hidden';
    startButton.style.transition = 'opacity 0.1s ease';
    timerDiv.style.visibility = 'visible';

    timerPa.textContent = duration + ':' + '00';
}