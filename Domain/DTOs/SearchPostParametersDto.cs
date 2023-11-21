namespace Domain.DTOs;

public class SearchPostParametersDto
{
    public string? Title { get; }
    public string? OwnerUsername { get; }
    public int? OwnerId { get; }

    public SearchPostParametersDto(string? title)
    {
        Title = title;
    }
}