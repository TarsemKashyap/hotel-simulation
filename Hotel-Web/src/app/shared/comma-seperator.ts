import { DecimalPipe } from '@angular/common';
import {
  Directive,
  ElementRef,
  HostListener,
  OnChanges,
  OnInit,
  Renderer2,
  SimpleChange,
  SimpleChanges,
} from '@angular/core';
import { AbstractControl, NgControl } from '@angular/forms';
import { map, Subscription } from 'rxjs';
import { Utility } from './utility';

@Directive({
  selector: '[numfmt]',
})
export class NumberCommaSeparatorDirective implements OnInit {
  constructor(private el: ElementRef, private renderer: Renderer2) {}

  ngOnInit(): void {}

  @HostListener('input', ['$event.target.value'])
  onInput(value: string) {
    const formattedValue = Utility.formatNumberWithComma(value);
    this.renderer.setProperty(this.el.nativeElement, 'value', formattedValue);
  }
}
