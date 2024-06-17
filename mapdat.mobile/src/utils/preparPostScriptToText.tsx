import { reduceLettersToSmall } from "./reduceLettersToSmall"

export const preparPostScriptToText = (subject: string) => {
    if(reduceLettersToSmall(subject) == "dochody" || reduceLettersToSmall(subject) == "wydatki")
        return "zł";
    if(reduceLettersToSmall(subject) == "ludnosc")
        return "tys.";
    if(reduceLettersToSmall(subject) == "kina" || reduceLettersToSmall(subject) == "gastronomia" || reduceLettersToSmall(subject) == "szpitale" || reduceLettersToSmall(subject) == "zlobki" || reduceLettersToSmall(subject) == "szkoly" )
        return "szt.";
    return "";
}