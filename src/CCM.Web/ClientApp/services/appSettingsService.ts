import { inject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { AppSettings } from "../settings/config/IAppSettings";
import { FetchErrorHandler as errorHandler } from '../helpers/fetch-error-handler';

@inject(HttpClient)
export class AppSettingsService {
    private baseUrl = '/api/AppSettings';

    constructor(private http: HttpClient) { }

    getSettings() {
        let promise = new Promise<AppSettings>((resolve, reject) => {
            this.http.fetch(this.baseUrl)
                .then(results => results.json() as Promise<AppSettings>)
                .then(data => {
                    resolve(data);
                })
                .catch(error => {
                    reject(errorHandler(error));
                });

        });

        return promise;
    }

    saveSettings(formData) {
        let promise = new Promise<AppSettings>((resolve, reject) => {
            this.http.fetch(this.baseUrl, {
                method: 'POST',
                body: formData,
                headers: new Headers()
            })
                .then(results => results.json())
                .then(data => {
                    resolve((<any>data).value as Promise<AppSettings>);
                })
                .catch(error => {
                    reject(errorHandler(error));
                });
        });

        return promise;
    }
}