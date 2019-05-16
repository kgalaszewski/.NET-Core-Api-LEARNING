using System.Net;

namespace TestNinja.Mocking
{
    public interface IFileDownloader
    {
        void DownloadClient(string customerName, string installerName, string path);
    }
}