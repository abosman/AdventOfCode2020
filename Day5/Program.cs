using System;
using System.IO;
using System.Linq;

var passes = File.ReadAllLines(@"Input.txt");

Part1();
Part2();

void Part1()
{
    var maxSeatId = 
        (from pass in passes
         let row = Convert.ToInt32(pass.Substring(0, 7).Replace("F", "0").Replace("B", "1"), 2)
         let column = Convert.ToInt32(pass.Substring(7, 3).Replace("L", "0").Replace("R", "1"), 2)
         let seatId = row * 8 + column select seatId).Aggregate(0L, (current, seatId) => Math.Max(seatId, current));

    Console.WriteLine($"Max SeatID: {maxSeatId}"); // 818
}

void Part2()
{
    var places = (from pass in passes
        let row = Convert.ToInt32(pass.Substring(0, 7).Replace("F", "0").Replace("B", "1"),2)
        let column = Convert.ToInt32(pass.Substring(7, 3).Replace("L", "0").Replace("R", "1"),2)
        let seatId = row * 8 + column
        select seatId).ToList();
    places.Sort();
    var index = places[0];
    foreach (var place in places)
    {
        if (index != place)
        {
            Console.WriteLine($"Free seat {index}"); // 559
            break;
        }
        index++;
    }
}