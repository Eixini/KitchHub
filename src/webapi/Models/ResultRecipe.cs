namespace webapi.Models;

public class ResultRecipe
{
    public long RecipeId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? DishType { get; set; }

    public string? NationalKitch { get; set; }

    public List<string>? Ingredients { get; set; }
    public long? IssuerID { get; set; }

    public DateTime DateAdded { get; set; }
    public DateTime DateUpdated { get; set; }

}
