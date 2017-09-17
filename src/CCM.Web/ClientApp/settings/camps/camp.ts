import { inject, NewInstance, bindable } from 'aurelia-framework';
import { Camp as camp } from './ICamp';
import { Router } from 'aurelia-router';
import { ValidationRules, ValidationController, validateTrigger } from 'aurelia-validation';
import { BootstrapFormRenderer } from '../../helpers/bootstrap-form-validation-renderer';
import { DialogService } from 'aurelia-dialog';
import { DeleteCampDialog } from './deleteCampDialog';
import { Notifier } from '../../helpers/notifier';
import { CampStates } from "../ICampStates";
import { CampService } from "../../services/campService";
import { UsState, UsStatesService } from "../../services/usStateService";


@inject(Router, NewInstance.of(ValidationController), DialogService, Notifier, CampService, UsStatesService)
export class Camp {
    @bindable fromPicker;
    notifier: Notifier;
    isNewCamp: boolean = false;
    currentCamp: camp = <camp>{ dateFrom: new Date(), dateTo: new Date() };
    router: Router;
    controller: ValidationController;
    dialogService: DialogService;
    campService: CampService;
    usStates: UsState[];
    usStatesService: UsStatesService;
    editorOptions = {
        placeholder: 'Type Location Instruction for Parking and the likes...'
    };

    constructor(router, controller, dialogService, notifier, campService, usStatesService) {
        this.router = router;
        this.controller = controller;
        this.controller.validateTrigger = validateTrigger.changeOrBlur;
        this.controller.addRenderer(new BootstrapFormRenderer);
        this.dialogService = dialogService;
        this.notifier = notifier;
        this.campService = campService;
        this.usStatesService = usStatesService;

        ValidationRules
            .ensure((c: camp) => c.locationName).required().minLength(3)
            .ensure((c: camp) => c.dateFrom).required()
            .ensure((c: camp) => c.dateTo).required()
            .ensure((c: camp) => c.dateTo).satisfies((val, c: camp) => val >= c.dateFrom).withMessage('Ends On must be greater or equal to Start On')
            .on(this.currentCamp);
    }

    activate(params) {
        this.usStatesService.getStateList()
            .then(data => {
                this.usStates = data;
            })
            .catch(error => console.log(error));

        if (params.id) {
            return this.campService
                .getCamp(params.id)
                .then(data => {
                    this.currentCamp = data
                })
                .catch(error => console.error(error));
        }
        this.isNewCamp = true;
    }

    fromPickerChanged() {
        this.fromPicker.events.onChange = (e) => {
            if (this.currentCamp.dateTo < e.date) {
                this.currentCamp.dateTo = e.date;
            }
        }
    }

    save() {
        this.controller.validate()
            .then(result => {
                if (result.valid) {
                    if (this.isNewCamp) {
                        this.campService
                            .addCamp(this.currentCamp)
                            .then(response => {
                                this.router.navigate('camps');
                            }).catch(error => {
                                this.notifier.error(error, "Oops! Didn't save")
                            });
                    } else {
                        this.campService
                            .updateCamp(this.currentCamp)
                            .then(response => {
                                this.router.navigate('camps');
                            }).catch(error => {
                                this.notifier.error(error, "Oops! Didn't save")
                            });
                    }
                }
            });
    }

    deleteCamp() {
        this.dialogService.open({ viewModel: DeleteCampDialog })
            .whenClosed(result => {
                if (result.wasCancelled) {
                    return;
                } else {
                    this.campService
                        .deleteCamp(this.currentCamp)
                        .then(response => {
                            this.router.navigate('camps');
                        })
                        .catch(error => {
                            this.notifier.error(error, "Oops! Did't delete");
                        });
                }
            });
    }    
}