namespace _4EPlatform.Application.Mappings
{
    public partial class MappingProfile
    {
        public void CategoryMapping()
        {
            CreateMap<PostCategoryPram, Category>();
            CreateMap<SubCategoryPram, SubCategory>();
            CreateMap<PutCategoryPram, Category>();
            CreateMap<PutSubCategoryPram, SubCategory>();
            CreateMap<Category, GetCategoryDTO>();
        }
    }
}
