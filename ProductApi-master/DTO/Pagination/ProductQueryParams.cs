namespace ProductApi.DTO.Pagination
{
    public class ProductQueryParams
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        //Filtering porameters and searching parameters
        public string? Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        //sorting parameters
        public string SortBy { get; set; } = "CreatedAt"; 
        public bool IsSortDescending { get; set; } = true;
    }
}
