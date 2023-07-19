namespace Hotelier.BackOffice.Models;

public class PaginationResponseModel
{
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public bool Previous { get; set; }
    public bool Next { get; set; }
    public string ControllerName { get; set; }
}