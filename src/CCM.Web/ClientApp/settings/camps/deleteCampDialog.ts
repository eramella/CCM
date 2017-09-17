import { DialogController } from 'aurelia-dialog';
import { inject } from 'aurelia-framework';

@inject(DialogController)
export class DeleteCampDialog {
    dialogController: DialogController;

    constructor(dialogController) {
        this.dialogController = dialogController;
    }

    activate() {

    }

    deleteCamp() {
        this.dialogController.ok();
    }

    cancel() {
        this.dialogController.cancel();
    }
}