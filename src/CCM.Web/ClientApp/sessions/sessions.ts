import { HttpClient } from 'aurelia-fetch-client';
import { inject } from 'aurelia-framework';
import { Session } from "./ISession";
import { DialogService } from 'aurelia-dialog';
import { SpeakerBioPopup } from './speakerBioPopup';
import { SessionPopup } from './SessionPopup';
import { Notifier } from '../helpers/notifier';
import { GetCampId } from '../helpers/get-camp-id';

@inject(HttpClient, DialogService, Notifier)
export class Sessions {
    public sessions: Session[];
    http: HttpClient;
    dialogService: DialogService;
    notifier: Notifier;

    constructor(http: HttpClient, dialogService: DialogService, notifier) {
        this.http = http;
        this.dialogService = dialogService;
        this.notifier = notifier;
    }

    activate() {
        let campId = GetCampId();
        if (campId === 0) {
            this.notifier.error("Missing CampId", "Oops! Cannot get Sessions");
            return;
        }
        return this.http.fetch('/api/Sessions?campId=' + campId)
            .then(results => results.json() as Promise<Session[]>)
            .then(data => {
                this.sessions = data;
            })
            .catch(error => {
                return false;
            });
    }

    seeSpeakerBio(userId: string) {
        this.dialogService.open({ viewModel: SpeakerBioPopup, model: userId, lock: false })
            .whenClosed(result => {
                return;
            });
    }

    seeFullSession(session) {
        this.dialogService.open({ viewModel: SessionPopup, model: session, lock: false })
            .whenClosed(result => {
                return;
            });
    }    
}