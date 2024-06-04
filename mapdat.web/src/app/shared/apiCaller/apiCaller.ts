import { HttpClient, HttpParams } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable()
export class ApiCaller {
    readonly APIUrl = "https://localhost:44324/api/";
    private typeName: string = "";

    constructor(private http: HttpClient) {
    }
    public setControllerPath(controllerPath: string) {
        this.typeName = controllerPath;
    }
    
    test(Name: any): Observable<any[]> {
        const payload = { Name };
        return this.http.get<any>(this.APIUrl + this.typeName, {params: payload});
    }
    getWojewodztwa(): Observable<any[]> {
        return this.http.get<any>(this.APIUrl + this.typeName);
    }

    getPowiaty(Wojewodztwo: any): Observable<any[]> {
        const payload = { Wojewodztwo };
        return this.http.get<any>(this.APIUrl + this.typeName, {params: payload});
    }

    getPowiat(id: any): Observable<any[]> {
        const payload = { id };
        return this.http.get<any>(this.APIUrl + this.typeName+"/ById", {params: payload});
    }

    getGminy(Powiat: any, PowiatId: any): Observable<any[]> {
        const payload = { Powiat, PowiatId };
        return this.http.get<any>(this.APIUrl + this.typeName, {params: payload});
    }

    getGmina(id: any): Observable<any[]> {
        const payload = { id };
        return this.http.get<any>(this.APIUrl + this.typeName+"/ById", {params: payload});
    }
      
}
