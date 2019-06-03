using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Firestore;

namespace UGAndroidCloud
{
    [Activity(Label = "WikiActivity")]
    public class WikiActivity : Activity
    {
        EditText AllInfo;
        Button BackButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.WikiLayout);
            string infoAboutAnimals = Intent.GetStringExtra("Dane" ?? "No animal info stored, Be the first one and add some");

          //  FindViewById<TextView>(Resource.Id.AllInfo) = "przekazane dane z bazy";
        }

        void ConnectViews()
        {
            //AllInfo = (EditText)FindViewById(Resource.Id.AllInfo);
            //BackButton = (Button)FindViewById(Resource.Id.BackButton);

            //WikiButton.Click += (s, e) => {
            //    Intent nextActivity = new Intent(this, typeof(WikiActivity));
            //    nextActivity.PutExtra("testoweDane", );
            //};
            //saveButton.Click += SaveButton_Click;
        }

        public FirebaseFirestore GetDataBase()
        {
            FirebaseFirestore database;

            var options = new FirebaseOptions.Builder().SetProjectId("TU MA BYC PROJECT_ID Z PLIKU GOOGLE-SERVICES.JSON")
                .SetApplicationId("to samo")
                .SetApiKey("CURRENT_KEY z tego samego pliku")
                .SetDatabaseUrl("FIREBASE_URL z tego samego pliku")
                .SetStorageBucket("STORAGE_BUCKET z tego samego").
                Build();

            var app = FirebaseApp.InitializeApp(this, options);
            database = FirebaseFirestore.GetInstance(app);

            return database;
        }
    }
}