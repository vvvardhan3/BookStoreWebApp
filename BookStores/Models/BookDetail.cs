using System;
using System.Collections.Generic;

namespace BookStores.Models;

public partial class BookDetail
{
    public string Title { get; set; } = null!;

    public string AuthorName { get; set; } = null!;

    public string Isbn { get; set; } = null!;

    public decimal Cost { get; set; }

    public string Genre { get; set; } = null!;
}
