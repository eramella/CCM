import { inject } from 'aurelia-framework';
import { HttpClient, json } from 'aurelia-fetch-client';
import { FetchErrorHandler as errorHandler } from '../helpers/fetch-error-handler';

@inject(HttpClient)
export class UsersService { 

    private baseUrl = '/api';

    constructor(private http: HttpClient) { }

    getUsers(query: DynamicRequest) {
        let promise = new Promise<DynamicResult>((resolve, reject) => {
            this.http.fetch(this.baseUrl + '/TeamMembers', {
                method: 'POST',
                body: json(query)
            })
                .then(result => result.json() as Promise<DynamicResult>)
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

export interface DynamicRequest {
    token: string,
    pageSize: number,
    skip: number,
    total: number
}

export interface DynamicResult {
    data: string,
    pageSize: number,
    skip: number,
    total: number
}

export interface TeamMember {
    Id: string,
    username: string,
    firstName: string,
    lastName: string
}