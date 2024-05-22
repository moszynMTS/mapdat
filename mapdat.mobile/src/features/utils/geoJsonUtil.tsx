import { prepareGeoJSON } from "../../utils/prepareGeoJson";

class GeoJsonUtil {
    constructor() {
    }
  
    transformGeoJsonToList(data: any){
        let list: any[] = [];
        data.content.forEach((x: any) => {
          list.push(prepareGeoJSON(x))
        })
        return list;
    }
  
    
  }
  
  export default GeoJsonUtil;
  