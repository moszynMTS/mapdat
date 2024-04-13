import { useEffect } from "react";
import { StatusBar, StyleSheet, Text, View } from "react-native"
import { HeaderView } from "../../components/HeaderView";

export const MainScreen = ({ navigation, route }) => {

    useEffect(() => {
        navigation.setOptions({
          header: ({ navigation, route, options, back }) => {
            return (
              <HeaderView
                title={"Main Page"}
                styles={{ minHeight: 70 }}
              ></HeaderView>
            );
          },
        });
      }, []);

    return(
        <View style={styles.container}>
        <Text>MapDat</Text>
        <StatusBar style="auto" />
      </View>
    )
}

const styles = StyleSheet.create({
    container: {
        display: "flex",
        flex: 1,
        justifyContent: "center",
        alignContent: "center",
        alignSelf: "center"
    }
  });