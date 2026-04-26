namespace _4EPlatform.Application.Features.Command
{
    public class ServiceProviderDTO
    {
        [PreventInjectionHTML]
        [PreventInjectionCSV]
        [PreventInjectionScript]
        public required string TitleAr { get; set; }
        [PreventInjectionHTML]
        [PreventInjectionCSV]
        [PreventInjectionScript]
        public required string TitleEn { get; set; }
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
        public required string Location { get; set; }
        public required string Email { get; set; }
        public required string Website { get; set; }
        public required string Mobile { get; set; }
        public List<Guid>? Ids { get; set; }
        public IFormFile? Image { get; set; }
        public Status Status { get; set; }
        public virtual ICollection<IFormFile> ServiceProviderImages { get; set; } = new HashSet<IFormFile>();

    }
}
