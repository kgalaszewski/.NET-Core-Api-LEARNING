using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotDonkeyApp_UG.Services
{
    public sealed class AnimalService
    {
        private static AnimalService ServiceInstance = null;
        // All supported animal types which are also avatar names
        // if you add more animals, add img with its name as well xd
        private readonly List<string> AnimalTypes = new List<string>() { "pies", "kot", "kon", "swinia", "papuga", "krab", "donkey", "niedzwiedz", "zyrafa" }; 

        public static AnimalService Instance
        {
            get
            {
                if (ServiceInstance == null)
                    ServiceInstance = new AnimalService();

                return ServiceInstance;
            }
        }

        #region Assigning Animal Type To New User
        public string SetAnimalType(string definedType)
        {
            var proceedAnimalType = ReplacePolishLetters(definedType);
            var recognizedAnimalType = TryToRecognizeAnimalType(proceedAnimalType);

            return recognizedAnimalType;
        }

        private string ReplacePolishLetters(string stringToProceed)
        {
            stringToProceed = stringToProceed.ToLower();

            stringToProceed.Replace('ą', 'a');
            stringToProceed.Replace('ć', 'c');
            stringToProceed.Replace('ę', 'e');
            stringToProceed.Replace('ł', 'l');
            stringToProceed.Replace('ń', 'n');
            stringToProceed.Replace('ó', 'o');
            stringToProceed.Replace('ś', 's');
            stringToProceed.Replace('ź', 'z');
            stringToProceed.Replace('ż', 'z');

            return stringToProceed;
        }

        private string TryToRecognizeAnimalType(string givenType)
        {
            foreach (var animalType in AnimalTypes)
            {
                if (givenType.Contains(animalType))
                    return animalType;
            }

            return "donkey"; // donkey will be assigned only if user animal is not supported :D
        }
        #endregion

        public List<int> ProceedUserIds(string ids)
        {
            if (!string.IsNullOrWhiteSpace(ids))
            {
                var idList = ids.Split('.');
                List<int> idsToReturn = new List<int>();

                foreach (var id in idList)
                {
                    if (!string.IsNullOrWhiteSpace(id))
                        idsToReturn.Add(Convert.ToInt32(id));
                }

                return idsToReturn;
            }
            return new List<int>();
        }
    }
}
