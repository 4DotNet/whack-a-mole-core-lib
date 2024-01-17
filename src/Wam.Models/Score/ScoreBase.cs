namespace Wam.Models.Score;

public abstract record ScoreBase : IWamModel
{
    protected ScoreBase(string id, DateTime timeStamp)
    {
        Id = id;
        TimeStamp = timeStamp;
    }
    public string Id { get; }
    public DateTime TimeStamp { get; }
}

public record UserScore : ScoreBase
{
    public UserScore(string id, DateTime timeStamp, string userId, int scoringValue) : base(id, timeStamp)
    {
        UserId = userId;
        ScoringValue = scoringValue;
    }
    public string UserId { get; }
    public int ScoringValue { get; }
}