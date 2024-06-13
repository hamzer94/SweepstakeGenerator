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
        private List<(int, string, string)> Results = new List<(int, string, string)>();
        private List<(int, int, string, string)> DisplayedResults = new List<(int, int, string, string)>();

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
                Results.Add((1, Pool1[index], person));
                Results.Add((2, Pool2[index], person));
                Results.Add((3, Pool3[index], person));
                Results.Add((4, Pool4[index], person));
                Results.Add((5, Pool5[index], person));
                Results.Add((6, Pool6[index], person));

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

        private static void Shuffle(Dictionary<string, int> dictionary)
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

            DisplayedResults.Add((Index, Results[Index].Item1, Results[Index].Item2, Results[Index].Item3));
            DisplayedResults = DisplayedResults.OrderByDescending(d => d.Item1).ToList();
            Index++;
        }

        private string RowStyleFunc((int, int, string, string) arg1, int index)
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
    }
}
