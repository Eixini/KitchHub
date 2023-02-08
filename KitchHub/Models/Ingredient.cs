using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KitchHub;

public partial class Ingredient
{
    public long IngredientId { get; set; }

    public string Name { get; set; } = null!;

    public IngredientsType IngredientsType { get; set; } = null!;

	public virtual ICollection<Recipe>? Recipes { get; set; }
}
