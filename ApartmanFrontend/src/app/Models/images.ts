export interface ApiResponse {
    status: string;
    message: string;
    timeStamp: string;
    model: null;
    models: Array<{ properties: Image }>;
  }
  
  export interface Image {
    Id: number;
    Url: string;
  }