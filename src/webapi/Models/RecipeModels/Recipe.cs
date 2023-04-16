using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi;

public class Recipe
{
    public long RecipeId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DishType? DishType { get; set; }

    public NationalKitch? NationalKitch { get; set; }

    public virtual ICollection<Ingredient>? Ingredients { get; set; }
    [ForeignKey("IssuerId")]
    public User? WhoAdded { get; set; }
    public long? IssuerId { get; set; }

    public DateTime? DateAdded { get; set; }
    public DateTime? DateUpdated { get; set; }
    public bool Published { get; set; }

}
