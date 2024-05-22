import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome'
import { StatusBar, StyleSheet, Text, TouchableOpacity, View, Platform, SafeAreaView } from "react-native"
import { useSafeAreaInsets } from "react-native-safe-area-context";
import { faArrowsRotate } from '@fortawesome/free-solid-svg-icons';

export const HeaderView = ({
  title,
  children,
  refreshControl,
  styles,
  color = "#1e88e5",
}) => {
  const insets = useSafeAreaInsets();
  const renderHederLeft = () => {
    return (
      <View style={{ flexDirection: "row", flex: 1 }}>
        <View
          style={{
            // paddingLeft: 10,
            flex: 1,
          }}
        >
          <Text style={HeaderStyles.headerTexts}>{title}</Text>
        </View>
      </View>
    );
  };
  const renderHeaderRight = () => {
    return (
      <View style={{
        // paddingLeft: 10,
        flex: 1,
        alignItems: "flex-end",
        justifyContent: "center"
      }}>
        <TouchableOpacity
          onPress={refreshControl}
        >
          <FontAwesomeIcon
            icon={faArrowsRotate}
            color="white"
            size={25}
          />
        </TouchableOpacity>
      </View>
    );
  };

  return (
    <View
      style={[
        {
          paddingTop: insets.top,
          width: "100%",
          backgroundColor: color,
          borderBottomLeftRadius: 25,
          borderBottomRightRadius: 25,
          // position: "absolute",
          zIndex: 10,
          display: "flex",
          ...Platform.select({
            ios: {
              // paddingTop: 30,
            },
          }),
        },
        styles,
      ]}
    >

      {Platform.OS == "android" && <StatusBar backgroundColor={color} />}
      <View
        style={{
          flex: 4,
          flexDirection: "row",
          paddingLeft: 15,
          paddingRight: 15,
          minHeight: 15,
          ...Platform.select({
            android: {
              // marginTop: 10
            }
          })
        }}
      >
        {renderHederLeft()}
        {renderHeaderRight()}
      </View>
      <View
        style={{
          flex: 4,
          display: "flex",
          flexDirection: "row",
          justifyContent: "space-evenly",
        }}
      >
        {children}
      </View>
    </View>
  );
};

const HeaderStyles = StyleSheet.create({
  headerTexts: {
    fontSize: 18,
    fontWeight: "bold",
    color: "#FFF",
  },

});