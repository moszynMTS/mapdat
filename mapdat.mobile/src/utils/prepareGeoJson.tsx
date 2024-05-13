export function prepareGeoJSON(geo: any) {
    if (!geo.geometry || !geo.geometry.coordinates || !geo.geometry.type) {
      console.error('Nieprawidłowe współrzędne:', geo);
      return null; 
    }
    var geojsonFeature = {
      "type": geo.type,
      "properties": {
          "name": geo.properties.name,
          "popupContent": geo.properties.name
      },
      "geometry": {
          "type": geo.geometry.type,
          "coordinates": geo.geometry.coordinates
      }
    };
    return geojsonFeature;
  }
  