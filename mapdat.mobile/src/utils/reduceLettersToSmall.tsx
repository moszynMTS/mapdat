export const  reduceLettersToSmall = (string: string) =>{
    if (string.length === 0) {
        return string;
    }
    return string.toLowerCase();
}