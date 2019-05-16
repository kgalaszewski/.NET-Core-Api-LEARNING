using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    class FileDownloader : IFileDownloader
    {
        public void DownloadClient(string customerName, string installerName, string path)
        {
            var webClient = new WebClient();

            webClient.DownloadFile(
                    string.Format("http://example.com/{0}/{1}",
                        customerName,
                        installerName),
                    path);
        }
    }
}
