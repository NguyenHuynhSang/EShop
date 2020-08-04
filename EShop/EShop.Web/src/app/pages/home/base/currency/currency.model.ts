export default interface Currency {
  id: number;
  name: string;
  code: string;
  symbol: string;
  rate: number;
  lastUpdate: Date;
}
