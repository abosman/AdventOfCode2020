using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var answers = File.ReadAllText(@"Input.txt").Split(Environment.NewLine + Environment.NewLine);

Part1();
Part2();

void Part1()
{
    var total = answers.Sum(answer => answer.Replace(Environment.NewLine, "").
        ToCharArray().Distinct().Count());
    Console.WriteLine($"Sum of yes answers: {total}"); // 6809
}

void Part2()
{
    int total = 0;
    foreach (var answer in answers)
    {
        var h = new Dictionary<char, int>();
        var answerParts = answer.Split(Environment.NewLine);
        foreach (var answerPerson in answerParts)
        {
            foreach (var c in answerPerson)
            {
                if (h.ContainsKey(c))
                {
                    h[c]++;
                }
                else
                {
                    h.Add(c, 1);
                }
            }
        }

        total += h.Count(i => i.Value == answerParts.Length);
    }
    
    Console.WriteLine($"Sum of yes answers: {total}"); // 3394
}