import { backendHostname } from "../environments/environments";
import axios from "axios";

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

  async get(path = "", options = {}) {
    const fullPath = this.getFullPath(path);
    console.log(fullPath)
    return axios.get(fullPath);
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
  
  
  
  getFullPath(path) {
    const baseUrl = `${API_BASE_URL}/api/${this.controllerPath}`;
    if(path == "")
      return baseUrl
    else
      return baseUrl + path;
  }
}

export default BaseApiCaller;
