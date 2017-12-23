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
    public class Brand
    {
        public int ImageID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Ability CommonAbility { get; set; }
        public Ability UncommonAbility { get; set; }

        public Brand(int id, string name, Ability CA, Ability UA) {
            this.ID = id;
            this.Name = name;
            this.CommonAbility = CA;
            this.UncommonAbility = UA;

            ImageID = ImageHelper.GetImageId(this.Name);
        }
    }
}