using DTO.Enums;

public class PaginationModel
{
    public int PageSize { get; set; } = 15;
    public int CurrentPage { get; set; } = 1;
}