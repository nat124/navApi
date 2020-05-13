using Localdb;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCore.Controllers;
using TestCore.Extension_Method;

namespace TestCore.Helper
{

    public static class DealHelper
    {
        private static List<Deal> deals(PistisContext db)
        {
            var todayTime = DateTime.Now.TimeOfDay;
            var finaldDeal = new List<Deal>();
            var deals=db.Deal.Where(x => x.IsActive == true && x.Status == "open" && x.ActiveFrom.Date <= DateTime.Now.Date && x.ActiveTo.Date >= DateTime.Now.Date
            ).Include(x => x.DealProduct).ToList();
            foreach (var d in deals)
            {

                var start = (Convert.ToDateTime(d.ActiveFromTime)).TimeOfDay;
                var end = (Convert.ToDateTime(d.ActiveToTime)).TimeOfDay;
                if (start < todayTime)
                {
                    if (d.ActiveTo.Date > DateTime.Now.Date)
                        finaldDeal.Add(d);
                    else if (d.ActiveTo.Date == DateTime.Now.Date && end > todayTime)
                    {
                        finaldDeal.Add(d);
                    }
                }

            }
          
            return finaldDeal;
        }

        
        private static List<Deal> dealId(PistisContext db,int Id)
        {
            var todayTime = DateTime.Now.TimeOfDay;
            var finaldDeal = new List<Deal>();
            var deals = db.Deal.Where(x => x.IsActive == true && x.Status == "open" && x.ActiveFrom.Date <= DateTime.Now.Date && x.ActiveTo.Date >= DateTime.Now.Date && x.Id==Id
            ).Include(x => x.DealProduct).ToList();
            foreach (var d in deals)
            {

                var start = (Convert.ToDateTime(d.ActiveFromTime)).TimeOfDay;
                var end = (Convert.ToDateTime(d.ActiveToTime)).TimeOfDay;
                if (start < todayTime)
                {
                    if (d.ActiveTo.Date > DateTime.Now.Date)
                        finaldDeal.Add(d);
                    else if (d.ActiveTo.Date == DateTime.Now.Date && end > todayTime)
                    {
                        finaldDeal.Add(d);
                    }
                }

            }

            return finaldDeal;
        }
        public static List<Deal> getdeals(PistisContext db)
        {
            return deals(db);
        }
        public static List<Deal> getdealsId(PistisContext db,int Id)
        {
            return dealId(db,Id);
        }
        public static List<ProductVariantDetail> calculateDealonetime(List<ProductVariantDetail> Data,int DealId, PistisContext db)
        {
            var finaldDeal = deals(db);
            var dealpro = new List<DealProduct>();
            dealpro = finaldDeal.Where(x => x.Id == DealId).SelectMany(x=>x.DealProduct).ToList();
            var commission =  new ProductCategoryCommission();
            var productCategoryId = 0;
          
            foreach (var d in Data)
            {

                var ProductCategoryId = db.Products.Where(x => x.Id == d.ProductId).FirstOrDefault().ProductCategoryId;
                var productCategory = db.ProductCategories.Where(x => x.Id == ProductCategoryId).FirstOrDefault();
                if (productCategory != null)
                {
                    if (productCategory.ParentId == null)
                    {
                        productCategoryId = productCategory.Id;
                    }
                    else
                    {
                        productCategory = db.ProductCategories.Where(x => x.Id == productCategory.ParentId).FirstOrDefault();
                        if (productCategory.ParentId == null)
                        {
                            productCategoryId = productCategory.Id;
                        }
                        else
                        {
                            productCategory = db.ProductCategories.Where(x => x.Id == productCategory.ParentId).FirstOrDefault();
                            productCategoryId = productCategory.Id;
                        }
                    }
                }
                if (productCategoryId > 0)
                {
                    var obj = new CategoryCommission(db);
                    commission = obj.CategoryCommissions.Where(x => x.IsActive == true && x.ProductCategoryId == productCategoryId).FirstOrDefault();
                    if (commission == null)
                    {
                        commission = new ProductCategoryCommission();
                        commission.Commission = 0;
                    }
                }




                if (dealpro.Any(z => z.ProductVariantId == d.Id))
                {
                    foreach (var p in dealpro.Where(z => z.ProductVariantId == d.Id))
                    {
                        d.Discount = d.Discount + Convert.ToInt32(p.Deal.Discount);
                        var priceaftercomm = d.Price + (d.Price * commission.Commission / 100);
                        d.Price = priceaftercomm;
                        d.PriceAfterdiscount = (priceaftercomm - (priceaftercomm * d.Discount / 100));
                        d.ActiveTo = p.Deal.ActiveTo;
                    }
                }
                else
                {
                    d.Discount = d.Discount;
                    var priceaftercomm = d.Price + (d.Price * commission.Commission / 100);
                    d.Price = priceaftercomm;
                    d.PriceAfterdiscount = (priceaftercomm - (priceaftercomm * d.Discount / 100));
                }
            }
            return Data;
        }
        //Product catalogue Page
        public static List<ProductVariantDetail> calculateDeal(List<ProductVariantDetail> Data, PistisContext db)
        {
             var finaldDeal = deals(db);
            var dealpro = new List<DealProduct>();
            foreach(var f in finaldDeal)
            {
                dealpro.AddRange(f.DealProduct);
            }
            foreach (var d in Data)
            {
                var pi = Convert.ToInt32(db.ProductVariantDetails.Where(x => x.Id == d.Id).Include(x => x.Product.ProductCategory).FirstOrDefault().Product?.ProductCategory?.ParentId);
                var catid = getparentCat(pi, db);
                d.Commission = GetCommissionByCategoryId(catid, db);
                if (dealpro.Any(z => z.ProductVariantId == d.Id))
                { 
                    foreach (var p in dealpro.Where(z => z.ProductVariantId == d.Id))
                    {
                        d.Discount = d.Discount + Convert.ToInt32(p.Deal.Discount);
                        var priceaftercomm = d.Price + (d.Price * d.Commission / 100);
                        d.Price = priceaftercomm;
                        d.PriceAfterdiscount = (priceaftercomm - (priceaftercomm * d.Discount / 100));
                        d.ActiveTo =Convert.ToDateTime(p.Deal.ActiveTo);
                    }
                }
                else
                {
                    d.Discount = d.Discount;
                    var priceaftercomm = d.Price + (d.Price * d.Commission / 100);
                    d.Price = priceaftercomm;
                    d.PriceAfterdiscount = (priceaftercomm - (priceaftercomm * d.Discount / 100));
                }
            }
            return Data;
        }

