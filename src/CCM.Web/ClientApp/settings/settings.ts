import { Router, RouterConfiguration } from 'aurelia-router';
import { PLATFORM } from 'aurelia-framework';

export class Settings {

    router: Router;

    configureRouter(config: RouterConfiguration, router: Router) {
        config.title = 'Camp Settings';
        config.map([{
            route: ['camps', '/'],
            title: 'Camps',
            name: 'camps',
            moduleId: PLATFORM.moduleName('./camps/camps'),
            nav: true
        }, {
            route: 'camp/:id?',
            title: 'Add/Edit Camp',
            name: 'camp',
            moduleId: PLATFORM.moduleName('./camps/camp'),
            nav: true,
            href: '#/settings/camp'
        }, {
            route: 'config',
            title: 'App Configuration',
            name: 'config',
            moduleId: PLATFORM.moduleName('./config/config'),
            nav: true,
            href: '#/settings/config'
        }, {
            route: 'usersroles',
            title: 'Users Roles',
            name: 'usersroles',
            moduleId: PLATFORM.moduleName('./users/usersroles'),
            nav: true,
            href: '#/settings/usersroles'
        }]);

        this.router = router;
    }
}