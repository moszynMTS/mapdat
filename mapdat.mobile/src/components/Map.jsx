import { View } from "react-native";
import WebView from "react-native-webview";
import { MapHtmlContent } from "./HTML_Content/MapHtmlContent";
import { useEffect, useState } from "react";
import MapDatApiCallerFactory from "../services/base-api-caller/MapDatApiCallerFactory";
import { useMutation, useQuery } from "@tanstack/react-query";
import { ModalLoader } from "./ModalLoader";
import { CustomAlert } from "./CustomAlert";
import { getWojewodztwa } from "../features/services/getWojewództwa";

export const Map = () => {
  const wojewodztwaData = getWojewodztwa();


  return (
    <View
      style={{
        width: "90%",
        height: "80%",
        borderRadius: 20,
        //   backgroundColor: "red",
        alignSelf: "center",
      }}
    >
      <ModalLoader
        show={wojewodztwaData.isLoading}
        message={"Ładowanie treści..."}
      />
      {/* <CustomAlert
        show={wojData.isError}
        title={"Błąd"}
        message={"Wystąpił bład"}
      /> */}
      <WebView
        nestedScrollEnabled={true}
        originWhitelist={["*"]}
        source={{
          html: MapHtmlContent({
            lat: 52,
            lon: 20,
            area: 5,
            draggable: true,
            // list: wojewodztwaData.data
          }),
        }}
        style={{ flex: 1, opacity: 0.99 }}
        javaScriptEnabled
      //   onMessage={(e) => {
      //     let data = JSON.parse(e.nativeEvent.data);
      //     const { lat, lng } = JSON.parse(e.nativeEvent.data);
      //     setFinalLocation({ lat: lat, lon: lng });
      //     if (data.lastCoords)
      //       getAdressFromCoordinates({
      //         coords: {
      //           lat: data.markerLatLng.lat,
      //           lon: data.markerLatLng.lng,
      //         },
      //       });
      //   }}
      />
    </View>
  )
}