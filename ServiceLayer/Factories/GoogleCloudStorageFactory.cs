using DataAccessLayer.Interfaces;
using DataModelLayer.Models;
using Google.Cloud.Storage.V1;
using ServiceLayer.ServiceInterfaces;
using System.IO;

namespace ServiceLayer.Factories
{
    public static class GoogleCloudStorageService
    {
        private static readonly string _bucketName = "am-images-dobotanya";

        public static string UploadImage(Stream imageStream, string imageName)
        {
            string basePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
            string credentialsFolderPath = Path.Combine(basePath, "ServiceLayer", "Credentials");
            string credentialFilePath = Path.Combine(credentialsFolderPath, "secret-tempest-380809-43f6a62e5c87.json");

            var storageClientBuilder = new StorageClientBuilder
            {
                CredentialsPath = credentialFilePath
            };
            var storageClient = storageClientBuilder.Build();

            // Kép feltöltése a Google Cloud Storage-be
            storageClient.UploadObject(_bucketName, imageName, "image/jpeg", imageStream);

            // URL generálása a feltöltött képhez
            string imageUrl = $"https://storage.googleapis.com/{_bucketName}/{imageName}";
            return imageUrl;
        }
    }
}
