
function trackElement() {
    var sourceElement = document.getElementById("sourceElement");
    var targetElementP = document.getElementById("targetElementP");
    var targetElementN = document.getElementById("targetElementN");
    
    var sourceHeight = sourceElement.offsetHeight;
    targetElementN.style.height = sourceHeight + "px";  
    targetElementP.style.height = sourceHeight + "px";
};

window.onload = function () {
    trackElement();
    window.addEventListener('resize', trackElement);
};

