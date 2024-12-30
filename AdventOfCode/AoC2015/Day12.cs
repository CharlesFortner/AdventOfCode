using AdventOfCode.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.AoC2015
{
    internal partial class Day12 : AoCDay
    {
        private JContainer? _data;
        private List<JObject> _toRemove = [];

        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsString(2015, 12, 1);

            var matches = NumberFinder().Matches(input);

            int sum = 0;

            foreach (Match match in matches)
            {
                sum += int.Parse(match.Groups[0].ToString());
            }

            return sum.ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsString(2015, 12, 1);

            if (input == null)
                return "";

            var data = JsonConvert.DeserializeObject(input);

            if (data == null)
                return "";

            if (data is not JContainer)
                return "";

            _data = (JContainer)data;

            if (_data == null)
                return "";

            if (_data is JObject jObject)
                RemovedRedObjectsObject(jObject);
            else if (_data is JArray jArray)
                RemoveRedObjectsArray(jArray);

            for (int i = 0; i < _toRemove.Count; i++)
            {
                if (_toRemove[i].Parent is JProperty)
                    _toRemove[i].Parent?.Remove();
                else
                    _toRemove[i]?.Remove();
            }

            int sum = 0;

            var matches = NumberFinder().Matches(_data.ToString());

            foreach (Match match in matches)
            {
                sum += int.Parse(match.Groups[0].ToString());
            }

            return sum.ToString();
        }

        private void RemoveRedObjectsArray(JArray data)
        {
            foreach (JToken token in data.Children())
            {
                if (token is JValue)
                    continue;

                else if (token is JArray jArray)
                {
                    RemoveRedObjectsArray(jArray);
                }

                else if (token is JObject jObject)
                {
                    RemovedRedObjectsObject(jObject);
                }
            }
        }

        private void RemovedRedObjectsObject(JObject data)
        {
            if (data.Children().Cast<JProperty>().Any(p => p.First is JValue jVal && jVal.Value != null && jVal.Value.ToString() == "red"))
            {
                _toRemove.Add(data);
                return;
            }

            foreach (JProperty token in data.Children().Cast<JProperty>())
            {
                if (token.First is JValue)
                    continue;

                else if (token.First is JArray jArray)
                {
                    RemoveRedObjectsArray(jArray);
                }

                else if (token.First is JObject jObject)
                {
                    RemovedRedObjectsObject(jObject);
                }
            }
        }

        [GeneratedRegex("-?\\d+")]
        private static partial Regex NumberFinder();
    }
}
