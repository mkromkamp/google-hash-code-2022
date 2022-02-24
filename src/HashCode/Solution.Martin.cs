using HashCode.Parsers;

namespace HashCode;

public partial class Solution
{
    public Solution SolveMartin(Challenge challenge)
    {
        var projects = new Dictionary<string, List<string>>();
        
        foreach (var project in challenge.Projects)
        {
            var contributors = new List<string>();
            
            // Try to find a mentor, if not available ignore the project
            if (!TryGetMentor(project.RequiredSkills, challenge.Contributors, out var mentor))
                continue;
            
            // Add contributor
            contributors.Add(mentor.Item1.Name);

            // If we need more than one skill we need to find for mentee
            if (project.RequiredSkills.Count > 1)
            {
                // Don´t look for the skill of the mentor
                var remainingSkills = project.RequiredSkills.Where(s => !s.Name.Equals(mentor.Item2.Name)).ToList();
                var remainingContributor = challenge.Contributors.Where(s => s.Name.Equals(mentor.Item1.Name)).ToList();

                // Look to fill remaining roles with mentee
                // If we don´t find a mentee for all remaining roles skip the project
                if (!GetMentee(remainingSkills, remainingContributor, out var mentee)
                    || mentee.Count != project.RequiredSkills.Count-1)
                    continue;

                contributors.AddRange(mentee.Select(m => m.Name));
            }
            
            projects.Add(project.Name, contributors);
        }

        return new Solution
        {
            Projects = projects
        };
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
                if (availableSkill.Level < requiredSkill.Level)
                {
                    mentee.Add(contributor);
                }
            }
        }

        return mentee.Any();
    }
}