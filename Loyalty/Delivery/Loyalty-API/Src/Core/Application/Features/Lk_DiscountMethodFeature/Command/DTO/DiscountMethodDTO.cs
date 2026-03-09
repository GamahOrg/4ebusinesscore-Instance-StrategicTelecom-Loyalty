namespace _4EPlatform.Application.Features.Lk_DiscountMethodFeature.Command.DTO
{
    public record DiscountMethodDTO
    {
        [PreventInjectionHTML]
        [PreventInjectionCSV]
        [PreventInjectionScript]
        public string NameAr { get; init; }
        [PreventInjectionHTML]
        [PreventInjectionCSV]
        [PreventInjectionScript]
        public string NameEn { get; init; }
    }
}
