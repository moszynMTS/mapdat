import MapDatBaseApiCaller from "./MapDatBaseApiCaller.service";


class MapDatApiCallerFactory {
  constructor() {
  }

  getApiImplementation(dictionaryType) {
    const apiCallerService = new MapDatBaseApiCaller();
    apiCallerService.setControllerPath(dictionaryType.toString());
    return apiCallerService;
  }

  
}

export default MapDatApiCallerFactory;
