namespace _4EPlatform.Application.Mappings
{
    public partial class MappingProfile
    {
        public void SubCategoryMapping()
        {
            CreateMap<PostSubCategoryDTO, SubCategory>();
        }
    }
}
