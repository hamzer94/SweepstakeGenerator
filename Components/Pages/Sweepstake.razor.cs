using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SweepstakeGenerator.Components.Pages
{
    public partial class Sweepstake
    {
        private List<(int, string, string)> Results = new List<(int, string, string)>();
        private List<(int, string, string)> DisplayedResults = new List<(int, string, string)>();

        private int Index = 0;

        private static Random rng = new Random();

        private List<string> People = new List<string>()
        {
            "Hayden",
            "Sheps",
            "Mikey",
            "Neil"
        };

        private List<string> Pool1 = new List<string>()
        {
            "France",
            "England",
            "Portugal",
            "Belgium"
        };
        private List<string> Pool2 = new List<string>()
        {
            "Germany",
            "Spain",
            "Netherlands",
            "Italy"
        };
        private List<string> Pool3 = new List<string>()
        {
            "Croatia",
            "Austria",
            "Denmark",
            "Hungary"
        };
        private List<string> Pool4 = new List<string>()
        {
            "Switzerland",
            "Ukraine",
            "Czechia",
            "Serbia"
        };
        private List<string> Pool5 = new List<string>()
        {
            "Scotland",
            "Turkey",
            "Poland",
            "Slovenia"
        };
        private List<string> Pool6 = new List<string>()
        {
            "Romania",
            "Slovakia",
            "Albania",
            "Georgia"
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

            Results = Results.OrderBy(r => r.Item1).ToList();
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

        private void Reveal()
        {
            if (DisplayedResults.Count >= Results.Count)
            {
                return;
            }

            int poolNumber = Results[Index].Item1;
            string poolName = $"Pool{poolNumber}";

            // Get the field info for the pool
            FieldInfo poolFieldInfo = this.GetType().GetField(poolName, BindingFlags.NonPublic | BindingFlags.Instance);

            if (poolFieldInfo != null)
            {
                // Get the list value from the field
                List<string> poolList = poolFieldInfo.GetValue(this) as List<string>;

                if (poolList != null)
                {
                    // Perform add or remove operations on the poolList
                    // Example: Add a new item to the list
                    poolList.Add("New Country");

                    // Example: Remove an existing item from the list
                    poolList.Remove("France");
                    
                    // Print the contents of the list to verify changes
                    Console.WriteLine($"Contents of {poolName} after modifications:");
                    foreach (var item in poolList)
                    {
                        Console.WriteLine(item);
                    }
                }
            }

            DisplayedResults.Add(Results[Index]);
            Index++;
        }

        private string RowStyleFunc((int, string, string) arg1, int index)
        {
            switch (arg1.Item1)
            {
                case 1:
                case 3:
                case 5:
                    return "background-color:#ffedba";
                default: return "background-color:white";
            }
        }
    }
}
