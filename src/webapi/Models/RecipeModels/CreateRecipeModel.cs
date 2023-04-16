namespace webapi;
public class CreateRecipeModel
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Type { get; set; }
    public string? NationalKitch { get; set; }
    public List<string>? Ingredients { get; set; }
    public string WhoAdded { get; set; }
}