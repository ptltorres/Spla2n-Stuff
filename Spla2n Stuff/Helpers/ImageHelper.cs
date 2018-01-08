using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Spla2n_Stuff.Helpers
{
    public static class ImageHelper
    {
        public static string tag = "ImageHelper";

        public static int GetImageId(string imageName) {
            if (Char.IsDigit(imageName[0])) {
                imageName = "_" + imageName;
            }

            imageName = imageName.Replace(" ", "_").Replace(".", "_").Replace("'", "_").Replace("-","_")
                                .Replace("(", "_").Replace(")", "_").Replace("&", "_").ToLower();

            Log.Debug(tag, "Image name: " + imageName);
           return (int)typeof(Resource.Drawable).GetField(imageName).GetValue(null);
        }
    }
}