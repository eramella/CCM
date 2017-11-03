import { inject } from 'aurelia-framework';
import { UsersService, DynamicRequest, DynamicResult } from "../../services/usersService";
import { Notifier } from '../../helpers/notifier';

@inject(UsersService, Notifier)
export class UsersRoles {
    query: DynamicRequest = {
        token: '',
        pageSize: 10,
        skip: 0
    }
    pages: number = 0;

    result: DynamicResult;

    constructor(
        private usersService: UsersService,
        private notifier: Notifier
    ) { }

    activate() {
        this.search();
    }

    search() {
        this.usersService
            .getUsers(this.query)
            .then(data => {
                this.result = data;
                this.updatePages();
            })
            .catch(error => {
                this.notifier.error(error);
            });
    }

    pageBack() {
        if (this.query.skip !== 0) {
            this.query.skip -= this.query.pageSize;
            if (this.query.skip < 0) {
                this.query.skip = 0;
            }
        }
        this.search();
    }

    pageForward() {
        this.query.skip += this.query.pageSize;
        this.search();
    }

    updatePages() {
        if (this.result) {
            let n = this.result.total / this.result.pageSize;
            this.pages = Math.ceil(n);
        }
    }

    gotToPage(n) {
        this.query.skip = this.query.pageSize * n;
        this.search();
    }

    addMemberToTeam(id) {
        this.usersService
            .addMemberToTeam(id)
            .then(data => {
                this.search();
            })
            .catch(error => {
                this.notifier.error(error);
            });
    }

    removeMemberFromTeam(id) {
        this.usersService
            .removeMemberFromTeam(id)
            .then(data => {
                this.search();
            })
            .catch(error => {
                this.notifier.error(error);
            });
    }
}