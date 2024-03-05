function previewImage(event) {

    const fileInput = event.target;
    const imagePreview = document.getElementById('imagePreview');

    if (fileInput.files.length > 0) {
        const selectedFile = fileInput.files[0];

        if (selectedFile.type.startsWith('image/')) {
            const reader = new FileReader();

            reader.onload = function (e) {
                imagePreview.src = e.target.result;
            };

            reader.readAsDataURL(selectedFile);
        } else {

            alert('Please select a valid image file.');
            fileInput.value = ''; // Clear the file input
            imagePreview.src = ''; // Clear the image preview
        }

    } else {

        imagePreview.src = '';
    }
}