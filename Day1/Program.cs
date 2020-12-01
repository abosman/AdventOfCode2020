using System;
using System.IO;
using System.Linq;

var input = File.ReadAllLines(@"Input.txt");
var expenses = input.Select(c => int.Parse(c.ToString())).ToList();

Part1();
Part2();

void Part1()
{
    foreach (var (expense, expense2) in from expense in expenses
        let expense2 = expenses.Find(e => expense + e == 2020)
        where expense2 != 0
        select (expense, expense2))
    {
        Console.WriteLine($"{expense} + {expense2} = 2020");
        Console.WriteLine($"result = {expense} * {expense2} = {expense * expense2}");
        return;
    }
}

void Part2()
{
    for (int i = 0; i < expenses.Count-2; i++)
    {
        for (int j = 1; j < expenses.Count-1; j++)
        {
            for (int k = 2; k < expenses.Count; k++)
            {
                if (expenses[i] + expenses[j] + expenses[k] == 2020)
                {
                    Console.WriteLine($"{expenses[i]} + {expenses[j]} + {expenses[k]} = 2020");
                    Console.WriteLine($"result = {expenses[i]} * {expenses[j]} * {expenses[k]} = {expenses[i] * expenses[j] * expenses[k]}");
                    return;
                }
            }
        }
    }
}