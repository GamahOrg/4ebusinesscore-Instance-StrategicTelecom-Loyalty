
namespace _4EPlatForm.Infrastructure.Context
{
    public class ContextSeed
    {
        private static AppDBContext? _dbContext;
        private static IServiceProvider _serviceProvider;
        private static IConfiguration _configuration;
        public static void Seed(AppDBContext appDbContext, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = appDbContext;
            _dbContext.Database.EnsureCreated();
            _serviceProvider = serviceProvider;

            SeedLK_DiscountType();
            SeedLK_DiscountMethod();

            var defaultDataEnabled = _configuration.GetValue<bool>("IsSeedData");

            if (defaultDataEnabled)
            {
                Seed_Categories();
                Seed_SubCategories();
                Seed_ServiceProvider();
                Seed_ServiceProviderLike();
                Seed_Offer();
                Seed_OfferLike();



            }




        }
        private static void SeedLK_DiscountType()
        {
            try
            {

                string LK_DiscountTypeJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "LKDiscountType_SeedData.json");
                LK_DiscountTypeJson = LK_DiscountTypeJson.Replace("\u00A0", " ");
                var LK_DiscountTypes = JsonConvert.DeserializeObject<List<Lk_DiscountType>>(LK_DiscountTypeJson);

                if (LK_DiscountTypes != null && LK_DiscountTypes.Any())
                    if (LK_DiscountTypes != null && LK_DiscountTypes.Any())
                    {
                        _dbContext.Database.OpenConnection();
                        try
                        {
                            if (!_dbContext.Lk_DiscountTypes.Any())
                            {
                                foreach (var LK_IdentityType in LK_DiscountTypes)
                                {
                                    LK_IdentityType.CreatedOn = DateTime.Now;
                                }

                                _dbContext.Lk_DiscountTypes.AddRange(LK_DiscountTypes);
                                _dbContext.SaveChanges();
                            }
                            else
                            {
                                var existingLK_IdentityTypes = _dbContext.Lk_DiscountTypes.ToList();
                                var newLK_IdentityTypes = LK_DiscountTypes
                                    .Where(na => existingLK_IdentityTypes.Any(ea => ea.NameEn == na.NameEn))
                                    .ToList();

                                if (!newLK_IdentityTypes.Any())
                                {
                                    foreach (var Agreement in newLK_IdentityTypes)
                                    {
                                        Agreement.CreatedOn = DateTime.Now;
                                    }

                                    _dbContext.Lk_DiscountTypes.AddRange(newLK_IdentityTypes);
                                    _dbContext.SaveChanges();
                                }
                            }
                        }
                        finally
                        {
                            _dbContext.Database.CloseConnection();
                        }
                    }
            }
            catch
            {

            }

        }
        private static void SeedLK_DiscountMethod()
        {
            try
            {

                string LK_DiscountMethodJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "LKDiscountMethod_SeedData.json");
                LK_DiscountMethodJson = LK_DiscountMethodJson.Replace("\u00A0", " ");
                var LK_DiscountMethods = JsonConvert.DeserializeObject<List<Lk_DiscountMethod>>(LK_DiscountMethodJson);

                if (LK_DiscountMethods != null && LK_DiscountMethods.Any())
                    if (LK_DiscountMethods != null && LK_DiscountMethods.Any())
                    {
                        _dbContext.Database.OpenConnection();
                        try
                        {
                            if (!_dbContext.Lk_DiscountMethods.Any())
                            {
                                foreach (var LK_IdentityType in LK_DiscountMethods)
                                {
                                    LK_IdentityType.CreatedOn = DateTime.Now;
                                }

                                _dbContext.Lk_DiscountMethods.AddRange(LK_DiscountMethods);
                                _dbContext.SaveChanges();
                            }
                            else
                            {
                                var existingLK_IdentityTypes = _dbContext.Lk_DiscountMethods.ToList();
                                var newLK_IdentityTypes = LK_DiscountMethods
                                    .Where(na => !existingLK_IdentityTypes.Any(ea => ea.NameEn == na.NameEn))
                                    .ToList();

                                if (newLK_IdentityTypes.Any())
                                {
                                    foreach (var Agreement in newLK_IdentityTypes)
                                    {
                                        Agreement.CreatedOn = DateTime.Now;
                                    }

                                    _dbContext.Lk_DiscountMethods.AddRange(newLK_IdentityTypes);
                                    _dbContext.SaveChanges();
                                }
                            }
                        }
                        finally
                        {
                            _dbContext.Database.CloseConnection();
                        }
                    }
            }
            catch
            {

            }

        }
        private static void Seed_Categories()
        {

            try
            {

                string CategoriesJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "Category_SeedData.json");
                var CategoriesJsonypes = JsonConvert.DeserializeObject<List<Category>>(CategoriesJson);

                if (CategoriesJsonypes != null && CategoriesJsonypes.Any())
                {
                    _dbContext.Database.OpenConnection();
                    try
                    {
                        //  _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Categories] ON");
                        if (!_dbContext.Categories.Any())
                        {
                            foreach (var LK_IntegrationType in CategoriesJsonypes)
                            {
                                LK_IntegrationType.CreatedOn = DateTime.Now;
                            }

                            _dbContext.Categories.AddRange(CategoriesJsonypes);

                            _dbContext.SaveChanges();
                        }

                    }
                    finally
                    {
                        _dbContext.Database.CloseConnection();
                    }
                }
            }
            catch
            {

            }
        }

        private static void Seed_SubCategories()
        {
            try
            {

                string SubCategoriesJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "SubCategory_SeedData.json");
                var SubCategoriesJsonTypes = JsonConvert.DeserializeObject<List<SubCategory>>(SubCategoriesJson);

                if (SubCategoriesJsonTypes != null && SubCategoriesJsonTypes.Any())
                {
                    _dbContext.Database.OpenConnection();
                    try
                    {
                        //  _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Categories] ON");
                        if (!_dbContext.SubCategories.Any())
                        {
                            foreach (var LK_IntegrationType in SubCategoriesJsonTypes)
                            {
                                LK_IntegrationType.CreatedOn = DateTime.Now;
                            }

                            _dbContext.SubCategories.AddRange(SubCategoriesJsonTypes);

                            _dbContext.SaveChanges();
                        }

                    }
                    finally
                    {
                        _dbContext.Database.CloseConnection();
                    }
                }
            }
            catch
            {

            }
        }
        private static void Seed_ServiceProvider()
        {
            try
            {


                string ServiceProviderJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "ServiceProvider_SeedData.json");
                var ServiceProviderJsonTypes = JsonConvert.DeserializeObject<List<ServiceProvider>>(ServiceProviderJson);

                if (ServiceProviderJsonTypes != null && ServiceProviderJsonTypes.Any())
                {
                    _dbContext.Database.OpenConnection();
                    try
                    {
                        //  _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Categories] ON");
                        if (!_dbContext.SubCategories.Any())
                        {
                            foreach (var LK_IntegrationType in ServiceProviderJsonTypes)
                            {
                                LK_IntegrationType.CreatedOn = DateTime.Now;
                            }

                            _dbContext.ServiceProviders.AddRange(ServiceProviderJsonTypes);

                            _dbContext.SaveChanges();
                        }

                    }
                    finally
                    {
                        _dbContext.Database.CloseConnection();
                    }
                }
            }

            catch
            {

            }

        }
        private static void Seed_ServiceProviderLike()
        {
            try
            {


                string ServiceProviderLikeJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "ServiceProviderLike_SeedData.json");
                var ServiceProviderLikeJsonTypes = JsonConvert.DeserializeObject<List<ServiceProviderLike>>(ServiceProviderLikeJson);

                if (ServiceProviderLikeJsonTypes != null && ServiceProviderLikeJsonTypes.Any())
                {
                    _dbContext.Database.OpenConnection();
                    try
                    {
                        //  _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Categories] ON");
                        if (!_dbContext.SubCategories.Any())
                        {
                            foreach (var LK_IntegrationType in ServiceProviderLikeJsonTypes)
                            {
                                LK_IntegrationType.CreatedOn = DateTime.Now;
                            }

                            _dbContext.ServiceProviderLikes.AddRange(ServiceProviderLikeJsonTypes);

                            _dbContext.SaveChanges();
                        }

                    }
                    finally
                    {
                        _dbContext.Database.CloseConnection();
                    }
                }
            }
            catch
            {

            }
        }
        private static void Seed_Offer()
        {
            try
            {


                string OfferJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "Offer_SeedData.json");
                var OfferJsonTypes = JsonConvert.DeserializeObject<List<Offer>>(OfferJson);

                if (OfferJsonTypes != null && OfferJsonTypes.Any())
                {
                    _dbContext.Database.OpenConnection();
                    try
                    {
                        //  _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Categories] ON");
                        if (!_dbContext.SubCategories.Any())
                        {
                            foreach (var LK_IntegrationType in OfferJsonTypes)
                            {
                                LK_IntegrationType.CreatedOn = DateTime.Now;
                            }

                            _dbContext.Offers.AddRange(OfferJsonTypes);

                            _dbContext.SaveChanges();
                        }

                    }
                    finally
                    {
                        _dbContext.Database.CloseConnection();
                    }
                }
            }
            catch
            {

            }
        }
        private static void Seed_OfferLike()
        {
            try
            {

                string OfferLikeJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "OfferLikeUsed_SeedData.json");
                var OfferLikeJsonTypes = JsonConvert.DeserializeObject<List<OfferLikeUsed>>(OfferLikeJson);

                if (OfferLikeJsonTypes != null && OfferLikeJsonTypes.Any())
                {
                    _dbContext.Database.OpenConnection();
                    try
                    {
                        //  _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Categories] ON");
                        if (!_dbContext.SubCategories.Any())
                        {
                            foreach (var LK_IntegrationType in OfferLikeJsonTypes)
                            {
                                LK_IntegrationType.CreatedOn = DateTime.Now;
                            }

                            _dbContext.OfferLikeUseds.AddRange(OfferLikeJsonTypes);

                            _dbContext.SaveChanges();
                        }

                    }
                    finally
                    {
                        _dbContext.Database.CloseConnection();
                    }
                }
            }
            catch
            {

            }
        }


    }



}

