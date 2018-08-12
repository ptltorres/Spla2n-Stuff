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

namespace Spla2n_Stuff.GearTypes {

    public abstract class Gear {

        public int ImageID {get;set;}
        public int ID { get; set; }
        public Brand GearBrand { get; set; }
        public string Name { get; set; }
        public Ability GearAbility { get; set; }
        public string Rarity { get; set; }
    }
}