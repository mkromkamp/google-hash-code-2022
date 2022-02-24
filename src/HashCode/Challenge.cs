using HashCode.Parsers;

namespace HashCode;

public partial class Challenge
{
    public List<Input.Project> Projects { get; set; } = new();

    public List<Input.Contributor> Contributors { get; set; } = new();
}