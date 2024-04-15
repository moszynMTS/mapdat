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

  getItem(uniqueId) {
    return this.get(uniqueId.toString())
      .then((response) => {
        if (!response.ok) {
          if (response.status === 404) {
            // Obsługa błędu 403 (Forbidden)
            // console.error("The given item doesn't exist .");
            return { error: "404" };
          } else {
            return { error: response.status };
            // console.error(`HTTP error: ${response.status}`);
            throw new Error(`HTTP error: ${response.status}`);
          }
        }
        return response.text();
      })
      .then((result) => {
        return result;
      });
  }
}

export default MargizBaseApiCaller;