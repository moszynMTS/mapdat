import { StatusBar, StyleSheet, Text, TouchableOpacity, View, Platform, SafeAreaView } from "react-native"
import { useSafeAreaInsets } from "react-native-safe-area-context";

export const HeaderView = ({
  title,
  children,
  backFunction,
  backButton = false,
  secondText = null,
  styles,
  color = "#1e88e5",
}) => {
  const insets = useSafeAreaInsets();
  const renderHederLeft = () => {
    return (
      <View style={{ flexDirection: "row", flex: 1 }}>
        {backButton && (
          <TouchableOpacity
            style={{ paddingTop: 6, width: 30, height: 60 }}
            onPress={() => backFunction()}
          >
            <FontAwesomeIcon icon={faChevronLeft} color="white" />
          </TouchableOpacity>
        )}
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
      <View  style={{
        // paddingLeft: 10,
        flex: 1,
      }}>
        {secondText != null && (
          <Text style={[HeaderStyles.headerTexts, { alignSelf: "flex-start" }]}>
            {secondText}
          </Text>
        )}
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

      {Platform.OS == "android" &&  <StatusBar backgroundColor={color} />}
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
        {secondText && renderHeaderRight()}
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