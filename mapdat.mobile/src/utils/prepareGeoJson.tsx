export function prepareGeoJSON(geo: any) {
  if (!geo.geometry || !geo.geometry.coordinates || !geo.geometry.type) {
    console.error('Nieprawidłowe współrzędne:', geo);
    return null;
  }
  if (geo.geometry.type == "Polygon") {
    for (let i = 0; i < geo.geometry.coordinates.length; i++) {
      var tmpCoord: any[] = []
      geo.geometry.coordinates[i].forEach((element: any) => {
        if (element.length == 1) {
          tmpCoord.push([element[0][0], element[0][1]])
        }
        else
          tmpCoord.push(element)
      });
      geo.geometry.coordinates[i] = tmpCoord;
    }
  }
  var geojsonFeature = {
    "type": geo.type,
    "properties": {
      "name": geo.properties.name,
      "popupContent": geo.properties.name,
      "id": geo.id != undefined ? geo.id : null
    },
    "geometry": {
      "type": geo.geometry.type,
      "coordinates": geo.geometry.coordinates
    }
  };
  return geojsonFeature;
}
