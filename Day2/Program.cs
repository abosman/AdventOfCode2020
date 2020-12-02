using System;
using System.IO;
using System.Linq;
using Day2;

var lines = File.ReadAllLines(@"Input.txt");
Part1();
Part2();

void Part1()
{
    var validPasswordsCount = lines.Select(ParseLine).Count(ValidPassword1);
    Console.WriteLine($"Number of valid passwords: {validPasswordsCount}"); // 454
}

void Part2()
{
    var validPasswordsCount = lines.Select(ParseLine).Count(ValidPassword2);
    Console.WriteLine($"Number of valid passwords: {validPasswordsCount}"); // 649
}

static bool ValidPassword1(PolicyAndPassword policyAndPassword)
{
    var index1 = policyAndPassword.Policy1;
    var index2 = policyAndPassword.Policy2;
    var letter = policyAndPassword.Letter;
    var password = policyAndPassword.Password;
    var letterCount = password.Count(l => l == letter);
    return letterCount >= index1 && letterCount <= index2;
}

static bool ValidPassword2(PolicyAndPassword policyAndPassword)
{
    var index1 = policyAndPassword.Policy1 - 1;
    var index2 = policyAndPassword.Policy2 - 1;
    var letter = policyAndPassword.Letter;
    var password = policyAndPassword.Password;
    return password[index1] == letter && password[index2] != letter ||
           password[index1] != letter && password[index2] == letter;
}

static PolicyAndPassword ParseLine(string line)
{
    var parts = line.Split(' ');
    return new PolicyAndPassword(int.Parse(parts[0].Split('-')[0]), int.Parse(parts[0].Split('-')[1]), parts[1][0],
        parts[2]);
}

namespace Day2
{
    internal record PolicyAndPassword(int Policy1, int Policy2, char Letter, string Password);
}
