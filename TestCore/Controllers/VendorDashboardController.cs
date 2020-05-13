using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Localdb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TestCore.Controllers
{
    [Route("api/vendorDashboard")]
    [ApiController]
    public class VendorDashboardController : ControllerBase
    {
        private readonly PistisContext db;

        public VendorDashboardController(PistisContext pistis)
        {
            db = pistis;
        }
        //[HttpGet]
        //[Route("getSales")]
        //public IActionResult GetSales(int Year, int Period,int VendorId)
        //{
        //    HighChartModel chart = new HighChartModel();
        //    chart.chart.type = "line";
        //    chart.title.text = "";
        //    chart.colors = new List<string>() { "#77cc15" };
        //    chart.yAxis.Add(new YAxis() { title = new Title() { text = "Amount" } });
        //    chart.xAxis.Add(new XAxis());

        //    var data = db.VendorTransaction.Where(x => x.IsActive == true && x.TransactionDate.Year == Year && x.VendorId==VendorId).ToList();
        //    var JanSale = data.Where(x => x.TransactionDate.Month == 01).Sum(x => x.CheckoutItems.Sum(y=>y.Amount));
        //    var FebSale = data.Where(x => x.TransactionDate.Month == 02).Sum(x => x.CheckoutItems.Sum(y => y.Amount));
        //    var MarSale = data.Where(x => x.TransactionDate.Month == 03).Sum(x => x.CheckoutItems.Sum(y => y.Amount));
        //    var AprSale = data.Where(x => x.TransactionDate.Month == 04).Sum(x => x.CheckoutItems.Sum(y => y.Amount));
        //    var MaySale = data.Where(x => x.TransactionDate.Month == 05).Sum(x => x.CheckoutItems.Sum(y => y.Amount));
        //    var JunSale = data.Where(x => x.TransactionDate.Month == 06).Sum(x => x.CheckoutItems.Sum(y => y.Amount));
        //    var JulSale = data.Where(x => x.TransactionDate.Month == 07).Sum(x => x.CheckoutItems.Sum(y => y.Amount));
        //    var AugSale = data.Where(x => x.TransactionDate.Month == 08).Sum(x => x.CheckoutItems.Sum(y => y.Amount));
        //    var SepSale = data.Where(x => x.TransactionDate.Month == 09).Sum(x => x.CheckoutItems.Sum(y => y.Amount));
        //    var OctSale = data.Where(x => x.TransactionDate.Month == 10).Sum(x => x.CheckoutItems.Sum(y => y.Amount));
        //    var NovSale = data.Where(x => x.CheckoutDate.Month == 11).Sum(x => x.CheckoutItems.Sum(y => y.Amount));
        //    var DecSale = data.Where(x => x.CheckoutDate.Month == 12).Sum(x => x.CheckoutItems.Sum(y => y.Amount));

        //    var Refund = db.Checkouts.Where(x => x.IsActive == true && x.IsPaid == true  && x.CheckoutDate.Year == Year).Include(y => y.CheckoutItems.Where(x => x.VendorId == VendorId && x.ReturnedQuantity>0 && x.IsActive == true)).ToList();

        //    var JanRefund = Refund.Where(x => x.CheckoutDate.Month == 01).Sum(x => x.CheckoutItems.Sum(y =>y. Amount));
        //    var FebRefund = data.Where(x => x.CheckoutDate.Month == 02).Sum(x => x.CheckoutItems.Sum(y => y.Amount));
        //    var MarRefund = data.Where(x => x.CheckoutDate.Month == 03).Sum(x => x.CheckoutItems.Sum(y => y.Amount));
        //    var AprRefund = data.Where(x => x.CheckoutDate.Month == 04).Sum(x => x.CheckoutItems.Sum(y => y.Amount));
        //    var MayRefund = data.Where(x => x.CheckoutDate.Month == 05).Sum(x => x.CheckoutItems.Sum(y => y.Amount));
        //    var JunRefund = data.Where(x => x.CheckoutDate.Month == 06).Sum(x => x.CheckoutItems.Sum(y => y.Amount));
        //    var JulRefund = data.Where(x => x.CheckoutDate.Month == 07).Sum(x => x.CheckoutItems.Sum(y => y.Amount));
        //    var AugRefund = data.Where(x => x.CheckoutDate.Month == 08).Sum(x => x.CheckoutItems.Sum(y => y.Amount));
        //    var SepRefund = data.Where(x => x.CheckoutDate.Month == 09).Sum(x => x.CheckoutItems.Sum(y => y.Amount));
        //    var OctRefund = data.Where(x => x.CheckoutDate.Month == 10).Sum(x => x.CheckoutItems.Sum(y => y.Amount));
        //    var NovRefund = data.Where(x => x.CheckoutDate.Month == 11).Sum(x => x.CheckoutItems.Sum(y => y.Amount));
        //    var DecRefund = data.Where(x => x.CheckoutDate.Month == 12).Sum(x => x.CheckoutItems.Sum(y => y.Amount));

        //    if (Period == 0)//yearly
        //    {
        //        chart.series.Add(new Series()
        //        {
        //            name = "Sales",
        //            data = new List<Data>() {
        //                new Data(){ name="Jan", y=JanSale-JanRefund},
        //                new Data(){ name="Feb", y=FebSale-FebRefund },
        //                new Data(){ name="Mar", y=MarSale-MarRefund },
        //                new Data(){ name="Apr", y=AprSale-AprRefund },
        //                new Data(){ name="May", y=MaySale-MayRefund },
        //                new Data(){ name="Jun", y=JunSale-JunRefund},
        //                new Data(){ name="Jul", y=JulSale-JulRefund },
        //                new Data(){ name="Aug", y=AugSale-AugRefund },
        //                new Data(){ name="Sep", y=SepSale-SepRefund },
        //                new Data(){ name="Oct", y=OctSale-OctRefund },
        //                new Data(){ name="Nov", y=NovSale-NovRefund },
        //                new Data(){ name="Dec", y=DecSale-DecRefund }

        //            }

        //        });
        //    }
        //    else
        //    {
        //        if (Period == 1)
        //        {
        //            var data1 = db.Checkouts.Where(x => x.IsActive == true && x.IsPaid == true && x.CheckoutDate.Date == DateTime.Now.Date).ToList();
        //            var today = data1.Sum(x => x.TotalAmount);
        //            chart.series.Add(new Series()
        //            {
        //                name = "Sales",
        //                data = new List<Data>()
        //                {
        //                    new Data() { name = "Today", y = today },
        //                }
        //            });
        //        }
        //        else if (Period == 2)
        //        {
        //            var data1 = db.Checkouts.Where(x => x.IsActive == true && x.IsPaid == true && x.CheckoutDate.Date == DateTime.Now.Date).ToList();
        //            var today = data1.Where(x => x.CheckoutDate >= DateTime.Now.AddHours(-1)).Sum(x => x.TotalAmount);
        //            chart.series.Add(new Series()
        //            {
        //                name = "Sales",
        //                data = new List<Data>()
        //                {
        //                    new Data() { name = "Last one Hour", y = today },
        //                }
        //            });
        //        }
        //    }
        //    //else
        //    //{
        //    //    var FirstWeek = data.Where(x => x.CheckoutDate.Month == Period && x.CheckoutDate.Day >= 1 && x.CheckoutDate.Day <= 7).Sum(x => x.TotalAmount);
        //    //    var SecondWeek = data.Where(x => x.CheckoutDate.Month == Period && x.CheckoutDate.Day >= 8 && x.CheckoutDate.Day <= 14).Sum(x => x.TotalAmount);
        //    //    var ThirdWeek = data.Where(x => x.CheckoutDate.Month == Period && x.CheckoutDate.Day >= 15 && x.CheckoutDate.Day <= 21).Sum(x => x.TotalAmount);
        //    //    var EndWeek = data.Where(x => x.CheckoutDate.Month == Period && x.CheckoutDate.Day >= 22 && x.CheckoutDate.Day <= 31).Sum(x => x.TotalAmount);
        //    //    if (Period == 1)
        //    //    {
        //    //        chart.series.Add(new Series()
        //    //        {
        //    //            name = "Sales",
        //    //            data = new List<Data>() {
        //    //            new Data(){ name="Jan 1", y=FirstWeek },
        //    //            new Data(){ name="", y=SecondWeek },
        //    //            new Data(){ name="", y=ThirdWeek },
        //    //            new Data(){ name="Jan 31", y=EndWeek },
        //    //        }

        //    //        });
        //    //    }
        //    //    else if (Period == 2)
        //    //    {
        //    //        if (Year % 4 == 0)
        //    //        {
        //    //            chart.series.Add(new Series()
        //    //            {
        //    //                name = "Sales",
        //    //                data = new List<Data>() {
        //    //            new Data(){ name="Feb 1", y=FirstWeek },
        //    //            new Data(){ name="", y=SecondWeek },
        //    //            new Data(){ name="", y=ThirdWeek },
        //    //            new Data() { name = "Feb 29", y = EndWeek  }

        //    //        }

        //    //            });
        //    //        }
        //    //        else
        //    //        {
        //    //            chart.series.Add(new Series()
        //    //            {
        //    //                name = "Sales",
        //    //                data = new List<Data>() {
        //    //            new Data(){ name="Feb 1", y=FirstWeek },
        //    //            new Data(){ name="", y=SecondWeek },
        //    //            new Data(){ name="", y=ThirdWeek },
        //    //            new Data() { name = "Feb 28", y = EndWeek  }
        //    //        }
        //    //            });
        //    //        }
        //    //    }
        //    //    else if (Period == 3)
        //    //    {
        //    //        chart.series.Add(new Series()
        //    //        {
        //    //            name = "Sales",
        //    //            data = new List<Data>() {
        //    //            new Data(){ name="Mar 1", y=FirstWeek },
        //    //            new Data(){ name="", y=SecondWeek },
        //    //            new Data(){ name="", y=ThirdWeek },
        //    //            new Data(){ name="Mar 31", y=EndWeek },
        //    //        }

        //    //        });
        //    //    }
        //    //    else if (Period == 4)
        //    //    {
        //    //        chart.series.Add(new Series()
        //    //        {
        //    //            name = "Sales",
        //    //            data = new List<Data>() {
        //    //            new Data(){ name="Apr 1", y=FirstWeek },
        //    //            new Data(){ name="", y=SecondWeek },
        //    //            new Data(){ name="", y=ThirdWeek },
        //    //            new Data(){ name="Apr 30", y=EndWeek },
        //    //        }

        //    //        });
        //    //    }
        //    //    else if (Period == 5)
        //    //    {
        //    //        chart.series.Add(new Series()
        //    //        {
        //    //            name = "Sales",
        //    //            data = new List<Data>() {
        //    //            new Data(){ name="May 1", y=FirstWeek },
        //    //            new Data(){ name="", y=SecondWeek },
        //    //            new Data(){ name="", y=ThirdWeek },
        //    //            new Data(){ name="May 31", y=EndWeek },
        //    //        }

        //    //        });
        //    //    }
        //    //    else if (Period == 6)
        //    //    {
        //    //        chart.series.Add(new Series()
        //    //        {
        //    //            name = "Sales",
        //    //            data = new List<Data>() {
        //    //            new Data(){ name="Jun 1", y=FirstWeek },
        //    //            new Data(){ name="", y=SecondWeek },
        //    //            new Data(){ name="", y=ThirdWeek },
        //    //            new Data(){ name="Jun 30", y=EndWeek },
        //    //        }

        //    //        });
        //    //    }
        //    //    else if (Period == 7)
        //    //    {
        //    //        chart.series.Add(new Series()
        //    //        {
        //    //            name = "Sales",
        //    //            data = new List<Data>() {
        //    //            new Data(){ name="Jul 1", y=FirstWeek },
        //    //            new Data(){ name="", y=SecondWeek },
        //    //            new Data(){ name="", y=ThirdWeek },
        //    //            new Data(){ name="Jul 31", y=EndWeek },
        //    //        }

        //    //        });
        //    //    }
        //    //    else if (Period == 8)
        //    //    {
        //    //        chart.series.Add(new Series()
        //    //        {
        //    //            name = "Sales",
        //    //            data = new List<Data>() {
        //    //            new Data(){ name="Aug 1", y=FirstWeek },
        //    //            new Data(){ name="", y=SecondWeek },
        //    //            new Data(){ name="", y=ThirdWeek },
        //    //            new Data(){ name="Aug 31", y=EndWeek },
        //    //        }

        //    //        });
        //    //    }
        //    //    else if (Period == 9)
        //    //    {
        //    //        chart.series.Add(new Series()
        //    //        {
        //    //            name = "Sales",
        //    //            data = new List<Data>() {
        //    //            new Data(){ name="Sep 1", y=FirstWeek },
        //    //            new Data(){ name="", y=SecondWeek },
        //    //            new Data(){ name="", y=ThirdWeek },
        //    //            new Data(){ name="Sep 30", y=EndWeek },
        //    //        }

        //    //        });
        //    //    }
        //    //    else if (Period == 10)
        //    //    {
        //    //        chart.series.Add(new Series()
        //    //        {
        //    //            name = "Sales",
        //    //            data = new List<Data>() {
        //    //            new Data(){ name="Oct 1", y=FirstWeek },
        //    //            new Data(){ name="", y=SecondWeek },
        //    //            new Data(){ name="", y=ThirdWeek },
        //    //            new Data(){ name="Oct 31", y=EndWeek },
        //    //        }

        //    //        });
        //    //    }
        //    //    else if (Period == 11)
        //    //    {
        //    //        chart.series.Add(new Series()
        //    //        {
        //    //            name = "Sales",
        //    //            data = new List<Data>() {
        //    //            new Data(){ name="Nov 1", y=FirstWeek },
        //    //            new Data(){ name="", y=SecondWeek },
        //    //            new Data(){ name="", y=ThirdWeek },
        //    //            new Data(){ name="Nov 30", y=EndWeek },
        //    //        }

        //    //        });
        //    //    }
        //    //    else if (Period == 12)
        //    //    {
        //    //        chart.series.Add(new Series()
        //    //        {
        //    //            name = "Sales",
        //    //            data = new List<Data>() {
        //    //            new Data(){ name="Dec 1", y=FirstWeek },
        //    //            new Data(){ name="", y=SecondWeek },
        //    //            new Data(){ name="", y=ThirdWeek },
        //    //            new Data(){ name="Dec 31", y=EndWeek },
        //    //        }

        //    //        });
        //    //    }
        //    //}

        //    return Ok(chart);
        //}

        [HttpGet]
        [Route("getSales")]
        public IActionResult GetSales(int Year, int Period, int VendorId)
        {
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "line";
            chart.title.text = "";
            chart.chart.height = 290;
           // chart.chart.width = 100 ;
            chart.colors = new List<string>() { "#77cc15" };
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Amount" } });
            chart.xAxis.Add(new XAxis());

            var data = db.VendorTransaction.Where(x => x.IsActive == true && x.TransactionDate.Year == Year && x.IsPaidByPistis==false && x.VendorId == VendorId).ToList();
            var JanSale = data.Where(x => x.TransactionDate.Month == 01).Sum(x => x.AmountPaid);
            var FebSale = data.Where(x => x.TransactionDate.Month == 02).Sum(x => x.AmountPaid);
            var MarSale = data.Where(x => x.TransactionDate.Month == 03).Sum(x => x.AmountPaid);
            var AprSale = data.Where(x => x.TransactionDate.Month == 04).Sum(x => x.AmountPaid);
            var MaySale = data.Where(x => x.TransactionDate.Month == 05).Sum(x => x.AmountPaid);
            var JunSale = data.Where(x => x.TransactionDate.Month == 06).Sum(x => x.AmountPaid);
            var JulSale = data.Where(x => x.TransactionDate.Month == 07).Sum(x => x.AmountPaid);
            var AugSale = data.Where(x => x.TransactionDate.Month == 08).Sum(x => x.AmountPaid);
            var SepSale = data.Where(x => x.TransactionDate.Month == 09).Sum(x => x.AmountPaid);
            var OctSale = data.Where(x => x.TransactionDate.Month == 10).Sum(x => x.AmountPaid);
            var NovSale = data.Where(x => x.TransactionDate.Month == 11).Sum(x => x.AmountPaid);
            var DecSale = data.Where(x => x.TransactionDate.Month == 12).Sum(x => x.AmountPaid);

          

            if (Period == 0)//yearly
            {
                chart.series.Add(new Series()
                {
                    name = "Sales",
                    data = new List<Data>() {
                        new Data(){ name="Jan", y=JanSale},
                        new Data(){ name="Feb", y=FebSale},
                        new Data(){ name="Mar", y=MarSale},
                        new Data(){ name="Apr", y=AprSale },
                        new Data(){ name="May", y=MaySale },
                        new Data(){ name="Jun", y=JunSale },
                        new Data(){ name="Jul", y=JulSale },
                        new Data(){ name="Aug", y=AugSale },
                        new Data(){ name="Sep", y=SepSale },
                        new Data(){ name="Oct", y=OctSale },
                        new Data(){ name="Nov", y=NovSale },
                        new Data(){ name="Dec", y=DecSale }

                    }

                });
            }
            else
            {
                if (Period == 1)
                {
                    var data1 = db.VendorTransaction.Where(x => x.IsActive == true && x.TransactionDate.Date == DateTime.Now.Date && x.IsPaidByPistis == false && x.VendorId == VendorId).ToList();
                    var today = data1.Sum(x => x.AmountPaid);
                    chart.series.Add(new Series()
                    {
                        name = "Sales",
                        data = new List<Data>()
                        {
                            new Data() { name = "Today", y = today },
                        }
                    });
                }
                else if (Period == 2)
                {
                    var data1 = db.VendorTransaction.Where(x => x.IsActive == true && x.TransactionDate.Date == DateTime.Now.Date && x.IsPaidByPistis == false && x.VendorId == VendorId).ToList();

                    var today = data1.Where(x => x.TransactionDate >= DateTime.Now.AddHours(-1)).Sum(x => x.AmountPaid);
                    chart.series.Add(new Series()
                    {
                        name = "Sales",
                        data = new List<Data>()
                        {
                            new Data() { name = "Last one Hour", y = today },
                        }
                    });
                }
            }
           

            return Ok(chart);
        }
        [HttpGet]
        [Route("getBalance")]
        public IActionResult getBalance(int VendorId)
        {
            var balance = db.VendorTransactionSummary.Where(x => x.IsActive == true  && x.VendorId == VendorId).FirstOrDefault()?.DueAmount;
            return Ok(balance);
        }
        [HttpGet]
        [Route("getLifeTimeSales")]
        public IActionResult getLifeTimeSales(int VendorId)
        {
            var sale = db.VendorTransaction.Where(x => x.IsActive == true && x.VendorId == VendorId).Sum(x => x.AmountPaid);
            //var refund = db.CheckoutItems.Where(x => x.IsActive == true && x.Checkout.IsPaid == true && x.VendorId == VendorId && x.ReturnedQuantity > 0).Sum(x => x.Amount);
            //var data = sale - refund;
           
            return Ok(sale);
        }
        [HttpGet]
        [Route("avgOrder")]
        public IActionResult avgOrder(int VendorId)
        {
            
            var sale = db.VendorTransaction.Where(x => x.IsActive == true && x.VendorId == VendorId).ToList();
            //var refund = db.CheckoutItems.Where(x => x.IsActive == true && x.Checkout.IsPaid == true && x.VendorId == VendorId && x.ReturnedQuantity > 0).Sum(x => x.Amount);
            var sumsale = sale.Sum(x => x.AmountPaid);
            decimal data = 0;
            if(sale.Count()>0)
           
             data = Convert.ToDecimal(sumsale / sale.Count());
            return Ok(Math.Round(data, 2));
        }

        [HttpGet] 
        [Route("lowStock6Products")]
        public IActionResult lowStock6Products(int VendorId)
        {
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "column";
            chart.title.text = "";
            chart.chart.height = 290;
            chart.colors = new List<string>() { "#50a6f8", "#6fc040", "#f5d427", "#f3901d", "#8085E9", "#e45e33" };
            //chart.colors = new List<string>() { "#77cc15" };
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Stock" } });
            chart.xAxis.Add(new XAxis());


            var data = db.ProductVariantDetails.Where(x => x.IsActive == true && x.Product.VendorId == VendorId && x.Product.IsActive==true).OrderBy(x => x.InStock).Take(6).Include(x => x.Product).ToList();
            var list = new List<Data>();

            foreach (var d in data)
            {
                var model = new Data();
                model.name = d.Product.Name;
                model.y = d.InStock;
                list.Add(model);
            }

            chart.series.Add(new Series()
            {
                name = "Low Stock 6 Products",
                data = list,
                colorByPoint = true,
            });
           
            return Ok(chart);

        }
        [HttpGet]
        [Route("avgRating")]
        public IActionResult avgRating(int VendorId)
        {
            var data = db.RatingReviews.Where(x => x.Product.VendorId == VendorId && x.IsActive == true).Include(x => x.Product).ToList();
            decimal avg=0;
            if(data.Count()>0)
             avg = data.Sum(x => x.Rating) / data.Count();
           
            return Ok(Math.Round(avg,2));
        }
        [HttpGet]
        [Route("latest6Reviews")]
        public IActionResult latest6Reviews(int VendorId)
        {
            var data = db.RatingReviews.Where(x => x.Product.VendorId == VendorId && x.IsActive == true).Take(6).Include(x => x.Product).Include(x=>x.User).ToList();
            var list = new List<LatestReviews>();
            if (data != null)
            {
                foreach (var d in data)
                {
                    var model = new LatestReviews();
                    model.Id = d.Id;
                    model.Product = d.Product.Name;
                    model.Customer = d.User?.FirstName ?? "" + " " + d.User?.LastName ?? "";
                    if (model.Customer == "")
                        model.Customer = d.User?.DisplayName ?? "";
                    model.Rate = d.Rating;
                    model.Review = d.Review;
                    list.Add(model);
                }
            }
            return Ok(list);

        }
    }
}