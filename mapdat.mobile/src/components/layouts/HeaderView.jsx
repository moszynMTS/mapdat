import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome'
import { StatusBar, StyleSheet, Text, TouchableOpacity, View, Platform, SafeAreaView } from "react-native"
import { useSafeAreaInsets } from "react-native-safe-area-context";
import { faArrowsRotate, faBars, faDownload } from '@fortawesome/free-solid-svg-icons';
import { SaveButton } from '../UI_design/SaveButton';
import { useContext } from 'react';
import { LayerContext } from '../../states/context/LayerContext';
import OfflineDataService from '../../lib/OfflineMode/OfflineDataService';

export const HeaderView = ({
  title,
  children,
  refreshControl = null,
  styles,
  color = "#1e88e5",
  saveGmina = null,
  navigation
}) => {
  const insets = useSafeAreaInsets();

  // - context
  const layerContext = useContext(LayerContext);
  const {layer} = layerContext;


  const renderHederLeft = () => {
    return (
      <View style={{ flexDirection: "row", flex: 1 }}>
        <View
          style={{
            // paddingLeft: 10,
            flex: 1,
            flexDirection: "row",
            alignItems: "center",
            justifyContent: "space-evenly"
          }}
        >
          <TouchableOpacity    onPress={() => navigation.toggleDrawer()} >
            <FontAwesomeIcon icon={faBars} size={25} color='white'/>
          </TouchableOpacity>
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
        justifyContent: "space-evenly",
        flexDirection: "row"
      }}>
        {saveGmina != null && <SaveButton onPress={saveGmina} disabled={layer >= 3} />}
        {refreshControl != null &&
          <TouchableOpacity
            onPress={refreshControl}
          >
            <FontAwesomeIcon
              icon={faArrowsRotate}
              color="white"
              size={25}
            />
          </TouchableOpacity>
        }
        {/* <TouchableOpacity
          onPress={async () => {console.log( await OfflineDataService.listJsonFiles())}}
        >
          <FontAwesomeIcon
            icon={faDownload}
            color="white"
            size={25}
          />
        </TouchableOpacity> */}
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