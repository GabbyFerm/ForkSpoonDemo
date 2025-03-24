using System;
using System.Collections.Generic;

namespace ForkSpoonDemo.Models;

public partial class Recipe
{
    public int RecipeId { get; set; }

    public string Title { get; set; } = null!;

    public string Ingredients { get; set; } = null!;

    public string Steps { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public int CreatedBy { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
}
