namespace HashCode;

public partial class Solution
{
    /// <summary>
    /// Project name + Contributor names
    /// </summary>
    public Dictionary<string, List<string>> Projects { get; set; } = new();

    public virtual Solution SolveChallenge(Challenge challenge)
    {
        return new();
    }

    public long Score()
    {
        return 0;
    }
}
