import React, { createContext, useEffect, useState } from "react";
import OfflineDataService from "../../lib/OfflineMode/OfflineDataService";
interface LayerContextValue {
  layer: number;
  setLayer: React.Dispatch<React.SetStateAction<number>>;
  saveLayer: (name: string, data: any, areaInfoData: unknown) => void;
}

export const LayerContext = createContext<LayerContextValue | undefined>(
  undefined
);

export const LayerProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [layer, setLayer] = useState<number>(1);

  const offlineDataService = OfflineDataService;

  const saveLayer = (name: string, data: any, areaInfoData: unknown) => {
    console.log(areaInfoData);
    offlineDataService.saveMapData(data, name, areaInfoData);
  };

  const value: LayerContextValue = {
    layer,
    setLayer,
    saveLayer,
  };

  return (
    <LayerContext.Provider value={value || null}>
      {children}
    </LayerContext.Provider>
  );
};
