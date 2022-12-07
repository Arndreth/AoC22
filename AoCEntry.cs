using AoC22.DayLogic;

// Request which day we're running
Console.Write("Which Day are we running with -> ");
var dayRequest = Console.ReadLine();

if (int.TryParse(dayRequest, out var day))
{
    // pad out the day
    string dayName = $"{day:D2}";

    var dayLogic = FindDay(dayName);

    dayLogic.PartOne();
    dayLogic.PartTwo();
}
else
{
    Console.WriteLine("Invalid input");
}

BaseDay FindDay(string name)
{
    var dayQualifier = Type.GetType($"AoC22.DayLogic.Day{name}");
    if (dayQualifier == null)
    {
        throw new Exception(
            $"[AOC-Exception] Cannot create instance of type `Day{name}` as it hasn't been defined");
    }
    return (BaseDay)Activator.CreateInstance(dayQualifier)!;
}