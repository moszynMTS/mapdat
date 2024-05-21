import { useEffect, useState } from "react";
import { StatusBar, StyleSheet, Text, TouchableOpacity, View } from "react-native"
import { HeaderView } from "../../components/HeaderView";
import { Map } from "../../components/Map";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowsRotate } from "@fortawesome/free-solid-svg-icons";

export const MainScreen = ({ navigation, route }) => {
  const [resetKey, setResetKey] = useState(Math.random());

    useEffect(() => {
        navigation.setOptions({
          header: ({ navigation, route, options, back }) => {
            return (
              <HeaderView
                title={"Main Page"}
                styles={{ minHeight: 70 }}
                refreshControl={() => setResetKey(Math.random())}
              >
              </HeaderView>
            );
          },
        });
      }, []);

    return(
        <View style={styles.container} key={resetKey}>
        <Map />
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