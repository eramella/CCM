import { Aurelia, PLATFORM, inject } from 'aurelia-framework';
import { Router, RouterConfiguration } from 'aurelia-router';
import { HttpClient } from 'aurelia-fetch-client';
import 'bootstrap-sass';

@inject(HttpClient)
export class App {
    public router: Router;
    fetch: HttpClient;

    constructor(http:HttpClient) {
        this.fetch = http;
    }

    configureRouter(config: RouterConfiguration, router: Router) {
        config.title = 'SoCal Code Camp';
        config.map([{
            route: 'speakers',
            name: 'speakers',
            settings: { icon: 'Speakers' },
            moduleId: PLATFORM.moduleName('../speakers/speakers'),
            nav: true,
            title: 'Speakers'
        }, {
            route: 'sessions',
            name: 'sessions',
            settings: { icon: 'education' },
            moduleId: PLATFORM.moduleName('../sessions/sessions'),
            nav: true,
            title: 'Sessions List'
        }, {
            route: 'tracks',
            name: 'tracks',
            settings: { icon: 'th-list' },
            moduleId: PLATFORM.moduleName('../tracks/tracks'),
            nav: true,
            title: 'Tracks'
        }, {
            route: 'schedule',
            name: 'schedule',
            settings: { icon: 'th-list' },
            moduleId: PLATFORM.moduleName('../schedule/schedule'),
            nav: true,
            title: 'Schedule'
        }, {
            route: 'addSession',
            name: 'addSession',
            moduleId: PLATFORM.moduleName('../sessions/addsession'),
            nav: true,
            title: 'Add Session'
        }, {
            route: 'settings',
            name: 'settings',
            moduleId: PLATFORM.moduleName('../settings/settings'),
            nav: true,
            title: 'Settings'
        }]);

        this.router = router;
    }
}
