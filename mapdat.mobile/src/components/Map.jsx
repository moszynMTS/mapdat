import { View } from "react-native";
import WebView from "react-native-webview";
import { MapHtmlContent } from "./HTML_Content/MapHtmlContent";
import { ModalLoader } from "./ModalLoader";
import GeoJSONCaller from "../features/services/GeoJSONCaller";
import { useContext, useEffect, useReducer, useState } from "react";
import { capitalizeFirstLetter } from "../utils/capitalizeFirstLetter";
import { MapDetailContext } from "../states/context/MapDetailContext";
import { ModalInfo } from "./Modals/ModalInfo";
import AreaInfoCaller from "../features/services/AreaInfoCaller";

export const Map = ({
  layer,
  setLayer,
  saveGmina,
  saveLayer,
  setSaveGmina,
}) => {
  const [params, dispatchParamName] = useReducer(
    (state, action) => ({ ...state, ...action }),
    { name: "", id: "", finalName: "" }
  );
  const geoJsonWoj = GeoJSONCaller.getRequest(layer, params.name, params.id);
  const geoAreaInfo = AreaInfoCaller.getRequest(layer, params.id);
  const gminyAreaInfo = AreaInfoCaller.getRequest("offline", params.id);

  const UmapDetailContext = useContext(MapDetailContext);
  const [data, setData] = useState(null);
  const [areaInfo, setAreaInfo] = useState(null);
  const [disableSave, setDisableSave] = useState(false);

  const onClickFeature = () => {
    switch (layer) {
      case 1:
        setLayer((prev) => prev + 1);

        break;
      case 2:
        gminyAreaInfo.refetch();

        break;
    }
  };

  useEffect(() => {
    if (layer >= 3 && saveGmina && !disableSave) {
      saveLayer(params.finalName, data, areaInfo);
      setSaveGmina(false);
      setDisableSave(true);
    }
  }, [saveGmina]);

  useEffect(() => {
    if (params.id.length != 0) {
      geoAreaInfo.refetch();
    }
  }, [params.id]);

  useEffect(() => {
    if (gminyAreaInfo.isSuccess) {
      setAreaInfo(gminyAreaInfo.data);
      if (layer == 2) setLayer((prev) => prev + 1);
    }
  }, [gminyAreaInfo.isSuccess, gminyAreaInfo.data]);

  useEffect(() => {
    if (geoJsonWoj.isSuccess) {
      setData(geoJsonWoj.data);
    }
  }, [geoJsonWoj.isSuccess, geoJsonWoj.data]);

  useEffect(() => {
    if (geoAreaInfo.isSuccess) {
      UmapDetailContext.setData(geoAreaInfo.data);
      UmapDetailContext.setDisplay(true);
    }
  }, [geoAreaInfo.isSuccess, geoAreaInfo.data]);

  if (
    geoJsonWoj.isLoading ||
    geoAreaInfo.isLoading ||
    gminyAreaInfo.isLoading
  ) {
    return (
      <>
        <ModalLoader
          show={
            geoJsonWoj.isLoading ||
            geoAreaInfo.isLoading ||
            gminyAreaInfo.isLoading
          }
          message={"Ładowanie treści..."}
        />
      </>
    );
  } else {
    return (
      <>
        {UmapDetailContext.display && (
          <ModalInfo
            visible={UmapDetailContext.display}
            setVisible={UmapDetailContext.setDisplay}
            areaName={params.name}
            setAccept={onClickFeature}
            disableSave={layer > 2}
          />
        )}
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
              const data = JSON.parse(event.nativeEvent.data);
              dispatchParamName({
                name: capitalizeFirstLetter(data.feature.properties.name),
              });
              dispatchParamName({ id: data.feature.properties.id });
              if (layer == 2)
                dispatchParamName({
                  finalName: capitalizeFirstLetter(
                    data.feature.properties.name
                  ),
                });
            }}
          />
        </View>
      </>
    );
  }
};
