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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using Android.Util;
using Spla2n_Stuff.News;
using System.Net.Http;

namespace Spla2n_Stuff.Helpers {
    public class NewsHelper {

        public interface INewsCallback {
            void OnNewsLoaded(List<NewsArticle> newsArticles);
        }

        private readonly static string URL = KeysHelper.NewsURL;

        static readonly string TAG = "NewsHelper";

        public static async Task GetNewsArticlesAsync(INewsCallback callback) {          
            JObject json = await GetJSONDataAsync();

            List<NewsArticle> newsList = GetNewsArticlesList(json);

            callback.OnNewsLoaded(newsList);         
        }

        private static async Task<JObject> GetJSONDataAsync() {
            HttpClient client = new HttpClient(new Xamarin.Android.Net.AndroidClientHandler());

            string data = await client.GetStringAsync(URL);

            return JObject.Parse(data);
        }

        private static List<NewsArticle> GetNewsArticlesList(JObject json) {
            List<NewsArticle> articles = new List<NewsArticle>();

            var rootNode = json["news"];

            foreach (var child in rootNode.Children()) {
                articles.Add(new NewsArticle {
                    NID          = (string)child.First["nID"],
                    Title        = (string)child.First["title"],
                    Content      = (string)child.First["content"],
                    ImageURI     = (string)child.First["imageURI"],
                    CreationDate = (string)child.First["creationDate"],
                    Source       = (string)child.First["source"]
                });              
            }
            articles.Reverse();
            return articles;
        }
    }
}