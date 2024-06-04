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
  public wojewodztwaList: any[] = [];
  public powiatyList: any[] = [];
  public gminyList: any[] = [];
  public subjects: any[] = [
    {id: 1, name: "Dochody powiatów według województwa"},
    {id: 2, name: "Wydatki powiatów według województwa"}, 
    {id: 3, name: "Ludność według województw"},
    {id: 4, name: "Mediana wieku według województw"},
    {id: 5, name: "Przestępstwa według województw"},
    {id: 6, name: "Biblioteki publiczne według województw"},
    {id: 7, name: "Kina według województw"},
    {id: 8, name: "Kluby sportowe według województw"},
    {id: 9, name: "Gastronomia według województw"},
    {id: 10, name: "Szpitale według województw"},
    {id: 11, name: "Żłobki według województw"},
    {id: 12, name: "Pracujący według województw"},
    {id: 13, name: "Stopa bezrobocia według województw"},
    {id: 14, name: "Szkoły według podziału administracyjnego"},
];

  public selectedSubjectNames: string[] = [];

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
    const onMouseOver = (feature: any, map: L.Map) => (e: any) => {
      var content = '<div class="custom-popup-content">' + feature.properties.name + '</div>';
      L.popup({ closeButton: false }) 
       .setLatLng(e.latlng)
       .setContent(content)
       .openOn(map);
    };
  
    const onMouseOut = () => (layer: any) => {
      layer._popup && layer._popup.remove();
    };

    switch(layers){
      case 1:
        this.geoJSONLayer1 = L.geoJSON(JSON.parse(JSON.stringify(tmp)), {
          style: {
            fillColor: 'rgba(128, 128, 128, 0.3)',
            weight: 2,
            opacity: 1,
            color: 'rgba(128, 128, 128, 1)', 
            dashArray: '3',
            fillOpacity: 0.5
          },
          onEachFeature: (feature: any, layer: any) => {
            layer.on('click', (e: any) => {
              if (e.originalEvent.button === 0) { //lpm
                  this.onClickFeature(feature, layers);
              }
              });
              layer.on('contextmenu', (e: any) => {
                  e.originalEvent.preventDefault();
                  this.onRightClickFeature(feature, layers);  //ppm
              });
              layer.on('mouseover', onMouseOver(feature, this.map));
              layer.on('mouseout', onMouseOut());
          }
        }).addTo(this.map);
        break;
      case 2:
        this.geoJSONLayer2 = L.geoJSON(JSON.parse(JSON.stringify(tmp)), {
          style: {
            fillColor: 'rgba(128, 128, 128, 0.3)',
            weight: 2,
            opacity: 1,
            color: 'rgba(128, 128, 128, 1)', 
            dashArray: '3',
            fillOpacity: 0.5
          },
          onEachFeature: (feature: any, layer: any) => {
            layer.on('click', (e: any) => {
              if (e.originalEvent.button === 0) { //lpm
                  this.onClickFeature(feature, layers);
              }
              });
              layer.on('contextmenu', (e: any) => {
                  e.originalEvent.preventDefault();
                  this.onRightClickFeature(feature, layers);  //ppm
              });
              layer.on('mouseover', onMouseOver(feature, this.map));
              layer.on('mouseout', onMouseOut());
          }
        }).addTo(this.map);
        break;
      case 3:
        this.geoJSONLayer2 = L.geoJSON(JSON.parse(JSON.stringify(tmp)), {
          style: {
            fillColor: 'rgba(128, 128, 128, 0.3)',
            weight: 2,
            opacity: 1,
            color: 'rgba(128, 128, 128, 1)', 
            dashArray: '3',
            fillOpacity: 0.5
          },
          onEachFeature: (feature: any, layer: any) => {
            layer.on('click', (e: any) => {
              if (e.originalEvent.button === 0) { //lpm
                  this.onClickFeature(feature, layers);
              }
              });
              layer.on('contextmenu', (e: any) => {
                  e.originalEvent.preventDefault();
                  this.onRightClickFeature(feature, layers);  //ppm
              });
              layer.on('mouseover', onMouseOver(feature, this.map));
              layer.on('mouseout', onMouseOut());
          }
        }).addTo(this.map);
        break;
    }
  }

  private onClickFeature(feature: any, layers: number) {
    if (feature.properties && feature.properties.name) {
      switch(layers){
        case 1:
          console.log("[P] ",feature.properties.name)
          this.getPowiaty(feature.properties.name);
          break;
          case 2:
            console.log("[G] ",feature.properties.name)
            this.getGminy(feature.properties.name, feature.properties.id)
            break;
          case 3:
            console.log("[A] ",feature.properties.name, feature.properties.id)
          break;
      }
    }
  }

  private onRightClickFeature(feature: any, layers: number) {
    if (feature.properties && feature.properties.name) {
      let item = {id: feature.properties.id, name: feature.properties.name}
      switch(layers){
        case 1:
          if (!this.wojewodztwaList.some(x => x?.id === item?.id)) { //check if entity is unique
              this.wojewodztwaList.push(item);
          }
        break;
        case 2:
          if (!this.powiatyList.some(x => x?.id === item?.id)) {
            this.powiatyList.push(item);
          }
          break;
        case 3:
          if (!this.gminyList.some(x => x?.id === item?.id)) { 
            this.gminyList.push(item);
          }
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
          "popupContent": geo.properties.name,
          "id": geo.id
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
      //this.disableMap(this.geoJSONLayer1);
      let list: any[] = [];
      res.content.forEach((x:any)=>{
        list.push(this.addGeoJSONLayer(x));
      })
      this.setLayers(list, 2);
    });
  }

  getGminy(powiat: any, PowiatId: any){
    this.apiCaller.setControllerPath('Gminy');
    this.apiCaller.getGminy(powiat,PowiatId).subscribe((res: any) => {
      //this.disableMap(this.geoJSONLayer2);
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
  
  private disableMap(layer: any) { //set layers to gray, deprecated
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
  private enableMap(layer: any) { //set layer to blue
    if (layer) {
      layer.setStyle({
        fillColor: 'rgba(29, 136, 229, 0.3)',
        weight: 2,
        opacity: 1,
        color: 'rgba(29, 136, 229, 1)',
        dashArray: '3',
        fillOpacity: 0.7
      });
    }
  }

  setLayerView(newMap: number){
    this.usedMap = newMap;
    this.changeTileLayer();
  }

  removeFromList(list: number, id: string) {
    switch(list){
      case 1:
        this.wojewodztwaList = this.wojewodztwaList.filter(item => item.id !== id);
        break;
      case 2:
        this.powiatyList = this.powiatyList.filter(item => item.id !== id);
        break;
      case 3:
        this.gminyList = this.gminyList.filter(item => item.id !== id);
        break;
    }
  }

  checkResult(){
    this.apiCaller.setControllerPath('RSPO');
    let sending: string[][] = [
      this.wojewodztwaList,
      this.powiatyList,
      this.gminyList,
      this.subjects
    ];
    console.log(sending, this.selectedSubjectNames);
    this.apiCaller.getSchools(sending).subscribe((res: any) => {
      console.log("RES", res)
      alert(res);
    })
  }
}
