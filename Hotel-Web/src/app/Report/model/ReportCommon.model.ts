export interface ReportCommon {
  hotelName: string;
  liquidtyRatios: ReportAttribute;
  solvencyRatios: ReportAttribute;
  profitablityRation: ReportAttribute;
  turnOverRatio: ReportAttribute;
}

export interface ReportAttribute {
  label: string;
  data: AbstractDecimal;
  childern: ReportAttribute[];
}

export interface AbstractDecimal {
  format: string;
  value: number;
}
export interface IncomeReport {
  revenue: revenue;
}
export interface revenue {
  rooms: ReportAttribute;
  foodBeverage: ReportAttribute;
  otherOperatedDocs: ReportAttribute;
  rentelOtherIncome: ReportAttribute;
  totalRevenue: ReportAttribute;
}
export interface departmentalExpenses {
  totalDepartmentalIncome: ReportAttribute;
}
export interface currentAssests {
  currentAssests: ReportAttribute;
  cash: ReportAttribute;
  accountReceivables: ReportAttribute;
  inventories: ReportAttribute;
  total: ReportAttribute;
}

export interface propertyEquipement {
  lessAccumlatedDepreciation: ReportAttribute;
  netPropertyAndEquipment: ReportAttribute;
  propertyAndEquipment: ReportAttribute;
}
export interface libilitiesAndOwnerEquity1 {
  libilitiesAndOwnerEquity: ReportAttribute;
  currentLibalities: ReportAttribute;
  emergencyLoan: ReportAttribute;
  longTermDebt: ReportAttribute;
  totalLibbalities: ReportAttribute;
}
export interface IoccupancyBySegment {
  segmentTitle: string;
  segments: occupancyReportAttribute[];
}
export interface occupancyReportAttribute {
  label: string;
  hotel: AbstractDecimal;
  index: number;
  marketAverage: AbstractDecimal;
}
export interface avgdailyrateReportAttribute {
  label: string;
  hotel: AbstractDecimal;
  index: number;
  marketAvg: AbstractDecimal;
}
export interface roomRateReportAttribute {
  label: string;
  weekDayRoomSold: AbstractDecimal;
  weekdayCost: Number;
  weekdayRate: AbstractDecimal;
  weekendCost: AbstractDecimal;
  weekendRate: AbstractDecimal;
  weekendRoomSold: AbstractDecimal;
  totalCost:Number;
}
export interface possitionAloneReportAttribute {
  label: string;
  actualMarketShare: AbstractDecimal;
  marketSharePosition: AbstractDecimal;
}
export interface AttributeAmentitesReportAttribute {
  label: string;
  accumulatedCapital: AbstractDecimal;
  laborBudget: AbstractDecimal;
  newCaptial: AbstractDecimal;
  operationBudget: AbstractDecimal;
}
export interface MarketExpenditureReportAttribute {
  label: string;
  labor: MarketExpenditureAttribute;
  other: MarketExpenditureAttribute;
  publicRelations: AbstractDecimal;
  salesForce: AbstractDecimal;
  total: AbstractDecimal;
}
export interface MarketExpenditureAttribute {
  label: string;
  advertising: AbstractDecimal;
  promotions: AbstractDecimal;
  publicRelations: AbstractDecimal;
  salesForce: AbstractDecimal;
  total: AbstractDecimal;
}
export interface PositionMapAttribute {
  classGroup: string;
  qualityRating: AbstractDecimal;
  roomRate: AbstractDecimal;
}
export interface QualityRatingAttribute {
  label: string;
  hotel: AbstractDecimal;
  marketAverage: AbstractDecimal;
}
export interface Sector {
  name: string;
  value: string;
}
export interface MarketingStrategy {
  name: string;
  value: string;
}
