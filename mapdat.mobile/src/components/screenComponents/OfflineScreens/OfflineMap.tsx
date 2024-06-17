import React, { useContext, useEffect, useReducer, useState } from "react";
import { OfflineMapProps } from "../../../models/interfaces/OfflineMap.interface";
import GeoJSONCaller from "../../../features/services/GeoJSONCaller";
import { ModalLoader } from "../../ModalLoader";
import { View } from "react-native";
import WebView from "react-native-webview";
import { MapHtmlContent } from "../../HTML_Content/MapHtmlContent";
import { capitalizeFirstLetter } from "../../../utils/capitalizeFirstLetter";
import { MapDetailContext } from "../../../states/context/MapDetailContext";
import { ModalInfo } from "../../Modals/ModalInfo";
import { TAreaGminaInfo } from "../../../models/types/AreaGminaInfo.type";

export const OfflineMap: React.FC<OfflineMapProps> = ({
  navigation,
  route,
}) => {
  const geoJsonWoj = GeoJSONCaller.getMapData(route.params.mapName);
  const geoAreaInfo = GeoJSONCaller.getMapAreaInfo(route.params.mapName);
  const [data, setData] = useState(null);
  const [areaInfo, setAreaInfo] = useState<TAreaGminaInfo[]>();

  // - context
  const UmapDetailContext = useContext(MapDetailContext);

  const [params, dispatchParamName] = useReducer(
    (state: any, action: any) => ({ ...state, ...action }),
    { name: "", id: ""}
  );

  useEffect(() => {
    if (geoJsonWoj.isSuccess) setData(geoJsonWoj.data);
  }, [geoJsonWoj.isSuccess, geoJsonWoj.data]);
  useEffect(() => {
    if (geoAreaInfo.isSuccess)
      {
        setAreaInfo(geoAreaInfo.data.content[0].powiatOfflineData);
      }
  }, [geoAreaInfo.isSuccess, geoAreaInfo.data]);
  useEffect(() => {
    if (params.id.length != 0) {
        let data: any;
        console.log(areaInfo);
        if(areaInfo != undefined)
        {
            data = areaInfo.filter( (element) => element.gminaId == params.id)
          if(data != undefined)
          {
            console.log(data[0].data);
            UmapDetailContext?.setData(data[0].data);
            UmapDetailContext?.setDisplay(true);
          }
        }
    }
  }, [params.id]);


  if (geoJsonWoj.isLoading) {
    return (
      <>
        <ModalLoader
          show={geoJsonWoj.isLoading}
          message={"Ładowanie treści..."}
        />
      </>
    );
  } else {
    return (
      <>
      {UmapDetailContext?.display && (
          <ModalInfo
            visible={UmapDetailContext.display}
            setVisible={UmapDetailContext.setDisplay}
            areaName={params.name}
            setAccept={() => {}}
            disableSave={true}
          />
        )}
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
            javaScriptEnabled
            onMessage={(event) => {
                const data = JSON.parse(event.nativeEvent.data);
                console.log(data.feature.properties.name);
              dispatchParamName({
                name: capitalizeFirstLetter(data.feature.properties.name),
              });
              dispatchParamName({ id: data.feature.properties.id });
            }}
          />
        </View>
      </>
    );
  }
};
