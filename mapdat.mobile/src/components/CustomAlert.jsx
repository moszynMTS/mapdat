import {
    Modal,
    TouchableOpacity,
    View,
    Text,
    TouchableWithoutFeedback,
    Platform,
  } from "react-native";
  
  export const CustomAlert = ({
    title,
    message,
    show,
    buttonFunction,
    buttonText = null,
    secondButtonFunction,
    secondButtonText = null,
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
                <Text style={{ fontWeight: "700", fontSize: 20, color:'#434647' }}>{title}</Text>
              </View>
              <View
                style={{
                  flex: 1,
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
                    color:'#434647'
                  }}
                >
                  {message}
                </Text>
              </View>
              <View
                style={{
                  flex: 1,
                  flexDirection: "row",
                  justifyContent: "space-around",
                  alignItems: "center",
                  paddingHorizontal: 35,
                }}
              >
                <TouchableOpacity
                  onPress={() => buttonFunction()}
                  style={{
                    padding: 7,
                    paddingHorizontal: 15,
                    borderRadius: 25,
                    backgroundColor: "#1e88e5",
                  }}
                >
                  <Text
                    style={{
                      fontWeight: "600",
                      color: "white",
                      fontSize: 17,
                    }}
                  >
                    {buttonText != null ? buttonText : "OK"}
                  </Text>
                </TouchableOpacity>
                {secondButtonText != null ? (
                  <TouchableOpacity
                    onPress={() => secondButtonFunction()}
                    style={{
                      padding: 7,
                      paddingHorizontal: 15,
                      borderRadius: 25,
                      backgroundColor: "#eb701e",
                    }}
                  >
                    <Text
                      style={{
                        fontWeight: "600",
                        color: "white",
                        fontSize: 17,
                      }}
                    >
                      {secondButtonText != null ? secondButtonText : "OK"}
                    </Text>
                  </TouchableOpacity>
                ) : null}
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>
    );
  };