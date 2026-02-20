namespace SIADAL.Models.DTOs
{
    public class PaginatedResultDTO<T>
    {
        public required List<T> Items { get; set; } = new List<T>();
        public required int CurrentPage { get; set; }
        public required int PerPage { get; set; }
        public required int TotalItems { get; set; }
        public required int TotalPages { get; set; }
        public int? PreviousPage { get; set; }
        public int? NextPage { get; set; }
    }
}
