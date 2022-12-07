using System.Text;
using System.Text.RegularExpressions;

namespace AoC22.DayLogic;

public class Day03 : BaseDay
{
    public override void PartOne()
    {
        var lines = ReadInput<string>();
        
        // Regex for finding duplicates
        Regex regex = new Regex(@"(.)(?=.*\1)");
        int sum = 0;
        int groupScore = 0;
        
        // part two groupings
        string[] group = new string[3];
        int index = 0;
        foreach (var pack in lines)
        {

            // Split the string into two halves.
            var sA = pack.Substring(0, pack.Length / 2);
            var sB = pack.Substring((pack.Length / 2));
            
            // remove duplicates with regex
            sA = regex.Replace(sA, string.Empty);
            sB = regex.Replace(sB, string.Empty);

            // Combine filtered packs, run dupe match again
            var match = regex.Matches((sA + sB));

            // Get value of the char, score it then add
            sum += _Score((match[0].Value[0]));

            // part two - groupings
            group[index] = sA + sB;
            ++index;
            index %= 3;
            if (index == 0)
            {
                // analyse the group - there will be one match
                for (var i = 0; i < group[0].Length; ++i)
                {
                    var test = group[0][i];
                    if (group[1].Contains(test) && group[2].Contains(test))
                    {
                        groupScore += _Score(test);
                        break;
                    }
                }
            }
        }

        int _Score(char letter)
        {
            int value = letter;
            return (value - (value >= 97 ? 96 : 64 - 26));
        }
        
        // Part two - post analysis

        Console.WriteLine("----");
        Console.WriteLine($"Sum -> {sum}");
        Console.WriteLine($"Group Sum -> {groupScore}");
    }
}