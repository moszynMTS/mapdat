import { StatusBar } from 'expo-status-bar';
import { useCallback, useEffect, useState } from 'react';
import { StyleSheet, Text, View } from 'react-native';
import { Background } from './src/components/Bacground';
import { Logo } from './src/components/Logo';
import * as SplashScreen from "expo-splash-screen";
import { SafeAreaProvider } from 'react-native-safe-area-context';
import { AppNavigator } from './src/screens/AppNavigator';

export default function App() {
  const [appIsReady, setAppIsReady] = useState(false);


  useEffect(() => {
    async function prepare() {
      try {
        await new Promise((resolve) => setTimeout(resolve, 2000));
      } catch (e) {
        console.warn(e);
      } finally {
        setAppIsReady(true);
      }
    }
    prepare();
  }, []);

  const onLayoutRootView = useCallback(async () => {
    if (appIsReady) {
      await SplashScreen.hideAsync();
    }
  }, [appIsReady]);


  if (!appIsReady) {
    return (
      <Background style={styles.background}>

        <Logo style={styles.logo} />
        {/* <BottomText style={styles.text} /> */}
      </Background>
    );
  }

  return (
    <SafeAreaProvider>
      <AppNavigator onLayout={onLayoutRootView} />
    </SafeAreaProvider>
  );
}

const styles = StyleSheet.create({
  background: {
    justifyContent: "center",
    alignItems: "center",
    backgroundColor: "white"
  },
  logo: {
    width: 250,
    height: 284,
  },
  text: {
    width: 251,
    height: 84,
    marginBottom: 100,
  },
});
