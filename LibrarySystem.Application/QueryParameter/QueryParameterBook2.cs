namespace LibrarySystem.Application.QueryParameter
{
    public class QueryParameterBook2
    {
        public string Title { get; set; }
        public string LogicOperator1 { get; set; }
        public string Author { get; set; }
        public string LogicOperator2 { get; set; }
        public string Category { get; set; }
        public string LogicOperator3 { get; set; }
        public string ISBN { get; set; }
        public string Language { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set;} =1;
    }

}
