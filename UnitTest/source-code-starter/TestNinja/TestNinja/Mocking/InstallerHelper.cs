using System.Net;

namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        private string _setupDestinationFile;

        private IFileDownloader _fileDownloader;

        public InstallerHelper(IFileDownloader fileDownloader = null)
        {
            _fileDownloader = fileDownloader ?? new FileDownloader();
        }

        public bool DownloadInstaller(string customerName, string installerName)
        {            
            try
            {
                _fileDownloader.DownloadClient(customerName, installerName, _setupDestinationFile);

                return true;
            }
            catch (WebException)
            {
                return false; 
            }
        }
    }
} 