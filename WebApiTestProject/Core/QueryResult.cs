namespace WebApiTestProject.Core
{
    public class QueryResult<T>
    {
        public int TotalItems { get; set; }

        public int Page { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
