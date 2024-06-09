import React, { useCallback, useEffect, useRef, useState } from "react";
import { OfflineScreenProps } from "../../models/interfaces/OfflineScreen.interface";
import { HeaderView } from "../../components/layouts/HeaderView";
import { FlatList, StyleSheet, View, RefreshControl } from "react-native";
import { StatusBar } from "expo-status-bar";
import GeoJSONCaller from "../../features/services/GeoJSONCaller";
import { ModalLoader } from "../../components/ModalLoader";
import { OfflineScreenList } from "./OfflineScreenList";
import { useFocusEffect } from "@react-navigation/native";
import { useQueryClient } from "@tanstack/react-query";

export const OfflineScreen: React.FC<OfflineScreenProps> = ({ navigation, route }) => {
  const [resetKey, setResetKey] = useState(Math.random());
  const [refreshing, setRefreshing] = useState(false);
  const geoJsonList = GeoJSONCaller.getListOfflineMap();

  useEffect(() => {
    navigation.setOptions({
      header: ({ navigation, route, options, back }) => {
        return (
          <HeaderView
            title={"Offline screen"}
            styles={{ minHeight: 70 }}
            navigation={navigation}
            children={undefined}
          ></HeaderView>
        );
      },
    });
  }, []);

  useEffect(() => {
    if (geoJsonList.isSuccess) {
      console.log(geoJsonList.data);
    }
  }, [geoJsonList.isSuccess, geoJsonList.data]);

  useFocusEffect(
    useCallback(() => {
        geoJsonList.refetch();

    }, [])
  );

  const handleRefresh = () => {
    setRefreshing(true);
    setTimeout(() => {
      setResetKey(Math.random());
    }, 500);
    setRefreshing(false);
  };

  if (geoJsonList.isLoading)
    return (
      <>
        <ModalLoader show={geoJsonList.isLoading} message={"Ładowanie treści..."} />
      </>
    );

  return (
    <View style={styles.container} key={resetKey}>
      <StatusBar style="auto" />
      <View style={{ flex: 1 }}>
        <FlatList
          data={geoJsonList.data?.layersName}
          renderItem={(info) => <OfflineScreenList {...info} refresh={geoJsonList.refetch} navigation={navigation} />}
          keyExtractor={(item, index) => index.toString()}
          refreshControl={
            <RefreshControl refreshing={refreshing} onRefresh={handleRefresh} />
          }
        />
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    display: "flex",
    flex: 1,
    justifyContent: "center",
  },
});