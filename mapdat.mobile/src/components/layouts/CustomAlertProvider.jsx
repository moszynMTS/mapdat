import React, { Component, createContext } from "react";
import {
  Modal,
  TouchableOpacity,
  View,
  Text,
  TouchableWithoutFeedback,
} from "react-native";

export const CustomAlertContext = createContext();


class CustomAlertProvider extends Component {
  constructor(props) {
    super(props);
    this.state = {
      show: false,
      title: "",
      message: "",
      buttonFunction: null,
      buttonText: null,
      secondButtonFunction: null,
      secondButtonText: null,
    };
  }

  static alert({
    title,
    message,
    buttonFunction,
    buttonText = "OK",
    secondButtonFunction,
    secondButtonText,
  }) {
    if (this.instance) {
      this.instance.setState({
        show: true,
        title,
        message,
        buttonFunction,
        buttonText,
        secondButtonFunction,
        secondButtonText,
      });
    } else {
      console.error("CustomAlertProvider instance not found.");
    }
  }

  componentDidMount() {
    CustomAlertProvider.instance = this;
  }

  componentWillUnmount() {
    CustomAlertProvider.instance = null;
  }

  closeModal = () => {
    this.setState({ show: false });
  };

  render() {
    const {
      show,
      title,
      message,
      buttonFunction,
      buttonText,
      secondButtonFunction,
      secondButtonText,
    } = this.state;

    return (
      <CustomAlertContext.Provider value={this}>
        {this.props.children}
        <Modal visible={show} animationType="slide" transparent={true}>
          <TouchableOpacity
            style={{
              backgroundColor: "rgba(0,0,0,0.4)",
              flex: 1,
              alignItems: "center",
              justifyContent: "center",
            }}
            onPress={() => {
              buttonFunction && buttonFunction();
              this.closeModal();
            }}
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
                    marginBottom: 20,
                  }}
                >
                  <Text style={{ fontWeight: "700", fontSize: 20, color: "#434647" }}>
                    {title}
                  </Text>
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
                      color: "#434647",
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
                    onPress={() => {
                      buttonFunction && buttonFunction();
                      this.closeModal();
                    }}
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
                      {buttonText}
                    </Text>
                  </TouchableOpacity>
                  {secondButtonText != null && (
                    <TouchableOpacity
                      onPress={() => {
                        secondButtonFunction && secondButtonFunction();
                        this.closeModal();
                      }}
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
                        {secondButtonText}
                      </Text>
                    </TouchableOpacity>
                  )}
                </View>
              </View>
            </TouchableWithoutFeedback>
          </TouchableOpacity>
        </Modal>
      </CustomAlertContext.Provider>
    );
  }
}


export default CustomAlertProvider;
