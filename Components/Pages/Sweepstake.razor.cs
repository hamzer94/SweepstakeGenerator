using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static MudBlazor.CategoryTypes;

namespace SweepstakeGenerator.Components.Pages
{
    public partial class Sweepstake
    {
        private List<(int, string, string, int)> Results = new List<(int, string, string, int)>();
        private List<(int, int, string, string, int)> DisplayedResults = new List<(int, int, string, string, int)>();

        private int Index = 0;

        private bool ShowSummary = false;

        private bool IsSpinning = false;
        private bool _isRevealingAll = false;
        private string SpinDisplayText = string.Empty;
        private int SpinPoolNumber = 0;
        private string SpinPersonName = string.Empty;
        private bool SpinLocked = false;
        private int SpinTickKey = 0;

        private bool AllRevealed => Results.Count > 0 &&
            !Pool1Remaining.Any() && !Pool2Remaining.Any() && !Pool3Remaining.Any() &&
            !Pool4Remaining.Any() && !Pool5Remaining.Any() && !Pool6Remaining.Any() &&
            !Pool7Remaining.Any() && !Pool8Remaining.Any() && !Pool9Remaining.Any() &&
            !Pool10Remaining.Any() && !Pool11Remaining.Any() && !Pool12Remaining.Any();

        private int CurrentPoolNumber => Index < Results.Count ? Results[Index].Item1 : 0;

        private IEnumerable<(int Pool, Dictionary<string, int> Teams)> RemainingPools =>
            new (int, Dictionary<string, int>)[]
            {
                (1, Pool1Remaining), (2, Pool2Remaining), (3, Pool3Remaining),
                (4, Pool4Remaining), (5, Pool5Remaining), (6, Pool6Remaining),
                (7, Pool7Remaining), (8, Pool8Remaining), (9, Pool9Remaining),
                (10, Pool10Remaining), (11, Pool11Remaining), (12, Pool12Remaining)
            }.Where(p => p.Item2.Any()).OrderByDescending(p => p.Item1);

        private Dictionary<string, List<(int Pool, string Team, int Seed)>> GetGroupedResults() =>
            DisplayedResults
                .GroupBy(r => r.Item4)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(r => (r.Item2, r.Item3, r.Item5)).OrderBy(x => x.Item1).ToList()
                );

