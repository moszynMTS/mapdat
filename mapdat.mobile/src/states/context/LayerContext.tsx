import React, { createContext, useState } from 'react';
import OfflineDataService from '../../lib/OfflineMode/OfflineDataService';

interface LayerContextValue {
  layer: number;
  setLayer: React.Dispatch<React.SetStateAction<number>>;
  saveLayer: (name: string, data: any) => void
}

export const LayerContext = createContext<LayerContextValue | undefined>(undefined);

export const LayerProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const offlineDataService = OfflineDataService; 
  const [layer, setLayer] = useState<number>(1);

  const saveLayer = (name: string, data: any) => {
    offlineDataService.saveMapData(data, name);
  } 

  const value: LayerContextValue = {
    layer,
    setLayer,
    saveLayer
  };


  return <LayerContext.Provider value={value || null}>{children}</LayerContext.Provider>;
};