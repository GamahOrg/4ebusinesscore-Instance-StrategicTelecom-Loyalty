

using _4EPlatform.Application.CustomValidation;

namespace _4EPlatform.Application.Features.Categories.Command.PutCategory
{
    public class PutCategoryPram
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionAEn { get; set; }
        [PreventInjectionScriptFileAttribute]
        public IFormFile? IconImage { get; set; }

        public ICollection<PutSubCategoryPram> SubCategoryPram { get; set; } = new HashSet<PutSubCategoryPram>();
    }
    public class PutSubCategoryPram
    {
        public Guid? Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }

    }
}
