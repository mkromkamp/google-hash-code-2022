namespace HashCode.Parsers;

public class Input
{
    private int NumOfContributors;
    private int NumOfProjects;

    public Challenge Parse(string file)
    {
        // TODO; Parse input file and return Challenge

        var fileLines = File.ReadLines(file).ToArray();
        int currentLine = 0;

        var firstLine = fileLines[0];
        firstLine.Split(' ').ToList();
        NumOfContributors = firstLine[0];
        NumOfProjects = firstLine[1];
        


        return new();
    }


    public class Skill
    {
        public string Name { get; set; }
        public string Level { get; set; }
    }

    public class Contributor
    {
        public string Name { get; set; }
        public List<Skill> Skills { get; set; }
    }

    public class Project
    {
        public string Name { get; set; }
        public int DaysToComplete { get; set; }
        public int Points { get; set; } //means prizes
        public int DaysBeforeBestBefore { get; set; }
        public List<Skill> RequiredSkills { get; set; }
    }
}