//Function to preview images
var loadFile = function (event) {
    let files = event.target.files;
    let imageList = document.getElementById("imageList");
    imageList.innerHTML = "";
    for (var i = 0; i < files.length; i++) {
        var reader = new FileReader();
        reader.addEventListener("load", function (event) {
            let image = document.createElement("IMG");
            image.src = event.target.result;
            image.className = "img-thumbnail col-4";

            document.getElementById("imageList").append(image);
        });

        reader.readAsDataURL(files[i]);
    }

};

