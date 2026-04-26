namespace _4EPlatform.Application.Features.SubCategories.Command.Post
{
    public class PostSubCategoryDTO
    {
        public Guid CategoryId { get; set; }
        [PreventInjectionHTML]
        [PreventInjectionCSV]
        [PreventInjectionScript]
        public required string NameAr { get; set; }
        [PreventInjectionHTML]
        [PreventInjectionCSV]
        [PreventInjectionScript]
        public required string NameEn { get; set; }
        public Status Status { get; set; }
    }
}
