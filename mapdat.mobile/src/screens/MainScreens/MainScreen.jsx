import { useContext, useEffect, useState } from "react";
import { StatusBar, StyleSheet, Text, TouchableOpacity, View } from "react-native"
import { HeaderView } from "../../components/layouts/HeaderView";
import { Map } from "../../components/Map";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowsRotate } from "@fortawesome/free-solid-svg-icons";
import { LayerContext } from "../../states/context/LayerContext";

export const MainScreen = ({ navigation, route }) => {
  const [resetKey, setResetKey] = useState(Math.random());
  const layerContext = useContext(LayerContext);
  const {layer, setLayer, saveLayer} = layerContext;
  const [saveGmina, setSaveGmina] = useState(false);

    useEffect(() => {
        navigation.setOptions({
          header: ({ navigation, route, options, back }) => {
            return (
              <HeaderView
                title={"Main Page"}
                styles={{ minHeight: 70 }}
                refreshControl={() => {setResetKey(Math.random()); setLayer(1)}}
                saveGmina={() => setSaveGmina(true)}
                navigation={navigation}
              >
              </HeaderView>
            );
          },
        });
      }, []);

    return(
        <View style={styles.container} key={resetKey}>
        <Map layer={layer} setLayer={setLayer} saveGmina={saveGmina} saveLayer={saveLayer} setSaveGmina={() => setSaveGmina(false)}/>
        <StatusBar style="auto" />
      </View>
    )
}

const styles = StyleSheet.create({
    container: {
        display: "flex",
        flex: 1,
        justifyContent: "center"
        // backgroundColor: 'red'
    }
  });