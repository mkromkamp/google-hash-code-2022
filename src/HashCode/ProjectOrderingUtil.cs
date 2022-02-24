using static HashCode.Parsers.Input;

namespace HashCode
{
    public static class ProjectOrderingUtil
    {
        public static List<Project> OrderProjectByPPDPR(List<Project> inputProjects)
        {
            List<PPDPRProjectScoringObj> building = new List<PPDPRProjectScoringObj>();

            foreach(var project in inputProjects)
            {
                building.Add(new PPDPRProjectScoringObj()
                {
                    Score = GetPPDPR(project),
                    Project = project
                });
            }

            return building.OrderByDescending(a => a.Score).Select(a => a.Project).ToList();

        }

        private static double GetPPDPR(Project p)
        {
            return (p.Points / p.DaysToComplete) /(double)p.RequiredSkills.Count();
        }
    }

    public class PPDPRProjectScoringObj
    {
        public double Score { get; set; }
        public Project Project { get; set; }
    }
}
