using System.Text.RegularExpressions;

namespace AoC22.DayLogic;

public class Day06 : BaseDay
{
    public override void PartOne()
    {
        var input = RawInput();
        Regex regex = new Regex(@"(.)(?=.*\1)");
        int distinct = 14; // 4 for part 1, 14 part 2
        // trawl through until we find 4 unique characters.
        for (int i = distinct; i < input.Length; ++i)
        {
            // check the last {distinct}
            var matches = regex.Match(input[(i - distinct)..(i)]);
            if (matches.Length == 0)
            {
                Console.WriteLine($"Marker starts at -> {i} -> Marker: {input[(i- distinct)..(i)]}");
                break;
            }
        }
    }
}