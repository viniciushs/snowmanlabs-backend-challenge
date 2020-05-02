namespace SnowmanLabsChallenge.Infra.CrossCutting.Core.Services
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.WindowsAzure.Storage.Auth;
    using Microsoft.WindowsAzure.Storage.Blob;
    using SnowmanLabsChallenge.Infra.CrossCutting.Core.Interfaces;
    using System;
    using System.IO;

    public class FileService : IFileService
    {
        private string AccountName { get; set; }

        private string AccountKey { get; set; }

        private StorageCredentials Credential { get; set; }

        public FileService(IConfiguration configuration)
        {
            this.AccountName = configuration["AzureBlobAccountName"];
            this.AccountKey = configuration["AzureBlobAccountKey"];

            this.Credential = new StorageCredentials(this.AccountName, this.AccountKey);
        }

        public FileService(string accountName, string accountKey)
        {
            this.AccountName = accountName;
            this.AccountKey = accountKey;

            this.Credential = new StorageCredentials(this.AccountName, this.AccountKey);
        }

        /// <summary>
        ///     Uploads a base 64 file to an Azure path.
        /// </summary>
        /// <param name="base64">
        ///     File in Base64 format.
        /// </param>
        /// <param name="fileExtension">
        ///     The file extension
        /// </param>
        /// <param name="azurePath">
        ///     The destination Azure path to upload the file.
        /// </param>
        public void Upload(string base64, string azurePath)
        {
            var localPath = Path.GetTempPath() + Guid.NewGuid().ToString() + ".tmp";
            var bytes = Convert.FromBase64String(base64.Contains(',') ? base64.Split(',', StringSplitOptions.None)[1] : base64);
            File.WriteAllBytes(localPath, bytes);

            var blob = new CloudBlockBlob(new Uri(azurePath), this.Credential);
            blob.UploadFromFileAsync(localPath).Wait();

            if (File.Exists(localPath))
            {
                File.Delete(localPath);
            }
        }

        /// <summary>
        ///     Deletes an Azure file.
        /// </summary>
        /// <param name="azurePath">
        ///     The Azure path to delete the file.
        /// </param>
        public void Remove(string azurePath)
        {
            var blob = new CloudBlockBlob(new Uri(azurePath), this.Credential);
            blob.DeleteIfExistsAsync().Wait();
        }
    }
}
