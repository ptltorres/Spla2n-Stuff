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

namespace Spla2n_Stuff.News {

    [Serializable]
    public class NewsArticle {

        public string NID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageURI { get; set; }
        public string CreationDate { get; set; }
        public string Source { get; set; }

        public string MetaData() => $"Date: {CreationDate}\nSource: {Source}";

        public override string ToString() {
            return $"ID: {NID}\nTitle: {Title}\nContent: {Content}";
        }

    }
}