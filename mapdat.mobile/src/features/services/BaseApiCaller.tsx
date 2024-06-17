import { UseMutationResult, UseQueryResult } from "@tanstack/react-query";

export type TOptionalParams = {
    optionalParam?:       string;
    secondOptionalParam?: string;
}

export type TMutationMethodFunction<T = any> = (params?: TOptionalParams) => UseMutationResult
export type TQueryMethodFunction<T = any> = (params?: TOptionalParams) => UseQueryResult;

export class BaseApiService{
    private methodMutationMap: { [key: number] : TMutationMethodFunction} = {};
    private methodQueryMap: { [key: number | string] : TQueryMethodFunction} = {};

    protected registerMutationMethod<T = any>(layer: number, method: TMutationMethodFunction<T>): void{
        this.methodMutationMap[layer] = method;
    }
    protected registerQueryMethod<T = any>(layer: number | string, method: TQueryMethodFunction<T>): void{
        this.methodQueryMap[layer] = method;
    }

    protected callMutationMethod<T = any>(layer: number, params?: TOptionalParams){
        const method = this.methodMutationMap[layer] as TMutationMethodFunction<T>;
        if(!method)
            throw new Error(`No method mapped for layer ${layer}`);
        return method(params);
    }
    protected callQueryMethod<T = any>(layer: number | string, params?: TOptionalParams){
        const method = this.methodQueryMap[layer] as TQueryMethodFunction<T>;
        if(!method)
            throw new Error(`No method mapped for layer ${layer}`);
        return method(params);
    }

}