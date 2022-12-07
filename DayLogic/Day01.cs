using AoC22.Interfaces;

namespace AoC22.DayLogic;

public class Day01 : BaseDay
{
    public override void PartOne()
    {
        var split = "\r\n";
        // find our input
        var inputs = RawInput();
        List<int> groupTotals = new();

        var input = inputs.Split($"{split}{split}");
        foreach (var elf in input)
        {
            var kcal = 0;
            var food = elf.Split(split);
            foreach (var f in food)
            {
                if (f.Length == 0) continue;
                kcal += int.Parse(f);
            }

            groupTotals.Add(kcal);

        }

        var max = groupTotals.Max();
        
        // part two

        groupTotals.Sort();
        var gobble = groupTotals.TakeLast(3).Sum();
        Console.WriteLine($"Fattest elf has {max} kcal");
        Console.WriteLine($"Fattest 3 elves have {gobble} kcal");

    }

    public override void PartTwo()
    {
    }
}