using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Components.Pages;

public sealed class CreateEventFormInputModel
{
    public string Name { get; set; }
    
    public string Region { get; set; }
    
    public string Address { get; set; }
    
    public List<string> Categories { get; set; }
    
    public string Description { get; set; }
    
    [DataType(DataType.DateTime)]
    public DateTime StartTime { get; set; } = DateTime.Now;
}