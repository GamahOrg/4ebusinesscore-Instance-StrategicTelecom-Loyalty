namespace _4EPlatform.Application.Features.Categories.Query.GetAllCategories
{
    public class GetAllCategoriesDto
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionAEn { get; set; }
        public string Iconpath { get; set; }
        public Status Status { get; set; }
        public int SubCategoriesCount { get; set; }
    }
}
