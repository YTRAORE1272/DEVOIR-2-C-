namespace GesBanqueAspNet.ViewModels
{
    public class PaginationViewModel
    {
        public int PageEncours { get; set; }
        public int NbrePage { get; set; }
        public string ActionName { get; set; } = string.Empty;
        public Dictionary<string, string?> QueryParams { get; set; } = new();
    }
}
