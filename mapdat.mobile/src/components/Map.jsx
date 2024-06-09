import { View } from "react-native";
import WebView from "react-native-webview";
import { MapHtmlContent } from "./HTML_Content/MapHtmlContent";
import { ModalLoader } from "./ModalLoader";
import GeoJSONCaller from "../features/services/GeoJSONCaller";
import { useEffect, useReducer, useState } from "react";
import { capitalizeFirstLetter } from "../utils/capitalizeFirstLetter";

export const Map = ({layer, setLayer, saveGmina, saveLayer, setSaveGmina}) => {

  const [params, dispatchParamName] = useReducer((state, action) => ({...state, ...action}), name = "");
  const geoJsonWoj = GeoJSONCaller.getRequest(layer, params.name);
  const [data, setData] = useState(null);

  const onClickFeature = ({feature, layer}) => {
    console.log(layer);
    if (feature.properties && feature.properties.name) {
      switch(layer){
        case 1:
          dispatchParamName({name: capitalizeFirstLetter(feature.properties.name)});
          setLayer((prev) => prev + 1);

        break;
        case 2:
          dispatchParamName({name: capitalizeFirstLetter(feature.properties.name)});
          setLayer((prev) => prev + 1);
        break;
      }
    }
  }
  
  useEffect(() => {
    if(layer >=3 && saveGmina)
      {
        saveLayer(params.name, data)
        setSaveGmina(false);
      }
  }, [saveGmina])
  
  
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
          key={layer}
        >
          <WebView
            nestedScrollEnabled={true}
            originWhitelist={["*"]}
            source={{
              html: MapHtmlContent(JSON.stringify(data), layer),
            }}            
            style={{ flex: 1, opacity: 0.99 }}
            javaScriptEnabled
            onMessage={(event) => {
              const data = JSON.parse(event.nativeEvent.data)
              onClickFeature({feature: data.feature, layer: data.layers})
            }}
          />
        </View>
      </>
    )
  }
}