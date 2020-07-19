(function () {
    var mapElement = document.getElementById("map-canvas");
    var isMapVisible = mapElement.style.visibility !== "hidden" && mapElement.offsetWidth > 500;
    if (isMapVisible) {
        var latitude = Number(document.getElementById("Municipality_Latitude").value);
        var longitude = Number(document.getElementById("Municipality_Longitude").value);
        var offset = 0.04;
        var center = SMap.Coords.fromWGS84(longitude, latitude + (offset));
        var m = new SMap(JAK.gel("map-canvas"), center, 13);
        m.addDefaultLayer(SMap.DEF_BASE).enable();

        var sync = new SMap.Control.Sync({ bottomSpace: 30 });
        m.addControl(sync);
    }
}());