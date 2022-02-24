using System.Collections.Generic;
using Xunit;
using static HashCode.Parsers.Input;

namespace HashCode.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {

    }

    [Fact]
    public void TestOrderingPPDPRWorks1()
    {
        List<Project> inputs = new List<Project>();
        inputs.Add(new Project()
        {
            Name = "1",
            DaysToComplete = 10,
            Points = 50,
            RequiredSkills = new List<Skill>()
            {
                new Skill()
                {

                },
                new Skill()
                {

                }
            }
        });

        inputs.Add(new Project()
        {
            Name="2",
            DaysToComplete = 5,
            Points = 15,
            DaysBeforeBestBefore = 6,
            RequiredSkills = new List<Skill>()
            {
                new Skill()
                { }
            }
        });
        var result = ProjectOrderingUtil.OrderProjectByPPDPR(inputs);
        Assert.Equal("2", result[0].Name);
    }
}