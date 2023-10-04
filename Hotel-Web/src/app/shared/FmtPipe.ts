import { Pipe, PipeTransform } from '@angular/core';


@Pipe({
  name: 'fmt',
})
export class FmtPipe implements PipeTransform {
  transform(data: { value: number; format: string; }) {
    switch (data.format) {
      case 'P': {
        return data.value.toString() + ' %';
      }
      case 'C0':
      case 'C': {
        return '$' + data.value.toString();
      }
      case 'N': {
        return  (Math.round(data.value*100)/100).toFixed(2); 
      }
      default: {
        return '0';
      }
    }
  }
}
