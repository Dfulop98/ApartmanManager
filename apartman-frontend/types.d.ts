export interface Window {
    ethereum ?: ethers.providers.ExternalProvider;
}
export type Params = {
    params:{
        roomId: string;
    }
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