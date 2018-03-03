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

namespace Spla2n_Stuff.Maps {
    public class MapRotation {
        public string GameMode { get; set; }
        public string Time { get; set; }
        public Map[] Maps { get; set; }

        public override string ToString() {
            return GameMode + ": {" + Maps[0].Name + ", " + Maps[1].Name+ "}";
        }
    }
}