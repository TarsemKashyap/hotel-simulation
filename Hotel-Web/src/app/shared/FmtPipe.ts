import { formatNumber } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'fmt',
})
export class FmtPipe implements PipeTransform {
  transform(data: { value: number; format: string }) {
    if (!data) {
      return '';
    }
    switch (data.format) {
      case 'P': {
        return `${formatNumber(data.value, 'en-US', '1.0-2')}%`;
      }
      case 'C0':
      case 'C': {
        return `$${formatNumber(data.value, 'en-US', '1.0-0')}`;
      }
      case 'N': {
        return `${formatNumber(data.value, 'en-US', '1.0-2')}`;
        // return (Math.round(data.value * 100) / 100).toFixed(2);
      }
      default: {
        return '0';
      }
    }
  }
}

@Pipe({
  name: 'fmt2',
})
export class Fmt2Pipe implements PipeTransform {
  transform(value: any, format: any) {
    switch (format) {
      case 'P': {
        return `${formatNumber(value * 100, 'en-US', '1.0-2')}%`;
      }
      case 'C0':
      case 'C': {
        return `$${formatNumber(value, 'en-US', '1.0-0')}`;
      }
      case 'N': {
        return `${formatNumber(value, 'en-US', '1.0-2')}`;
        // return (Math.round(data.value * 100) / 100).toFixed(2);
      }
      default: {
        return '0';
      }
    }
  }
}
