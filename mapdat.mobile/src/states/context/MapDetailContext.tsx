import React, { createContext, useState } from "react";

export type infoData = [{count: string, subject: string}];

type TMapDetailContext = {
    display: boolean;
    setDisplay: React.Dispatch<React.SetStateAction<boolean>>;
    data: infoData
    setData: React.Dispatch<React.SetStateAction<any>>;
}
type TMapDisplay = boolean;

export const MapDetailContext = createContext<TMapDetailContext | undefined>(undefined);

export const MapDetailProvider: React.FC<{children: React.ReactNode}> = ({children}) => {
    const [display, setDisplay] = useState<boolean>(false);
    const [data, setData] = useState<infoData>([{count: "", subject: ""}]);

    const value: TMapDetailContext = {
        display,
        setDisplay,
        data,
        setData
    }

    return <MapDetailContext.Provider value={value || null}>{children}</MapDetailContext.Provider>
} 