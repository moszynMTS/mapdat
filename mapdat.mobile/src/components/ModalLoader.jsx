import { ActivityIndicator, Modal, Text, TouchableOpacity, TouchableWithoutFeedback, View } from "react-native"
import { Logo } from "./layouts/Logo"

export const ModalLoader = ({
    message,
    show
}) => {
    return (
        <Modal visible={show} animationType="slide" transparent={true}>
            <TouchableOpacity
                style={{
                    backgroundColor: "rgba(0,0,0,0.4)",
                    flex: 1,
                    alignItems: "center",
                    justifyContent: "center",
                }}
                onPress={() => buttonFunction()}
            >
                <TouchableWithoutFeedback>
                    <View
                        style={{
                            width: 300,
                            height: 300,
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
                            <Logo
                                style={{
                                    width: 50,
                                    height: 50,
                                    marginBottom: 20,
                                    borderRadius: 45,
                                }}
                            />
                        </View>
                        <View
                            style={{
                                flex: 1,
                                flexDirection: "row",
                                justifyContent: "center",
                                alignItems: "center",
                            }}
                        >
                            <ActivityIndicator size={"large"} color={"#0000ff"}/>
                        </View>
                        <View
                            style={{
                                flex: 2,
                                flexDirection: "row",
                                justifyContent: "center",
                                alignItems: "center",
                            }}
                        >
                            <Text
                                style={{
                                    textAlign: "center",
                                    fontWeight: "600",
                                    fontSize: 16,
                                    color: '#434647'
                                }}
                            >
                                {message}
                            </Text>
                        </View>
                    </View>
                </TouchableWithoutFeedback>
            </TouchableOpacity>
        </Modal>
    )
}