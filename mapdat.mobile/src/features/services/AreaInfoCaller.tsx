import { UseMutationResult, UseQueryResult, useMutation, useQuery } from "@tanstack/react-query";
import MapDatApiCallerFactory from "../../services/base-api-caller/MapDatApiCallerFactory";
import { BaseApiService, TOptionalParams } from "./BaseApiCaller"

class AreaInfoCaller extends BaseApiService {
    private apiFactory: MapDatApiCallerFactory;

    constructor() {
        super()
        this.apiFactory = new MapDatApiCallerFactory();

        this.registerQueryMethod(1, (params) => this.getWojewodztwa(params?.optionalParam))
        this.registerQueryMethod(2, (params) => this.getPowiaty(params?.optionalParam))
        this.registerQueryMethod(3, (params) => this.getGminy(params?.optionalParam))
    }

    public getRequest(layer: number, id?: string)
    {
        const params: TOptionalParams = {optionalParam: id};
        return this.callQueryMethod(layer,params);
    }

    private getWojewodztwa(id?: string): UseQueryResult {
        return useQuery({
            queryKey: ['Wojewodztwa', id],
            queryFn: async () => await this.apiFactory
                .getApiImplementation("RSPO")
                .getInfo("Wojewodztwa", id)
                .then( (x: any) => x.data.content[0].data),
            enabled: false
        })
    }
    private getPowiaty(id?: string): UseQueryResult {
        return useQuery({
            queryKey: ['Powiaty', id],
            queryFn: async () => await this.apiFactory
                .getApiImplementation("RSPO")
                .getInfo("Powiaty", id)
                .then( (x: any) => x.data.content[0].data),
            enabled: false
        })
    }
    private getGminy(id?: string): UseQueryResult {
        return useQuery({
            queryKey: ['Gminy', id],
            queryFn: async () => await this.apiFactory
                .getApiImplementation("RSPO")
                .getInfo("Gminy", id)
                .then( (x: any) => x.data.content[0].data),
            enabled: false
        })
    }   
}

export default new AreaInfoCaller();