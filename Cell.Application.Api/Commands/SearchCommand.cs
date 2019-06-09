namespace Cell.Application.Api.Commands
{
    public class SearchCommand
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string[] Sorts { get; set; }
        public string Query { get; set; }
    }
}