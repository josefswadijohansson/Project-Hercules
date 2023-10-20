using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ProjectHercules.Pages;

namespace ProjectHercules.Models
{
    public static class FoodDatabase
    {
        private static string pathToLivsmedelsDB = "LivsmedelsDB_202004061002(csv).csv";
        private static List<MealComponent> _mealComponentsDatabase = new List<MealComponent>();

        public async static void LoadData()
        {
            string loadedData = string.Empty;

            using var stream = await FileSystem.OpenAppPackageFileAsync(pathToLivsmedelsDB);
            using var reader = new StreamReader(stream);

            var contents = reader.ReadToEnd();

            loadedData = contents;

            string[] mealComponents = loadedData.Split(new char[] { '\n' });

            foreach(string mealComponent in mealComponents)
            {
                if(!string.IsNullOrEmpty(mealComponent) && !string.IsNullOrWhiteSpace(mealComponent))
                {
                    string adjustableMealComponentString = mealComponent;
                    string problematicName = string.Empty;
                    if (mealComponent[0] == '\"')
                    {
                        int nameIndex = 1;
                        List<char> chars = new List<char>();

                        for (int i = nameIndex; i < mealComponent.Length; i++)
                        {
                            char c = mealComponent[i];

                            if (c == '\"')
                            {
                                problematicName = new string(chars.ToArray());
                                adjustableMealComponentString = mealComponent.Remove(0, i + 2);
                                break;
                                //problematic name is finished searching
                            }
                            else
                            {
                                chars.Add(c);
                            }
                        }
                    }

                    List<string> mealComponentsValue = new List<string>(adjustableMealComponentString.Split(new char[] { ',' }));

                    string name = string.Empty;
                    if (problematicName == string.Empty)
                    {
                        name = mealComponentsValue[0];
                    }
                    else
                    {
                        name = problematicName;
                        mealComponentsValue.Insert(0, problematicName);
                    }

                    if (name == "Livsmedelsnamn" || string.IsNullOrEmpty(name) == true)
                    {
                        continue;
                    }

                    if (mealComponentsValue.Count >= 9)
                    {
                        float calories = float.Parse(mealComponentsValue[2], CultureInfo.InvariantCulture);
                        float fat = float.Parse(mealComponentsValue[4], CultureInfo.InvariantCulture);
                        float carbs = float.Parse(mealComponentsValue[3], CultureInfo.InvariantCulture);
                        float protein = float.Parse(mealComponentsValue[5], CultureInfo.InvariantCulture);
                        float salt = float.Parse(mealComponentsValue[8], CultureInfo.InvariantCulture);

                        MealComponent newMealComponent = new MealComponent(name, -1, calories, fat, carbs, protein, salt);

                        _mealComponentsDatabase.Add(newMealComponent);
                    }
                }
            }

            FoodIntake.LoadDataFromPrefences();
        }

        public static List<MealComponent> GetAllMealComponents() => _mealComponentsDatabase;

        public static MealComponent GetMealComponent(string name)
        {
            foreach(MealComponent mealComponent in _mealComponentsDatabase)
            {
                if (mealComponent.Name == name)
                {
                    return mealComponent;
                }
            }

            return null;
        }

        public static MealComponent[] GetMealComponents(string filterText)
        {
            if(string.IsNullOrWhiteSpace(filterText) == true)
            {
                return GetAllMealComponents().ToArray();
            }

            var mealComponents = _mealComponentsDatabase.Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.Contains(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();

            if(mealComponents != null)
            {
                return mealComponents.ToArray();
            }

            return null;
        }
    }
}
