import { IValueFormat } from './PerformanceResponse.model';


export interface SummeryAllHotelsReport {
    hotelName: string;
    occupancyPercentage: IValueFormat;
    roomRevenue: IValueFormat;
    totalRevenue: IValueFormat;
    marketShareRoomsSold: IValueFormat;
    marketShareRevenue: IValueFormat;
    revpar: IValueFormat;
    adr: IValueFormat;
    yieldMgmt: IValueFormat;
    operatingEfficiencyRatio: IValueFormat;
    profitMargin: IValueFormat;
}
