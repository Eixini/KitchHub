using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KitchHub;

public class Recipe
{
    public long RecipeId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DishType? DishType { get; set; }

    public NationalKitch? NationalKitch { get; set; }

    public virtual ICollection<Ingredient>? Ingredients { get; set; }

}
