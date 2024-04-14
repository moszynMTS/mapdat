import { Component, AfterViewInit } from '@angular/core';
import * as L from 'leaflet';
import { ApiCaller } from 'src/app/shared/apiCaller/apiCaller';

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
  private usedLayer: number = 3;

  private geoJSONLayer: any;

  constructor(private apiCaller: ApiCaller) {
   }

  ngAfterViewInit(): void {
    this.initMap();
    this.addGeoJSONLayer(); 
    this.testConnection();
  }

  private initMap(): void {
    this.map = L.map('map').setView([52, 20], 6); // init on Poland

    L.tileLayer(this.mapStrings[this.usedMap].url, this.mapStrings[this.usedMap].options).addTo(this.map);
  }

  private addGeoJSONLayer(): void {
    fetch(this.layerStrings[this.usedLayer]).then(res => res.json()).then(data => {
      this.geoJSONLayer = L.geoJSON(data, {
        style: {
          fillColor: 'rgba(29, 136, 229, 0.3)',
          weight: 2,
          opacity: 1,
          color: 'rgba(29, 136, 229, 1)',
          dashArray: '3',
          fillOpacity: 0.7
        }
      }).addTo(this.map);
    });
  }

  private testConnection(){
    console.log("test connection")
    this.apiCaller.setControllerPath('Test');
    this.apiCaller.test('test').subscribe((res:any)=>{
      let time = `${res.content.name} z API`;
      alert(time);
    })
  }
}
