namespace ProductApi.DTO.Pagination
{
    public class OrderQueryParams
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        // filtri
        public string? CustomerName { get; set; }
        public string? Status { get; set; }
        public decimal? MinTotal { get; set; }
        public decimal? MaxTotal { get; set; }

        public string SortBy { get; set; } = "createddate";
        public bool IsSortDescending { get; set; } = true;
    }
}
