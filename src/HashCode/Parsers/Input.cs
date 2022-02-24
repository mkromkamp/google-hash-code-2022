namespace HashCode.Parsers;

public class Input
{
    private int NumOfContributors;
    private int NumOfProjects;


    public Challenge Parse(string file)
    {
        // TODO; Parse input file and return Challenge

        var fileLines = File.ReadLines(file).ToArray();
        var challenge = new Challenge();


        var firstLine = fileLines[0].Split(' ').ToList();
        NumOfContributors = int.Parse(firstLine[0]);
        NumOfProjects = int.Parse(firstLine[1]);

        int currentLine = 0;
        
        // interate over contributors
        for (int i = 0; i < NumOfContributors; i++)
        {
            currentLine++;
            var contriLine = fileLines[currentLine].Split(' ');

            var skills = new List<Skill>();
            var name = contriLine[0];
            var numOfSkills = int.Parse(contriLine[1]);

            for (int j = 0; j < numOfSkills; j++)
            {
                currentLine++;

                var skillLine = fileLines[currentLine].Split(' ');
                skills.Add(new Skill() { Name =  skillLine[0], Level = int.Parse(skillLine[1])});
            }


            challenge.Contributors.Add(new Contributor()
            {
                Name = name,
                Skills = skills
            });
        }
        
        // interate over projects
        for (int i = 0; i < NumOfProjects; i++)
        {
            currentLine++;
            var projectLine = fileLines[currentLine].Split(' ');
            
            var skills = new List<Skill>();
            
            var name = projectLine[0];
            var daysToComplete = int.Parse(projectLine[1]);
            var points = int.Parse(projectLine[2]);
            var daysBeforeBestBefore = int.Parse(projectLine[3]);
            var numOfSkills = int.Parse(projectLine[4]);
            
            for (int j = 0; j < numOfSkills; j++)
            {
                currentLine++;

                var skillLine = fileLines[currentLine].Split(' ');
                skills.Add(new Skill() { Name =  skillLine[0], Level = int.Parse(skillLine[1])});
            }

            challenge.Projects.Add(new Project()
            {
                Name = name,
                DaysToComplete = daysToComplete,
                Points = points,
                DaysBeforeBestBefore = daysBeforeBestBefore,
                RequiredSkills = skills
            });
        }


        return challenge;
    }


    public class Skill
    {
        public string Name { get; set; }
        public int Level { get; set; }
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