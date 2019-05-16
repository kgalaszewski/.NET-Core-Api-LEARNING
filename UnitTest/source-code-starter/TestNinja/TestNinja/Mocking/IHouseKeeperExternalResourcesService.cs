using System;
using System.Linq;

namespace TestNinja.Mocking
{
    public interface IHouseKeeperExternalResourcesService // should be 2 diffrent classes instead : EmailSender and StatementSaver, a zamiast query
        // w tym moejscu lepiej zrobic interfajce z unitOfWork bezposrednio, robiąc tak jak tutaj dziala ale single responsibility ssie :)
    {
        void EmailFile(Housekeeper housekeeper, string filename, string subject);

        string SaveStatement(Housekeeper housekeeper, DateTime statementDate);

        IQueryable<T> Query<T>();
    }
}