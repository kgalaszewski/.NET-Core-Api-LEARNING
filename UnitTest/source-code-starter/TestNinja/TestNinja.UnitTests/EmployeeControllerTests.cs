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
    class EmployeeControllerTests
    {
        private Mock<IEmployeeStorage> _employeeStorage;
        private EmployeeController _employeeController;

        [SetUp]
        public void SetUp()
        {
            _employeeStorage = new Mock<IEmployeeStorage>();
            _employeeStorage.Setup(es => es.Db).Returns(new EmployeeContext());
            _employeeController = new EmployeeController(_employeeStorage.Object);
        }

        [Test]
        public void DeleteEmployee_WhenDeleted_ReturnsNewRedirectResult()
        {
            var result = _employeeController.DeleteEmployee(It.IsAny<int>());

            Assert.That(result, Is.TypeOf<RedirectResult>());
        }

        [Test]
        public void DeleteEmployee_WhenDeleted_DeleteTheEmployeeFromDb()
        {
            _employeeController.DeleteEmployee(1);

            _employeeStorage.Verify(es => es.DeleteEmployee(1));
        }
    }
}
