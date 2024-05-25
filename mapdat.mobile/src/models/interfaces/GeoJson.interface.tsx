export interface GeoJson{
    type: string
    properties: {
        name: string,
        popupContent: any
    }
    geometry: {
        type: string,
        coordinates: any
    }
}