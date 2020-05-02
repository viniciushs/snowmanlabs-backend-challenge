using System;
using System.Collections.Generic;
using System.Text;

namespace SnowmanLabsChallenge.Infra.CrossCutting.Core.Interfaces
{
    public interface IFileService
    {
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
        void Upload(string base64, string azurePath);

        /// <summary>
        ///     Deletes an Azure file.
        /// </summary>
        /// <param name="azurePath">
        ///     The Azure path to delete the file.
        /// </param>
        void Remove(string azurePath);
    }
}
