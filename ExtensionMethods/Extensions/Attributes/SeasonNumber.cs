namespace Extensions.Attributes;

public class SeasonNumber : Attribute
{
    public int Season { get; set; }
    
    public SeasonNumber(int season)
    {
        Season = season;
    }
}