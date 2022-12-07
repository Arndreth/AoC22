using System.Collections;

namespace AoC22.DayLogic;

public class Day02 : BaseDay
{
    private int _draw = 3;
    private int _loss = 0;
    private int _win = 6;

    private int _rock = 1;
    private int _paper = 2;
    private int _scissors = 3;

    private string _firstInput = "ABC";
    private string _secondInput = "XYZ";
    public override void PartOne()
    {
        List<string> input = new List<string>(ReadInput<string>());

        int[,] scoring = new int[3, 3];

        scoring[0, 0] = _draw + _rock;
        scoring[0, 1] = _win + _paper;
        scoring[0, 2] = _loss + _scissors;

        scoring[1, 0] = _loss + _rock;
        scoring[1, 1] = _draw + _paper;
        scoring[1, 2] = _win + _scissors;

        scoring[2, 0] = _win + _rock;
        scoring[2, 1] = _loss + _paper;
        scoring[2, 2] = _draw + _scissors;

        string[] mod = 
        {
            "ZXY",
            "XYZ",
            "YZX",
        };
        
        int score = 0;

        int thrownScore = 0;
        input.ForEach(x =>
        {
            var game = x.Split(' ');
            score += _GetScore(game[0], game[1]);
            
            // part two.
            // change our second input based on first input
            game[1] = mod[_firstInput.IndexOf(game[0], StringComparison.Ordinal)][_secondInput.IndexOf(game[1], StringComparison.Ordinal)].ToString();
            thrownScore += _GetScore(game[0], game[1]);
        });

        Console.WriteLine($"Final Score? {score}");
        Console.WriteLine($"Thrown Score? {thrownScore}");

        int _GetScore(string a, string b)
        {
            int s =  scoring[_firstInput.IndexOf(a, StringComparison.Ordinal), _secondInput.IndexOf(b, StringComparison.Ordinal)];
            Console.WriteLine($"{a} vs {b} = {s}");
            return s;
        }
    }

}