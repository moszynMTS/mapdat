import { useQuery } from "@tanstack/react-query"
import MapDatApiCallerFactory from "../../services/base-api-caller/MapDatApiCallerFactory"
import GeoJsonUtil from "../utils/geoJsonUtil";

class GeoJSONCaller {
    private apiFactory: MapDatApiCallerFactory;
    private geoJsonUtils: GeoJsonUtil;

    constructor() {
        this.apiFactory = new MapDatApiCallerFactory();
        this.geoJsonUtils = new GeoJsonUtil();
    }
    public getRequest(layer: number, optionalParam: string | undefined){
        console.log(optionalParam);
        switch(layer)
        {
            case 1:
                return this.getWojewodztwa();
            break;
            case 2:
                return this.getPowiat(optionalParam);
            break;
            case 3:
                return this.getGminy(optionalParam);
            break
        }
    }

    private getWojewodztwa() {
        return useQuery({
            queryKey: ['wojewodztwa'],
            queryFn: async () => await this.apiFactory
                .getApiImplementation("Wojewodztwa")
                .getItem()
                .then((x: any) => this.geoJsonUtils.transformGeoJsonToList(x.data))
        })
    }
    private getPowiat(wojewodztwoName: string | undefined)
    {
        return useQuery({
            queryKey: ['powiat'],
            queryFn: async () => await this.apiFactory
            .getApiImplementation(`Powiaty`)
            .getItem(`?Wojewodztwo=${wojewodztwoName}`)
            .then((x: any) => this.geoJsonUtils.transformGeoJsonToList(x.data))
        })
    }
    private getGminy(powiatName: string | undefined){
        return useQuery({
            queryKey: ['gminy'],
            queryFn: async () => await this.apiFactory
            .getApiImplementation(`Gminy`)
            .getItem(`?Powiat=${powiatName}`)
            .then((x: any) => this.geoJsonUtils.transformGeoJsonToList(x.data))
        })
    }
}

export default new GeoJSONCaller();