﻿namespace DXMauiApp.Data;

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public ApplicationUser Author { get; set; }
}
