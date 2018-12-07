using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using NewClassrooms.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NewClassrooms.Services
{
    public class AnalyticsService
    {
        public Analytics[] GenerateDefaultAnalytics(Person[] data)
        {
            List<Analytics> analytics = new List<Analytics>
            {
                // men vs. women
                GetCountOfMenVsWomen(data),
                // first names
                GetCountOfAMvsNZFirstNames(data),
                // last names
                GetCountOfAMvsNZLastNames(data),
                // people in top 10 states
                GetCountOfTopTenPopulousStates(data),
                // women in top 10 states
                GetCountOfWomenInTopTenPopulousStates(data),
                // men in top 10 states
                GetCountOfMenInTopTenPopulousStates(data),
                // age ranges
                GetCountOfAgeRanges(data),
            };


            return analytics.ToArray();

        }


        public Analytics GetCountOfMenVsWomen(Person[] data)
        {
            return new Analytics
            {
                ChartType = Constants.ChartType.Pie,
                Title = "Percentage female versus male",
                Data = new DataPoint[2]
                {
                    new DataPoint { Label = Constants.Gender.Male, Statistic = ConvertToPercentage(data.Count(x => x.Gender == Constants.Gender.Male), data.Count()) },
                    new DataPoint { Label = Constants.Gender.Female, Statistic = ConvertToPercentage(data.Count(x => x.Gender == Constants.Gender.Female), data.Count()) }
                }
            };
        }

        public Analytics GetCountOfAMvsNZFirstNames(Person[] data)
        {
            return new Analytics
            {
                ChartType = Constants.ChartType.Pie,
                Title = "Percentage of first names that start with A‐M versus N‐Z",
                Data = new DataPoint[2]
                {
                    new DataPoint { Label = Constants.AlphabetRange.AM, Statistic = ConvertToPercentage(data.Count(x => GetIsLetterWithinRange(x.Name.First[0], 'a', 'm')), data.Count()) },
                    new DataPoint { Label = Constants.AlphabetRange.NZ, Statistic = ConvertToPercentage(data.Count(x => GetIsLetterWithinRange(x.Name.First[0], 'n', 'z')), data.Count()) },
                }
            };
        }

        public Analytics GetCountOfAMvsNZLastNames(Person[] data)
        {
            return new Analytics
            {
                ChartType = Constants.ChartType.Pie,
                Title = "Percentage of last names that start with A‐M versus N‐Z",
                Data = new DataPoint[2]
                {
                    new DataPoint { Label = Constants.AlphabetRange.AM, Statistic = ConvertToPercentage(data.Count(x => GetIsLetterWithinRange(x.Name.Last[0], 'a', 'm')), data.Count()) },
                    new DataPoint { Label = Constants.AlphabetRange.NZ, Statistic = ConvertToPercentage(data.Count(x => GetIsLetterWithinRange(x.Name.Last[0], 'n', 'z')), data.Count()) },
                }
            };
        }

        public Analytics GetCountOfTopTenPopulousStates(Person[] data)
        {
            IEnumerable<KeyValuePair<string, int>> states = data.GroupBy(x => x.Location.State)
                                                            .OrderByDescending(x => x.Count())
                                                            .Take(10)
                                                            .Select(x => new KeyValuePair<string, int>(x.Key, x.Count()));

            return new Analytics
            {
                ChartType = Constants.ChartType.Bar,
                Title = "Percentage of people in each state, up to the top 10 most populous states",
                Data = ExtractStatePercentages(states)
            };
        }

        public Analytics GetCountOfWomenInTopTenPopulousStates(Person[] data)
        {
            IEnumerable<IGrouping<string, Person>> group = data
                                                            .GroupBy(x => x.Location.State)
                                                            .OrderByDescending(x => x.Count())
                                                            .Take(10);
            IEnumerable<Tuple<string, int, int>> states = group.Select(x => new Tuple<string, int, int>(x.Key, x.Count(y => y.Gender == Constants.Gender.Female), x.Count()));

            return new Analytics
            {
                ChartType = Constants.ChartType.Bar,
                Title = "Percentage of females in each state, up to the top 10 most populous states",
                Data = ExtractStateGenderPercentages(states)
            };
        }

        public Analytics GetCountOfMenInTopTenPopulousStates(Person[] data)
        {
            IEnumerable<IGrouping<string, Person>> group = data
                                                            .GroupBy(x => x.Location.State)
                                                            .OrderByDescending(x => x.Count())
                                                            .Take(10);
            IEnumerable<Tuple<string, int, int>> states = group.Select(x => new Tuple<string, int, int>(x.Key, x.Count(y => y.Gender == Constants.Gender.Male), x.Count()));
            return new Analytics
            {
                ChartType = Constants.ChartType.Bar,
                Title = "Percentage of males in each state, up to the top 10 most populous states",
                Data = ExtractStateGenderPercentages(states)
            };
        }

        public Analytics GetCountOfAgeRanges(Person[] data)
        {
            return new Analytics
            {
                ChartType = Constants.ChartType.Pie,
                Title = "Percentage of people in the following age ranges: 0‐20, 21‐40, 41‐60, 61‐80, 81‐100, 101+",
                Data = new DataPoint[6]
                {
                    new DataPoint { Label = Constants.AgeRange.ZeroTwenty, Statistic = ConvertToPercentage(data.Count(x => GetIsAgeWithinRange(x.Dob.Age, 0, 20)), data.Count()) },
                    new DataPoint { Label = Constants.AgeRange.TwentyForty, Statistic = ConvertToPercentage(data.Count(x => GetIsAgeWithinRange(x.Dob.Age, 21, 40)), data.Count()) },
                    new DataPoint { Label = Constants.AgeRange.FortySixty, Statistic = ConvertToPercentage(data.Count(x => GetIsAgeWithinRange(x.Dob.Age, 41, 60)), data.Count()) },
                    new DataPoint { Label = Constants.AgeRange.SixtyEighty, Statistic = ConvertToPercentage(data.Count(x => GetIsAgeWithinRange(x.Dob.Age, 61, 80)), data.Count()) },
                    new DataPoint { Label = Constants.AgeRange.EightyOneHundred, Statistic = ConvertToPercentage(data.Count(x => GetIsAgeWithinRange(x.Dob.Age, 81, 100)), data.Count()) },
                    new DataPoint { Label = Constants.AgeRange.OneHundredPlus, Statistic = ConvertToPercentage(data.Count(x => GetIsAgeWithinRange(x.Dob.Age, 101, int.MaxValue)), data.Count()) },
                }
            };
        }

        private DataPoint[] ExtractStatePercentages(IEnumerable<KeyValuePair<string, int>> states)
        {
            List<DataPoint> dataPoints = new List<DataPoint>();
            foreach (var state in states)
            {
                dataPoints.Add(new DataPoint
                {
                    Label = state.Key,
                    Statistic = ConvertToPercentage(state.Value, states.Sum(x => x.Value))
                });
            }

            return dataPoints.ToArray();
        }

        private DataPoint[] ExtractStateGenderPercentages(IEnumerable<Tuple<string, int, int>> states)
        {
            List<DataPoint> dataPoints = new List<DataPoint>();
            foreach (var state in states)
            {
                dataPoints.Add(new DataPoint
                {
                    Label = state.Item1,
                    Statistic = ConvertToPercentage(state.Item2, state.Item3)
                });
            }

            return dataPoints.ToArray();
        }

        private decimal ConvertToPercentage(int numerator, int denominator)
        {
            decimal percentage = ((decimal)numerator / (decimal)denominator) * 100m;
            // round to 2 places
            return decimal.Round(percentage, 2, MidpointRounding.AwayFromZero);
        }

        public bool GetIsLetterWithinRange(char letter, char lower, char upper)
        {
            int letterIndex = (int)letter % 32;
            int lowerIndex = (int)lower % 32;
            int upperIndex = (int)upper % 32;

            return lowerIndex <= letterIndex && letterIndex <= upperIndex;
        }

        public bool GetIsAgeWithinRange(int age, int lower, int upper)
        {
        
            return lower <= age && age <= upper;
        }

        public T LoadJson<T>(string path)
        {
            using (StreamReader r = System.IO.File.OpenText(path))
            {
                string json = r.ReadToEnd();
                T items = JsonConvert.DeserializeObject<T>(json);
                return items;
            }
        }

        public IFormFile LoadJsonAsIFormFile(string path)
        {
            using (FileStream stream = System.IO.File.OpenRead(path))
            {
                IFormFile formFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/json"
                };
                return formFile;
            }
        }
    }
}
