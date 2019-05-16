using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class HouseKeeperServiceTests
    {
        private const string _filename = "fileName";
        private const string _statementEmailBody = "kgalaszewski";
        private const string _fullName = "KarolGalaszewski";
        private const string _email = ":)@o2.pl";
        private const int _oid = 1;

        private Mock<IHouseKeeperExternalResourcesService> _externalDependenciesService;
        private HouseKeeperService _service;
        private Housekeeper _housekeeperInstance;
        private Mock<IXtraMessageBox> _msgBoxService; 

        [SetUp]
        public void SetUp()
        {
            _externalDependenciesService = new Mock<IHouseKeeperExternalResourcesService>();

            _housekeeperInstance = new Housekeeper() { Email = _email, Oid = _oid, FullName = _fullName, StatementEmailBody = _statementEmailBody };

            _externalDependenciesService.Setup(ks => ks.Query<Housekeeper>()).Returns(new List<Housekeeper>() { _housekeeperInstance }.AsQueryable<Housekeeper>);

            _service = new HouseKeeperService(_externalDependenciesService.Object);

            _msgBoxService = new Mock<IXtraMessageBox>();
        }

        // Interaction Unit Tests ---------------------------------------------------------------------------------------------------------------------------------------

        [Test]  
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            InvokeSendStatementEmails();

            _externalDependenciesService.Verify(eds => eds.SaveStatement(_housekeeperInstance, It.IsAny<DateTime>()));
        }        

        [Test]
        public void SendStatementEmails_WhenCalled_SendsEmail()
        {
            _externalDependenciesService.Setup(eds => eds.SaveStatement(_housekeeperInstance, It.IsAny<DateTime>())).Returns(_filename);

            InvokeSendStatementEmails();

            _externalDependenciesService.Verify(eds => eds.EmailFile(_housekeeperInstance, _filename, It.IsAny<string>()));
        }

        [Test]
        public void SendStatementEmails_WhenCouldNotSendEmail_MsgBoxThrowsMsgWithException()
        {
            _externalDependenciesService.Setup(eds => eds.SaveStatement(_housekeeperInstance, It.IsAny<DateTime>())).Returns(_filename);

            _externalDependenciesService.Setup(eds => eds.EmailFile(_housekeeperInstance, _filename, It.IsAny<string>())).Throws<Exception>();

            InvokeSendStatementEmails();

            _msgBoxService.Verify(mbs => mbs.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }

        [Test]
        public void SendStatementEmails_WhenHousekeepersEmailsAreNull_ShouldNotGenerateStatements()
        {
            _housekeeperInstance.Email = null; // the _housekeeperInstance is returned in query

            InvokeSendStatementEmails();

            _externalDependenciesService.Verify(eds => eds.SaveStatement(_housekeeperInstance, It.IsAny<DateTime>()), Times.Never);
        }

        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_StatementFileNameIsNullOrWhiteSpace_ShouldNotSendEmail(string returnedValue)
        {
            _externalDependenciesService.Setup(eds => eds.SaveStatement(_housekeeperInstance, It.IsAny<DateTime>())).Returns(returnedValue);

            InvokeSendStatementEmails();

            _externalDependenciesService.Verify(eds => eds.EmailFile(_housekeeperInstance, It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        // Normal Unit Tests ---------------------------------------------------------------------------------------------------------------------------------------

        [Test]
        public void SendStatementEmails_WhenSuccessfull_ReturnTrue()
        {
            bool result = _service.SendStatementEmails(It.IsAny<DateTime>(), It.IsAny<IXtraMessageBox>());

            Assert.That(result == true);
        }

        [Test]
        public void SendStatementEmails_WhenExceptionCaughtWhileSendingEmail_ReturnTrue()
        {
            _externalDependenciesService.Setup(ks => ks.EmailFile(It.IsAny<Housekeeper>(), It.IsAny<string>(), It.IsAny<string>())).Throws<Exception>();
            _externalDependenciesService.Setup(ks => ks.SaveStatement(It.IsAny<Housekeeper>(), It.IsAny<DateTime>())).Returns(_statementEmailBody);

            bool result = _service.SendStatementEmails(It.IsAny<DateTime>(), new XtraMessageBox());
            // tu powyzej nie moze byc It.IsAny bo it is any deklaruje, ze przy wywolaniu moznaby dac cokolwiek A TU JUZ JEST WYWOLANIE
            // i mamy cos przekazac

            Assert.That(result == true);
        }

        [Test]
        public void SendStatementEmails_WhenExceptionCaughtWhileSavingStatement_ThrowsException()
        {
            _externalDependenciesService.Setup(ks => ks.SaveStatement(It.IsAny<Housekeeper>(), It.IsAny<DateTime>())).Throws<Exception>();

            Assert.That(() => {
                _service.SendStatementEmails(It.IsAny<DateTime>(), It.IsAny<IXtraMessageBox>());
            }, Throws.Exception);
        }

        [Test]
        public void SendStatementEmails_WhenExceptionCaughtWhileGettingQuery_ThrowsException()
        {
            _externalDependenciesService.Setup(ks => ks.Query<Housekeeper>()).Throws<Exception>();

            Assert.That(() => {
                _service.SendStatementEmails(It.IsAny<DateTime>(), It.IsAny<IXtraMessageBox>());
            }, Throws.Exception);
        }

        // private methods -------------------------------------------------------------------------------------------------------------------

        private void InvokeSendStatementEmails()
        {
            _service.SendStatementEmails(It.IsAny<DateTime>(), _msgBoxService.Object);
        }
    }
}
