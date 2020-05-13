using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCore.Extension_Method
{
    public static class ExtensionMethod
    {
        /////////////public List Model/////////////////
        public static List<UserLog> RemoveReferences(this List<Models.UserLog> list)
        {
            var newList = new List<Models.UserLog>();
            list.ForEach(item =>
            {
                var obj = new Models.UserLog();
                obj = item.RemoveReferences();
                newList.Add(obj);
            });
            return newList;
        }
        public static List<Models.ProductCategory> RemoveReferences(this List<Models.ProductCategory> list)
        {
            var newList = new List<Models.ProductCategory>();
            list.ForEach(item =>
            {
                var obj = new Models.ProductCategory();
                obj = item.RemoveReferences();
                newList.Add(obj);
            });
            return newList;
        }
        public static List<Models.Product> RemoveReferences(this List<Models.Product> list)
        {
            var newList = new List<Models.Product>();
            list.ForEach(item =>
            {
                var obj = new Models.Product();
                obj = item.RemoveReferences();
                newList.Add(obj);
            });
            return newList;
        }
        public static List<Models.ProductImage> RemoveReferences(this List<Models.ProductImage> list)
        {
            var newList = new List<Models.ProductImage>();
            list.ForEach(item =>
            {
                var obj = new Models.ProductImage();
                obj = item.RemoveReference();
                newList.Add(obj);
            });
            return newList;
        }
        public static List<Models.FooterUrl> RemoveReferences(this List<Models.FooterUrl> list)
        {
            var newList = new List<Models.FooterUrl>();
            list.ForEach(item =>
            {
                var obj = new Models.FooterUrl();
                obj = item.RemoveReferences();
                newList.Add(obj);
            });
            return newList;
        }
        public static List<Models.FeatureProduct> RemoveReferences(this List<Models.FeatureProduct> list)
        {
            var newList = new List<Models.FeatureProduct>();
            list.ForEach(item =>
            {
                var obj = new Models.FeatureProduct();
                obj = item.RemoveReferences();
                newList.Add(obj);
            });
            return newList;
        }
        public static List<Models.FooterHeader> RemoveReferences(this List<Models.FooterHeader> list)
        {
            var newList = new List<Models.FooterHeader>();
            list.ForEach(item =>
            {
                var obj = new Models.FooterHeader();
                obj = item.RemoveReferences();
                newList.Add(obj);
            });
            return newList;
        }
        public static List<FooterUrlData> RemoveReferences(this List<FooterUrlData> list)
        {
            var newList = new List<FooterUrlData>();
            list.ForEach(item =>
            {
                var obj = new FooterUrlData();
                obj = item.RemoveReferences();
                newList.Add(obj);
            });
            return newList;
        }
        public static Models.FooterHeader RemoveReferences(this Models.FooterHeader item)
        {
            var obj = new Models.FooterHeader();
            if (item != null)
            {
                obj = item.RemoveReference();
                if (item.FooterUrls != null)
                {
                    obj.FooterUrls = new List<FooterUrl>();
                    foreach (var abc in item.FooterUrls)
                    {
                        obj.FooterUrls.Add(abc.RemoveReference());
                    }
                }
            }
            return obj;
        }
        public static List<Models.CustomerHistory> RemoveReferences(this List<Models.CustomerHistory> list)
        {
            var newList = new List<Models.CustomerHistory>();

            list.ForEach(item =>
            {
                var obj = new Models.CustomerHistory();
                obj = item.RemoveReferences();

                newList.Add(obj);
            });
            return newList;
        }
        public static List<Models.User> RemoveReferences(this List<Models.User> list)
        {
            var newList = new List<Models.User>();

            list.ForEach(item =>
            {
                var obj = new Models.User();
                obj = item.RemoveRefernces();
                newList.Add(obj);
            });
            return newList;
        }
        public static List<Models.Newsletter> RemoveReferences(this List<Models.Newsletter> list)
        {
            var newList = new List<Models.Newsletter>();

            list.ForEach(item =>
            {
                var obj = new Models.Newsletter();
                obj = item.RemoveReferences();
                newList.Add(obj);
            });
            return newList;
        }
        public static List<Models.CustomerGroupUsers> RemoveReferences(this List<Models.CustomerGroupUsers> list)
        {
            var newList = new List<Models.CustomerGroupUsers>();

            list.ForEach(item =>
            {
                var obj = new Models.CustomerGroupUsers();
                obj = item.RemoveReferences();
                newList.Add(obj);
            });
            return newList;
        }
        public static List<Models.ProductionSpecification> RemoveReferences(this List<Models.ProductionSpecification> list)
        {
            var newList = new List<Models.ProductionSpecification>();

            list.ForEach(item =>
            {
                var obj = new Models.ProductionSpecification();
                obj = item.RemoveReferences();
                newList.Add(obj);
            });
            return newList;
        }
        public static List<Models.Variant> RemoveReferences(this List<Models.Variant> list)
        {
            var newList = new List<Models.Variant>();

            list.ForEach(item =>
            {
                var obj = new Models.Variant();
                obj = item.RemoveReferences();
                newList.Add(obj);
            });
            return newList;
        }
        public static List<Models.VariantOption> RemoveReferences(this List<Models.VariantOption> list)
        {
            var newList = new List<Models.VariantOption>();

            list.ForEach(item =>
            {
                var obj = new Models.VariantOption();
                obj = item.RemoveReference();
                newList.Add(obj);
            });
            return newList;
        }
        public static List<Models.SpinnerPromotion> RemoveReferences(this List<Models.SpinnerPromotion> list)
        {
            var newList = new List<Models.SpinnerPromotion>();

            list.ForEach(item =>
            {
                var obj = new Models.SpinnerPromotion();
                obj = item.RemoveReference();
                newList.Add(obj);
            });
            return newList;
        }
        public static List<Models.ProductVariantOption> RemoveReferences(this List<Models.ProductVariantOption> list)
        {
            var newList = new List<Models.ProductVariantOption>();

            list.ForEach(item =>
            {
                var obj = new Models.ProductVariantOption();
                obj = item.RemoveReference();
                newList.Add(obj);
            });
            return newList;
        }
        public static List<Models.CartItem> RemoveReferences(this List<Models.CartItem> list)
        {
            var newList = new List<Models.CartItem>();

            list.ForEach(item =>
            {
                var obj = new Models.CartItem();
                obj = item.RemoveReference();
                newList.Add(obj);
            });
            return newList;
        }
        public static List<Models.RatingReview> RemoveReferences(this List<Models.RatingReview> list)
        {
            var newList = new List<Models.RatingReview>();

            list.ForEach(item =>
            {
                var obj = new Models.RatingReview();
                obj = item.RemoveReference();
                newList.Add(obj);
            });
            return newList;
        }

        /////////////public Model/////////////////////
         public static Deal RemoveReferences(this Deal item)
        {
            var obj = new Deal();
        obj = item.RemoveReference();
            
            if (item?.DealProduct != null)
            {
                obj.DealProduct = new List<DealProduct>();
                var templist = item.DealProduct.ToList();
                foreach (var img in templist)
                {
                    obj.DealProduct.Add(img.RemoveReference());
                }
            }
           
            return obj;
        }
        public static Product RemoveReferences(this Product item)
        {
            var obj = new Product();
            obj = item.RemoveReference();
            if (item?.ProductCategory != null)
            {
                obj.ProductCategory = new ProductCategory();

                obj.ProductCategory = item.ProductCategory.RemoveReference();
            }
            //if(item.Unit!=null)
            //{
            // obj.Unit = new Unit();

            // obj.Unit = Unit.RemoveReference();
            //}
            if (item?.ProductImages != null)
            {
                obj.ProductImages = new List<ProductImage>();
                var templist = item.ProductImages.Where(x => x.IsActive == true).ToList();
                foreach (var img in templist)
                {
                    obj.ProductImages.Add(img.RemoveReference());
                }
            }
            if (item?.ProductVariantDetails != null)
            {
                obj.ProductVariantDetails = new List<ProductVariantDetail>();
                var templist = item.ProductVariantDetails.Where(x => x.IsActive == true).ToList();
                foreach (var img in templist)
                {
                    obj.ProductVariantDetails.Add(img.RemoveReference());
                }
            }

            return obj;
        }
        public static CustomerHistory RemoveReferences(this CustomerHistory item)
        {
            var obj = new CustomerHistory();
            obj = item.RemoveRefernce();
            if (item.Product != null)
            {
                obj.Product = new Product();
                obj.Product = item.Product.RemoveReference();
            }
            if (item.User != null)
            {
                obj.User = new User();
                obj.User = item.User.RemoveReference();
            }
            return obj;
        }
        public static User RemoveRefernces(this User item)
        {
            var obj = new User();
            obj = item.RemoveReference();
            if (item.State != null)
            {
                obj.State = new State();
                obj.State = item.State.RemoveReference();
            }
            if (item.Gender != null)
            {
                obj.Gender = new Gender();
                obj.Gender = item.Gender.RemoveReference();
            }
            if (item.Language != null)
            {
                obj.Language = new Language();
                obj.Language = item.Language.RemoveReference();
            }
            if (item.Country != null)
            {
                obj.Country = new Country();
                obj.Country = item.Country.RemoveReference();
            }

            return obj;
        }
        public static Models.ProductCategory RemoveReferences(this Models.ProductCategory item)
        {
            var obj = new Models.ProductCategory();
            obj = item.RemoveReference();
            if (item?.ProductCategory1 != null)
            {
                obj.ProductCategory1 = new List<Models.ProductCategory>();
                foreach (var itemk in item.ProductCategory1)
                {
                    obj.ProductCategory1.Add(itemk.RemoveReference());
                }

            }
            if (item?.Parent != null)
            {
                obj.Parent = new Models.ProductCategory();
                obj.Parent = item.RemoveReference();
            }
            return obj;
        }
        public static Models.FeatureProduct RemoveReferences(this Models.FeatureProduct item)
        {
            var obj = new Models.FeatureProduct();
            obj = item.RemoveReference();
            if (item.Product != null)
            {
                if (item.Product.IsActive == true)
                {
                    obj.Product = new Models.Product();
                    obj.Product = item.Product.RemoveReference();
                    if (item.Product.ProductImages != null)
                    {
                        obj.Product.ProductImages = new List<ProductImage>();
                        var templist = item.Product.ProductImages.Where(x => x.IsActive == true).ToList();
                        foreach (var img in templist)
                        {
                            obj.Product.ProductImages.Add(img.RemoveReference());
                        }
                    }
                }
            }
            return obj;
        }
        public static Models.ProductImage RemoveReferences(this Models.ProductImage item)
        {
            var obj = new Models.ProductImage();
            obj = item.RemoveReference();
            if (item.Product != null)
            {
                obj.Product = new Models.Product();
                obj.Product = item.Product.RemoveReference();

            }
            if (item.ProductVariantDetail != null)
            {
                obj.ProductVariantDetail = new Models.ProductVariantDetail();
                obj.ProductVariantDetail = item.ProductVariantDetail.RemoveReference();

            }
            return obj;
        }
        public static Models.FooterUrl RemoveReferences(this Models.FooterUrl item)
        {
            var obj = new Models.FooterUrl();
            obj = item.RemoveReference();
            if (item.FooterHeader != null)
            {
                obj.FooterHeader = new FooterHeader();
                obj.FooterHeader = item.FooterHeader.RemoveReference();
            }
            return obj;
        }
        public static FooterUrlData RemoveReferences(this FooterUrlData item)
        {
            var obj = new FooterUrlData();
            obj = item.RemoveReference();
            if (item.FooterUrl != null)
            {
                obj.FooterUrl = new FooterUrl();
                obj.FooterUrl = item.FooterUrl.RemoveReference();
            }
            return obj;
        }
        public static Newsletter RemoveReferences(this Newsletter item)
        {
            var obj = new Newsletter();
            obj = item.RemoveReference();
            if (item.User != null)
            {
                obj.User = new User();
                obj.User = item.User.RemoveReference();
            }
            return obj;
        }
        public static CustomerGroupUsers RemoveReferences(this CustomerGroupUsers item)
        {
            var obj = new CustomerGroupUsers();
            obj = item.RemoveReference();
            if (item.User != null)
            {
                obj.User = new User();
                obj.User = item.User.RemoveReference();
            }
            if (item.CustomerGroup != null)
            {
                obj.CustomerGroup = new CustomerGroup();
                obj.CustomerGroup = item.CustomerGroup.RemoveReference();
            }

            return obj;
        }
        public static UserLog RemoveReferences(this UserLog item)
        {
            var obj = new UserLog();
            obj = item.RemoveReference();
            if (item.User != null)
            {
                obj.User = new User();
                obj.User = item.User.RemoveReference();
            }
            if (item.Product != null)
            {
                obj.Product = new Product();
                obj.Product = item.Product.RemoveReference();
            }
            if (item.Action != null)
            {
                obj.Action = new Models.Action();
                obj.Action = item.Action.RemoveReference();
            }
            if (item.Page != null)
            {
                obj.Page = new Models.Page();
                obj.Page = item.Page.RemoveReference();
            }
            return obj;
        }
        public static VariantOption RemoveReferences(this VariantOption item)
        {
            var obj = new VariantOption();
            obj = item.RemoveReference();
            if (item?.Variant != null)
            {
                obj.Variant = new Variant();
                obj.Variant = item.Variant.RemoveReference();
            }
            if (item?.ProductVariantOptions != null)
            {
                obj.ProductVariantOptions = new List<Models.ProductVariantOption>();
                var templist = item.ProductVariantOptions.Where(x => x.IsActive == true).ToList();
                foreach (var img in templist)
                {
                    obj.ProductVariantOptions.Add(img.RemoveReference());
                }
            }

            return obj;
        }
        public static ProductionSpecification RemoveReferences(this ProductionSpecification item)
        {
            var obj = new ProductionSpecification();
            obj = item.RemoveReference();
            if (item.Product != null)
            {
                obj.Product = new Product();
                obj.Product = item.Product.RemoveReference();
            }
            return obj;
        }
        public static SpinnerPromotion RemoveReferences(this SpinnerPromotion item)
        {
            var obj = new SpinnerPromotion();
            obj = item.RemoveReference();
            if (item.SpinnerOptionsPeriod != null)
            {
                obj.SpinnerOptionsPeriod = new SpinnerOptionsPeriod();
                obj.SpinnerOptionsPeriod = item.SpinnerOptionsPeriod.RemoveReference();
            }
            return obj;
        }
        public static Variant RemoveReferences(this Variant item)
        {
            var obj = new Variant();
            obj = item.RemoveReference();
            if (item.VariantOptions.Count > 0)
            {
                obj.VariantOptions = new List<VariantOption>();
                obj.VariantOptions = item.VariantOptions.ToList().RemoveReferences();
            }


            return obj;
        }
        public static ProductVariantDetail RemoveReferences(this ProductVariantDetail item)
        {
            var obj = new ProductVariantDetail();
            obj = item?.RemoveReference();
            if (item?.Product != null)
            {
                obj.Product = new Product();
                obj.Product = item.Product.RemoveReference();
            }
            if (item?.ProductVariantOptions.Count > 0)
            {
                obj.ProductVariantOptions = new List<Models.ProductVariantOption>();
                obj.ProductVariantOptions = item.ProductVariantOptions.ToList().RemoveReferences();
            }
            if (item?.ProductImages.Count > 0)
            {
                obj.ProductImages = new List<Models.ProductImage>();
                obj.ProductImages = item.ProductImages.ToList().RemoveReferences();
            }
            if (item?.CartItems != null && item.CartItems.Count > 0)
            {
                obj.CartItems = new List<Models.CartItem>();
                obj.CartItems = item.CartItems.ToList().RemoveReferences();
            }
            return obj;
        }
        public static CartItem RemoveReferences(this CartItem item)
        {
            var obj = new CartItem();
            obj = item.RemoveReference();
            if (item.Cart != null)
            {
                obj.Cart = new Cart();
                obj.Cart = item.Cart.RemoveReference();
            }
            if (item.Unit != null)
            {
                obj.Unit = new Unit();
                obj.Unit = item.Unit.RemoveReference();
            }
            if (item.ProductVariantDetail != null)
            {
                obj.ProductVariantDetail = new ProductVariantDetail();
                obj.ProductVariantDetail = item.ProductVariantDetail.RemoveReference();
            }
            if (item.User != null)
            {
                obj.User = new User();
                obj.User = item.User.RemoveReference();
            }
            return obj;
        }
        public static Cart RemoveReferences(this Cart item)
        {
            var obj = new Cart();
            obj = item.RemoveReference();
            if (item?.User != null)
            {
                obj.User = new User();
                obj.User = item.User.RemoveReference();
            }
            if (item?.CartItems.Count > 0)
            {
                obj.CartItems = new List<Models.CartItem>();
                obj.CartItems = item.CartItems.ToList().RemoveReferences();
            }

            return obj;
        }
        public static RatingReview RemoveReferences(this RatingReview item)
        {
            var obj = new RatingReview();
            obj = item.RemoveReference();
            if (item?.User != null)
            {
                obj.User = new User();
                obj.User = item.User.RemoveReference();
            }
            if (item.Product != null)
            {
                obj.Product = new Product();
                obj.Product = item.Product.RemoveReference();
            }
            return obj;
        }

        ////////////private Model////////////////////
         private static Models.Deal RemoveReference(this Models.Deal item)
        {
            var obj = new Models.Deal();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.Name = item.Name;
                obj.ActiveFrom = item.ActiveFrom;
                obj.ActiveFromTime = item.ActiveFromTime;
                obj.ActiveTo = item.ActiveTo;
                obj.ActiveToTime = item.ActiveToTime;
                obj.CategoryId = item.CategoryId;
                obj.DealQty = item.DealQty;
                obj.Discount = item.Discount;
                obj.IsActive = item.IsActive;
                obj.IsFeatured = item.IsFeatured;
                obj.ProductCategoryId = item.ProductCategoryId;
                obj.QuantityPerUser = item.QuantityPerUser;
                obj.SoldQty = item.SoldQty;
                obj.Status = item.Status;
                obj.IsShow = item.IsShow;
            }
            return obj;
        }
        private static Models.DealProduct RemoveReference(this Models.DealProduct item)
        {
            var obj = new Models.DealProduct();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.DealId = item.DealId;
                obj.ProductId = item.ProductId;
                obj.ProductVariantId = item.ProductVariantId;
            }
            return obj;
        }
        private static Models.Action RemoveReference(this Models.Action item)
        {
            var obj = new Models.Action();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.Name = item.Name;
                obj.IsActive = item.IsActive;
            }
            return obj;
        }
        private static Models.SpinnerOptionsPeriod RemoveReference(this Models.SpinnerOptionsPeriod item)
        {
            var obj = new Models.SpinnerOptionsPeriod();
            if (item != null)
            {
                obj.Id = item.Id;

                obj.SpinnerPromotionId = item.SpinnerPromotionId;
                obj.Chances = item.Chances;
                obj.Period = item.Period;

                obj.IsActive = item.IsActive;
                obj.SpinnerPromotion = item.SpinnerPromotion;
    }
            return obj;
        }
        private static Models.Page RemoveReference(this Models.Page item)
        {
            var obj = new Models.Page();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.Name = item.Name;
                obj.IsActive = item.IsActive;
            }
            return obj;
        }
        private static Models.ProductCategory RemoveReference(this Models.ProductCategory item)
        {
            var obj = new Models.ProductCategory();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.Name = item.Name;
                obj.SpanishName = item.SpanishName;
                obj.ParentId = item.ParentId;
                obj.Icon = item.Icon;
                obj.IsActive = item.IsActive;
                obj.ProductCategory1 = item.ProductCategory1;
                
            }
            return obj;
        }
        private static Models.FeatureProduct RemoveReference(this Models.FeatureProduct item)
        {
            var obj = new Models.FeatureProduct();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.ProductId = item.ProductId;
                obj.IsActive = item.IsActive;

            }
            return obj;
        }
        public static Models.ProductImage RemoveReference(this Models.ProductImage item)
        {
            var obj = new Models.ProductImage();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.ProductId = item.ProductId;
                obj.IsActive = item.IsActive;
                obj.ImagePath = item.ImagePath;
                obj.ImagePath150x150 = item.ImagePath150x150;
                obj.ImagePath450x450 = item.ImagePath450x450;
                obj.IsDefault = item.IsDefault;
                obj.ProductVariantDetailId = item.ProductVariantDetailId;
            }
            return obj;
        }
        private static Models.Product RemoveReference(this Models.Product item)
        {
            var obj = new Models.Product();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.IsActive = item.IsActive;
                obj.Barcode = item.Barcode;
                obj.CostPrice = item.CostPrice;
                obj.Description = item.Description;
                obj.Discount = item.Discount;
                obj.PriceAfterdiscount = item.PriceAfterdiscount;
                obj.ProductTags = item.ProductTags;
                obj.SellingPrice = item.SellingPrice;
                obj.UnitId = item.UnitId;
                obj.Name = item.Name;
                obj.ProductCategoryId = item.ProductCategoryId;
                obj.VendorId = item.VendorId;
                obj.IsEnable = item.IsEnable;
                obj.ShipmentCost = item.ShipmentCost;
                obj.ShipmentTime = item.ShipmentTime;
                obj.ShipmentVendor = item.ShipmentVendor;
            }
            return obj;
        }
        private static Models.FooterUrl RemoveReference(this Models.FooterUrl item)
        {
            var obj = new Models.FooterUrl();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.FooterHeaderId = item.FooterHeaderId;
                obj.IsActive = item.IsActive;
                obj.Name = item.Name;
                obj.Url = item.Url;

            }
            return obj;
        }
        private static Models.FooterHeader RemoveReference(this Models.FooterHeader item)
        {
            var obj = new Models.FooterHeader();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.IsActive = item.IsActive;
                obj.Name = item.Name;
            }
            return obj;
        }
        private static Models.FooterUrlData RemoveReference(this Models.FooterUrlData item)
        {
            var obj = new Models.FooterUrlData();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.IsActive = item.IsActive;
                obj.Data = item.Data;
            }
            return obj;
        }
        private static User RemoveReference(this User item)
        {
            var obj = new User();
            if (item != null)
            {
                obj.Address = item.Address;
                obj.Email = item.Email;
                obj.Phone = item.Phone;
                obj.StateId = item.StateId;
                obj.CountryId = item.CountryId;
                obj.DOB = item.DOB;
                obj.City = item.City;
                obj.LanguageId = item.LanguageId;
                obj.GenderId = item.GenderId;
                obj.TwitterId = item.TwitterId;
                obj.FacebookId = item.FacebookId;
                obj.PostalCode = item.PostalCode;
                obj.FirstName = item.FirstName;
                obj.LastName = item.LastName;
                obj.DisplayName = item.DisplayName;
                obj.UserName = item.UserName;
                obj.RoleId = item.RoleId;
                obj.Image = item.Image;
                obj.Id = item.Id;
                obj.IsActive = item.IsActive;
                obj.IsVerified = item.IsVerified;
                obj.MiddleName = item.MiddleName;

            }
            return obj;
        }
        private static State RemoveReference(this State item)
        {
            var obj = new State();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.Name = item.Name;
                obj.CountryId = item.CountryId;
                obj.IsActive = item.IsActive;
            }
            return obj;
        }
        private static Country RemoveReference(this Country item)
        {
            var obj = new Country();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.IsActive = item.IsActive;
                obj.Name = item.Name;
            }
            return obj;
        }
        private static Language RemoveReference(this Language item)
        {
            var obj = new Language();


            if (item != null)
            {
                obj.Id = item.Id;
                obj.IsActive = item.IsActive;
                obj.Name = item.Name;
            }
            return obj;
        }
        private static Gender RemoveReference(this Gender item)
        {
            var obj = new Gender();


            if (item != null)
            {
                obj.Id = item.Id;
                obj.IsActive = item.IsActive;
                obj.Name = item.Name;
            }
            return obj;
        }
        private static CustomerHistory RemoveRefernce(this CustomerHistory item)
        {
            var obj = new CustomerHistory();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.Date = item.Date;
                obj.CustomerId = item.CustomerId;
                obj.IpAddress = item.IpAddress;
                obj.IsActive = item.IsActive;

            }
            return obj;
        }
        public static Newsletter RemoveReference(this Newsletter item)
        {
            var obj = new Newsletter();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.IpAddress = item.IpAddress;
                obj.UserId = item.UserId;
                obj.IsSubscribed = item.IsSubscribed;
                obj.Email = item.Email;
                obj.IsActive = item.IsActive;
            }
            return obj;
        }
        public static UserLog RemoveReference(this UserLog item)
        {
            var obj = new UserLog();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.IPAddress = item.IPAddress;
                obj.UserId = item.UserId;
                obj.ActionId = item.ActionId;
                obj.ProductId = item.ProductId;
                obj.Url = item.Url;
                obj.LogInDate = item.LogInDate;
                obj.LogOutDate = item.LogOutDate;
                obj.PageId = item.PageId;
                obj.IsActive = item.IsActive;

            }
            return obj;
        }
        public static CustomerGroupUsers RemoveReference(this CustomerGroupUsers item)
        {
            var obj = new CustomerGroupUsers();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.CustomerGroupId = item.CustomerGroupId;
                obj.UserId = item.UserId;
                obj.IsActive = item.IsActive;

            }
            return obj;
        }
        public static CustomerGroup RemoveReference(this CustomerGroup item)
        {
            var obj = new CustomerGroup();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.GroupName = item.GroupName;
                obj.IsActive = item.IsActive;

            }
            return obj;
        }
        public static ProductionSpecification RemoveReference(this ProductionSpecification item)
        {
            var obj = new ProductionSpecification();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.HeadingName = item.HeadingName;
                obj.Description = item.Description;
                obj.ProductId = item.ProductId;
                obj.Product = item.Product;
                obj.IsActive = item.IsActive;

            }
            return obj;
        }
        public static Variant RemoveReference(this Variant item)
        {
            var obj = new Variant();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.CategoryVariants = item.CategoryVariants;
                obj.Name = item.Name;
                obj.IsActive = item.IsActive;

            }
            return obj;
        }
        public static VariantOption RemoveReference(this VariantOption item)
        {
            var obj = new VariantOption();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.Name = item.Name;
                obj.ProductVariantOptions = item.ProductVariantOptions;

                obj.VariantId = item.VariantId;
                obj.IsActive = item.IsActive;


            }
            return obj;
        }
        private static ProductVariantDetail RemoveReference(this ProductVariantDetail item)
        {
            var obj = new ProductVariantDetail();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.InStock = item.InStock;
                obj.IsActive = item.IsActive;
                obj.IsDefault = item.IsDefault;
                obj.Price = item.Price;
                obj.ProductId = item.ProductId;
                obj.Weight = item.Weight;
                obj.ProductSKU = item.ProductSKU;
                obj.Discount = item.Discount;
                obj.PriceAfterdiscount = item.PriceAfterdiscount;
            }
            return obj;
        }
        private static Models.ProductVariantOption RemoveReference(this Models.ProductVariantOption item)
        {
            var obj = new Models.ProductVariantOption();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.IsActive = item.IsActive;
                obj.CategoryVariantId = item.CategoryVariantId;
                obj.ProductVariantDetailId = item.ProductVariantDetailId;
                obj.VariantOptionId = item.VariantOptionId;
            }
            return obj;
        }
        private static Models.CartItem RemoveReference(this Models.CartItem item)
        {
            var obj = new Models.CartItem();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.IsActive = item.IsActive;
                obj.ProductVariantDetailId = item.ProductVariantDetailId;
                //obj.Amount = item.Amount;
                obj.CartId = item.CartId;
                //obj.Discount = item.Discount;
                obj.Quantity = item.Quantity;
                obj.UnitId = item.UnitId;
                //obj.UnitPrice = item.UnitPrice;
                obj.VendorId = item.VendorId;
            }
            return obj;
        }
        private static Models.Cart RemoveReference(this Models.Cart item)
        {
            var obj = new Models.Cart();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.IsActive = item.IsActive;
                obj.AdditionalCost = item.AdditionalCost;
                obj.IpAddress = item.IpAddress;
                obj.IsConvertToCheckout = item.IsConvertToCheckout;
                obj.OrderDate = item.OrderDate;
                obj.OrderNumber = item.OrderNumber;
                //obj.TotalAmount = item.TotalAmount;
                obj.UserId = item.UserId;
                obj.VendorId = item.VendorId;
            }
            return obj;
        }
        private static Models.Unit RemoveReference(this Models.Unit item)
        {
            var obj = new Models.Unit();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.IsActive = item.IsActive;
                obj.Name = item.Name;
            }
            return obj;
        }
        private static Models.RatingReview RemoveReference(this Models.RatingReview item)
        {
            var obj = new Models.RatingReview();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.IsActive = item.IsActive;
                obj.IsDefault = item.IsDefault;
                obj.Product = item.Product;
                obj.ProductId = item.ProductId;
                obj.Rating = item.Rating;
                obj.Review = item.Review;
                obj.ReviewDate = item.ReviewDate;
                obj.ReviewStatus = item.ReviewStatus;
                obj.ReviewStatusId = item.ReviewStatusId;
                obj.User = item.User;
                obj.UserId = item.UserId;
                
            }
            return obj;
        }
        private static Models.SpinnerPromotion RemoveReference(this Models.SpinnerPromotion item)
        {
            var obj = new Models.SpinnerPromotion();
            if (item != null)
            {
                obj.Id = item.Id;
                obj.Image = item.Image;
             obj.DisplayMessage = item.DisplayMessage;
               obj.ProductCategoryId = item.ProductCategoryId;
                obj.CategoryId = item.CategoryId;
                obj.MoodId = item.MoodId;
                obj.ActiveTo = item.ActiveTo;
                obj.ActiveFrom = item.ActiveFrom;
                obj.DiscountPrice = item.DiscountPrice;
                obj.DiscountPercentage = item.DiscountPercentage;
                obj.IsActive = item.IsActive;
                obj.Description = item.Description;
                obj.ActiveFromTime = item.ActiveFromTime;
                obj.ActiveToTime = item.ActiveToTime;
                obj.ProductId = item.ProductId;
                obj.Filterurl = item.Filterurl;
                obj.MaxQty = item.MaxQty;

                obj.Mood = item.Mood;
                obj.SpinnerOptionsPeriod = item.SpinnerOptionsPeriod;
    }
            return obj;
        }

        //private static Models.User RemoveReference(this Models.User item)
        //{
        //    var obj = new Models.User();
        //    if (item != null)
        //    {
        //        obj.Address = item.Address;
        //        obj.CartItems = item.CartItems;
        //        obj.Carts = item.Carts;
        //        obj.City = item.City;
        //        obj.Company = item.Company;
        //        obj.CompanyId = item.CompanyId;
        //        obj.CompareProducts = item.CompareProducts;
        //        obj.Country = item.Country;
        //        obj.CountryId = item.CountryId;
        //        obj.DisplayName = item.DisplayName;
        //        obj.DOB = item.DOB;
        //        obj.Email = item.Email;
        //        obj.FacebookId = item.FacebookId;
        //        obj.FirstName = item.FirstName;
        //        obj.Gender = item.Gender;
        //        obj.GenderId = item.GenderId;
        //        obj.Id = item.Id;
        //        obj.Image = item.Image;
        //        obj.IsActive = item.IsActive;
        //        obj.IsVerified
        //            obj.Language
        //            obj.LanguageId
        //    obj.LastName
        //            obj.MiddleName
        //            obj.Otp
        //            obj.Password
        //            obj.PasswordHash
        //            obj.PasswordSalt
        //            obj.Phone
        //            obj.PostalCode
        //            obj.ReturnCode

        //            }
        //}
    }
}