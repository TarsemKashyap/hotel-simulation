export interface Segment {
  title: string;
  value: string;
}

export class SeedData {
  public static SegmentList(): Segment[] {
    return [
      { value: 'Overall', title: 'Overall' },
      { value: 'Business', title: 'Business' },
      { value: 'Small Business', title: 'Small Business' },
      { value: 'Corporate contract', title: 'Corporate contract' },
      { value: 'Families', title: 'Families' },
      { value: 'Afluent Mature Travelers', title: 'Afluent Mature Travelers' },
      {
        value: 'International leisure travelers',
        title: 'International Leisure Travelers',
      },
      {
        value: 'Corporate/Business Meetings',
        title: 'Corporate/Business Meetings',
      },
      { value: 'Association Meetings', title: 'Association Meetings' },
    ];
  }
}
