
export interface StatisticsDto {
    monthlyProfit: StatisticsType;
    accumulativeProfit: StatisticsType;
    marketShareRevenueBased: StatisticsType;
    marketShareRoomSoldBased: StatisticsType;
    occupancy: StatisticsType;
    revpar: StatisticsType;
}

export interface StatisticsType{
    format:string,
    value:number
}