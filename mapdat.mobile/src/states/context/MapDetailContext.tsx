import React, { createContext, useState } from "react";

type TMapDetailContext = {
    display: boolean;
    setDisplay: React.Dispatch<React.SetStateAction<boolean>>;
}
type TMapDisplay = boolean;

export const MapDetailContext = createContext<TMapDetailContext | undefined>(undefined);

export const MapDetailProvider: React.FC<{children: React.ReactNode}> = ({children}) => {
    const [display, setDisplay] = useState<boolean>(false);

    const value: TMapDetailContext = {
        display,
        setDisplay
    }

    return <MapDetailContext.Provider value={value || null}>{children}</MapDetailContext.Provider>
} 