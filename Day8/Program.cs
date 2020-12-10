using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Day8;

Stopwatch sw = new Stopwatch();
sw.Start();
var instructions = new List<Instruction>();
var lines = File.ReadAllLines(@"Input.txt");
instructions.AddRange(from line in lines
                      let parts = line.Split(" ")
                      select new Instruction { Operation = parts[0], Argument = int.Parse(parts[1]) });

Part1();
Part2();
Console.WriteLine($"Total time: {sw.Elapsed.TotalMilliseconds}");

void Part1()
{
    var accValue = 0;
    var index = 0;
    var instruction = instructions[index];
    while (!instruction.Performed)
    {
        switch (instruction.Operation)
        {
            case "acc":
                accValue += instruction.Argument;
                index++;
                break;
            case "jmp":
                index += instruction.Argument;
                break;
            case "nop":
                index++;
                break;
        }
        instruction.Performed = true;
        instruction = instructions[index];

    }
    Console.WriteLine($"Value of accumulator: {accValue}"); // 1744
}

void Part2()
{
    var changedInstructionIndex = 0;
    while (true)
    {
        var copyList = Copy(instructions);
        changedInstructionIndex =
            copyList.FindIndex(changedInstructionIndex, i => i.Operation == "jmp" || i.Operation == "nop");
        var instruction = copyList[changedInstructionIndex];
        instruction.Operation = instruction.Operation == "jmp" ? "nop" : "jmp";
        var result = RunProgram(copyList);
        if (result.Item1)
        {
            Console.WriteLine($"Value of accumulator: {result.Item2}"); // 1174
            return;
        }
        changedInstructionIndex++;
    }
}

List<Instruction> Copy(List<Instruction> originalInstructions) => originalInstructions
        .Select(i => new Instruction()
        {
            Operation = i.Operation,
            Argument = i.Argument,
            Performed = false
        })
        .ToList();   


Tuple<bool,int> RunProgram(List<Instruction> changedInstructions)
{
    var accValue = 0;
    var index = 0;
    var instruction = changedInstructions[index];
    while (!instruction.Performed )
    {
        switch (instruction.Operation)
        {
            case "acc":
                accValue += instruction.Argument;
                index++;
                break;
            case "jmp":
                index += instruction.Argument;
                break;
            case "nop":
                index++;
                break;
        }
        instruction.Performed = true;
        if (index == changedInstructions.Count)
        {
            return new Tuple<bool, int>(true, accValue);
        }
            
        instruction = changedInstructions[index];
    }
    return new Tuple<bool, int>(false, accValue);
}


namespace Day8
{
    record Instruction
    {
        public string Operation { get; set; }
        public int Argument { get; set; }
        public bool Performed { get; set; }
    }
}