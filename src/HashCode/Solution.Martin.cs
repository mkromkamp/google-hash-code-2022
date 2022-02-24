using HashCode.Parsers;

namespace HashCode;

public partial class Solution
{
    public Solution SolveMartin(Challenge challenge)
    {
        var projects = new Dictionary<string, List<string>>();
        
        foreach (var project in ProjectOrderingUtil.OrderProjectByPPDPR(challenge.Projects))
        {
            var contributors = new List<string>();
            foreach (var skill in project.RequiredSkills)
            {
                foreach (var contributor in challenge.Contributors)
                {
                    if (contributor.Skills.Any(s => s.Name.Equals(skill.Name))
                        && contributor.Skills.First(s => s.Name.Equals(skill.Name)).Level > skill.Level + 1)
                    {
                        contributors.Add(contributor.Name);
                        break;
                    }
                }
            }
            
            projects.Add(project.Name, contributors);
        }

        return new Solution
        {
            Projects = new Dictionary<string, List<string>>()
        };
    }

    private Input.Contributor GetMentor(Input.Project project, List<Input.Contributor> contributors)
    {
        return null;
    }

    private List<Input.Contributor> GetMentee(Input.Project project, List<Input.Contributor> contributors)
    {
        return new();
    }

    private List<Input.Contributor> GetFillers(Input.Project project, List<Input.Contributor> contributors)
    {
        return new();
    }
}