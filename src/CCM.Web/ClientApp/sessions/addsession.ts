import { inject } from 'aurelia-framework';
import { HttpClient, json } from 'aurelia-fetch-client';
import { Session } from "./ISession";
import { SessionLevel } from "./ISessionLevel";
import { Router } from 'aurelia-router';
import { Notifier } from '../helpers/notifier';
import { GetCampId } from '../helpers/get-camp-id';

@inject(HttpClient, Router, Notifier)
export class AddSession {
    fetch: HttpClient;
    session: Session;
    sessionLevels: SessionLevel[];
    router: Router;
    notifier: Notifier;

    constructor(fetch: HttpClient, router: Router, notifier: Notifier) {
        this.router = router;
        this.notifier = notifier
        this.fetch = fetch;
        this.fetch.fetch('/api/Sessions/GetLevels')
            .then(results => results.json() as Promise<SessionLevel[]>)
            .then(data => {
                this.sessionLevels = data;
            })
            .catch(error => {
                console.error(error.message ? error.message : error);
            });
    }

    save() {
        let campId = GetCampId();
        if (campId === 0) {
            this.notifier.error("Missing CampId", "Oops! Cannot save");
            return;
        }
        this.session.campId = campId;
        this.fetch.fetch('/api/Sessions', {
            method: 'post',
            body: json(this.session)
        })
            .then(response => {
                this.router.navigate('sessions');
            })
            .catch(error => {
                this.notifier.error(error, "Oops! Did't save");
            });
    }
}