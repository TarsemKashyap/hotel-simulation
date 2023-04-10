import {
    AfterViewInit,
    Component,
    ViewChild,
    ViewContainerRef,
  } from '@angular/core';
  import { ICellEditorAngularComp } from 'ag-grid-angular';
  import { ICellEditorParams } from 'ag-grid-community';
  
  // backspace starts the editor on Windows
  const KEY_BACKSPACE = 'Backspace';
  const KEY_F2 = 'F2';
  const KEY_ENTER = 'Enter';
  const KEY_TAB = 'Tab';
  
  @Component({
    selector: 'textbox',
    templateUrl:'./textbox.editor.html'
  })
  export class TextboxEditor implements ICellEditorAngularComp, AfterViewInit {
    private params: any;
    public value!: string;
    public highlightAllOnFocus = true;
    private cancelBeforeStart = false;
  
    @ViewChild('input', { read: ViewContainerRef })
    public input!: ViewContainerRef;
  
    agInit(params: ICellEditorParams): void {
      this.params = params;
      this.setInitialState(this.params);
  
      // only start edit if key pressed is a number, not a letter
   
    }
  
    setInitialState(params: ICellEditorParams) {
      let startValue;
      let highlightAllOnFocus = true;
  
      if (params.eventKey === KEY_BACKSPACE) {
        // if backspace or delete pressed, we clear the cell
        startValue = '';
      } else if (params.charPress) {
        // if a letter was pressed, we start with the letter
        startValue = params.charPress;
        highlightAllOnFocus = false;
      } else {
        // otherwise we start with the current value
        startValue = params.value;
        if (params.eventKey === KEY_F2) {
          highlightAllOnFocus = false;
        }
      }
  
      this.value = startValue;
      this.highlightAllOnFocus = highlightAllOnFocus;
    }
  
    getValue(): any {
      return this.value;
    }
  
    isCancelBeforeStart(): boolean {
      return this.cancelBeforeStart;
    }
  
    // will reject the number if it greater than 1,000,000
    // not very practical, but demonstrates the method.
    isCancelAfterEnd(): boolean {
     return false;
    }
  
    onKeyDown(event: any): void {
    }
    
  
    // dont use afterGuiAttached for post gui events - hook into ngAfterViewInit instead for this
    ngAfterViewInit() {
      window.setTimeout(() => {
        this.input.element.nativeElement.focus();
        if (this.highlightAllOnFocus) {
          this.input.element.nativeElement.select();
  
          this.highlightAllOnFocus = false;
        } else {
          // when we started editing, we want the caret at the end, not the start.
          // this comes into play in two scenarios:
          //   a) when user hits F2
          //   b) when user hits a printable character
          const length = this.input.element.nativeElement.value
            ? this.input.element.nativeElement.value.length
            : 0;
          if (length > 0) {
            this.input.element.nativeElement.setSelectionRange(length, length);
          }
        }
  
        this.input.element.nativeElement.focus();
      });
    }
  
  
  
  }