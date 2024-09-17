import { Validators } from '@angular/forms';

export interface ClassSession {
  title: string;
  startDate: string;
  endDate: string;
  hotelsCount: number;
  roomInEachHotel: number;
  currentQuater: number;
  code: string;
  createdBy: string;
  groups: ClassGroup[];
  classId?: number;
  className?: string;
}

export interface ClassGroup {
  groupId?: number;
  serial: number;
  name: string;
  balance: number;
  action: Action;
}

export enum Action {
  Updated = 0,
  Added = 1,
  Removed = 2,
}

export interface ClassMapping {
  title: string;
}

export interface ClassSessionUpdate extends ClassSession {}

export interface AddRemoveClassDto {
  selectedClasses: Array<ClassSession>;
  availableClasses: Array<ClassSession>;
}

export interface ClassInformation extends ClassSession {
  totalStudentCount: number;
}

export interface RoomAllocations {
  id: number;
  monthID: number;
  quarterNo: number;
  groupID: number;
  weekday: boolean;
  segment: string;
  roomsAllocated: number;
  actualDemand: number;
  roomsSold: number;
  confirmed: boolean;
  revenue: number;
}

export interface RoomAllocationDetails {
  roomAllocation: RoomAllocations[];
  weekdayTotal: number;
  weekendTotal: number;
}

export interface AttributeDecision {
  iD: number;
  quarterNo: number;
  groupID: number;
  attribute: string;
  accumulatedCapital: number;
  newCapital: number;
  operationBudget: number;
  laborBudget: number;
  confirmed: boolean;
  quarterForecast: number;
  monthID: number;
}

export interface PriceDecision {
  ID: number;
  monthID: number;
  quarterNo: number;
  groupID: number;
  weekday: boolean;
  distributionChannel: string;
  segment: string;
  price: number;
  actualDemand: number;
  confirmed: boolean;
  priceNOFormat: string;
}

export interface MarketingDecision {
  iD: number;
  monthID: number;
  quarterNo: number;
  groupID: number;
  marketingTechniques: string;
  segment: string;
  spendingFormatN0: string;
  laborSpendingFormatN0: string;
  spending: number;
  laborSpending: number;
  actualDemand: number;
  confirmed: boolean;
}

export interface Goal {
  iD: number;
  monthID: number;
  quarterNo: number;
  groupID: number;
  occupancyM: number;
  occupancyY: number;
  roomRevenM: number;
  roomRevenY: number;
  totalRevenM: number;
  totalRevenY: number;
  shareRoomM: number;
  shareRoomY: number;
  shareRevenM: number;
  shareRevenY: number;
  revparM: number;
  revparY: number;
  adrm: number;
  aDRY: number;
  yieldMgtM: number;
  yieldMgtY: number;
  mgtEfficiencyM: number;
  mgtEfficiencyY: number;
  profitMarginM: number;
  profitMarginY: number;
}

export interface BalanceSheet {
  iD: number;
  monthID: number;
  quarterNo: number;
  groupID: number;
  cash: number;
  acctReceivable: number;
  inventories: number;
  totCurrentAsset: number;
  netPrptyEquip: number;
  totAsset: number;
  totCurrentLiab: number;
  longDebt: number;
  longDebtPay: number;
  shortDebt: number;
  shortDebtPay: number;
  totLiab: number;
  retainedEarn: number;
  longBorrow: number;
}

export const DecimalValidator = Validators.pattern('^(?:\\d*.\\d{1,12}|\\d+)$');
