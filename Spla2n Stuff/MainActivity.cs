using Android.App;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Spla2n_Stuff.GearTypes;
using Spla2n_Stuff.Helpers;
using System.Collections.Generic;
using Android.Content;

namespace Spla2n_Stuff
{
    [Activity(Label = "Spla2n Stuff", MainLauncher = true)]
    public class MainActivity : Activity
    {
        //DatabaseHelper dbHelper;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            CopyDatabase("Spla2n.db");

            Typeface tf = Typeface.CreateFromAsset(Assets, "Paintball.ttf");

            Button abilityButton = FindViewById<Button>(Resource.Id.abilityButton);
            Button brandsButton = FindViewById<Button>(Resource.Id.brandButton);
            Button headgearButton = FindViewById<Button>(Resource.Id.headgearButton);
            Button clothingButton = FindViewById<Button>(Resource.Id.clothingButton);
            Button shoesButton = FindViewById<Button>(Resource.Id.shoesButton);
            Button specialsButton = FindViewById<Button>(Resource.Id.specialsButton);
            Button weaponsButton = FindViewById<Button>(Resource.Id.weaponsButton);
            Button subButton = FindViewById<Button>(Resource.Id.subButton);

            Button[] buttons = { abilityButton, brandsButton, headgearButton, clothingButton,
                                 shoesButton, specialsButton,weaponsButton,subButton};

            foreach (var button in buttons) {
                button.SetTypeface(tf, TypefaceStyle.Normal);
            }

            abilityButton.Click += delegate {
                StartActivity(typeof(AbilityActivity));
            };

            brandsButton.Click += delegate {
                StartActivity(typeof(BrandActivity));
            };

            headgearButton.Click += delegate {
                //StartActivity(typeof());
            };

            clothingButton.Click += delegate {
                //StartActivity(typeof());
            };

            shoesButton.Click += delegate {
                //StartActivity(typeof());
            };

            specialsButton.Click += delegate {
                StartActivity(typeof(SpecialActivity));
            };

            weaponsButton.Click += delegate {
                StartActivity(typeof(WeaponActivity));
            };

            subButton.Click += delegate {
                StartActivity(typeof(SubWeaponActivity));
            };
        }

        private void CopyDatabase(string dataBaseName) {
            var dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dataBaseName);

            if (!System.IO.File.Exists(dbPath)) {
                var dbAssetStream = Assets.Open(dataBaseName);
                var dbFileStream = new System.IO.FileStream(dbPath, System.IO.FileMode.OpenOrCreate);
                var buffer = new byte[1024];

                int b = buffer.Length;
                int length;

                while ((length = dbAssetStream.Read(buffer, 0, b)) > 0) {
                    dbFileStream.Write(buffer, 0, length);
                }

                dbFileStream.Flush();
                dbFileStream.Close();
                dbAssetStream.Close();
            }
        }
    }
}

