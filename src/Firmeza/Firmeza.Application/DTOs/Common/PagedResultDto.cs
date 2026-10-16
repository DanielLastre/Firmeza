namespace Firmeza.Application.DTOs.Common;

public class PagedResultDto<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalRecords / PageSize) : 0;
    public IEnumerable<T> Data { get; set; } = [];

    public PagedResultDto()
    {
    }

    public PagedResultDto(IEnumerable<T> data, int totalRecords, int pageNumber, int pageSize)
    {
        Data = data;
        TotalRecords = totalRecords;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
