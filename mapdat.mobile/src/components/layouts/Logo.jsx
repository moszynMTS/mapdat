import { Image } from "react-native";

export const Logo = ({ style }) => {
  return <Image style={style} resizeMode={'contain'} source={require("../../../assets/icon.png")} />;
};
