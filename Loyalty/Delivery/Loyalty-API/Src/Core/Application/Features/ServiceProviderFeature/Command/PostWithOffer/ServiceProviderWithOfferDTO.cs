namespace _4EPlatform.Application.Features.ServiceProviderFeature.Command.PostWithOffer
{
    public class ServiceProviderWithOfferDTO
    {
        [PreventInjectionHTML]
        [PreventInjectionCSV]
        [PreventInjectionScript]
        public string TitleAr { get; set; }
        [PreventInjectionHTML]
        [PreventInjectionCSV]
        [PreventInjectionScript]
        public string TitleEn { get; set; }
        [PreventInjectionHTML]
        [PreventInjectionCSV]
        [PreventInjectionScript]
        public string NameAr { get; set; }
        [PreventInjectionHTML]
        [PreventInjectionCSV]
        [PreventInjectionScript]
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionAEn { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Mobile { get; set; }

        public IFormFile Image { get; set; }

        public virtual ICollection<IFormFile> ServiceProviderImages { get; set; } = new HashSet<IFormFile>();

        //offer

        public ServiceProviderOfferDTO offerDTO { get; set; }

    }




}
