namespace Extensions.Attributes;

public abstract class EpisodeData
{
    public virtual string Title { get; set; }
    public virtual int Season { get; set; }
    public virtual int NumberOfEpisodes { get; set; }
    public virtual string Writer { get; set; }
}

[SeasonNumber(11)]
public class SeasonElevenData : EpisodeData
{
    [CsvPosition(1)]
    public override string Title { get; set; }
}

[SeasonNumber(12)]
public class SeasonTwelveData : EpisodeData
{
    [CsvPosition(2)]
    public override string Title { get; set; }
}