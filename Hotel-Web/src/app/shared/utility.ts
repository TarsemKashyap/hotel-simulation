import { formatCurrency, formatPercent } from '@angular/common';

export class Utility {
  public static copyToClipboard(val: any) {
    const selBox = document.createElement('textarea');
    selBox.style.position = 'fixed';
    selBox.style.left = '0';
    selBox.style.top = '0';
    selBox.style.opacity = '0';
    selBox.value = val;
    document.body.appendChild(selBox);
    selBox.focus();
    selBox.select();
    document.execCommand('copy');
    document.body.removeChild(selBox);
  }

  public static ToCurrency(
    value: number | any,
    digitInfo: string = '1.0-0'
  ): string {
    return formatCurrency(value, 'en-US', '$', undefined, digitInfo);
  }

  public static ToPercent(value: number | any) {
    return formatPercent(value, 'en-US', '1.2-2');
  }

  public static formatNumberWithComma(value: string): string {
    if (value === '') {
      return '';
    }

    const number = parseFloat(value.replace(/,/g, ''));

    if (isNaN(number)) {
      return '';
    }

    return number.toLocaleString('en-US');
  }

  public static formatNumber(value: string): number {
    if (value === '') {
      return 0;
    }

    const number = parseFloat(value.replace(/,/g, ''));

    if (isNaN(number)) {
      return 0;
    }

    return parseFloat(number.toString());
  }
}

export const ChartConfig = {
  BarThickness: 0.7,
  LineBgColor: 'red',
  BarBgColor: 'skyblue',
  LineBorderColor: 'red',
};
