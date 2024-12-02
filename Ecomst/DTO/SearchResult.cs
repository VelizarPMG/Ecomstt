namespace Ecomst.DTO
{
    public class SearchResult<T>
    {
        public int Start {  get; set; }
        public int TotalPages {  get; set; }
        public int RecordsTotal {  get; set; }
        public int RecordsFiltered { get; set; }
        public List<T> Data { get; set; }
    }
}
