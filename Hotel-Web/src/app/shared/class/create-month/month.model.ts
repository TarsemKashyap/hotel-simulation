export interface MonthDto {
  monthId: string;
  classId: string;
  sequence: string;
  totalMarket: string;
  isComplete: boolean;
  configId: string;
  status: string;
}
export interface ClassDto {
  classId: string;
  title: string;
  memo: string;
  startDate: string;
  endDate: string;
  hotelsCount: string;
  roomInEachHotel: string;
  currentQuater: string;
  createdOn: string;
  code: string;
  createdBy: string;
  status: string;
}

export enum ClassStatus {
  T = 'T',
  I = 'I',
  S = 'S',
  A = 'A',
  C = 'C',
}
