namespace AoC22.DayLogic;

public class Day04 : BaseDay
{
    public override void PartOne()
    {
        var input = ReadInput<string>();
        int contained = 0, overlap = 0;
        foreach (var input in input)
        {
            var assignments = Pairing(input);

            var topContained = (assignments.top.left >= assignments.bot.left && assignments.top.right <= assignments.bot.right);
            var botContained = (assignments.bot.left >= assignments.top.left && assignments.bot.right <= assignments.top.right);
            
            if (topContained || botContained)
            {
                contained++;
            }
            
            // check for overlaps
            if (   _CheckOverlap(assignments.bot.left, assignments.top)
                   || _CheckOverlap(assignments.bot.right, assignments.top)
                   || _CheckOverlap(assignments.top.left, assignments.bot)
                   || _CheckOverlap(assignments.top.right, assignments.bot))
            {
                overlap++;
            }
        }

        bool _CheckOverlap(int test, (int min, int max) bounds)
        {
            return (test >= bounds.min && test <= bounds.max);
        }

        Console.WriteLine($"Contained Groups: {contained}");
        Console.WriteLine($"Overlapped Groups: {overlap}");

    }


    ((int left, int right) top, (int left, int right) bot) Pairing(string input)
    {
        var temp = input.Split(',');
        var a = temp[0].Split('-');
        var b = temp[1].Split('-');
        return ((int.Parse(a[0]), int.Parse(a[1])), (int.Parse(b[0]), int.Parse(b[1])));
    }
}