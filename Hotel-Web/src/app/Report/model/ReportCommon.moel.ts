export interface ReportCommon {
    liquidtyRatios: ReportAttribute,
    solvencyRatios: ReportAttribute,
    profitablityRation: ReportAttribute,
    turnOverRatio: ReportAttribute

}

export interface ReportAttribute {
    label: string,
    data: AbstractDecimal,
    childern: ReportAttribute[]
}

export interface AbstractDecimal {
    format: string,
    value: number,
}
export interface IncomeReport{
    revenue:revenue

}
export interface revenue{
    rooms:ReportAttribute,
    foodBeverage:ReportAttribute,
    otherOperatedDocs:ReportAttribute
    rentelOtherIncome:ReportAttribute,
    totalRevenue:ReportAttribute,
    
}
export interface departmentalExpenses{
    totalDepartmentalIncome:ReportAttribute		

}
export interface currentAssests{
currentAssests:ReportAttribute
cash:ReportAttribute,
accountReceivables:ReportAttribute,
inventories:ReportAttribute,
total:ReportAttribute
}
   
export interface propertyEquipement{
    lessAccumlatedDepreciation:ReportAttribute,
    netPropertyAndEquipment:ReportAttribute,
    propertyAndEquipment:ReportAttribute
}
export interface libilitiesAndOwnerEquity1{
    libilitiesAndOwnerEquity:ReportAttribute ,
    currentLibalities:ReportAttribute,
    emergencyLoan:ReportAttribute,
    longTermDebt:ReportAttribute,
    totalLibbalities:ReportAttribute
}
export interface IoccupancyBySegment{
    segmentTitle:string,
    segments:occupancyReportAttribute[]
}
export interface occupancyReportAttribute {
    label: string,
    hotel: AbstractDecimal,
    index:Number,
    marketAverage: AbstractDecimal
}