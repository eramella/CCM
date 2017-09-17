import { inject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { FetchErrorHandler as errorHandler } from '../helpers/fetch-error-handler';

@inject(HttpClient)
export class UsStatesService {
    constructor(private http: HttpClient) { }

    getStateList() {
        let promise = new Promise<UsState[]>((resolve, reject) => {
            this.http.fetch('/USStates.json')
                .then(results => results.json() as Promise<UsState[]>)
                .then(data => {
                    resolve(data);
                })
                .catch(error => {
                    reject(errorHandler(error));
                });
        });

        return promise;
    }
}

export interface UsState {
    name: string,
    abbriviation: string
}