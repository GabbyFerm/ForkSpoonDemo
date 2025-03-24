using System;
using System.Collections.Generic;

namespace ForkSpoonDemo.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
