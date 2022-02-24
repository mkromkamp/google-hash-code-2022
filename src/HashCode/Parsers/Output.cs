using System.Net;

namespace HashCode.Parsers;

public class Output
{
    public async Task Write(Solution solution, string fileName, string dir = "_output")
    {
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        // TODO: Write output to file
        var outputFileName = Path.Join(dir, fileName);
        await File.WriteAllLinesAsync(outputFileName, OutputUtil.OutputToListOfLines(solution.Projects));
    }
}