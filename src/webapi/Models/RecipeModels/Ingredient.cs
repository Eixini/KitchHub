using System.ComponentModel.DataAnnotations.Schema;

namespace webapi;

public partial class Ingredient
{
    public long IngredientId { get; set; }

    public string Name { get; set; } = null!;

    [ForeignKey("IngredientsTypeId")]
    public IngredientsType IngredientsType { get; set; } = null!;
    public long? IngredientsTypeId { get; set; }
    public virtual ICollection<Recipe>? Recipes { get; set; }
}