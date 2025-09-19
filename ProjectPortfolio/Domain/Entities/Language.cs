namespace ProjectPortfolio.Domain.Entities;

public class Language
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ImgUrl { get; set; }
    public int Id_Project { get; set; }
}
