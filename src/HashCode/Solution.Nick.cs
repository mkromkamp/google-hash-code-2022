using HashCode.Parsers;

namespace HashCode;

public partial class Solution
{
    private Dictionary<string, int> usersNumberOfProjects;
    public Solution SolveNick(Challenge challenge)
    {
        usersNumberOfProjects = new Dictionary<string, int>();
        foreach (var project in challenge.Projects)
        {
            if(project.RequiredSkills.Count() == 1)
            {
                // get person with minimum
                string personWithMinProjcsOnName = GetPersonWithSkillMinimumProjects(project.RequiredSkills[0].Name, project.RequiredSkills[0].Level, challenge, new List<string>());
                if (personWithMinProjcsOnName.Length >= 1)
                {
                    if (usersNumberOfProjects.TryGetValue(personWithMinProjcsOnName, out int count))
                    {
                        usersNumberOfProjects[personWithMinProjcsOnName]++;
                    }
                    else
                    {
                        usersNumberOfProjects[personWithMinProjcsOnName] = 1;
                    }
                    Projects.Add(project.Name, new List<string>() { personWithMinProjcsOnName });
                }
            }
            else
            {
                List<string> peopleOnThisProject = new List<string>();
                foreach (var skill in project.RequiredSkills)
                {
                    string personWithMinProjcsOnName = GetPersonWithSkillMinimumProjects(skill.Name, skill.Level, challenge, peopleOnThisProject);
                    if (personWithMinProjcsOnName.Length >= 1)
                    {
                        if (usersNumberOfProjects.TryGetValue(personWithMinProjcsOnName, out int count))
                        {
                            usersNumberOfProjects[personWithMinProjcsOnName]++;
                        }
                        else
                        {
                            usersNumberOfProjects[personWithMinProjcsOnName] = 1;
                        }

                        peopleOnThisProject.Add(personWithMinProjcsOnName);
                    }

                }

                if(peopleOnThisProject.Count() == project.RequiredSkills.Count)
                {
                    Projects.Add(project.Name, peopleOnThisProject);
                }
            }
        }
        return this;
    }

    public string GetPersonWithSkillMinimumProjects(string skill, int level, Challenge challenge, List<string> existingOnProjc)
    {
        int numProjectsCur = 1000000;
        string personCurrent = "";
        foreach(var user in challenge.Contributors)
        {
            if(existingOnProjc.Contains(user.Name))
            {
                continue;
            }

            if(user.Skills.Any(a => a.Name == skill && a.Level >= level))
            {
                int numProjectsCurrentUser = 0;
                if(usersNumberOfProjects.TryGetValue(user.Name, out int numProjects))
                {
                    numProjectsCurrentUser = numProjects;
                }
                if (numProjectsCur > numProjectsCurrentUser)
                {
                    personCurrent = user.Name;
                    numProjectsCur = numProjects;
                }
            }
        }
        return personCurrent;
    }
}