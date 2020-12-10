using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

Dictionary<string, Bag> bags = new Dictionary<string, Bag>();
List<BagRule> rules = new List<BagRule>();
HashSet<BagRule> filteredRules = new HashSet<BagRule>();
List<BagRule> childRules = new List<BagRule>();

var lines = File.ReadAllLines(@"Input.txt");

Part1();
Part2();

void Part1()
{
    foreach (var line in lines)
    {
        var parts = line.Replace(".", "").Split("bags contain");
        var bagColor = parts[0].Trim();
        var bag = FindOrCreateBag(bagColor);

        if (parts[1].Trim() != "no other bags")
        {
            var subParts = parts[1].Replace(" bags", "").
                Replace(" bag","").Split(",");
            var contents = (
                from subPart in subParts 
                select subPart.Trim() into subRule 
                let index = subRule.IndexOf(" ") 
                let bagChild = subRule.Substring(index + 1) 
                select new BagContent(int.Parse(subRule.Substring(0, index)), 
                    FindOrCreateBag(bagChild))).ToList();
            rules.Add(new BagRule(bag, contents));
        }
    }

    FindRules("shiny gold");
    Console.WriteLine($"Number of bag colors that can contain at least one shiny gold bag: {filteredRules.Count}"); //213
}

void Part2()
{
}


void FindRules(string color){
    
    foreach (var rule in rules)
    {
        foreach (var ruleContent in rule.Contents)
        {
            if (ruleContent.Bag == FindOrCreateBag(color))
            {
                filteredRules.Add(rule);
                FindRules(rule.Parent.Color);
            }
        }
    }
}

Bag FindOrCreateBag(string bagColor) {
    if (bags.ContainsKey(bagColor))
    {
        return bags[bagColor];
    }
    var bag = new Bag(bagColor);
    bags.Add(bagColor, bag);
    return bag;
}

record Bag(string Color);

record BagContent(int Amount, Bag Bag);
record BagRule(Bag Parent, List<BagContent> Contents);