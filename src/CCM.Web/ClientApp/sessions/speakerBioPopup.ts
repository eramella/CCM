import { DialogController } from 'aurelia-dialog';
import { inject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';

@inject(DialogController, HttpClient)
export class SpeakerBioPopup {
    dialogController: DialogController;
    userId: string;
    http: HttpClient;
    speaker: Speaker;

    constructor(dialogController, http) {
        this.dialogController = dialogController;
        this.http = http;
    }

    activate(userId) {
        this.userId = userId;
        return this.http.fetch('/api/Speaker/' + this.userId)
            .then(results => results.json() as Promise<Speaker>)
            .then(data => {
                this.speaker = data;
            })
            .catch(error => {
                return false;
            });
    }

}

export interface Speaker {
    firstName: string,
    lastName: string,
    okToContact: boolean,
    bio: string
}