import React, { useContext } from "react";
import { FlatList, Modal, StyleSheet, Text, TouchableOpacity, TouchableWithoutFeedback, View } from "react-native";
import { MapDetailContext } from "../../states/context/MapDetailContext";
import { preparPostScriptToText } from "../../utils/preparPostScriptToText";
import { reduceLettersToSmall } from "../../utils/reduceLettersToSmall";
import { capitalizeFirstLetter } from "../../utils/capitalizeFirstLetter";
import { opacity } from "react-native-reanimated/lib/typescript/reanimated2/Colors";

type TModalInfo = {
    visible: boolean;
    setVisible: (visible: boolean) => void;
    areaName: string;
    setAccept: () => void;
    disableSave?: boolean | undefined
};

export const ModalInfo: React.FC<TModalInfo> = ({ visible, setVisible, areaName, setAccept, disableSave }) => {
    const UMapData = useContext(MapDetailContext);

    const renderItem = ({ item }: { item: any }) => {
        return (
            <View style={styles.itemContainer}>
                <Text style={styles.itemSubject}>
                    {capitalizeFirstLetter(reduceLettersToSmall(item.subject))}
                </Text>
                <Text style={styles.itemCount}>
                    {item.count + " " + preparPostScriptToText(item.subject)}
                </Text>
            </View>
        );
    };

    return (
        <Modal visible={visible} transparent animationType="fade">
            <View
                style={styles.modalOverlay}
                // onPress={() => setVisible(false)}
            >
                {/* <TouchableWithoutFeedback style={{opacity: 0.9}}> */}
                    <View style={styles.modalContainer}>
                        <View style={styles.header}>
                            <Text style={styles.areaName}>
                                {areaName}
                            </Text>
                        </View>
                        <View style={styles.content}>
                            <FlatList
                                data={UMapData?.data}
                                renderItem={renderItem}
                                keyExtractor={(item) => item.subject}
                                style={{
                                    opacity: 1
                                }}
                            />
                        </View>
                        <View style={styles.footer}>
                            <TouchableOpacity style={styles.button} onPress={() => setVisible(false)}>
                                <Text style={styles.buttonText}>
                                    Anuluj
                                </Text>
                            </TouchableOpacity>
                            <TouchableOpacity 
                            style={[styles.button, {opacity: disableSave ? 0 : 1}]} 
                            disabled={disableSave}
                            onPress={() => {
                                setAccept(),
                                setVisible(false);
                            }}>
                                <Text style={styles.buttonText}>
                                    Zapisz
                                </Text>
                            </TouchableOpacity>
                        </View>
                    </View>
                {/* </TouchableWithoutFeedback> */}
            </View>
        </Modal>
    );
};

const styles = StyleSheet.create({
    modalOverlay: {
        backgroundColor: "rgba(0,0,0,0.4)",
        flex: 1,
        alignItems: "center",
        justifyContent: "center",
        zIndex: 0
    },
    modalContainer: {
        width: "80%",
        height: "80%",
        backgroundColor: "#FEFEFE",
        borderRadius: 15,
        paddingHorizontal: 10,
    },
    header: {
        flex: 1,
        justifyContent: "center",
        alignItems: "center",
        marginTop: 40,
        marginBottom: 20,
    },
    areaName: {
        fontSize: 24,
        fontWeight: "bold",
    },
    content: {
        flex: 8,
        justifyContent: "center",
        alignItems: "center",
    },
    itemContainer: {
        flexDirection: "row",
        justifyContent: "space-between",
        alignItems: "center",
        padding: 10,
        marginVertical: 5,
        borderWidth: 1,
        borderColor: "#1e88e5",
        borderRadius: 5,
        width: "90%",
    },
    itemSubject: {
        fontSize: 18,
        fontWeight: "bold",
    },
    itemCount: {
        fontSize: 18,
        color: "#1e88e5",
    },
    footer: {
        flex: 1,
        flexDirection: "row",
        justifyContent: "space-around",
        alignItems: "center",
        marginBottom: 20,
    },
    button: {
        padding: 15,
        borderRadius: 100,
        backgroundColor: "#eb701e",
        justifyContent: "center",
    },
    buttonText: {
        fontWeight: "bold",
        color: "white",
    },
});
