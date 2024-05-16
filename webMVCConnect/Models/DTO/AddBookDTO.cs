﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class AddBookDTO
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsRead { get; set; }
    public DateTime? DateRead { get; set; }

    [Range(0, 5, ErrorMessage = "Rate must be between 0 and 5")]
    public int? Rate { get; set; }

    public string? Genre { get; set; }
    public string? CoverUrl { get; set; }
    public DateTime DateAdded { get; set; }
    public int PublisherID { get; set; }
    public List<int>? AuthorIds { get; set; }
}
