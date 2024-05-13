import { useQuery } from "@tanstack/react-query"
import MapDatApiCallerFactory from "../../services/base-api-caller/MapDatApiCallerFactory"
import GeoJsonFactory from "../utils/geoJsonFactory";

export function getWojewodztwa(){
    const apiFactory: MapDatApiCallerFactory = new MapDatApiCallerFactory();
    const geoJsonFactory: GeoJsonFactory = new GeoJsonFactory();
    return useQuery({
        queryKey: ['wojewodztwa'],
        queryFn: async () => await apiFactory.getApiImplementation("Wojewodztwa").getItem().then((x : any) => geoJsonFactory.transformGeoJsonToList(x.data)) 
    })
}