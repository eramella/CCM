import { bindable } from 'aurelia-framework';

export class LoadingIndicator {
    @bindable loading = false;

    isLoading: boolean = false;

    loadingChanged(newValue) {
        if (newValue) {
            this.isLoading = true;
        } else {
            this.isLoading = false;
        }
    }
}