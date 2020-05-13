using Localdb;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCore.Controllers;

namespace TestCore.Helper
{
    public static class PriceIncrementHelper
    {
        public static List<ProductVariantDetail> calculatePrice(List<ProductVariantDetail> Data, PistisContext db)
        {
            var increment = db.PaymentConfiguration.Where(x => x.IsApplied == true).FirstOrDefault();
            if (increment != null)
            {
                foreach (var d in Data)
                {
                    if (d.Price >= increment.Amount)
                    {
                        d.Price = d.Price + (d.Price * increment.Percentage / 100);
                        d.PriceAfterdiscount = (d.Price - (d.Price * d.Discount / 100));

                    }
                }
            }
            return Data;
        }

         //Product Cart Page

        public static List<GetCart> calculatePrice(List<GetCart> Data, PistisContext db)
        {
            var increment = db.PaymentConfiguration.Where(x => x.IsApplied == true).FirstOrDefault();
            if (increment != null)
            {
                foreach (var d in Data)
                {
                    if (d.SellingPrice >= increment.Amount)
                    {
                        d.SellingPrice = d.SellingPrice + (d.SellingPrice * increment.Percentage / 100);
                        d.PriceAfterDiscount = (d.SellingPrice - (d.SellingPrice * d.Discount / 100));
                        d.Amount = d.PriceAfterDiscount * d.Quantity;
                        var TotalAmount = Data.Sum(x => x.Amount);
                        d.TotalAmount = Convert.ToDecimal(TotalAmount.ToString("#.00"));

                    }
                }

                foreach (var d in Data)
                {
                        var TotalAmount = Data.Sum(x => x.Amount);
                        d.TotalAmount = Convert.ToDecimal(TotalAmount.ToString("#.00"));

                }
            }
            return Data;
        }

        //all product info
        public static product1 calculatePriceForProducts(product1 d, PistisContext db)
        {

            var increment = db.PaymentConfiguration.Where(x => x.IsApplied == true).FirstOrDefault();
            if (increment != null)
            {
                    if (d.SellingPrice >= increment.Amount)
                    {
                        d.SellingPrice = d.SellingPrice + (d.SellingPrice * increment.Percentage / 100);
                        d.PriceAfterdiscount = (d.SellingPrice - (d.SellingPrice * d.Discount / 100));
                    }
            }
            return d;
        }
        //for product detai;
        public static ProductDetail calculatePriceForProductDetail(ProductDetail d, PistisContext db)
        {
            var increment = db.PaymentConfiguration.Where(x => x.IsApplied == true).FirstOrDefault();
            if (increment != null)
            {
                if (d.SellingPrice >= increment.Amount)
                {
                    d.SellingPrice = d.SellingPrice + (d.SellingPrice * increment.Percentage / 100);
                    d.PriceAfterDiscount = (d.SellingPrice - (d.SellingPrice * d.Discount / 100));
                }
            }
            return d;
        }
        public static ProductVariantDetail calculatePriceForProductDetailModel(ProductVariantDetail d, PistisContext db)
        {
            var increment = db.PaymentConfiguration.Where(x => x.IsApplied == true).FirstOrDefault();
            if (increment != null)
            {
                if (d.Price >= increment.Amount)
                {
                    d.Price = d.Price + (d.Price * increment.Percentage / 100);
                    d.PriceAfterdiscount = (d.Price - (d.Price * d.Discount / 100));
                }
            }
            return d;
        }

        //FOrList
        public static List<product1> calculatePriceForProductsList(List<product1> Data, PistisContext db)
        {
            var increment = db.PaymentConfiguration.Where(x => x.IsApplied == true).FirstOrDefault();
            if (increment != null)
            {
                foreach (var d in Data)
                {
                    if (d.SellingPrice >= increment.Amount)
                    {
                        d.SellingPrice = d.SellingPrice + (d.SellingPrice * increment.Percentage / 100);
                        d.PriceAfterdiscount = (d.SellingPrice - (d.SellingPrice * d.Discount / 100));
                    }
                }
            }
            return Data;
        }

        public static product7 calculatePriceForProducts(product7 d, PistisContext db)
        {
            var increment = db.PaymentConfiguration.Where(x => x.IsApplied == true).FirstOrDefault();
            if (increment != null)
            {
                    if (d.SellingPrice >= increment.Amount)
                    {
                        d.SellingPrice = d.SellingPrice + (d.SellingPrice * increment.Percentage / 100);
                        d.PriceAfterdiscount = (d.SellingPrice - (d.SellingPrice * d.Discount / 100));
                    }
            }
            return d;
        }

    }
}
