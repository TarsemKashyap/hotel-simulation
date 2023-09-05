import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'ValueFormatterPipe'
  })
  export class ValueFormatterPipe implements PipeTransform {
  
    transform(value: number, formatter : string): string {
      switch(formatter)  {
          case('P'): {
              return value.toString() + ' %';
              break;
          }
          case('C0')  : case('C') :{
            return '$' + value.toString();
            break;
          }
          default: {
            return '0';
          }
      }
      
    }
}