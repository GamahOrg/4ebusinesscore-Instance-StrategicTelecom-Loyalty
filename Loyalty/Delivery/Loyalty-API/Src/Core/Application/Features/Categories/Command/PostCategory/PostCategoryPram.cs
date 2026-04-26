


using _4EPlatform.Application.CustomValidation;

namespace _4EPlatform.Application.Features.Categories.Command.PostCategory
{
    public class PostCategoryPram
    {
        [PreventInjectionHTML]
        [PreventInjectionCSV]
        [PreventInjectionScript]
        public required string NameAr { get; set; }
        [PreventInjectionHTML]
        [PreventInjectionCSV]
        [PreventInjectionScript]
        public required string NameEn { get; set; }
        public required string DescriptionAr { get; set; }
        public required string DescriptionAEn { get; set; }
        [PreventInjectionScriptFileAttribute]
        public required IFormFile IconImage { get; set; }

        public ICollection<SubCategoryPram> SubCategories { get; set; } = new HashSet<SubCategoryPram>();
    }
    public class SubCategoryPram
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }

    }
}
