import { View } from "react-native";
import WebView from "react-native-webview";
import { MapHtmlContent } from "./HTML_Content/MapHtmlContent";
import { ModalLoader } from "./ModalLoader";
import GeoJSONCaller from "../features/services/GeoJSONCaller";
import { useEffect, useReducer, useState } from "react";
import { capitalizeFirstLetter } from "../utils/capitalizeFirstLetter";

export const Map = () => {
  const [layers, setLayers] = useState(1);
  const [params, dispatchParamName] = useReducer((state, action) => ({...state, ...action}), name = "");
  const geoJsonWoj = GeoJSONCaller.getRequest(layers, params.name);
  const [data, setData] = useState(null);

  const onClickFeature = ({feature, layers}) => {
    if (feature.properties && feature.properties.name) {
      switch(layers){
        case 1:
          dispatchParamName({name: capitalizeFirstLetter(feature.properties.name)});
          setLayers((prevData) => prevData + 1);
        break;
        case 2:
          dispatchParamName({name: capitalizeFirstLetter(feature.properties.name)});
          setLayers((prevData) => prevData + 1);
        break;
        case 3:
        break;
      }
    }
  }

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
          key={layers}
        >
          <WebView
            nestedScrollEnabled={true}
            originWhitelist={["*"]}
            source={{
              html: MapHtmlContent(JSON.stringify(data), layers),
            }}            
            style={{ flex: 1, opacity: 0.99 }}
            javaScriptEnabled
            onMessage={(event) => {
              const data = JSON.parse(event.nativeEvent.data)
              onClickFeature({feature: data.feature, layers: data.layers})
            }}
          />
        </View>
      </>
    )
  }
}