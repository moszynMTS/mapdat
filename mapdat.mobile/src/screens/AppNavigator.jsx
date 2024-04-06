import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import { MainScreen } from "./MainScreens/MainScreen";

const Stack = createNativeStackNavigator();

export const AppNavigator = () => {
  return (
    <NavigationContainer>
      <Stack.Navigator
        screenOptions={{
          animationTypeForReplace: "push",
          headerShown: true,
          animationEnabled: false,
        }}
        initialRouteName="MainPage"
      >
        <Stack.Screen
          name="MainPage"
          component={MainScreen}
          options={{
            headerTitle: "MainPage",
            headerTintColor: "#989898",
          }}
        />
      </Stack.Navigator>
    </NavigationContainer>
  );
};
