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