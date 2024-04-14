import { HttpClient, HttpParams } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable()
export class ApiCaller {
    readonly APIUrl = "https://localhost:7100/api/";
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
      
}
