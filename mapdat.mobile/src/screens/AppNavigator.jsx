import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import { createDrawerNavigator } from '@react-navigation/drawer';
import { MainScreen } from "./MainScreens/MainScreen";
import { OfflineScreen } from "./OfflineScreen/OfflineScreen";
import { OfflineMap } from "../components/screenComponents/OfflineScreens/OfflineMap";

const Stack = createNativeStackNavigator();
const Drawer = createDrawerNavigator()

const StackNaVigations = () => {
  return (
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
  )
}
const OfflineStackNavigations = () => {
  return(
    <Stack.Navigator
      screenOptions={{
        animationTypeForReplace: "push",
        headerShown: true,
        animationEnabled: false,
      }}
      initialRouteName="OfflinePage"
    >
      <Stack.Screen
        name="OfflinePage"
        component={OfflineScreen}
        options={{
          headerTitle: "OfflinePage",
          headerTintColor: "#989898",
        }}
      />
      <Stack.Screen
        name="OfflineMap"
        component={OfflineMap}
        options={{
          headerTitle: "OfflineMap",
          headerTintColor: "#989898",
        }}
      />
    </Stack.Navigator>
  )
}

export const AppNavigator = () => {
  return (
    <NavigationContainer>
      <Drawer.Navigator screenOptions={{
        headerShown: false
      }}>
        <Drawer.Screen name="Main Map" component={StackNaVigations} />
        <Drawer.Screen name="Saved Maps" component={OfflineStackNavigations} />
      </Drawer.Navigator>
    </NavigationContainer>
  );
};
