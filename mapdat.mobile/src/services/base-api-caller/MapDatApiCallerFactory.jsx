import MargizBaseApiCaller from './MargizBaseApiCaller.service'; 

class MapDatApiCallerFactory {
  constructor() {
  }

  getApiImplementation(dictionaryType) {
    const apiCallerService = new MargizBaseApiCaller();
    apiCallerService.setControllerPath(dictionaryType.toString());
    return apiCallerService;
  }
}

export default MargizApiCallerFactory;
