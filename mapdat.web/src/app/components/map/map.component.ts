import { Component, AfterViewInit } from '@angular/core';
import { EOVERFLOW } from 'constants';
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
  public mapStrings: any[] = [
    {
      url: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
      options: {
        attribution: '© OpenStreetMap contributors'
      }
    },
    {
      url: 'https://{s}.basemaps.cartocdn.com/dark_all/{z}/{x}/{y}{r}.png',
      options: {
        attribution: '© CARTO'
      }
    },
    {
      url: 'https://{s}.tile.opentopomap.org/{z}/{x}/{y}.png',
      options: {
        attribution: '© OpenTopoMap contributors'
      }
    }
  ];

  private usedMap: number = 0;

  public geoJSONLayer1: any; //województwa
  public geoJSONLayer2: any; //powiaty
  public geoJSONLayer3: any; //gminy
  public voivodeshipsList: any[] = [];

  private tileLayer: any;
  constructor(private apiCaller: ApiCaller) {
  }

  ngAfterViewInit(): void {
    this.initMap();
    this.getCommunes();
  }

  private initMap(): void {
    this.map = L.map('map').setView([52, 20], 6); // init on poland

    this.updateTileLayer();
  }

  private updateTileLayer(): void {
      if (this.tileLayer) {
          this.tileLayer.remove();
      }
      this.tileLayer = L.tileLayer(this.mapStrings[this.usedMap].url, this.mapStrings[this.usedMap].options).addTo(this.map);
  }

  private changeTileLayer(): void {
      this.updateTileLayer();
  }

  private getCommunes(){
    this.apiCaller.setControllerPath('Wojewodztwa');
    this.apiCaller.getWojewodztwa().subscribe((res: any) => {;
      let list: any[] = [];
      res.content.forEach((x:any)=>{
        list.push(this.addGeoJSONLayer(x));
      })
      this.setLayers(list, 1);
      this.voivodeshipsList = list;
    });
  }

  private setLayers(list: any[], layers: number) {
    var tmp = {
      "type": "FeatureCollection",
      "features": list
    };
    switch(layers){
      case 1:
        this.geoJSONLayer1 = L.geoJSON(JSON.parse(JSON.stringify(tmp)), {
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
              this.onClickFeature(feature, layers);
            });
          }
        }).addTo(this.map);
        break;
      case 2:
        this.geoJSONLayer2 = L.geoJSON(JSON.parse(JSON.stringify(tmp)), {
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
              this.onClickFeature(feature, layers);
            });
          }
        }).addTo(this.map);
        break;
      case 3:
        this.geoJSONLayer2 = L.geoJSON(JSON.parse(JSON.stringify(tmp)), {
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
              this.onClickFeature(feature, layers);
            });
          }
        }).addTo(this.map);
        break;
    }
  }

  private onClickFeature(feature: any, layers: number) {
    if (feature.properties && feature.properties.name) {
      switch(layers){
        case 1:
          this.getPowiaty(feature.properties.name);
        break;
        case 2:
          this.getGminy(feature.properties.name)
        break;
        case 3:
          //nothing
        break;
      }
    }
  }

  private addGeoJSONLayer(geo: any) {
    if (!geo.geometry || !geo.geometry.coordinates || !geo.geometry.type) {
      console.error('Nieprawidłowe współrzędne:', geo);
      return null; 
    }
    if(geo.geometry.type=="Polygon") {
      for(let i=0;i<geo.geometry.coordinates.length;i++){
        var tmpCoord: any[] = []
        geo.geometry.coordinates[i].forEach((element:any) => {
          if(element.length==1){
            tmpCoord.push([element[0][0],element[0][1]])
          }
          else
            tmpCoord.push(element)
        });
        geo.geometry.coordinates[i]=tmpCoord; 
      }
    }
    var geojsonFeature = {
      "type": geo.type,
      "properties": {
          "name": geo.properties.name,
          "popupContent": geo.properties.name
      },
      "geometry": {
          "type":  geo.geometry.type,
          "coordinates": geo.geometry.coordinates
      }
    };
    return geojsonFeature
  }


  getPowiaty(powiat: any){
    this.apiCaller.setControllerPath('Powiaty');
    this.apiCaller.getPowiaty(powiat).subscribe((res: any) => {
      this.disableMap(this.geoJSONLayer1);
      let list: any[] = [];
      res.content.forEach((x:any)=>{
        list.push(this.addGeoJSONLayer(x));
      })
      this.setLayers(list, 2);
    });
  }

  getGminy(gmina: any){
    this.apiCaller.setControllerPath('Gminy');
    this.apiCaller.getGminy(gmina).subscribe((res: any) => {
      this.disableMap(this.geoJSONLayer2);
      let list: any[] = [];
      res.content.forEach((x:any)=>{
        list.push(this.addGeoJSONLayer(x));
      })
      this.setLayers(list, 3);
    });
  }

  private clearMap(layer: any) {
    if (layer) {
      layer.clearLayers();
    }
  }
  
  private disableMap(layer: any) { //set layers to gray
    if (layer) {
      layer.setStyle({
        fillColor: 'rgba(128, 128, 128, 0.3)',
        weight: 2,
        opacity: 1,
        color: 'rgba(128, 128, 128, 1)', 
        dashArray: '3',
        fillOpacity: 0.5
      });
    }
  }

  setLayerView(newMap: number){
    this.usedMap = newMap;
    this.changeTileLayer();
  }

}
