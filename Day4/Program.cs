using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

var passports = File.ReadAllText(@"Input.txt").Split(Environment.NewLine + Environment.NewLine);
var requiredPassportItems = new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

Part1();
Part2();

void Part1()
{
    var validPassports = 0;
    foreach (var passport in passports)
    {
        var parts = passport.Replace(Environment.NewLine, " ").Split(" ");
        var passportItems = (from item in parts
                             select item.Split(":")[0]).ToList();
        if (requiredPassportItems.All(passportItems.Contains))
        {
            validPassports++;
        }
    }
    Console.WriteLine($"Number of valid passports: {validPassports}"); // 230
}

void Part2()
{
    var validPassports = 0;
    foreach (var passport in passports)
    {
        var parts = passport.Replace(Environment.NewLine, " ").Split(" ");

        var passportItems = 
            parts.Select(part => part.Split(":")).Select(data => new KeyValuePair<string, string>(data[0], data[1])).ToList();
        if (requiredPassportItems.All(passportItems.Select(p => p.Key).Contains))
        {
            var valid = false;
            foreach (var passportItem in passportItems)
            {
                var value = passportItem.Value;
                valid = passportItem.Key switch
                {
                    "byr" => int.Parse(value) is >= 1920 and <= 2002,
                    "iyr" => int.Parse(value) is >= 2010 and <= 2020,
                    "eyr" => int.Parse(value) is >= 2020 and <= 2030,
                    "hgt" => CheckHeight(passportItem.Value),
                    "hcl" => Regex.Match(value, @"^#(\d|[a-f]){6}$", RegexOptions.IgnoreCase).Success,
                    "ecl" => Regex.Match(value, @"^(amb|blu|brn|gry|grn|hzl|oth)$").Success,
                    "pid" => Regex.Match(value, @"^(\d){9}$").Success,
                    "cid" => true,
                    _ => throw new ArgumentOutOfRangeException()
                };
                if (!valid)
                {
                    break;
                }
            }
            if (valid)
            {
                validPassports++;
            }
        }
    }
    Console.WriteLine($"Number of valid passports: {validPassports}"); // 156
}

static bool CheckHeight(string value)
{
    var length = int.Parse(value.Replace("in", "").Replace("cm", ""));
    if (value.EndsWith("in"))
    {
        return length is >= 59 and <= 76;
    }
    if (value.EndsWith("cm"))
    {
        return length is >= 150 and <= 193;
    }
    return false;
}
