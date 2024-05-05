import { Component, AfterViewInit } from '@angular/core';
import * as L from 'leaflet';
import { ApiCaller } from 'src/app/shared/apiCaller/apiCaller';
import { GeoObjects } from 'src/app/shared/apiCaller/interfaces';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements AfterViewInit {

  private map: any;
  private mapStrings: any[] = [
    {
      url: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
      options: {
        attribution: '© OpenStreetMap contributors'
      }
    },
    {
      url: 'https://{s}.tile.opentopomap.org/{z}/{x}/{y}.png',
      options: {
        attribution: '© OpenTopoMap contributors'
      }
    },
    {
      url: 'https://{s}.basemaps.cartocdn.com/dark_all/{z}/{x}/{y}{r}.png',
      options: {
        attribution: '© CARTO'
      }
    }
  ];

  private layerStrings: string[] = [
    'https://raw.githubusercontent.com/ppatrzyk/polska-geojson/master/wojewodztwa/wojewodztwa-max.geojson',
    'https://raw.githubusercontent.com/ppatrzyk/polska-geojson/master/wojewodztwa/wojewodztwa-medium.geojson',
    'https://raw.githubusercontent.com/ppatrzyk/polska-geojson/master/wojewodztwa/wojewodztwa-min.geojson',
    'https://raw.githubusercontent.com/ppatrzyk/polska-geojson/master/powiaty/powiaty-medium.geojson',
    'https://raw.githubusercontent.com/ppatrzyk/polska-geojson/master/powiaty/powiaty-min.geojson'
  ];
  private usedMap: number = 0;
  private usedLayer: number = 1;

  private geoJSONLayer: any;
  
  public voivodeshipsList: any[] = [];
  constructor(private apiCaller: ApiCaller) {
   }

  ngAfterViewInit(): void {
    this.initMap();
    //this.addGeoJSONLayer(); 
    this.getCommunes();
  }

  private initMap(): void {
    this.map = L.map('map').setView([52, 20], 6); // init on Poland

    L.tileLayer(this.mapStrings[this.usedMap].url, this.mapStrings[this.usedMap].options).addTo(this.map);
  }

  private getCommunes(){
    this.apiCaller.setControllerPath('Wojewodztwa');
    this.apiCaller.getWojewodztwa().subscribe((res: any) => {;
      console.log("getCommunes", res)
      let list: any[] = [];
      res.content.forEach((x:any)=>{
        list.push(this.addGeoJSONLayer(x));
      })
      this.setLayers(list);
      this.voivodeshipsList = list;
    });
  }
  private setLayers(list: any[]) {
    var tmp = {
      "type": "FeatureCollection",
      "features": list
    };

    this.geoJSONLayer = L.geoJSON(JSON.parse(JSON.stringify(tmp)), {
      style: {
        fillColor: 'rgba(29, 136, 229, 0.3)',
        weight: 2,
        opacity: 1,
        color: 'rgba(29, 136, 229, 1)',
        dashArray: '3',
        fillOpacity: 0.7
      },
      onEachFeature: (feature: any, layer: any) => {
        layer.on('click', () => {
          this.onClickFeature(feature);
        });
      }
    }).addTo(this.map);
  }

  private onClickFeature(feature: any) {
    if (feature.properties && feature.properties.name) { //get powiat from vividship
      this.getPowiaty(feature.properties.name);
    }
  }

  private addGeoJSONLayer(geo: any) {
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
    return geojsonFeature
  }


  private getPowiaty(powiat: any){
    powiat = "slaskie"
    this.apiCaller.setControllerPath('Powiaty');
    this.apiCaller.getPowiaty(powiat).subscribe((res: any) => {
      this.disableMap();
      console.log("GETPOWIATY", res)
      let list: any[] = [];
      res.content.forEach((x:any)=>{
        list.push(this.addGeoJSONLayer(x));
      })
      this.setLayers(list);
    });
  }

  private clearMap() {
    if (this.geoJSONLayer) {
      this.geoJSONLayer.clearLayers();
    }
  }
  
  private disableMap() { //set layers to gray
    if (this.geoJSONLayer) {
      this.geoJSONLayer.setStyle({
        fillColor: 'rgba(128, 128, 128, 0.3)',
        weight: 2,
        opacity: 1,
        color: 'rgba(128, 128, 128, 1)', 
        dashArray: '3',
        fillOpacity: 0.5
      });
    }
  }

}
