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
using Mono.Data.Sqlite;
using SQLitePCL;
using Spla2n_Stuff.GearTypes;
using System.IO;
using Android.Util;

namespace Spla2n_Stuff.Helpers
{
    public static class DatabaseHelper 
    {
        private static bool IsDbClose = true;

        private static string Tag = "DatabaseHelper";
        private static string DbName = "Spla2n.db";
        private static string DbPath;

        private static SqliteConnection database; //db connection
        private static SqliteCommand com;

        // List of gear vars
        private static List<Ability> abilities;
        private static List<Brand> brands;
        private static List<SubWeapon> subWeapons;
        private static List<Special> specials;
        private static List<Weapon> weapons;


        static DatabaseHelper() {
            DbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), DbName);

            try {
                database = new SqliteConnection("Data Source=" + DbPath);
            } catch (SqliteException e) {
                Log.Debug(Tag,"Error: " + e);
            }
        }

        public static List<Ability> GetAbilities() {
            abilities = new List<Ability>();
            string sql = "SELECT _id,Name,Description FROM Abilities";
            com = new SqliteCommand(sql, database);

            if (IsDbClose) {
                database.Open();
                IsDbClose = false;
            }

            com.ExecuteNonQuery();

            using (SqliteDataReader reader = com.ExecuteReader()) {
                while (reader.Read()) {
                    abilities.Add(new Ability (
                        (int)reader["_id"],
                        (string)reader["Name"],
                        (string)reader["Description"]));
                }
            }

            if (!IsDbClose) {
                database.Close();
                IsDbClose = true;
            }

            abilities.Sort((x, y) => x.Name.CompareTo(y.Name));

            return abilities;
        }

        public static List<Brand> GetBrands() {
            List<Ability> abilities = GetAbilities(); // temp variable to compare IDs
            string sql = "SELECT _id,Name,CAbility1,UAbility1 FROM Brands";
            com = new SqliteCommand(sql, database);

            brands = new List<Brand>();
            Ability CAbilityName;
            Ability UAbilityName; 
            int CAbilityID, UAbilityID;

            if (IsDbClose) {
                database.Open();
                IsDbClose = false;
            }
            com.ExecuteNonQuery();

            using (SqliteDataReader reader = com.ExecuteReader()) {
                while (reader.Read()) {
                    CAbilityName = null;
                    UAbilityName = null;

                   if (!(reader.IsDBNull(reader.GetOrdinal("CAbility1")) || reader.IsDBNull(reader.GetOrdinal("UAbility1")))) { // If neither column is null
                        CAbilityID = (int)reader["CAbility1"];
                        UAbilityID = (int)reader["UAbility1"];

                        // compares FKs between Abilities and Brands to get the Common/Uncommon Ability
                        foreach (var n in abilities) {
                            if (n.ID == CAbilityID)
                                CAbilityName = n;
                            if (n.ID == UAbilityID)
                                UAbilityName = n;
                        }
                    }
                    brands.Add(new Brand (
                        (int)reader["_id"],
                        (string)reader["Name"],
                        CAbilityName,
                        UAbilityName));
                }
            }
            if (!IsDbClose) {
                database.Close();
                IsDbClose = true;
            }
            brands.Sort((x, y) => x.Name.CompareTo(y.Name));

            return brands;
        }

        public static List<SubWeapon> GetSubWeapons() {
            string sql = "SELECT _id, Name, Description FROM SubWeapons";
            com = new SqliteCommand(sql, database);

            if (IsDbClose) {
                database.Open();
                IsDbClose = false;
            }
            com.ExecuteNonQuery();
            subWeapons = new List<SubWeapon>();

            using (SqliteDataReader reader = com.ExecuteReader()) {
                while (reader.Read()) {
                    subWeapons.Add(new SubWeapon (
                        (int)reader["_id"],
                        (string)reader["Name"],
                        (string)reader["Description"]
                    ));
                }
            }
            if (!IsDbClose) {
                database.Close();
                IsDbClose = true;
            }
            subWeapons.Sort((x, y) => x.Name.CompareTo(y.Name));

            return subWeapons;
        }

        public static List<Special> GetSpecials() {
            string sql = "SELECT _id, Name, Description FROM Specials";
            com = new SqliteCommand(sql, database);

            if (IsDbClose) {
                database.Open();
                IsDbClose = false;
            }
            com.ExecuteNonQuery();
            specials = new List<Special>();

            using (SqliteDataReader reader = com.ExecuteReader()) {
                while (reader.Read()) {
                    specials.Add(new Special (
                        (int)reader["_id"],
                        (string)reader["Name"],
                        (string)reader["Description"]
                    ));
                }
            }
            if (!IsDbClose) {
                database.Close();
                IsDbClose = true;
            }
            specials.Sort((x, y) => x.Name.CompareTo(y.Name));

            return specials;
        }

        public static List<Weapon> GetWeapons() {
            List<SubWeapon> tempSubs = GetSubWeapons();
            List<Special> tempSpecials = GetSpecials();

            string sql = "SELECT _id, Name,SubWeapon,Special,Stats FROM Weapons";
            com = new SqliteCommand(sql, database);

            weapons = new List<Weapon>();

            if (IsDbClose) {
                database.Open();
                IsDbClose = false;
            }
            com.ExecuteNonQuery();

            using (SqliteDataReader reader = com.ExecuteReader()) {
                while (reader.Read()) {
                    weapons.Add(new Weapon (
                        (int)reader["_id"],
                        (string)reader["Name"],
                        (string)reader["Stats"]
                    ));

                    foreach (var sub in tempSubs) {
                        if (sub.ID == (int)reader["SubWeapon"])
                            weapons[weapons.Count - 1].WeaponSub = sub;
                    }

                    foreach (var sp in tempSpecials) {
                        if (sp.ID == (int)reader["Special"])
                            weapons[weapons.Count - 1].WeaponSpecial = sp;
                    }
                }
            }
            if (!IsDbClose) {
                database.Close();
                IsDbClose = true;
            }
            weapons.Sort((x, y) => x.Name.CompareTo(y.Name));

            return weapons;
        }

        public static List<Gear> GetGear<T>() where T : Gear {
            List<Ability> tempAbilities = GetAbilities(); // temp variables to later compare its properties
            List<Brand> tempBrands = GetBrands();

            string tableName = "";

            if (typeof(T) == typeof(Headgear)) {
                tableName = "Headgears";
            } else if (typeof(T) == typeof(Clothe)) {
                tableName = "Clothes";
            } else if (typeof(T) == typeof(Shoe)) {
                tableName = "Shoes";
            }

            string sql = "Select _id,Brand,Name,Ability1,Rarity FROM " + tableName;
            com = new SqliteCommand(sql, database);

            List<Gear> gears = new List<Gear>();

            if (IsDbClose) {
                database.Open();
                IsDbClose = false;
            }
            com.ExecuteNonQuery();

            using (SqliteDataReader reader = com.ExecuteReader()) {
                while (reader.Read()) {
                    if (typeof(T) == typeof(Headgear)) {
                        gears.Add(new Headgear (
                            (int)reader["_id"],
                            (string)reader["Name"],
                            (string)reader["Rarity"]
                        ));
                    } else if (typeof(T) == typeof(Clothe)) {
                        gears.Add(new Clothe (
                            (int)reader["_id"],
                            (string)reader["Name"],
                            (string)reader["Rarity"]
                        ));
                    } else if (typeof(T) == typeof(Shoe)) {
                        gears.Add(new Shoe (
                            (int)reader["_id"],
                            (string)reader["Name"],
                            (string)reader["Rarity"]
                        ));
                    }

                    foreach (var b in tempBrands) {
                        if (b.ID == (int)reader["Brand"])
                            gears[gears.Count - 1].GearBrand = b;
                    }

                    if (!(reader.IsDBNull(reader.GetOrdinal("Ability1")))) { // If gear has no ability, skip
                        foreach (var a in tempAbilities) {
                            if (a.ID == (int)reader["Ability1"])
                                gears[gears.Count - 1].GearAbility = a;
                        }
                    }
                }
            }
            if (!IsDbClose) {
                database.Close();
                IsDbClose = true;
            }
            gears.Sort((x, y) => x.Name.CompareTo(y.Name));

            return gears;
        }
    }
}   