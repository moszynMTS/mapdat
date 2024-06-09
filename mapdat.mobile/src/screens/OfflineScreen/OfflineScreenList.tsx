import { faChevronRight, faTrash } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-native-fontawesome";
import { ListRenderItemInfo, StyleSheet, Text, TouchableOpacity, View } from "react-native"
import CustomAlertProvider from "../../components/layouts/CustomAlertProvider";
import OfflineDataService from "../../lib/OfflineMode/OfflineDataService";
import { OfflineScreenNavigationProps } from "../../models/interfaces/OfflineScreen.interface";

export const OfflineScreenList: React.FC<{ item: string | null; refresh: () => void; navigation: OfflineScreenNavigationProps}> = ({ item, refresh, navigation }) => {

    if (item == null) {
        return null;
    }

    const showDeleteAlert = () => {
        CustomAlertProvider.alert({
            title: "Uwaga",
            message: "Czy chcesz usunąć tą mapę?",
            buttonFunction: null,
            buttonText: "No",
            secondButtonFunction: async () => {await OfflineDataService.deleteMap(item); refresh()},
            secondButtonText: "Yes",
          });
    }
    const moveToDetails = () => {
        navigation.navigate("OfflineMap", {mapName: item})
    }

    return (
        <TouchableOpacity style={[styles.container]} onPress={moveToDetails}>
            <View
                style={{
                    flex: 1,
                    display: "flex",
                    flexDirection: "row",
                    paddingLeft: 3,
                    paddingRight: 3,
                }}
            >
                <Text style={{ fontWeight: "600", width: "35%" }}>Nazwa mapy:</Text>
                <Text style={{ fontWeight: "bold", width: "65%" }}>{item}</Text>
            </View>
            <View style={{
                flex: 2,
                display: "flex",
                flexDirection: "row",
                justifyContent: "space-evenly",
                paddingLeft: 3,
                paddingRight: 3,
            }}>
                <View style={{justifyContent: "space-evenly", flexDirection: 'row', alignItems: 'center', gap: 40}}>

                    <TouchableOpacity style={{ padding: 15, borderRadius: 100, backgroundColor: "#eb701e"}} onPress={showDeleteAlert}>
                        <FontAwesomeIcon icon={faTrash} size={20} color="#fff" />
                    </TouchableOpacity>
                    <TouchableOpacity style={{ padding: 15, borderRadius: 100, backgroundColor: "#eb701e"}} onPress={moveToDetails}>
                        <FontAwesomeIcon icon={faChevronRight} size={20} color="#fff" />
                    </TouchableOpacity>
                </View>
            </View>
        </TouchableOpacity>
    )
}

const styles = StyleSheet.create({
    container: {
        flexDirection: "column",
        width: "90%",
        minHeight: 120,
        borderBottomWidth: 1,
        margin: 10,
        borderRadius: 5,
        borderWidth: 1,
        borderColor: "#1e88e5",
        borderLeftWidth: 6,
        paddingLeft: 12,
        marginLeft: 18
    },
    leftPanelImage: {
        width: 85,
        height: 85,
        marginLeft: 10,
        alignSelf: "center",
    },
    rightPanel: {
        marginLeft: 10,
        width: "70%",
        alignSelf: "center",
    },
    title: {
        color: "#FF7E2B",
        fontWeight: "bold",
    },
    date: {
        color: "#FF7E2B",
        fontWeight: "bold",
    },
    location: {
        color: "#5E86AE",
        fontWeight: "bold",
    },
    desc: {
        color: "#5E86AE",
        fontWeight: "bold",
    },
    colour: {
        color: "#5E86AE",
        fontWeight: "bold",
    },
    specialUserStyle: {
        borderColor: "#eb701e",
    },
});