namespace Extensions.Attributes;

public class CsvPosition : Attribute
{
    public int Position { get; set; }
    
    public CsvPosition(int position)
    {
        Position = position;
    }
}