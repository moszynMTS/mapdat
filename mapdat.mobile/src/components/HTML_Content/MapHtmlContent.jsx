export const MapHtmlContent = ({
    lat = null,
    lon = null,
    area = null,
    draggable = false,
    list = null
  })  => {
    /*
  marker.on('drag', () => {
          const markerLatLng = marker.getLatLng();
          radius.setLatLng(markerLatLng, ${lat != null ? lat :50.866077}, ${ lon != null ? lon : 20.628568});
          window.ReactNativeWebView.postMessage(JSON.stringify(markerLatLng));
        });
  
        marker.on('dragend', () => {
          const markerLatLng = marker.getLatLng();
          window.ReactNativeWebView.postMessage(JSON.stringify({ markerLatLng, lastCoords: true }));
        });
    */
  return `
  <!DOCTYPE html>
  <html>
    <head>
      <title>Leaflet Map</title>
      <meta charset="utf-8" />
      <meta name="viewport" content="width=device-width, initial-scale=1" />
      <link rel="stylesheet" href="https://unpkg.com/leaflet/dist/leaflet.css" />
      <style>
        #map { height: 100%; }
        html, body { height: 100%; margin: 0; padding: 0; }
      </style>
    </head>
    <body>
      <div id="map"></div>
      <script src="https://unpkg.com/leaflet/dist/leaflet.js"></script>
      <script>
  
        var myIcon = L.icon({
          iconSize: [32, 37],
          iconAnchor: [16, 37],
          shadowUrl: '', 
        })
        var map = L.map('map').setView([52, 20], 5); 
        
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
          attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        }).addTo(map);

        var tmp = {
          "type": "FeatureCollection",
          "features": ${list}
        };
  
        var geoJSONLayer = L.geoJSON(JSON.parse(JSON.stringify(tmp)), {
          style: {
            fillColor: 'rgba(29, 136, 229, 0.3)',
            weight: 2,
            opacity: 1,
            color: 'rgba(29, 136, 229, 1)',
            dashArray: '3',
            fillOpacity: 0.7
          },
          onEachFeature: (feature, layer) => {
            layer.on('click', () => {
              window.ReactNativeWebView.postMessage(JSON.stringify(feature));
            });
          }
        }).addTo(map);
        </script>
    </body>
  </html>
  `;
  };
    