import { inject } from 'aurelia-framework';
import { HttpClient, json } from 'aurelia-fetch-client';
import { Camp } from '../settings/camps/ICamp';
import { FetchErrorHandler as errorHandler } from '../helpers/fetch-error-handler';

@inject(HttpClient)
export class CampService {
    baseUrl = '/api/Camp';

    constructor(private http: HttpClient) { }

    getCamps(state?: number) {
        let promise = new Promise<Camp[]>((resolve, reject) => {
            let url = this.baseUrl + 's' + (state ? '/' + state : '');

            this.http.fetch(url)
                .then(response => response.json() as Promise<Camp[]>)
                .then(data => {
                    data.forEach(item => {
                        item.dateFrom = new Date(item.dateFrom);
                        item.dateTo = new Date(item.dateTo);
                    });
                    resolve(data);
                })
                .catch(error => {
                    reject(errorHandler(error));
                });
        });

        return promise;
    }

    getCamp(id) {
        let promise = new Promise<Camp>((resolve, reject) => {
            this.http.fetch(this.baseUrl + '/' + id)
                .then(response => response.json() as Promise<Camp>)
                .then(data => {
                    data.dateFrom = new Date(data.dateFrom);
                    data.dateTo = new Date(data.dateTo);
                    resolve(data);
                })
                .catch(error => {
                    reject(errorHandler(error));
                });
        });

        return promise;
    }

    addCamp(camp: Camp) {
        let promise = new Promise<Camp>((resolve, reject) => {
            this.http.fetch(this.baseUrl, {
                method: 'post',
                body: json(camp)
            })
                .then(response => response.json() as Promise<Camp>)
                .then(data => {
                    resolve(data);
                })
                .catch(error => {
                    reject(errorHandler(error));
                });
        });

        return promise;
    }

    updateCamp(camp: Camp) {
        let promise = new Promise((resolve, reject) => {
            this.http.fetch(this.baseUrl + '/' + camp.id, {
                method: 'put',
                body: json(camp)
            })
                .then(response => resolve())
                .catch(error => {
                    reject(errorHandler(error));
                });
        });

        return promise;
    }

    deleteCamp(camp: Camp) {
        let promise = new Promise((resolve, reject) => {
            this.http.fetch(this.baseUrl + '/' + camp.id, {
                method: 'delete'
            }).then(response => resolve())
                .catch(error => {
                    reject(errorHandler(error));
                });
        });

        return promise;
    }
}