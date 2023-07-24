export interface Window {
    // eslint-disable-next-line no-undef
    ethereum ?: ethers.providers.ExternalProvider;
}

export type ReservationDates ={
    [checkInDate: string]: number
    }

export type SlideImage = {
    src: string;
    width: number;
    height: number;
  };

export type ImageType = {
    "properties": {
        "Id": number,
        "Url": string
    }
}

export type Room = {
    "properties": {
    "Id": number,
    "RoomNumber": string,
    "Capacity": number,
    "IsAvailable": boolean,
    "PricePerNight": number,
    "Description": string,
    "Images": ImageType[]
    }
}

export type Day = {
    date: Date;
    dayOfWeek: number;
    isCurrentMonth: boolean;
    isSelected: boolean;
  };

export type FormValues = {
    firstName: string;
    lastName: string;
    nationality: string;
    country: string;
    postalCode: string;
    city: string;
    province: string;
    street: string;
    streetNumber: string;
    numberOfGuest: string;
    pets: boolean;
    description: string;
  
  }