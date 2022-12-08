using System.Security.AccessControl;
using AoC22.Interfaces;

namespace AoC22.DayLogic;

public abstract class BaseDay : IDay
{
    public abstract void PartOne();

    // Making virtual, sometimes part-two is incorporated into part-one
    public virtual void PartTwo()
    {
    }

    protected string RawInput(bool testInput=false)
    {
        var fullName = this.GetType().FullName;
        var day = fullName?.Substring(fullName.Length - 2, 2);
        var path =(testInput ? "Test" : string.Empty) + $"Inputs/day_{day}.txt";

        if (File.Exists(path))
        {
            return File.ReadAllText(path);
        }
        
        
        throw new FileNotFoundException($"Cannot find input data for day {day}");
    }
    protected T[] ReadInput<T>(bool testInput=false)
    {
        // get our day name
        var fullName = this.GetType().FullName;
        var day = fullName?.Substring(fullName.Length - 2, 2);
        var path = (testInput ? "Test" : string.Empty) + $"Inputs/day_{day}.txt";

        if (File.Exists(path))
        {
            var temp =  File.ReadAllLines(path);

            T[] final = new T[temp.Length];
            for (int i = 0; i < temp.Length; ++i)
            {
                final[i] = (T)Convert.ChangeType(temp[i], typeof(T));
            }

            return final;
        }

        throw new FileNotFoundException($"Cannot find input data for day {day}");
    }
}