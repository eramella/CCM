import { inject } from 'aurelia-framework';
import { Camp } from "./ICamp";
import { Router } from 'aurelia-router';
import { CampService } from '../../services/campService';

@inject(Router,CampService)
export class Camps {
    camps: Camp[];
    router: Router;
    campService: CampService;

    constructor(router, campService) {
        this.router = router;
        this.campService = campService;
    }

    activate() {
        return this.campService.getCamps()
            .then(data => {
                this.camps = data;
            })
            .catch(error => {
                console.error(error);
            });
    }

    selectCamp(id) {
        this.router.navigate('camp/' + id);
    }
}