        private List<(string Person, List<(int Pool, string Team, int Seed)> Picks)> GetPersonGrid()
        {
            var grouped = GetGroupedResults();
            return People.Select(p =>
            {
                grouped.TryGetValue(p, out var picks);
                return (p, picks ?? new List<(int, string, int)>());
            }).ToList();
        }

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
            { "Spain", 1 },
            { "France", 2 },
            { "England", 3 },
            { "Brazil", 4 }
        };
        private Dictionary<string, int> Pool2 = new Dictionary<string, int>
        {
            { "Argentina", 5 },
            { "Portugal", 6 },
            { "Germany", 7 },
            { "Netherlands", 8 }
        };
        private Dictionary<string, int> Pool3 = new Dictionary<string, int>
        {
            { "Belgium", 9 },
            { "Norway", 10 },
            { "Colombia", 11 },
            { "Japan", 12 }
        };
        private Dictionary<string, int> Pool4 = new Dictionary<string, int>
        {
            { "Morocco", 13 },
            { "USA", 14 },
            { "Uruguay", 15 },
            { "Mexico", 16 }
        };
        private Dictionary<string, int> Pool5 = new Dictionary<string, int>
        {
            { "Switzerland", 17 },
            { "Croatia", 18 },
            { "T\u00fcrkiye", 19 },
            { "Ecuador", 20 }
        };
        private Dictionary<string, int> Pool6 = new Dictionary<string, int>
        {
            { "Senegal", 21 },
            { "Austria", 22 },
            { "Canada", 23 },
            { "Sweden", 24 }
        };
        private Dictionary<string, int> Pool7 = new Dictionary<string, int>
        {
            { "Ivory Coast", 25 },
            { "Paraguay", 26 },
            { "Egypt", 27 },
            { "Scotland", 28 }
        };
        private Dictionary<string, int> Pool8 = new Dictionary<string, int>
        {
            { "Algeria", 29 },
            { "Bosnia & Herzegovina", 30 },
            { "Ghana", 31 },
            { "Czech Republic", 32 }
        };
        private Dictionary<string, int> Pool9 = new Dictionary<string, int>
        {
            { "South Korea", 33 },
            { "Iran", 34 },
            { "Tunisia", 35 },
            { "Australia", 36 }
        };
        private Dictionary<string, int> Pool10 = new Dictionary<string, int>
        {
            { "Cape Verde", 37 },
            { "Cura\u00e7ao", 38 },
            { "DR Congo", 39 },
            { "Haiti", 40 }
        };
        private Dictionary<string, int> Pool11 = new Dictionary<string, int>
        {
            { "Iraq", 41 },
            { "Jordan", 42 },
            { "New Zealand", 43 },
            { "Panama", 44 }
        };
        private Dictionary<string, int> Pool12 = new Dictionary<string, int>
        {
            { "Qatar", 45 },
            { "Saudi Arabia", 46 },
            { "South Africa", 47 },
            { "Uzbekistan", 48 }
        };

        private Dictionary<string, int> Pool1Remaining = new Dictionary<string, int>
        {
            { "Spain", 1 },
            { "France", 2 },
            { "England", 3 },
            { "Brazil", 4 }
        };
        private Dictionary<string, int> Pool2Remaining = new Dictionary<string, int>
        {
            { "Argentina", 5 },
            { "Portugal", 6 },
            { "Germany", 7 },
            { "Netherlands", 8 }
        };
        private Dictionary<string, int> Pool3Remaining = new Dictionary<string, int>
        {
            { "Belgium", 9 },
            { "Norway", 10 },
            { "Colombia", 11 },
            { "Japan", 12 }
        };
        private Dictionary<string, int> Pool4Remaining = new Dictionary<string, int>
        {
            { "Morocco", 13 },
            { "USA", 14 },
            { "Uruguay", 15 },
            { "Mexico", 16 }
        };
        private Dictionary<string, int> Pool5Remaining = new Dictionary<string, int>
        {
            { "Switzerland", 17 },
            { "Croatia", 18 },
            { "T\u00fcrkiye", 19 },
            { "Ecuador", 20 }
        };
        private Dictionary<string, int> Pool6Remaining = new Dictionary<string, int>
        {
            { "Senegal", 21 },
            { "Austria", 22 },
            { "Canada", 23 },
            { "Sweden", 24 }
        };
        private Dictionary<string, int> Pool7Remaining = new Dictionary<string, int>
        {
            { "Ivory Coast", 25 },
            { "Paraguay", 26 },
            { "Egypt", 27 },
            { "Scotland", 28 }
        };
        private Dictionary<string, int> Pool8Remaining = new Dictionary<string, int>
        {
            { "Algeria", 29 },
            { "Bosnia & Herzegovina", 30 },
            { "Ghana", 31 },
            { "Czech Republic", 32 }
        };
        private Dictionary<string, int> Pool9Remaining = new Dictionary<string, int>
        {
            { "South Korea", 33 },
            { "Iran", 34 },
            { "Tunisia", 35 },
            { "Australia", 36 }
        };
        private Dictionary<string, int> Pool10Remaining = new Dictionary<string, int>
        {
            { "Cape Verde", 37 },
            { "Cura\u00e7ao", 38 },
            { "DR Congo", 39 },
            { "Haiti", 40 }
        };
        private Dictionary<string, int> Pool11Remaining = new Dictionary<string, int>
        {
            { "Iraq", 41 },
            { "Jordan", 42 },
            { "New Zealand", 43 },
            { "Panama", 44 }
        };
        private Dictionary<string, int> Pool12Remaining = new Dictionary<string, int>
        {
            { "Qatar", 45 },
            { "Saudi Arabia", 46 },
            { "South Africa", 47 },
            { "Uzbekistan", 48 }
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
            ShowSummary = false;

            Pool1Remaining = new Dictionary<string, int>();
            Pool2Remaining = new Dictionary<string, int>();
            Pool3Remaining = new Dictionary<string, int>();
            Pool4Remaining = new Dictionary<string, int>();
            Pool5Remaining = new Dictionary<string, int>();
            Pool6Remaining = new Dictionary<string, int>();
            Pool7Remaining = new Dictionary<string, int>();
            Pool8Remaining = new Dictionary<string, int>();
            Pool9Remaining = new Dictionary<string, int>();
            Pool10Remaining = new Dictionary<string, int>();
            Pool11Remaining = new Dictionary<string, int>();
            Pool12Remaining = new Dictionary<string, int>();

            AddRange(Pool1Remaining, Pool1);
            AddRange(Pool2Remaining, Pool2);
            AddRange(Pool3Remaining, Pool3);
            AddRange(Pool4Remaining, Pool4);
            AddRange(Pool5Remaining, Pool5);
            AddRange(Pool6Remaining, Pool6);
            AddRange(Pool7Remaining, Pool7);
            AddRange(Pool8Remaining, Pool8);
            AddRange(Pool9Remaining, Pool9);
            AddRange(Pool10Remaining, Pool10);
            AddRange(Pool11Remaining, Pool11);
            AddRange(Pool12Remaining, Pool12);

            Shuffle(People);
            Shuffle(Pool1);
            Shuffle(Pool2);
            Shuffle(Pool3);
            Shuffle(Pool4);
            Shuffle(Pool5);
            Shuffle(Pool6);
            Shuffle(Pool7);
            Shuffle(Pool8);
            Shuffle(Pool9);
            Shuffle(Pool10);
            Shuffle(Pool11);
            Shuffle(Pool12);

            var index = 0;
            foreach (var person in People)
            {
                Results.Add((1, Pool1.Keys.ElementAt(index), person, Pool1.Values.ElementAt(index)));
                Results.Add((2, Pool2.Keys.ElementAt(index), person, Pool2.Values.ElementAt(index)));
                Results.Add((3, Pool3.Keys.ElementAt(index), person, Pool3.Values.ElementAt(index)));
                Results.Add((4, Pool4.Keys.ElementAt(index), person, Pool4.Values.ElementAt(index)));
                Results.Add((5, Pool5.Keys.ElementAt(index), person, Pool5.Values.ElementAt(index)));
                Results.Add((6, Pool6.Keys.ElementAt(index), person, Pool6.Values.ElementAt(index)));
                Results.Add((7, Pool7.Keys.ElementAt(index), person, Pool7.Values.ElementAt(index)));
                Results.Add((8, Pool8.Keys.ElementAt(index), person, Pool8.Values.ElementAt(index)));
                Results.Add((9, Pool9.Keys.ElementAt(index), person, Pool9.Values.ElementAt(index)));
                Results.Add((10, Pool10.Keys.ElementAt(index), person, Pool10.Values.ElementAt(index)));
                Results.Add((11, Pool11.Keys.ElementAt(index), person, Pool11.Values.ElementAt(index)));
                Results.Add((12, Pool12.Keys.ElementAt(index), person, Pool12.Values.ElementAt(index)));

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

        private async Task RevealAsync(bool isFast = false)
        {
            if (IsSpinning || DisplayedResults.Count >= Results.Count)
                return;

            var poolNumber = Results[Index].Item1;
            var winner = Results[Index].Item2;
            var person = Results[Index].Item3;
            var poolName = $"Pool{poolNumber}Remaining";

            // Gather remaining candidates for the slot roll
            var poolFieldInfo = GetType().GetField(poolName, BindingFlags.NonPublic | BindingFlags.Instance);
            Dictionary<string, int> poolList = null;
            if (poolFieldInfo != null)
                poolList = poolFieldInfo.GetValue(this) as Dictionary<string, int>;

            var candidates = poolList?.Keys.ToList() ?? new List<string> { winner };
            if (!candidates.Contains(winner))
                candidates.Add(winner);

            IsSpinning = true;
            SpinLocked = false;
            SpinPoolNumber = poolNumber;
            SpinPersonName = person;

            // Slot-machine tick: fast ? slow, quadratic easing
            int totalTicks = isFast ? 10 : 22;
            for (int i = 0; i < totalTicks; i++)
            {
                SpinDisplayText = candidates[rng.Next(candidates.Count)];
                SpinTickKey++;
                StateHasChanged();
                double progress = (double)i / totalTicks;
                int delay = (int)((isFast ? 30 : 60) + (isFast ? 150 : 380) * progress * progress);
                await Task.Delay(delay);
            }

            // Land on the winner
            SpinDisplayText = winner;
            SpinTickKey++;
            SpinLocked = true;
            StateHasChanged();

            await Task.Delay(isFast ? 500 : 1400);

            // Commit result
            poolList?.Remove(winner);
            DisplayedResults.Add((Index, Results[Index].Item1, Results[Index].Item2, Results[Index].Item3, Results[Index].Item4));
            DisplayedResults = DisplayedResults.OrderByDescending(d => d.Item1).ToList();
            Index++;

            IsSpinning = false;
            StateHasChanged();
        }

        private async Task RevealAllAsync()
        {
            if (IsSpinning || _isRevealingAll || DisplayedResults.Count >= Results.Count)
                return;

            _isRevealingAll = true;
            try
            {
                while (Index < Results.Count)
                {
                    await RevealAsync(isFast: true);
                }
            }
            finally
            {
                _isRevealingAll = false;
                StateHasChanged();
            }
        }

        private string RowStyleFunc((int, int, string, string, int) arg1, int index)
        {
            switch (arg1.Item2)
            {
                case 12:
                case 10:
                case 8:
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
