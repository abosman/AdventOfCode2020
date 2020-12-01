using System;
using System.IO;
using System.Linq;

var input = File.ReadAllLines(@"Input.txt");
var expenses = input.Select(c => int.Parse(c.ToString())).ToList();

PartA();

void PartA()
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