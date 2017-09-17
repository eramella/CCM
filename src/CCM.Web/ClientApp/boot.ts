import 'isomorphic-fetch';
import { Aurelia, PLATFORM, FrameworkConfiguration } from 'aurelia-framework';
import 'eonasdan-bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.min.css';
import { HttpClient } from 'aurelia-fetch-client';

declare const IS_DEV_BUILD: boolean; // The value is supplied by Webpack during the build

export function configure(aurelia: Aurelia) {

    aurelia.use
        .standardConfiguration()
        .globalResources(PLATFORM.moduleName('components/loading-indicator'))
        .plugin(PLATFORM.moduleName('aurelia-validation'))
        .plugin(PLATFORM.moduleName('aurelia-dialog'))
        .plugin(PLATFORM.moduleName('aurelia-bootstrap-datetimepicker'));

    if (IS_DEV_BUILD) {
        aurelia.use.developmentLogging();
    }

    let container = aurelia.container;

    let http = new HttpClient();
    http.configure(conf => conf.useStandardConfiguration());
    container.registerInstance(HttpClient, http);

    aurelia.start().then(() => aurelia.setRoot(PLATFORM.moduleName('app/app')));
}