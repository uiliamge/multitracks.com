namespace Domain
{
    public class PaginationParameters
    {
        private int _itemsPerPage = 100;
        
        public string Search { get; set; }
        
        public int ItemsPerPage
        {
            get => _itemsPerPage;
            set => _itemsPerPage = value;
        }

        public int Page { get; set; } = 1;
        public string SortColumn { get; set; }
        public OrderByEnum SortDirection { get; set; }
    }
}
