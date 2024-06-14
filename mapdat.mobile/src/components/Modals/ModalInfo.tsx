import React, { useContext, useEffect } from "react";
import { FlatList, Modal, StyleSheet, Text, TouchableOpacity, TouchableWithoutFeedback, View } from "react-native"
import { MapDetailContext } from "../../states/context/MapDetailContext";

type TModalInfo = {
    visible: boolean;
    setVisible: (visible: boolean) => void;
    areaName: string
}

export const ModalInfo: React.FC<TModalInfo> = ({ visible, setVisible, areaName }) => {
    const UMapData = useContext(MapDetailContext)

    const renderItem = (item: any) => {
        console.log(item);
        return (
            <View style={styles.container}>
                <Text>
                    {item.item.subject}
                </Text>
                <Text>
                    {item.item.count}
                </Text>
            </View>
        )
    }

    return (
        <Modal visible={visible} transparent animationType="fade">
            <TouchableOpacity
                style={{
                    backgroundColor: "rgba(0,0,0,0.4)",
                    flex: 1,
                    alignItems: "center",
                    justifyContent: "center",
                }}
                onPress={() => setVisible(false)}
            >
                <TouchableWithoutFeedback>
                    <View
                        style={{
                            width: "80%",
                            height: "80%",
                            backgroundColor: "#FEFEFE",
                            display: "flex",
                            justifyContent: "flex-start",
                            borderRadius: 15,
                            paddingHorizontal: 10,
                        }}
                    >
                        <View
                            style={{
                                flexDirection: "column",
                                justifyContent: "center",
                                flex: 1,
                                alignItems: "center",
                                marginTop: 40,
                                marginBottom: 20
                            }}
                        >
                            <Text>
                                {areaName}
                            </Text>
                        </View>
                        <View
                            style={{
                                flex: 9,
                                flexDirection: "row",
                                justifyContent: "center",
                                alignItems: "center",
                            }}
                        >
                            <FlatList
                                data={UMapData?.data}
                                renderItem={renderItem}
                            />
                        </View>
                        <View style={{
                            flex: 1,
                            justifyContent: "space-evenly",
                            flexDirection: "row"
                        }}>
                            <TouchableOpacity style={{ padding: 15, borderRadius: 100, backgroundColor: "#eb701e", justifyContent: "center" }} onPress={() => setVisible(false)} >
                                <Text style={{ fontWeight: "bold", color: "white" }}>
                                    Anuluj
                                </Text>
                            </TouchableOpacity>
                            <TouchableOpacity style={{ padding: 15, borderRadius: 100, backgroundColor: "#eb701e", justifyContent: "center" }} >
                                <Text style={{ fontWeight: "bold", color: "white" }}>
                                    Zapisz
                                </Text>
                            </TouchableOpacity>
                        </View>
                    </View>
                </TouchableWithoutFeedback>
            </TouchableOpacity>
        </Modal>
    )
}

const styles = StyleSheet.create({
    container: {
        flexDirection: "row",
        justifyContent: "space-between",
        paddingHorizontal: 50,
        width: "90%",
        minHeight: 20,
        borderBottomWidth: 1,
        margin: 10,
        borderRadius: 5,
        borderWidth: 1,
        borderColor: "#1e88e5",
        borderLeftWidth: 6,
        paddingLeft: 12,
        marginLeft: 18
    },
})