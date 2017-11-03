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

    addMemberToTeam(id: string) {
        let promise = new Promise<any>((resolve, reject) => {
            this.http.fetch(this.baseUrl + '/MakeMember/' + id, {
                method: 'PUT'
            })
                .then(result => {
                    return result.json() as Promise<any>
                })
                .then(data => {
                    resolve(data);
                })
                .catch(error => {
                    reject(errorHandler(error));
                });
        });

        return promise;
    }

    removeMemberFromTeam(id: string) {
        let promise = new Promise((resolve, reject) => {
            this.http.fetch(this.baseUrl + '/RemoveMember/' + id, {
                method: 'PUT'
            })
                .then(result => result.json() as Promise<any>)
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
    skip: number
}

export interface DynamicResult {
    data: TeamMember[],
    pageSize: number,
    skip: number,
    total: number
}

export interface TeamMember {
    Id: string,
    username: string,
    firstName: string,
    lastName: string,
    isTeamMember: boolean
}