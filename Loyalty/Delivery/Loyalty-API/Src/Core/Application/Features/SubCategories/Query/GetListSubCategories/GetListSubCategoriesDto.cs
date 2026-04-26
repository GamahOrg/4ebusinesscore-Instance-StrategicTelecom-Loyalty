namespace _4EPlatform.Application.Features.SubCategories.Query.GetListSubCategories
{
    public class GetListSubCategoriesDto
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public Status? Status { get; set; }
        public int OfferCount { get; set; }
    }
}
