function loadFile(event) {

    let image = document.getElementById('image-to-upload');
    $(image).show();
    image.src = URL.createObjectURL(event.target.files[0]);
}
/*
document.getElementById('map').onload = (function () {
    $('img.leaflet-marker-icon').click(function () {
        let id = this.src.substring(61);
        let dogElement = document.getElementById(id);
        $(dogElement).addClass('hover');
    });

    $('img.leaflet-marker-icon').mouseleave(function () {
        let id = this.src.substring(61);
        let dogElement = document.getElementById(id);
        $(dogElement).removeClass('hover');
    });
}); */

function updateMap(dogJson) {

    //Get an array of the locations in the right format for geocode argument
    let dogLocations = [];
    for (let i = 0; i < dogJson.length; i++)
    {
        dogLocations.push(dogJson[i].StreetAddress + ', ' + dogJson[i].Zipcode);
    }
    L.mapquest.key = 'mMnXGCQhZquPUCwudUmc4aAzCDyOmJ2m';

    // Geocode three locations, then call the createMap callback
    L.mapquest.geocoding().geocode(dogLocations, createMap);

    function createMap(error, response) {
        // Initialize the Map
        var map = L.mapquest.map('map', {
            layers: L.mapquest.tileLayer('map'),
            /* in the future, get users zip location and center there */
            center: [0, 0],
            zoom: 10
        });

        // Generate the feature group containing markers from the geocoded locations
        var featureGroup = generateMarkersFeatureGroup(response);

        // Add markers to the map and zoom to the features
        featureGroup.addTo(map);
        map.fitBounds(featureGroup.getBounds());
    }

    function generateMarkersFeatureGroup(response) {
        var group = [];
        for (var i = 0; i < response.results.length; i++) {
            var location = response.results[i].locations[0];
            var locationLatLng = location.latLng;

            var markerSize = { 'sm': [28, 35], 'md': [35, 44], 'lg': [42, 53] };

            var thisIcon = new L.Icon({
                iconUrl: 'http://assets.mapquestapi.com/icon/v2/marker-sm----@2x.png?i=' + (dogJson[i].Id),
                iconSize: markerSize.md,
                /* This aligns popup to marker, don't know why */
                iconAnchor: new L.Point(16, 16)
            });

            // Create a marker for each location
            var marker = L.marker(locationLatLng, { icon: thisIcon })
                .on('click', function (e) { /*
                 * 
                 * Add click functionality here to highlight dogtile,
                 * following doesnt work: doesnt recognize this because 
                 * it is firing before the event happens to set up
                    let id = this.src.substring(61);
                    let dogElement = document.getElementById(id);
                    $(dogElement).addClass('hover'); */
                })
                .bindPopup('<a href=' + 'http://localhost:56830/Home/Details/' + dogJson[i].Id + '>'
                    /*Had to add a <br> and <span>'s because <p> without <br> adds an extra space*/
                + dogJson[i].Name + '</a></br><span class="location">' + dogJson[i].CityLocation + ', ' +
                dogJson[i].StateLocation + '</span>');
                
            group.push(marker);
        }
        return L.featureGroup(group);
    }
}


let marker;

$('.dog-tile').hover(function () {
    let markers = $('.leaflet-marker-icon');
    for (let i = 0; i < markers.length; i++) {
        if (markers[i].src.substring(61) == $(this).attr('id')) {
            marker = markers[i];
            if (!($('div.leaflet-container a').html().trim() == $(this).find('h3').html().trim())) {
                marker.click();
            } 
        }
    }


    /*
     * somehow stays clicked now. Going to leave this for now.
    $('.dog-tile').mouseleave(function () {
        marker.click();
    });
    */
});

