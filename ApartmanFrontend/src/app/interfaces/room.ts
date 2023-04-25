export interface Room {
  properties: {
    Id: number;
    RoomNumber: string;
    Capacity: number;
    IsAvailable: boolean;
    PricePerNight: number;
    Description: string;
    Images: Array<{
      properties: {
        Url: string;
      };
    }>;
  };
}