        //Product Cart Page
      
        public static List<GetCart> calculateDeal(List<GetCart> Data,PistisContext db)
        {

            var finaldDeal = deals(db);
            var dealpro = new List<DealProduct>();
            foreach (var f in finaldDeal)
            {
                dealpro.AddRange(f.DealProduct);
            }
            foreach (var d in Data)
            {
                var pi = Convert.ToInt32(db.ProductVariantDetails.Where(x => x.Id == d.ProductVariantDetailId)
                    .Include(x => x.Product.ProductCategory)
                    .FirstOrDefault()?.Product?.ProductCategory?.ParentId);
                var catid = getparentCat(pi, db);
                d.Commission = GetCommissionByCategoryId(catid, db);
                if (dealpro.Any(z => z.ProductVariantId == d.ProductVariantDetailId ))
                {
                    foreach (var p in dealpro.Where(z => z.ProductVariantId == d.ProductVariantDetailId))
                    {
                        if (p.Deal.QuantityPerUser >= d.Quantity)
                        {
                            d.Discount = d.Discount + Convert.ToInt32(p.Deal.Discount);
                            d.DealDiscount =  Convert.ToInt32(p.Deal.Discount);

                            var priceaftercomm = d.SellingPrice + (d.SellingPrice * d.Commission / 100);
                            d.SellingPrice = priceaftercomm;
                            d.PriceAfterDiscount = (priceaftercomm - (priceaftercomm * d.Discount / 100)) * d.Quantity;
                     //       d.DealPriceAfterDiscount = (priceaftercomm - (priceaftercomm * d.Discount / 100)) * d.Quantity;
                            d.Amount = d.PriceAfterDiscount;
                            d.DealQuantityPerUser = p.Deal?.QuantityPerUser ?? 0;


                        }
                        else
                        {
                            for (int i = 0; i < p.Deal.QuantityPerUser; i++)
                            {
                                d.DealDiscount = d.Discount + Convert.ToInt32(p.Deal.Discount);
                                var priceaftercomm1 = d.SellingPrice + (d.SellingPrice * d.Commission / 100);
                               d.DealPriceAfterDiscount = Convert.ToDecimal((priceaftercomm1 - (priceaftercomm1 * d.DealDiscount / 100)) * p.Deal.QuantityPerUser);
                               // d.Amount = d.PriceAfterDiscount;
                            }
                            d.DealQuantityPerUser = p.Deal?.QuantityPerUser??0;
                            
                            d.Discount = d.Discount ;
                            var priceaftercomm = d.SellingPrice + (d.SellingPrice * d.Commission / 100);
                            d.SellingPrice = priceaftercomm;
                            d.PriceAfterDiscount = (priceaftercomm - (priceaftercomm * d.Discount / 100)) * (d.Quantity- p.Deal.QuantityPerUser );
                           d.Amount = d.PriceAfterDiscount + d.DealPriceAfterDiscount;

                        }
                        if (d.DealQuantityPerUser == null)
                            {
                                d.DealQuantityPerUser = 0;
                            }


                    }
                }
                else
                {
                    d.Discount = d.Discount;
                    var priceaftercomm = d.SellingPrice + (d.SellingPrice * d.Commission / 100);
                    d.SellingPrice = priceaftercomm;
                    d.PriceAfterDiscount = (priceaftercomm - (priceaftercomm * d.Discount / 100)) * d.Quantity;
                    d.Amount = d.PriceAfterDiscount;
                }
            }
            foreach (var d in Data)
            {
                var TotalAmount = Data.Sum(x => x.PriceAfterDiscount)+ Data.Sum(x => x.DealPriceAfterDiscount);
                d.TotalAmount= Convert.ToDecimal(TotalAmount.ToString("#.00"));
                d.PriceAfterDiscount+=d.DealPriceAfterDiscount;

            }
            return Data;
        }

