namespace _4EPlatform.Application.Features.KPIs
{
    public class KPIsDTO
    {

        public int CountOfOffersAvailable { get; set; }
        public int CountOffersActivelyMonthly { get; set; }
        public int CountOffersInDiscountType { get; set; }
        public List<LookupIndicatorDTO> CountOfferInDiscountMethod { get; set; }
        public List<LookupIndicatorDTO> CountOfferInDiscountType { get; set; }
        public double AverageDiscountAmount { get; set; }
        public int CountOfferLikes { get; set; }
        public int CountServiceProviderLikes { get; set; }
        public int CountServiceProvider { get; set; }

        public CountCategoriesAndSubDTO countCategoriesAndSubDTO { get; set; }





    }
}
