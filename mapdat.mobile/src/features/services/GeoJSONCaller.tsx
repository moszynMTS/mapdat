import { useQuery } from "@tanstack/react-query"
import MapDatApiCallerFactory from "../../services/base-api-caller/MapDatApiCallerFactory"
import GeoJsonUtil from "../utils/geoJsonUtil";
import OfflineDataService from "../../lib/OfflineMode/OfflineDataService";

class GeoJSONCaller {
    private apiFactory: MapDatApiCallerFactory;
    private geoJsonUtils: GeoJsonUtil;

    constructor() {
        this.apiFactory = new MapDatApiCallerFactory();
        this.geoJsonUtils = new GeoJsonUtil();
    }
    public getRequest(layer: number, optionalParam: string | undefined, secondOptionalParam: string | undefined){
        // console.log(optionalParam);
        switch(layer)
        {
            case 1:
                return this.getWojewodztwa();
            break;
            case 2:
                return this.getPowiat(optionalParam);
            break;
            case 3:
                return this.getGminy(optionalParam, secondOptionalParam);
            break
        }
    }
    public getMapData(name:string)
    {
        return this.getOfflineMapData(name);
    }
    public getMapAreaInfo(name:string)
    {
        return this.getOfflineAreaInfo(name);
    }
    public getListOfflineMap()
    {
        return this.getOfflineData();
    }

    private getWojewodztwa() {
        return useQuery({
            queryKey: ['wojewodztwa'],
            queryFn: async () => await this.apiFactory
                .getApiImplementation("Wojewodztwa")
                .getItem()
                .then((x: any) => this.geoJsonUtils.transformGeoJsonToList(x.data)),
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
    private getGminy(powiatName: string | undefined, powiatId: string | undefined){
        return useQuery({
            queryKey: ['gminy'],
            queryFn: async () => await this.apiFactory
            .getApiImplementation(`Gminy`)
            .getItem(`?Powiat=${powiatName}&PowiatId=${powiatId}`)
            .then((x: any) => this.geoJsonUtils.transformGeoJsonToList(x.data))
        })
    }
    private getOfflineData()
    {
        return useQuery({
            queryKey: ['offline_data'],
            queryFn: async () => await OfflineDataService.listJsonFiles(),
            refetchOnWindowFocus: true,
            enabled: false
        })
    }
    private getOfflineAreaInfo(name: string)
    {
        return useQuery({
            queryKey: ['offline_area_info'],
            queryFn: async () => await OfflineDataService.loadAreaInfoData(name),
            refetchOnWindowFocus: true,
        })
    }
    private getOfflineMapData(name: string)
    {
        return useQuery({
            queryKey: ['offline_JSON_data'],
            queryFn: async () => await OfflineDataService.loadMapData(name),
            refetchOnWindowFocus: true
        })
    }
}

export default new GeoJSONCaller();