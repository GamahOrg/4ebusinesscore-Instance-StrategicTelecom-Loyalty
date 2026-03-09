namespace _4EPlatform.Application.Features.Categories.Query.GetCategoryById
{
    public class GetCategoryByIdDto
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionAEn { get; set; }
        public string Iconpath { get; set; }
        public Status? Status { get; set; }
        public ICollection<SubCategoryDto> SubCategories { get; set; } = new HashSet<SubCategoryDto>();
    }
    public class SubCategoryDto
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public Status? Status { get; set; }
    }
}
