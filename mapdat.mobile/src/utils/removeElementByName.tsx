export const removeElementByName = (array: unknown[], name: string )=> {
    return array.filter(element => element != name);
}