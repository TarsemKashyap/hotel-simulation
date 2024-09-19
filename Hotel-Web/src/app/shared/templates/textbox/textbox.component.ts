import { Component, forwardRef, HostBinding, Input, OnDestroy, OnInit } from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  FormControl,
  NG_VALUE_ACCESSOR,
  NgControl,
} from '@angular/forms';
import { Observable, Subscription } from 'rxjs';
import { Utility } from '../../utility';

@Component({
  selector: 'textbox',
  templateUrl: './textbox.component.html',
  styleUrls: ['./textbox.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TextboxComponent),
      multi: true,
    },
  ],
})
export class TextboxComponent implements ControlValueAccessor, OnInit {
  disabled: boolean = false;
  constructor() {}

  formattedValue: string;
  private onChange!: Function;
  private onTouched!: Function;
  set value(val: any) {
    console.log('Setvalue', val);
    // this.writeValue(val);
  }

  ngOnInit(): void {}

  valueChange() {
    this.onTouched();
    this.onChange(Utility.formatNumber(this.formattedValue));
  }

  writeValue(obj: any): void {
    this.formattedValue = Utility.formatNumberWithComma(obj);
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }
}
