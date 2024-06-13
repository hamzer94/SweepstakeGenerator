using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Reflection;
using static MudBlazor.CategoryTypes;

namespace SweepstakeGenerator.Components.Pages
{
    public partial class Sweepstake
    {
        private List<(int, string, string, int)> Results = new List<(int, string, string, int)>();
        private List<(int, int, string, string, int)> DisplayedResults = new List<(int, int, string, string, int)>();

        private int Index = 0;

        private static Random rng = new Random();

        private List<string> People = new List<string>()
        {
            "Hayden",
            "Sheps",
            "Mikey",
            "Neil"
        };

        private Dictionary<string, int> Pool1 = new Dictionary<string, int>
        {
            { "France", 1 },
            { "England", 2 },
            { "Portugal", 3 },
            { "Belgium", 4 }
        };
        private Dictionary<string, int> Pool2 = new Dictionary<string, int>
        {
            { "Germany", 5 },
            { "Spain", 6 },
            { "Netherlands", 7 },
            { "Italy", 8 }
        };
        private Dictionary<string, int> Pool3 = new Dictionary<string, int>
        {
            { "Croatia", 9 },
            { "Austria", 10 },
            { "Denmark", 11 },
            { "Hungary", 12 }
        };
        private Dictionary<string, int> Pool4 = new Dictionary<string, int>
        {
            { "Switzerland", 13 },
            { "Ukraine", 14 },
            { "Czechia", 15 },
            { "Serbia", 16 }
        };
        private Dictionary<string, int> Pool5 = new Dictionary<string, int>
        {
            { "Scotland", 17 },
            { "Turkey", 18 },
            { "Poland", 19 },
            { "Slovenia", 20 }
        };
        private Dictionary<string, int> Pool6 = new Dictionary<string, int>
        {
            { "Romania", 21 },
            { "Slovakia", 22 },
            { "Albania", 23 },
            { "Georgia", 24 }
        };

        private Dictionary<string, int> Pool1Remaining = new Dictionary<string, int>
        {
            { "France", 1 },
            { "England", 2 },
            { "Portugal", 3 },
            { "Belgium", 4 }
        };
        private Dictionary<string, int> Pool2Remaining = new Dictionary<string, int>
        {
            { "Germany", 5 },
            { "Spain", 6 },
            { "Netherlands", 7 },
            { "Italy", 8 }
        };
        private Dictionary<string, int> Pool3Remaining = new Dictionary<string, int>
        {
            { "Croatia", 9 },
            { "Austria", 10 },
            { "Denmark", 11 },
            { "Hungary", 12 }
        };
        private Dictionary<string, int> Pool4Remaining = new Dictionary<string, int>
        {
            { "Switzerland", 13 },
            { "Ukraine", 14 },
            { "Czechia", 15 },
            { "Serbia", 16 }
        };
        private Dictionary<string, int> Pool5Remaining = new Dictionary<string, int>
        {
            { "Scotland", 17 },
            { "Turkey", 18 },
            { "Poland", 19 },
            { "Slovenia", 20 }
        };
        private Dictionary<string, int> Pool6Remaining = new Dictionary<string, int>
        {
            { "Romania", 21 },
            { "Slovakia", 22 },
            { "Albania", 23 },
            { "Georgia", 24 }
        };

        public override Task SetParametersAsync(ParameterView parameters)
        {
            Randomise();

            return base.SetParametersAsync(parameters);
        }

        private void Randomise()
        {
            Results.Clear();
            DisplayedResults.Clear();
            Index = 0;

            Pool1Remaining = new Dictionary<string, int>();
            Pool2Remaining = new Dictionary<string, int>();
            Pool3Remaining = new Dictionary<string, int>();
            Pool4Remaining = new Dictionary<string, int>();
            Pool5Remaining = new Dictionary<string, int>();
            Pool6Remaining = new Dictionary<string, int>();

            AddRange(Pool1Remaining, Pool1);
            AddRange(Pool2Remaining, Pool2);
            AddRange(Pool3Remaining, Pool3);
            AddRange(Pool4Remaining, Pool4);
            AddRange(Pool5Remaining, Pool5);
            AddRange(Pool6Remaining, Pool6);

            Shuffle(People);
            Shuffle(Pool1);
            Shuffle(Pool2);
            Shuffle(Pool3);
            Shuffle(Pool4);
            Shuffle(Pool5);
            Shuffle(Pool6);

            var index = 0;
            foreach (var person in People)
            {
                Results.Add((1, Pool1.Keys.ElementAt(index), person, Pool1.Values.ElementAt(index)));
                Results.Add((2, Pool2.Keys.ElementAt(index), person, Pool2.Values.ElementAt(index)));
                Results.Add((3, Pool3.Keys.ElementAt(index), person, Pool3.Values.ElementAt(index)));
                Results.Add((4, Pool4.Keys.ElementAt(index), person, Pool4.Values.ElementAt(index)));
                Results.Add((5, Pool5.Keys.ElementAt(index), person, Pool5.Values.ElementAt(index)));
                Results.Add((6, Pool6.Keys.ElementAt(index), person, Pool6.Values.ElementAt(index)));

                index++;
            }

            Results = Results.OrderByDescending(r => r.Item1).ToList();
        }

        private static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void Shuffle(Dictionary<string, int> dictionary)
        {
            // Extract the dictionary entries into a list
            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>(dictionary);

            // Shuffle the list
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                KeyValuePair<string, int> value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            // Clear the dictionary and add the shuffled entries back
            dictionary.Clear();
            foreach (KeyValuePair<string, int> entry in list)
            {
                dictionary[entry.Key] = entry.Value;
            }
        }

        private void Reveal()
        {
            if (DisplayedResults.Count >= Results.Count)
            {
                return;
            }

            var poolNumber = Results[Index].Item1;
            var country = Results[Index].Item2;
            var poolName = $"Pool{poolNumber}Remaining";

            // Get the field info for the pool
            var poolFieldInfo = GetType().GetField(poolName, BindingFlags.NonPublic | BindingFlags.Instance);

            if (poolFieldInfo != null)
            {
                // Get the list value from the field
                Dictionary<string, int> poolList = poolFieldInfo.GetValue(this) as Dictionary<string, int>;

                if (poolList != null)
                {
                    poolList.Remove(country);
                }
            }

            DisplayedResults.Add((Index, Results[Index].Item1, Results[Index].Item2, Results[Index].Item3, Results[Index].Item4));
            DisplayedResults = DisplayedResults.OrderByDescending(d => d.Item1).ToList();
            Index++;
        }

        private string RowStyleFunc((int, int, string, string, int) arg1, int index)
        {
            switch (arg1.Item2)
            {
                case 6:
                case 4:
                case 2:
                    return "background-color:#ffedba";
                default: return "background-color:white";
            }
        }

        private static void AddRange(Dictionary<string, int> target, Dictionary<string, int> source)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            foreach (var element in source)
                target.Add(element.Key, element.Value);
        }
    }
}
