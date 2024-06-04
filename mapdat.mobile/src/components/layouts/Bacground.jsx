import { ImageBackground, Dimensions, View } from "react-native";

export const Background = ({ children, style }) => {
  const screenWidth = Dimensions.get("screen").width;
  const screenHeight = Dimensions.get("screen").height;

  return (
    // <ImageBackground
    //   source={require("../../assets/background.png")}
    //   style={[style, { width: screenWidth, height: screenHeight }]}
    // >
    <View 
    style={[style, { width: screenWidth, height: screenHeight }]}
    >
      {children}
    </View>
    // </ImageBackground>
  );
};
