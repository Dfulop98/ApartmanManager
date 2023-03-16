namespace ServiceLayer.ServiceInterfaces
{
    public interface IGoogleCloudStorageService
    {
        public string UploadImage(Stream imageStream, string imageName);
    }
}
