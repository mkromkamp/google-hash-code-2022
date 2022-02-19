namespace HashCode.Parsers;

public class Output
{
    public void Write(Solutution solution, string fileName, string dir = "_output")
    {
        // TODO: Write output to file
        File.WriteAllLinesAsync(Path.Join(dir, fileName), new[] { "Some", "Solution" });
    }
}