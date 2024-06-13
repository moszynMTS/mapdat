import { prepareSubjectsRequest } from "../../utils/prepareSubjectsRequest";
import BaseApiCaller, { API_BASE_URL } from "../api-services/BaseApiCaller";

class MapDatBaseApiCaller extends BaseApiCaller {
  constructor(httpClient) {
    super(httpClient);
  }
  setControllerPath(controllerPath) {
    this.controllerPath = controllerPath;
  }

  addItem(dto) {
    return this.post("", dto);
  }

  getItem(id = "") {
    return this.get(id);
  }
  getInfo(arrayName, id){
    return this.get(`?${arrayName}=${id}${prepareSubjectsRequest()}`) ;
  }
  getPowiat(woj){
    return this.get(`?Wojewodztwo=${woj}`);
  }
}

export default MapDatBaseApiCaller;