using HashCode.Parsers;

namespace HashCode;

public partial class Solution
{
    public Solution SolveMartin(Challenge challenge)
    {
        var projects = new Dictionary<string, List<string>>();

        foreach (var project in challenge.Projects.OrderBy(p => p.DaysBeforeBestBefore))
        {
            // Try to fill all required skills, if not possible skip project
            if (!TryGetContributors(project.RequiredSkills, challenge.Contributors, out var contributors)
                || contributors.Count < project.RequiredSkills.Count)
                continue;

            // // Try to find a mentor, if not available ignore the project
            // if (!TryGetMentor(project.RequiredSkills, challenge.Contributors, out var mentor))
            //     continue;
            //
            // // Add contributor
            // contributors.Add(mentor.Item1.Name);
            //
            // // If we need more than one skill we need to find for mentee
            // if (project.RequiredSkills.Count > 1)
            // {
            //     // Don´t look for the skill of the mentor
            //     var remainingSkills = project.RequiredSkills.Where(s => !s.Name.Equals(mentor.Item2.Name)).ToList();
            //     var remainingContributor = challenge.Contributors.Where(s => !s.Name.Equals(mentor.Item1.Name)).ToList();
            //
            //     // Look to fill remaining roles with mentee
            //     // If we don´t find a mentee for all remaining roles skip the project
            //     if (!GetMentee(remainingSkills, remainingContributor, out var mentee)
            //         || mentee.Count != project.RequiredSkills.Count-1)
            //         continue;
            //
            //     contributors.AddRange(mentee.Select(m => m.Name));
            // }
            
            projects.Add(project.Name, contributors.Select(c => c.Name).ToList());
        }

        return new Solution
        {
            Projects = projects
        };
    }
    
    private bool TryGetContributors(List<Input.Skill> skills, List<Input.Contributor> contributors, out List<Input.Contributor> result)
    {
        result = new();

        var skillNames = skills.Select(s => s.Name).ToList();
        // Order potential contributor by number of matching skills
        var ordered = contributors
            .OrderBy(c =>
            {
                // Matching skill count
                return c.Skills.Count(s => skillNames.Contains(s.Name));
            })
            .Select(c => c)
            .ToList();

        // Remaining skills
        var remainingSkills = skills;
        var remainingContributors = ordered;
        while (remainingSkills.Any())
        {
            var foundMatch = false;
            foreach (var contributor in remainingContributors)
            {
                var firstSkill = remainingSkills.First();
                var matchingSkill = contributor.Skills.FirstOrDefault(s =>
                    s.Name.Equals(firstSkill.Name) && s.Level >= firstSkill.Level);
                
                // Find if this contributor has the ability to be mentor for the first skill
                if (matchingSkill is null)
                    continue;
                
                remainingSkills.Remove(firstSkill);
                remainingContributors.Remove(contributor);
                result.Add(contributor); // Add or ignore if already added
                foundMatch = true;
                break;
            }

            if (!foundMatch)
                break;
        }

        if (remainingSkills.Any())
            result = new();

        return result.Any();
    }

    private bool TryGetMentor(List<Input.Skill> skills, List<Input.Contributor> contributors, out (Input.Contributor?, Input.Skill) mentor)
    {
        foreach (var requiredSkill in skills)
        {
            foreach (var contributor in contributors)
            {
                var availableSkill = contributor.Skills.FirstOrDefault(s => s.Name.Equals(requiredSkill.Name));

                // Skip if no matching skill
                if (availableSkill is null)
                    continue;

                // If skill is higher than required, become mentor
                if (availableSkill.Level >= requiredSkill.Level)
                {
                    mentor = (contributor, availableSkill);
                    return true;
                }
            }
        }

        mentor = (null, null);
        return false;
    }

    private bool GetMentee(List<Input.Skill> skills, List<Input.Contributor> contributors, out List<Input.Contributor> mentee)
    {
        mentee = new();
        foreach (var requiredSkill in skills)
        {
            foreach (var contributor in contributors)
            {
                var availableSkill = contributor.Skills.FirstOrDefault(s => s.Name.Equals(requiredSkill.Name));

                // Skip if no matching skill
                if (availableSkill is null)
                    continue;

                // If skill is equal or less than required, become mentee
                if (availableSkill.Level >= requiredSkill.Level)
                {
                    mentee.Add(contributor);
                }
            }
        }

        return mentee.Any();
    }
}