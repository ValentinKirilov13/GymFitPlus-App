
var screenHeight = window.screen.height;

var skip = 0;
var take = Math.ceil(screenHeight / 42.5);
var rowNum = 0;
function fetchRecords() {
    $.ajax({
        url: '/Workout/Index',
        type: 'GET',
        data: { skip: skip, take: take },
        success: function (data) {

            data.forEach(function (d) {
                addRow(d);
            });

            skip += take;
        },
        error: function () {
            console.error('Error fetching records.');
        }
    });
}

function addRow(record) {
    var table = document.getElementById("recordsTable");
    var newRow = document.createElement("tr");

    var id = document.createElement("th");
    var programName = document.createElement("td");
    var duration = document.createElement("td");
    var date = document.createElement("td");
    var dateActual = new Date(record.date);
    var formattedDate = dateActual.toLocaleDateString('en-US', {
        day: 'numeric',
        weekday: 'long',
        year: 'numeric',
        month: 'long',
    });

    id.textContent = ++rowNum;
    programName.textContent = record.fitnessProgramName;
    duration.textContent = record.duration + " " + "minutes";
    duration.classList.add("text-center");
    date.textContent = formattedDate;
    date.classList.add("text-center");

    newRow.appendChild(id);
    newRow.appendChild(programName);
    newRow.appendChild(duration);
    newRow.appendChild(date);

    newRow.onclick = function () {
        handleRowClick(record.id);
    };


    table.querySelector("tbody").appendChild(newRow);
}

$(window).scroll(function () {
    if ($(window).scrollTop() == $(document).height() - $(window).height()) {
        fetchRecords();
    }
});

$(document).ready(function () {
    fetchRecords();
});

function handleRowClick(id) {
    $.ajax({
        url: '/Workout/Details',
        type: 'GET',
        data: { workoutId: encodeURIComponent(id)},
        dataType: "json",
        success: function (data) {

            $("#programName").text(data.fitnessProgramName);
            $("#duration").text(data.duration);
            $("#note").text(data.note);
            $("#deleteElementId").val(data.id);
            $("#userId").val(data.userId);


            var dateActual = new Date(data.date);
            var formattedDate = dateActual.toLocaleDateString('en-US', {
                day: 'numeric',
                weekday: 'long',
                year: 'numeric',
                month: 'long',
            });
            $("#date").text(formattedDate);

            document.getElementById("detailsModalButton").click();
        },
        error: function () {
            console.error('Error fetching records.');
        }
    });
}
