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
using Android.Content.Res;
using Spla2n_Stuff.Helpers;

namespace Spla2n_Stuff.GearTypes {

    public class Special {

        public int ImageID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Special(int id, string name, string desc) {
            this.ID = id;
            this.Name = name;
            this.Description = desc;

            ImageID = ImageHelper.GetImageId(this.Name);

        }
    }
}