        //all product info
        public static product1 calculateDealForProducts(product1 Data,PistisContext db)
        {

            var finaldDeal = deals(db);
            var dealpro = new List<DealProduct>();
            foreach (var f in finaldDeal)
            {
                dealpro.AddRange(f.DealProduct);
            }
            var pi = Convert.ToInt32(db.ProductVariantDetails.Where(x => x.Id ==Data.VariantDetailId).Include(x => x.Product.ProductCategory).FirstOrDefault().Product?.ProductCategory?.ParentId);
            var catid = getparentCat(pi, db);
            Data.Commission = GetCommissionByCategoryId(catid, db);
            if (dealpro.Any(z => z.ProductVariantId == Data.VariantDetailId))
            {
                foreach (var p in dealpro.Where(z => z.ProductVariantId == Data.VariantDetailId))
                {
                    Data.Discount = Data.Discount + Convert.ToInt32(p.Deal.Discount);
                    var priceaftercomm = Data.SellingPrice + (Data.SellingPrice * Data.Commission / 100);
                    Data.SellingPrice = priceaftercomm;
                    Data.PriceAfterdiscount = (priceaftercomm - (priceaftercomm * Data.Discount / 100));
                    Data.ActiveTo = p.Deal.ActiveTo;
                }
            }
            else
            {
                Data.Discount = Data.Discount;
                var priceaftercomm = Data.SellingPrice + (Data.SellingPrice * Data.Commission / 100);
                Data.SellingPrice = priceaftercomm;
                Data.PriceAfterdiscount = (priceaftercomm - (priceaftercomm * Data.Discount / 100));
            }
            return Data;
        }

        //for product detai;
        public static ProductDetail calculateDealForProductDetail(ProductDetail Data, PistisContext db)
        {

            var finaldDeal = deals(db);
            var dealpro = new List<DealProduct>();
            foreach (var f in finaldDeal)
            {
                dealpro.AddRange(f.DealProduct);
            }
            var pi = Convert.ToInt32(db.ProductVariantDetails.Where(x => x.Id == Data.ProductVariantDetailId).Include(x => x.Product.ProductCategory).FirstOrDefault().Product?.ProductCategory?.ParentId);
            var catid = getparentCat(pi, db);
            Data.Commission = GetCommissionByCategoryId(catid, db);
            if (dealpro.Any(z => z.ProductVariantId == Data.ProductVariantDetailId))
            {
                foreach (var p in dealpro.Where(z => z.ProductVariantId == Data.ProductVariantDetailId))
                {
                    Data.Discount = Data.Discount + Convert.ToInt32(p.Deal.Discount);
                    var priceaftercomm = Data.SellingPrice + (Data.SellingPrice * Data.Commission / 100);
                    Data.SellingPrice = priceaftercomm;
                    Data.PriceAfterDiscount = (priceaftercomm - (priceaftercomm * Data.Discount / 100));
                    Data.ActiveTo = p.Deal.ActiveTo;
                }
            }
            else
            {
                Data.Discount = Data.Discount;
                var priceaftercomm = Data.SellingPrice + (Data.SellingPrice * Data.Commission / 100);
                Data.SellingPrice = priceaftercomm;
                Data.PriceAfterDiscount = (priceaftercomm - (priceaftercomm * Data.Discount / 100));
            }
            return Data;
        }

