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
using Spla2n_Stuff.Helpers;

namespace Spla2n_Stuff.GearTypes {

    public class Weapon {

        public int ImageID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public SubWeapon WeaponSub { get; set; }
        public Special WeaponSpecial { get; set; }
        public string Stats { get; set; }

        public Weapon(int id, string name, string stats) {
            this.ID = id;
            this.Name = name;
            this.Stats = stats;

            ImageID = ImageHelper.GetImageId(this.Name);
        }
    }
}