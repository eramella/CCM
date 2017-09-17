import { inject } from 'aurelia-framework';
import { UsersService, DynamicRequest, DynamicResult } from "../../services/usersService";
import { Notifier } from '../../helpers/notifier';

@inject(UsersService, Notifier)
export class UsersRoles {
    query: DynamicRequest = {
        token: '',
        pageSize: 10,
        skip: 0,
        total: 0
    }

    result: DynamicResult;

    constructor(
        private usersService: UsersService,
        private notifier: Notifier
    ) { }

    search() {
        this.usersService
            .getUsers(this.query)
            .then(data => {
                this.result = data;
                this.refreshGrid();
            })
            .catch(error => {
                this.notifier.error(error);
            });
    }

    refreshGrid() {

    }
}