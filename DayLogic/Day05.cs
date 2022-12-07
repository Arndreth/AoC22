using System.Text.RegularExpressions;

namespace AoC22.DayLogic;

public class Day05 : BaseDay
{

    
    public override void PartOne()
    {
        var input = ReadInput<string>();
        var startCrates =  Array.Empty<string>();
        var commandsRaw =  Array.Empty<string>();
        // need to get our crates
        
        // a list of... stacks?
        List<Stack<char>> stacks = new();
        for (int i = 0; i < 10; ++i)
        {
            stacks.Add(new Stack<char>());
        }
        for (int i = 0; i < 32; ++i)
        {
            if (string.IsNullOrEmpty(input[i]))
            {
                startCrates = input[..(i - 1)];
                commandsRaw = input[(i+1)..];
                break;
            }
        }

        IEnumerable<(int qty, int from, int to)> commands = _ProcessCommands(commandsRaw);
        
        
        // start from the bottom of the crates and work up.

        for (int i = startCrates.Length-1; i >= 0; --i)
        {
            // check each stack
            for (int j = 1; j < startCrates[i].Length; j += 4)
            {
                // is there anything at j
                char test = startCrates[i][j];
                if (test != ' ')
                {
                    // houston we have a stack
                    stacks[(j/4)].Push(test);
                }
            }
        }
        
        // print our stacks
        _PrintStacks();
        
        // process our commands and run them
        foreach (var command in commands)
        {
            // queue for part two
            var queue = new Stack<char>();
            for (int i = 0; i < command.qty; ++i)
            {
                // part-one
                var crate = stacks[command.from].Pop();
                // stacks[command.to].Push(crate);
                
                // part-two
                
                // cache the order
                queue.Push(crate);
            }
            
            // now put queue to other stack

            for (int i = 0; i < command.qty; ++i)
            {
                stacks[command.to].Push(queue.Pop());
            }
        }
        Console.WriteLine("------------");

        _PrintStacks();
        
        // take our top from each one
        string final = string.Empty;

        foreach (var stack in stacks)
        {
            if (stack.TryPop(out char c))
            {
                final += c;
            }
        }

        Console.WriteLine($"Final Top -> {final}");

        void _PrintStacks()
        {
            foreach (var item in stacks)
            {
                var whole = item.Reverse().ToArray();
                Console.Write("STACK -> ");
                foreach (var c in whole)
                {
                    Console.Write($"[{c}] ");
                }

                Console.WriteLine("");
            }
        }
        
        IEnumerable<(int qty, int from, int to)> _ProcessCommands(string[] input)
        {

            Regex reg = new Regex(@"([0-9]+)");
            (int qty, int from, int to)[] output = new (int qty, int from, int to)[input.Length];
            for (int i = 0; i < input.Length; ++i)
            {
                var matches = reg.Matches(input[i]);
                output[i].qty = int.Parse(matches[0].Value);
                output[i].from = int.Parse(matches[1].Value)-1;
                output[i].to = int.Parse(matches[2].Value)-1;
            }

            return output;
        }
        
    }
}