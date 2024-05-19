import { prepareGeoJSON } from "../../utils/prepareGeoJson";

class GeoJsonFactory {
    constructor() {
    }
  
    transformGeoJsonToList(data: any){
        let list: any[] = [];
        data.content.forEach((x: any) => {
          list.push(prepareGeoJSON(x))
        })
        console.log(list)
        return list;
    }
  
    
  }
  
  export default GeoJsonFactory;
  