import { bindable, bindingMode, inject } from 'aurelia-framework';

export class ImageUploadCustomElement {
    selectedFiles: any;
    fileName: string;
    @bindable id;
    @bindable({ defaultBindingMode: bindingMode.twoWay }) file;
    fileInput: HTMLInputElement;

    constructor() { }

    attached() {
        
    }

    updateFileName(target) {
        this.fileName = target.files[0].name;
        if (this.selectedFiles.length > 0) {
            this.file = this.selectedFiles[0];
        };
    }

    clear() {
        this.fileInput.value = '';
        this.fileName = '';
        this.file = null;
    }
}