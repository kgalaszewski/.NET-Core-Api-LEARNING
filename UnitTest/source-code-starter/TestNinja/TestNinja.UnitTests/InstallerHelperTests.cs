using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class InstallerHelperTests
    {
        private Mock<IFileDownloader> _fileDownloader;
        private InstallerHelper _installerHelper;

        [SetUp]
        public void SetUp()
        {
            _fileDownloader = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloader.Object);
        }

        [Test]
        public void DownloadInstaller_WhenDownloadedClient_ReturnsTrue()
        {
            _fileDownloader.Setup(fd => fd.DownloadClient(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            bool result = _installerHelper.DownloadInstaller("customerName", "installerName");

            Assert.That(result, Is.True);
        }

        [Test]
        public void DownloadInstaller_WhenWebExceptoinCaught_ReturnsFalse()
        {
            _fileDownloader.Setup(fd => fd.DownloadClient(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>();

            bool result = _installerHelper.DownloadInstaller("wrongCustomerName", "wrongInstallerName");

            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Sposob jak mozna wykorzystac, kiedy nie uzywamy It.IsAny, wtedy z takimi parametrami jakie sa w setupie rzuci exception ale z innymi 
        /// juz nie wyrzuci :P
        /// </summary>
        [Test]  
        public void DownloadInstaller_WhenCalled_ReturnsTrueIfPassedOrFalseIfCaughtException()
        {
            _fileDownloader.Setup(fd => fd.DownloadClient("wrongCustomerName", "wrongInstallerName", It.IsAny<string>())).Throws<WebException>();

            var result1 = _installerHelper.DownloadInstaller("wrongCustomerName", "wrongInstallerName");
            var result2 = _installerHelper.DownloadInstaller("CustomerName", "InstallerName");

            Assert.That(result1, Is.False);
            Assert.That(result2);
        }
    }
}
