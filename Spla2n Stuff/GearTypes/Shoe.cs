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

namespace Spla2n_Stuff.GearTypes
{
    public class Shoe : Gear
    {
        public Shoe(int id, string name, string rarity) {
            this.ID = id;
            this.Name = name;
            this.Rarity = rarity;

            ImageID = ImageHelper.GetImageId(this.Name);
        }
    }
}