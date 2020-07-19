//(function () {
//    var mapElement = document.getElementById("map-canvas");
//    var isMapVisible = mapElement.style.visibility !== "hidden" && mapElement.offsetWidth > 500; 
//    if (isMapVisible) {
//        var latitude = Number(document.getElementById("Municipality_Latitude").value);
//        var longitude = Number(document.getElementById("Municipality_Longitude").value);
//        var offset = 0.016408460434895744;
//        var center = new google.maps.LatLng(latitude + offset, longitude);

//        var mapOptions = {
//            center: center,
//            zoom: 14,
//            mapTypeId: google.maps.MapTypeId.HYBRID,
//            disableDefaultUI: true,
//            draggable: false,
//            zoomControl: false,
//            scrollwheel: false,
//            disableDoubleClickZoom: true 
//        };
//        new google.maps.Map(mapElement, mapOptions);
//    }
//})();

(function () {
    var mapElement = document.getElementById("map-canvas");
    var isMapVisible = mapElement.style.visibility !== "hidden" && mapElement.offsetWidth > 500;
    if (isMapVisible) {
        var latitude = Number(document.getElementById("Municipality_Latitude").value);
        var longitude = Number(document.getElementById("Municipality_Longitude").value);
        var offset = 0.016408460434895744;
        //var center = new google.maps.LatLng(latitude + offset, longitude);

        console.log(longitude, latitude);

        var center = SMap.Coords.fromWGS84(longitude + offset, latitude + offset);
        var m = new SMap(JAK.gel("map-canvas"), center, 13);
        m.addDefaultLayer(SMap.DEF_BASE).enable();

        var sync = new SMap.Control.Sync({ bottomSpace: 30 });
        m.addControl(sync);
    }
}());