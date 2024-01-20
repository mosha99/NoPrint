namespace NoPrint.Application.Infra;

public class ListResult<T>
{
    public ListResult(List<T> list)
    {
        List = list;
        TotalCount = list.Count;
    }

    public ListResult()
    {
        
    }
    public List<T> List { get; set; }
    public int TotalCount { get; set; }
}