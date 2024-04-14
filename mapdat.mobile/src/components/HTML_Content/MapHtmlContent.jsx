export const MapHtmlContent = ({
    lat = null,
    lon = null,
    area = null,
    draggable = false,
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
        var map = L.map('map').setView([${lat != null ? lat :50.866077}, ${ lon != null ? lon : 20.628568}], 14);
        
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
          attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        }).addTo(map);
        var marker = L.marker([${lat != null ? lat :50.866077}, ${ lon != null ? lon : 20.628568}], {draggable: ${draggable}}).addTo(map);
  
        var radius = L.circle([${lat != null ? lat :50.866077}, ${ lon != null ? lon : 20.628568}], {
          radius: ${area},
          color: '#006ca2',
          fillColor: 'rgba(51, 187, 255, 0.2)',
          fillOpacity: 1
        }).addTo(map);
  
        marker.on('drag', () => {
          const markerLatLng = marker.getLatLng();
          radius.setLatLng(markerLatLng, ${lat != null ? lat :50.866077}, ${ lon != null ? lon : 20.628568});
        });
  
        </script>
    </body>
  </html>
  `;
  };
    