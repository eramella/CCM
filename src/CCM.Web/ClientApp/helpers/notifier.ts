import * as toastr from 'toastr';
import 'toastr/build/toastr.css';

export class Notifier {
    constructor() {
        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-bottom-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "100",
            "hideDuration": "1500",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
    }

    private errorOptions = {
        "hideDuration": "1500",
        "timeOut": "5000"
    }

    private otherOptions = {
        "hideDuration": "200",
        "timeOut": "3000"        
    }

    success(message, title?, options?) {
        let newOptions = this.otherOptions;
        if (options) {
            Object.assign(newOptions, options);
        }
        toastr.success(message, title, newOptions);
    }

    error(message, title?, options?) {
        let newOptions = this.errorOptions;
        if (options) {
            Object.assign(newOptions, options);
        }
        toastr.error(message, title, newOptions);
    }

    info(message, title?, options?) {
        let newOptions = this.otherOptions;
        if (options) {
            Object.assign(newOptions, options);
        }
        toastr.info(message, title, newOptions);
    }

    warning(message, title?, options?) {
        let newOptions = this.otherOptions;
        if (options) {
            Object.assign(newOptions, options);
        }
        toastr.warning(message, title, newOptions);
    }
}