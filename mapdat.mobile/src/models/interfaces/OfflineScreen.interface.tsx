import { RouteProp } from '@react-navigation/native';
import { DrawerContentComponentProps } from '@react-navigation/drawer';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';

type RootStackParamList ={
    OfflinePage: undefined,
    OfflineMap: {mapName: string}
}

export type OfflineScreenNavigationProps = NativeStackNavigationProp<RootStackParamList, 'OfflinePage'> | NativeStackNavigationProp<RootStackParamList, 'OfflineMap'>;


 type OfflineScreenRouteProp = RouteProp<RootStackParamList, 'OfflinePage'>

 export interface OfflineScreenProps {
    navigation: OfflineScreenNavigationProps,
    route: OfflineScreenRouteProp
 }