import { RouteProp } from '@react-navigation/native';
import { DrawerContentComponentProps } from '@react-navigation/drawer';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';

type RootStackParamList ={
    OfflineMap: {mapName: string}
}

 type OfflineMapNavigationProps = NativeStackNavigationProp<RootStackParamList, 'OfflineMap'>;

 type OfflineMapRouteProp = RouteProp<RootStackParamList, 'OfflineMap'>

 export interface OfflineMapProps {
    navigation: OfflineMapNavigationProps,
    route: OfflineMapRouteProp
 }