        //for product detai;
        public static ProductVariantDetail calculateDealForProductDetailModel(ProductVariantDetail Data, PistisContext db)
        {

            var finaldDeal = deals(db);
            var dealpro = new List<DealProduct>();
            foreach (var f in finaldDeal)
            {
                dealpro.AddRange(f.DealProduct);
            }
            var pi = Convert.ToInt32(db.ProductVariantDetails.Where(x => x.Id == Data.Id).Include(x => x.Product.ProductCategory).FirstOrDefault().Product?.ProductCategory?.ParentId);
            var catid = getparentCat(pi, db);
            Data.Commission = GetCommissionByCategoryId(catid, db);
            if (dealpro.Any(z => z.ProductVariantId == Data.Id))
            {
                foreach (var p in dealpro.Where(z => z.ProductVariantId == Data.Id))
                {
                    Data.Discount = Data.Discount + Convert.ToInt32(p.Deal.Discount);
                    var priceaftercomm = Data.Price + (Data.Price * Data.Commission / 100);
                    Data.Price = priceaftercomm;
                    Data.PriceAfterdiscount = (priceaftercomm - (priceaftercomm * Data.Discount / 100));
                    Data.ActiveTo = p.Deal.ActiveTo;
                }
            }
            else
            {
                Data.Discount = Data.Discount;
                var priceaftercomm = Data.Price + (Data.Price * Data.Commission / 100);
                Data.Price = priceaftercomm;
                Data.PriceAfterdiscount = (priceaftercomm - (priceaftercomm * Data.Discount / 100));
            }
            return Data;
        }

        //FOrList
        public static List<product1> calculateDealForProductsList(List<product1> Data,PistisContext db)
        {
            var finaldDeal = deals(db);
            var dealpro = new List<DealProduct>();
            foreach (var f in finaldDeal)
            {
                dealpro.AddRange(f.DealProduct);
            }
            foreach (var d in Data)
            {
                var pi = Convert.ToInt32(db.ProductVariantDetails.Where(x => x.Id == d.LandingVariant.Id).Include(x => x.Product.ProductCategory).FirstOrDefault().Product?.ProductCategory?.ParentId);
                var catid = getparentCat(pi, db);
                d.Commission = GetCommissionByCategoryId(catid, db);
                if (dealpro.Any(z => z.ProductVariantId == d.VariantDetailId))
                {
                    foreach (var p in dealpro.Where(z => z.ProductVariantId == d.VariantDetailId))
                    {
                        d.Discount = d.Discount + Convert.ToInt32(p.Deal.Discount);
                        var priceaftercomm = d.SellingPrice + (d.SellingPrice * d.Commission / 100);
                        d.SellingPrice = priceaftercomm;
                        d.PriceAfterdiscount = (priceaftercomm - (priceaftercomm * d.Discount / 100));
                        d.ActiveTo = p.Deal.ActiveTo;
                    }
                }
                else
                {
                    d.Discount = d.Discount;
                    var priceaftercomm = d.SellingPrice + (d.SellingPrice * d.Commission / 100);
                    d.SellingPrice = priceaftercomm;
                    d.PriceAfterdiscount = (priceaftercomm - (priceaftercomm * d.Discount / 100));
                }
            }
            return Data;
        }

        public static product7 calculateDealForProducts(product7 Data,PistisContext db)
        {
            var finaldDeal = deals(db);
            var dealpro = new List<DealProduct>();
            foreach (var f in finaldDeal)
            {
                dealpro.AddRange(f.DealProduct);
            }
            var pi = Convert.ToInt32(db.ProductVariantDetails.Where(x => x.Id == Data.VariantDetailId).Include(x => x.Product.ProductCategory).FirstOrDefault().Product?.ProductCategory?.ParentId);
            var catid = getparentCat(pi, db);
            Data.Commission = GetCommissionByCategoryId(catid, db);
            if (dealpro.Any(z => z.ProductVariantId == Data.LandingVariant.Id))
            {
                foreach (var p in dealpro.Where(z => z.ProductVariantId == Data.LandingVariant.Id))
                {
                    Data.Discount = Data.Discount + Convert.ToInt32(p.Deal.Discount);
                    var priceaftercomm = Data.SellingPrice + (Data.SellingPrice * Data.Commission / 100);
                    Data.SellingPrice = priceaftercomm;
                    Data.PriceAfterdiscount = (priceaftercomm - (priceaftercomm * Data.Discount / 100));

                }
            }
            else
            {
                Data.Discount = Data.Discount;
                var priceaftercomm = Data.SellingPrice + (Data.SellingPrice * Data.Commission / 100);
                Data.SellingPrice = priceaftercomm;
                Data.PriceAfterdiscount = (priceaftercomm - (priceaftercomm * Data.Discount / 100));
            }
            return Data;
        }

        public static int getparentCat(int Id, PistisContext db)
        {
            var data = db.ProductCategories.Where(x => x.IsActive == true && x.Id == Id).FirstOrDefault().RemoveReferences();
            if (data.ParentId == null)
                return data.Id;
            else
                return getparentCat(Convert.ToInt32(data.ParentId),db);
        }
        public static int GetCommissionByCategoryId(int id, PistisContext db)
        {
            var obj = new CategoryCommission(db);
            var data = obj.CategoryCommissions.Where(x => x.IsActive == true && x.ProductCategoryId == id).FirstOrDefault();
            if (data != null)
                return data.Commission;
            else
                return 0;
        }
    }
}
