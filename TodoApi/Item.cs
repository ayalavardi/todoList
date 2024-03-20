using System;
using System.Collections.Generic;

namespace TodoApi;

public partial class Item
{
    // static int count=100;
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? IsCompliete { get; set; }
    public Item(string Name)
    {
        // count++;
        // this.Id=count;
        this.Name=Name;
        this.IsCompliete=false;
    }
}
