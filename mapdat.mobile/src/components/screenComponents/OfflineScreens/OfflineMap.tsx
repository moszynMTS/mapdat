import React, { useEffect, useState } from "react";
import { OfflineMapProps } from "../../../models/interfaces/OfflineMap.interface";
import GeoJSONCaller from "../../../features/services/GeoJSONCaller";
import { ModalLoader } from "../../ModalLoader";
import { View } from "react-native";
import WebView from "react-native-webview";
import { MapHtmlContent } from "../../HTML_Content/MapHtmlContent";

export const OfflineMap: React.FC<OfflineMapProps> = (
    { navigation, route }
) => {
    const geoJsonWoj = GeoJSONCaller.getMapData(route.params.mapName);
    const [data, setData] = useState(null);

    useEffect(() => {
        if(geoJsonWoj.isSuccess)
        {
          setData(geoJsonWoj.data);
        }
      }, [geoJsonWoj.isSuccess, geoJsonWoj.data])

    if (geoJsonWoj.isLoading) {
        return (<>
            <ModalLoader
                show={geoJsonWoj.isLoading}
                message={"Ładowanie treści..."}
            />
        </>)
    } else {
        return (
            <>
                <View
                    style={{
                        width: "90%",
                        height: "80%",
                        borderRadius: 20,
                        alignSelf: "center",
                    }}
                    key={0}
                >
                    <WebView
                        nestedScrollEnabled={true}
                        originWhitelist={["*"]}
                        source={{
                            html: MapHtmlContent(JSON.stringify(data), 0),
                        }}
                        style={{ flex: 1, opacity: 0.99 }}
                    />
                </View>
            </>
        )
    }
}