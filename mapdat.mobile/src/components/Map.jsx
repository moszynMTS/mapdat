import { View } from "react-native";
import WebView from "react-native-webview";
import { MapHtmlContent } from "./HTML_Content/MapHtmlContent";
import { useState } from "react";

export const Map = () => {
    const [userLocation, setUserLocation] = useState({
        lat: 50.866077,
        lon: 20.628568,
      });
      const [finalLocation, setFinalLocation] = useState({
        lat: 50.866077,
        lon: 20.628568,
      });
    return(
        <View
        style={{
          width: "90%",
          height: "80%",
          borderRadius: 20,
        //   backgroundColor: "red",
          alignSelf: "center",
        }}
      >
        <WebView
          nestedScrollEnabled={true}
          originWhitelist={["*"]}
          source={{
            html: MapHtmlContent({
              lat: userLocation.lat,
              lon: userLocation.lon,
              area: 10,
              draggable: true,
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