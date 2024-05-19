import { View } from "react-native";
import WebView from "react-native-webview";
import { MapHtmlContent } from "./HTML_Content/MapHtmlContent";
import { ModalLoader } from "./ModalLoader";
import { getWojewodztwa } from "../features/services/getWojewództwa";

export const Map = () => {
  const wojewodztwaData = getWojewodztwa();


  if (wojewodztwaData.isLoading) {
    return (<>
      <ModalLoader
        show={wojewodztwaData.isLoading}
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
            //   backgroundColor: "red",
            alignSelf: "center",
          }}
        >
          <WebView
            nestedScrollEnabled={true}
            originWhitelist={["*"]}
            source={{
              html: MapHtmlContent(JSON.stringify(wojewodztwaData.data)),
            }}            
            style={{ flex: 1, opacity: 0.99 }}
            javaScriptEnabled
            onMessage={(event) => {
              console.log(event.nativeEvent.data);
            }}
          />
        </View>
      </>
    )
  }
}