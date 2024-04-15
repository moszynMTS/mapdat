import { backendHostname } from "../environments/environments";

export const API_BASE_URL = backendHostname;

export const HttpOptions = {
  headers: {},
  params: {},
  responseType: "json",
  withCredentials: false,
  timeout: 5000,
};

class BaseApiCaller {
  constructor() {
    this.controllerPath = "";
  }

  async get(path, options = {}) {

    if (options?.params ) {
      const searchParams = new URLSearchParams();

      Object.keys(options.params).forEach((key) => {
        if (options.params[key] != null) {
          searchParams.append(key, options.params[key]);
        }
      });
      if(searchParams.toString() != undefined)
        path += "?" +searchParams.toString();
    }
    const fullPath = this.getFullPath(path);
    // console.log(fullPath);

    const response = await fetch(fullPath , {
      method: "GET",
      headers: {
        Accept: "text/plain",
      },
      params: options.params || {},
    })
    // this.checkError(response);
    return response;
  }

  async post(path, body, options = {}, contentType = "application/json") {
    const fullPath = this.getFullPath(path);
    // console.log(fullPath);

    const response = await fetch(fullPath, {
      method: "POST",
      headers: {
        'Content-Type': contentType,
        'accept': 'text/plain'
      },
      params: options.params || {},
      body: JSON.stringify(body),
    });
   
    // this.checkError(response);
    return response;
  }

  async postWithReponse(path, body, options = {}) {
    const fullPath = this.getFullPath(path);
    const response = await fetch(fullPath, {
      method: "POST",
      headers: options.headers || {},
      params: options.params || {},
      body: JSON.stringify(body),
    });
    this.checkError(response);

    return response.json();
  }

  async put(path, body, options = {}, contentType = "application/json") {

    const fullPath = this.getFullPath(path);
    // console.log(body);
    console.log(fullPath);
    const response = await fetch(fullPath, {
      method: "PUT",
      headers: {
        'Content-Type': contentType,
      },
      params: options.params || {},
      body: JSON.stringify(body),
    });
   
    this.checkError(response);
    return response;
  }

  async delete(path, body, options = {}, contentType = "application/json") {
    const data = body != null ? body+"/false" : "";
    const fullPath = this.getFullPath(path)+data;
    console.log(fullPath);
    const response = await fetch(fullPath, {
      method: "DELETE",
      headers: {
        'Content-Type': contentType,
      },
      params: options.params || {},
      body: options != {} ? JSON.stringify(options) : {},
    });
   
    this.checkError(response);
    return response;
  }

  checkError(response) {
    if (!response.ok) {
      console.error("Request failed with status:", response.status);
      console.error("Response body:", response.body); 
      throw new Error("Error with request");
    }
  }
  
  
  getPageableParams(filter) {
    const result = {
      desc: filter.desc.toString(),
      orderBy: filter.orderBy,
      pageNumber: filter.pageNumber.toString(),
      pageSize: filter.pageSize.toString(),
      searchTerm: undefined,
    };
    if (filter.searchTerm != null && filter.searchTerm.length > 0) {
      result.searchTerm = filter.searchTerm;
    }
  
    return result;
  }
  

  prepareParams(data) {
    const params = {};
  
    Object.keys(data).forEach((item) => {
      if (data[item] !== null) {
        if (Array.isArray(data[item])) {
          data[item].forEach((element) => {
            if (!params[item]) {
              params[item] = [];
            }
            params[item].push(element);
          });
        } else {
          params[item] = data[item].toString();
        }
      }
    });
  
    const paramString = Object.keys(params)
      .map((key) => `${encodeURIComponent(key)}=${encodeURIComponent(params[key])}`)
      .join('&');
  
    return paramString;
  }
  
  getFullPath(path) {
    return `${API_BASE_URL}/api/${this.controllerPath}/${path}`;
  }
}

export default BaseApiCaller;
