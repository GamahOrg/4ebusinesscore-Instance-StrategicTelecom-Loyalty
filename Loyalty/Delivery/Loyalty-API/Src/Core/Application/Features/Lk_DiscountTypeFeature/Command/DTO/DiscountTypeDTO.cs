namespace _4EPlatform.Application.Features.Lk_DiscountTypeFeature.Command
{
    public record DiscountTypeDTO
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
