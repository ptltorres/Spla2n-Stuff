﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Spla2n_Stuff.Maps;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using ModernHttpClient;

namespace Spla2n_Stuff.Helpers {

    public class MapRotationHelper {

        private static string URL = "https://splatoon.ink/schedule2.json";

        static string TAG = "MapRotationHelper";

        private const string regular = "regular";
        private const string ranked = "gachi";
        private const string league = "league";

        public static async Task<List<MapRotation>> GetMapRotationAsync() {
            JObject json = await GetJSONDataAsync();

            List<MapRotation> mapRotation = new List<MapRotation> {
                GetRotationForBattleType(json, regular),
                GetRotationForBattleType(json, ranked),
                GetRotationForBattleType(json, league)
            };

            return mapRotation;
        }


        private static async Task<JObject> GetJSONDataAsync() {
            Android.Util.Log.Debug(TAG, "Getting JSON data");
            HttpClient client = new HttpClient(new Xamarin.Android.Net.AndroidClientHandler());

            string data = await client.GetStringAsync(URL);

            return JObject.Parse(data);
        }

        private static JObject GetJSONData() {
            string data = "";

            using (HttpClient client = new HttpClient(new Xamarin.Android.Net.AndroidClientHandler())) {
                using (HttpResponseMessage response = client.GetAsync(URL).Result) {
                    using (HttpContent content = response.Content) {
                        data = content.ReadAsStringAsync().Result;
                    }
                }
            }
            return JObject.Parse(data);
        }

        private static MapRotation GetRotationForBattleType(JObject json, string mode) {
            DateTime local = DateTime.Now;
            DateTime utc = local.ToUniversalTime();

            var rotations = json["modes"][mode];

            foreach (var rot in rotations) {
                TimeSpan start = TimeSpan.FromSeconds((double)rot["startTime"]);

                if (TimeSpan.FromHours(utc.Hour) + TimeSpan.FromMinutes(utc.Minute) > 
                    TimeSpan.FromHours(start.Hours) + TimeSpan.FromMinutes(start.Minutes)) {

                    return new MapRotation {
                        GameMode = rot["rule"]["name"].ToString(),
                        Maps = new Map[] {
                            new Map  {Name = rot["maps"][0].ToString()},
                            new Map  {Name = rot["maps"][1].ToString()}
                        }
                    };
                }
            }

            return null;
        }
    }
}