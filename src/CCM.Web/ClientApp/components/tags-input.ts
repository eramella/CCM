import { bindable, bindingMode, inject } from 'aurelia-framework';
import * as Bloodhound from 'typeahead.js/dist/bloodhound.js';
import 'typeahead.js';
import 'bootstrap-tagsinput/dist/bootstrap-tagsinput.js';
declare var $: any;

@inject(Element)
export class TagsInputCustomElement {
    @bindable({ defaultBindingMode: bindingMode.twoWay }) tags = [];
    @bindable options: TagInputOptions;
    @bindable placeholder = '';

    tagInput: HTMLElement;
    tagsInputElem: any;

    tagsnames: Bloodhound;
    customElement: Element;

    constructor(element) {
        this.customElement = element;
    }

    bind() {

    }
    attached() {
        this.tagsnames = new Bloodhound({
            initialize: true,
            datumTokenizer: Bloodhound.tokenizers.obj.whitespace(this.options.displayKey),
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            prefetch: this.options.url
        });

        this.tagsInputElem = $(this.tagInput);
        this.tagsInputElem.tagsinput({
            typeaheadjs: {
                name: 'tagsnames',
                displayKey: this.options.displayKey,
                valueKey: this.options.displayKey,
                source: this.tagsnames.ttAdapter()
            },
            trimValue: true
        });
        this.tagsInputElem.on('itemAdded', (e) => this.updateTags(e));
        this.tagsInputElem.on('itemRemoved', (e) => this.updateTags(e));
        let taginputWrapper = $(this.customElement).find('div.bootstrap-tagsinput');
        taginputWrapper.addClass('form-control');

    }

    detached() {
        if (this.tagsInputElem) {
            this.tagsInputElem.tagsinput('destroy');
        }
    }

    private addTags() {
        this.tagsInputElem.tagsinput('removeAll');
        for (let i of this.tags) {
            this.tagsInputElem.tagsinput('add', i[this.options.displayKey]);
        }
    }

    private updateTags(e: Event) {
        this.tags = this.tagsInputElem.tagsinput('items');        
    }
}

export interface TagInputOptions {
    url: string,
    displayKey: string,
    valueKey: string
}