export interface GeoObjects {
    border: Border[];
}
export interface Border {
    id: string;
    type: string;
    properties: Properties;
    geometry: Geometry;
}

export interface Properties {
    name: any; //now int, might change
}

export interface Geometry {
    type: string;
    coordinates: any;
}