using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2015
{
    internal partial class Day15 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsList(2015, 15, 1);

            var ingredients = new List<Ingredient>();

            foreach (var line in input)
            {
                var matchGroups = IngredientParser().Match(line).Groups;

                var name = matchGroups[1].Value;
                var capacity = int.Parse(matchGroups[2].Value);
                var durability = int.Parse(matchGroups[3].Value);
                var flavor = int.Parse(matchGroups[4].Value);
                var texture = int.Parse(matchGroups[5].Value);
                var calories = int.Parse(matchGroups[6].Value);

                ingredients.Add(new Ingredient(name, capacity, durability, flavor, texture, calories));
            }

            int maxScore = 0;

            for (int i = 1; i <= 100; i++)
            {
                ingredients[0].Amount = i;

                for (int j = 1; i + j <= 100; j++)
                {
                    ingredients[1].Amount = j;

                    for (int k = 1; i + j + k <= 100; k++)
                    {
                        ingredients[2].Amount = k;

                        ingredients[3].Amount = 100 - (i + j + k);

                        var score = CalculateScore(ingredients);

                        if (score > maxScore)
                        {
                            maxScore = score;

                            Console.WriteLine($"{ingredients[0].Name}: {i}, {ingredients[1].Name}: {j}, {ingredients[2].Name}: {k}, {ingredients[3].Name}: {100 - (i + j + k)}, Score: {score}");
                        }
                    }
                }
            }

            return maxScore.ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsList(2015, 15, 1);

            var ingredients = new List<Ingredient>();

            foreach (var line in input)
            {
                var matchGroups = IngredientParser().Match(line).Groups;

                var name = matchGroups[1].Value;
                var capacity = int.Parse(matchGroups[2].Value);
                var durability = int.Parse(matchGroups[3].Value);
                var flavor = int.Parse(matchGroups[4].Value);
                var texture = int.Parse(matchGroups[5].Value);
                var calories = int.Parse(matchGroups[6].Value);

                ingredients.Add(new Ingredient(name, capacity, durability, flavor, texture, calories));
            }

            int maxScore = 0;

            for (int i = 1; i <= 100; i++)
            {
                ingredients[0].Amount = i;

                for (int j = 1; i + j <= 100; j++)
                {
                    ingredients[1].Amount = j;

                    for (int k = 1; i + j + k <= 100; k++)
                    {
                        ingredients[2].Amount = k;

                        ingredients[3].Amount = 100 - (i + j + k);

                        if (ingredients.Sum(i => i.CaloriesScore) != 500)
                            continue;

                        var score = CalculateScore(ingredients);

                        if (score > maxScore)
                        {
                            maxScore = score;

                            Console.WriteLine($"{ingredients[0].Name}: {i}, {ingredients[1].Name}: {j}, {ingredients[2].Name}: {k}, {ingredients[3].Name}: {100 - (i + j + k)}, Score: {score}");
                        }
                    }
                }
            }

            return maxScore.ToString();
        }

        private int CalculateScore(List<Ingredient> ingredients)
        {
            var capacityScore = ingredients.Sum(i => i.CapacityScore);

            if (capacityScore <= 0)
                return 0;

            var durabilityScore = ingredients.Sum(i => i.DurabilityScore);

            if (durabilityScore <= 0)
                return 0;

            var flavorScore = ingredients.Sum(i => i.FlavorScore);

            if (flavorScore <= 0)
                return 0;

            var textureScore = ingredients.Sum(i => i.TextureScore);

            if (textureScore <= 0)
                return 0;

            return capacityScore * durabilityScore * flavorScore * textureScore;
        }

        [GeneratedRegex("([A-Z]\\w+).* (-?\\d+).* (-?\\d+).* (-?\\d+).* (-?\\d+).* (-?\\d+)")]
        private static partial Regex IngredientParser();
    }

    internal record class Ingredient(string Name, int Capacity, int Durability, int Flavor, int Texture, int Calories, int Amount = 0)
    {
        public int Amount { get; set; } = Amount;
        public int CapacityScore => Capacity * Amount;
        public int DurabilityScore => Durability * Amount;
        public int FlavorScore => Flavor * Amount;
        public int TextureScore => Texture * Amount;
        public int CaloriesScore => Calories * Amount;
    }
}
