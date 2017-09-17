import {
    bindable,
    bindingMode,
    inlineView,
    customElement,
    Container
} from 'aurelia-framework';
import * as Quill from 'quill';
import 'quill/dist/quill.snow.css';
import 'quill/dist/quill.bubble.css';


//@customElement('quill-editor')
export class QuillEditorCustomElement {
    public quillEditor: HTMLElement;
    @bindable({ defaultBindingMode: bindingMode.twoWay }) value;
    @bindable options; // per instance options

    editor: Quill;

    private defaultConfig = {
        modules: {
            toolbar: [
                [{ header: [1, 2, false] }],
                ['bold', 'italic', 'underline'],
                ['image', 'code-block']
            ]
        },
        theme: 'snow'
    };

    bind() {
        this.options = Object.assign({}, this.defaultConfig, this.options);
    }

    attached() {
        // initialize a new instance of the Quill editor
        // with the supplied options
        this.editor = new Quill(this.quillEditor, this.options);
        if (this.value) {
            this.editor.root.innerHTML = this.value;
        }
        // listen for changes and update the value
        this.editor.on('text-change', this.onTextChanged);
    }

    onTextChanged = () => {
        this.value = this.editor.root.innerHTML;
    }

    detached() {
        // clean up
        this.editor.off('text-change', this.onTextChanged);
        this.editor = null;
    }
}