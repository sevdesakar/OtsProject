namespace Ots.Base;

public class BaseEntity
{
    public long Id { get; set; }
    public string InsertUser { get; set; }
    public DateTime InsertDate { get; set; }
    public string UpdateUser { get; set; }
    public DateTime UpdateDate { get; set; }
}
