import React from "react";
import { Modal, Text, TouchableOpacity, TouchableWithoutFeedback, View } from "react-native"

type TModalInfo = {
    visible: boolean;
    setVisible: (visible: boolean) => void
}

export const ModalInfo: React.FC<TModalInfo> = ({visible, setVisible}) => {
    return(
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
                                test
                            </Text>
                        </View>
                    </View>
                </TouchableWithoutFeedback>
            </TouchableOpacity>
        </Modal>
    )
}