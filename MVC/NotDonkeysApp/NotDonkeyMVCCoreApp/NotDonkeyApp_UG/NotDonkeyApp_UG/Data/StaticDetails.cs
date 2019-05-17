using NotDonkeyApp_UG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotDonkeyApp_UG.Data
{
    

 // @"\" + @"images\AnimalImages" + @"\" + ProductsVM.Products.Id + ".jpg";
    public static class StaticDetails
    {
        /// <summary>
        /// Currently logged user info
        /// </summary>
        public static int CurrentUserId { get; set; }

        /// <summary>
        /// AvatarUrl + AnimalType from AnimalNotDonkeyModel == path to img
        /// </summary>
        public static readonly string AvatarUrl = @"\images\AnimalImages\";
        public static readonly string FullPathToErrorImg = @"\images\AnimalImages\ErrorImg.jpg";

        /// <summary>
        /// List will be edited after you like some animal
        /// </summary>
        public static List<AnimalNotDonkey> DonkeysAvailableToLike = new List<AnimalNotDonkey>();

        /// <summary>
        /// Erorr MSG and AdditionalErrorInfo will be assigned and used when TryCatchError method inside AnimalNotDonkeysController catches an error
        /// </summary>
        public static string CurrentErrorMsg { get; set; }
        public static string AdditionalErrorInfo { get; set; }

        public static bool IsUserLogged = false;
    }
}
