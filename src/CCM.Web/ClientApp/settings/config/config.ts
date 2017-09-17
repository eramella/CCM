import { autoinject } from 'aurelia-framework';
import { HttpClient, json } from 'aurelia-fetch-client';
import { Camp } from '../camps/ICamp';
import { AppSettings } from './IAppSettings';
import { CampService } from '../../services/campService';
import { AppSettingsService } from '../../services/appSettingsService';
import { Notifier } from '../../helpers/notifier';
import { FetchErrorHandler as errorHandler } from '../../helpers/fetch-error-handler';

@autoinject
export class Config {
    upcomingCamps: Camp[];
    settings: AppSettings;
    fileName: String;
    campService: CampService;
    appSettingService: AppSettingsService;
    notifier: Notifier;
    pic1file: any;
    pic2file: any;
    pic3file: any;
    pic4file: any;
    pic5file: any;
    upload1vm: any;
    upload2vm: any;
    upload3vm: any;
    upload4vm: any;
    upload5vm: any;
    image1deleted = false;
    image2deleted = false;
    image3deleted = false;
    image4deleted = false;
    image5deleted = false;
    img1available = true;
    img2available = true;
    img3available = true;
    img4available = true;
    img5available = true;

    constructor(campService: CampService, appSettingsService: AppSettingsService, notifier: Notifier) {
        this.campService = campService;
        this.appSettingService = appSettingsService;
        this.notifier = notifier;

        this.getUpcamingCamps();

    }

    getUpcamingCamps() {
        let camps: Camp[];

        Promise.all([this.campService.getCamps(1), this.campService.getCamps(2)])
            .then(results => {
                camps = results[0];
                camps.push(results[1][0]);
                camps.sort(function (a, b) {
                    let left = new Date(a.dateFrom);
                    let right = new Date(b.dateFrom);
                    return left < right ? -1 : left > right ? 1 : 0;
                });
                this.upcomingCamps = camps;
            })
            .catch(error => {
                console.error(errorHandler(error));
            })
    }

    activate() {
        return this.appSettingService.getSettings()
            .then(data => {
                this.settings = data;
            })
            .catch(error => {
                console.error(error.message ? error.message : error);
            });
    }

    save() {
        let formData = new FormData();

        formData.append('campName', this.settings.campName);
        formData.append('nextCamp', this.settings.nextCamp ? this.settings.nextCamp.toString() : '');
        formData.append('tagLine', this.settings.tagLine);

        formData.append('image1deleted', this.image1deleted ? 'true' : 'false');
        formData.append('image2deleted', this.image2deleted ? 'true' : 'false');
        formData.append('image3deleted', this.image3deleted ? 'true' : 'false');
        formData.append('image4deleted', this.image4deleted ? 'true' : 'false');
        formData.append('image5deleted', this.image5deleted ? 'true' : 'false');

        if (this.pic1file) {
            formData.append('pic1Upload', this.pic1file);
        }
        if (this.pic2file) {
            formData.append('pic2Upload', this.pic2file);
        }
        if (this.pic3file) {
            formData.append('pic3Upload', this.pic3file);
        }
        if (this.pic4file) {
            formData.append('pic4Upload', this.pic4file);
        }
        if (this.pic5file) {
            formData.append('pic5Upload', this.pic5file);
        }

        this.appSettingService.saveSettings(formData)
            .then(data => {
                this.settings = data;
                this.clearUploads();
                this.refreshImages();
                this.notifier.success('', 'Saved!');
            })
            .catch(error => {
                this.notifier.error(error, 'Something went wrong!');
            });
    }

    clearUploads() {
        this.upload1vm.clear();
        this.upload2vm.clear();
        this.upload3vm.clear();
        this.upload4vm.clear();
        this.upload5vm.clear();
    }

    refreshImages() {
        let timeStamp = new Date().getTime().toString();
        this.settings.image1Url ? this.settings.image1Url + timeStamp : '';
        this.settings.image2Url ? this.settings.image2Url + timeStamp : '';
        this.settings.image3Url ? this.settings.image3Url + timeStamp : '';
        this.settings.image4Url ? this.settings.image4Url + timeStamp : '';
        this.settings.image5Url ? this.settings.image5Url + timeStamp : '';
        this.image1deleted = this.image2deleted = this.image3deleted = this.image4deleted = this.image5deleted = false;
    }
}