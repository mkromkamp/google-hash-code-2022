using HashCode;
using HashCode.Parsers;

Console.WriteLine("Hello, World!");

foreach (var file in Directory.EnumerateFiles("_input/"))
{
    Console.WriteLine($"Solving {file}");
    
    var challenge = new Input().Parse(file);
    var solution = new Solution().SolveMartin(challenge);
    
    Console.WriteLine($"Estimated score {solution.Score()}");
    await new Output().Write(solution, Path.GetFileName(file));
}

Console.WriteLine("All done :)");