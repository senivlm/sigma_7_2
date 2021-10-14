using System;
using System.Collections.Generic;
using System.IO;

namespace sigma_7_2
{
    class ProductsParser
    {
        private Dictionary<string, double> _recipes = new Dictionary<string, double>();
        private Dictionary<string, int> _prices = new Dictionary<string, int>();
        public ProductsParser(string recipesFile = "recipes.txt", string priceFile = "price.txt")
        {
            ParseRecipes(recipesFile);
            ParsePrice(priceFile);
        }

        public void ParseRecipes(string filename)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    int lineCount = File.ReadAllLines(filename).Length;

                    for (int i = 0; i < lineCount; i++)
                    {
                        string[] line = sr.ReadLine().Split(" ");
                        if (line.Length == 2 && !_recipes.ContainsKey(line[0]))
                        {
                            _recipes.Add(line[0], double.Parse(line[1]));
                        }
                        else if (line.Length == 2 && _recipes.ContainsKey(line[0]))
                        {
                            _recipes[line[0]] += double.Parse(line[1]);
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {filename} was not found.");
                Console.Read();
                Environment.Exit(-1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
                Environment.Exit(-1);
            }
        }

        public void ParsePrice(string filename)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    int lineCount = File.ReadAllLines(filename).Length;

                    for (int i = 0; i < lineCount; i++)
                    {
                        string[] line = sr.ReadLine().Split(" ");
                        if (line.Length == 2 && !_prices.ContainsKey(line[0]))
                        {
                            _prices.Add(line[0], int.Parse(line[1]));
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {filename} was not found.");
                Console.Read();
                Environment.Exit(-1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
                Environment.Exit(-1);
            }
        }

        public override string ToString()
        {
            string res = "";
            try
            {
                foreach (var item in _recipes)
                {
                    res += item.Key;
                    res += "\t";
                    res += string.Format("{0:F2}", item.Value);
                    res += "\t";
                    res += string.Format("{0:F2}", _prices[item.Key] * item.Value);
                    res += "\n";
                }
                return res;
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
    }
}
