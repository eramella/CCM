import { DialogController } from 'aurelia-dialog';
import { inject } from 'aurelia-framework';
import { Session } from "./ISession";

@inject(DialogController)
export class SessionPopup {
    dialogController: DialogController;
    session: Session;

    constructor(dialogController) {
        this.dialogController = dialogController;
    }

    activate(session) {
        this.session = session;
    }

}