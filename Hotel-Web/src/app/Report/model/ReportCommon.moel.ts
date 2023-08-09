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