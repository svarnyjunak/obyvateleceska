(function () {
    var mapElement = document.getElementById("map-canvas");
    var isMapVisible = mapElement.style.visibility !== "hidden" && mapElement.offsetWidth > 400;
    if (isMapVisible) {
        Loader.async = true;
        Loader.load(null, null, createMap);
    }

    function createMap() {
        var latitude = Number(document.getElementById("Municipality_Latitude").value);
        var longitude = Number(document.getElementById("Municipality_Longitude").value);
        var center = SMap.Coords.fromWGS84(longitude, latitude);
        var m = new SMap(JAK.gel("map-canvas"), center, 13);
        m.addDefaultLayer(SMap.DEF_BASE).enable();

        var markerLayer = new SMap.Layer.Marker();
        m.addLayer(markerLayer);
        markerLayer.enable();

        var markerElement = JAK.mel("div");
        var img = JAK.mel("img", { src: SMap.CONFIG.img + "/marker/drop-blue.png" });
        markerElement.appendChild(img);

        var marker = new SMap.Marker(m.getCenter(), null, { url: markerElement });
        markerLayer.addMarker(marker);

        var sync = new SMap.Control.Sync({ bottomSpace: 30 });
        m.addControl(sync);
    };
}());