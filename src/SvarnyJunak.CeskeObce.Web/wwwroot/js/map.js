(function () {
    var mapElement = document.getElementById("map-canvas");
    var isMapVisible = mapElement.style.visibility !== "hidden";
    if (isMapVisible) {
        var latitude = Number(document.getElementById("Municipality_Latitude").value);
        var longitude = Number(document.getElementById("Municipality_Longitude").value);
        var offset = 0.016408460434895744;
        var center = new google.maps.LatLng(latitude + offset, longitude);

        var mapOptions = {
            center: center,
            zoom: 14,
            mapTypeId: google.maps.MapTypeId.HYBRID,
            disableDefaultUI: true,
        };
        new google.maps.Map(mapElement, mapOptions);
    }
})();