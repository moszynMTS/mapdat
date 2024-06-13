import { subjects } from "../models/consts/Details.const";

export const prepareSubjectsRequest = () => {
    let requestString: string = ``;
    subjects.forEach(element => {
        requestString += `&Subjects=${element}`
    });
    return requestString;
}