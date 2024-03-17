function loadData(exerciseId) {
    $.ajax({
        url: '/FitnessProgram/GetAllProgramsAjax?withoutExerciseId=' + exerciseId,
        type: 'GET',
        dataType: 'json',
        success: function (data) {

            var ul = document.getElementById("myList");

            data.forEach(function (itemText) {
                var li = document.createElement("li");
                var a = document.createElement("a");
                a.textContent = itemText.name;
                a.href = "/FitnessProgram/AddExerciseToProgram?programId=" + itemText.id + "&exerciseId=" + exerciseId + "&exerciseCount=" + itemText.exerciseCount;
                a.classList.add("dropdown-item");
                a.classList.add("text-light");
                li.appendChild(a);
                ul.appendChild(li);
            });
        },
    });
}