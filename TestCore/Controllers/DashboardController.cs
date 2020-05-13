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
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly PistisContext db;

        public DashboardController(PistisContext pistis)
        {
            db = pistis;
        }
        [HttpGet]
        [Route("getSales")]
        public IActionResult GetSales(int Year, int Period)
        {
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "line";
            chart.title.text = "";
            chart.colors = new List<string>() { "#77cc15" };
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Amount" } });
            chart.xAxis.Add(new XAxis());
            var sale = db.PaymentTransaction.Where(x => x.intent == "sale" && x.paymentID != null && x.CheckoutId != null && x.CheckoutId != 0 && x.Date.Value.Year == Year);

            var refund = db.PaymentTransaction.Where(x => x.intent == "refund" && x.ReturnId != null && x.Date.Value.Year == Year);
            //Sale
            var JanSale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 01).Sum(x => x.TransactionAmount));
            var FebSale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 02).Sum(x => x.TransactionAmount));
            var MarSale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 03).Sum(x => x.TransactionAmount));
            var AprSale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 04).Sum(x => x.TransactionAmount));
            var MaySale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 05).Sum(x => x.TransactionAmount));
            var JunSale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 06).Sum(x => x.TransactionAmount));
            var JulSale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 07).Sum(x => x.TransactionAmount));
            var AugSale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 08).Sum(x => x.TransactionAmount));
            var SepSale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 09).Sum(x => x.TransactionAmount));
            var OctSale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 10).Sum(x => x.TransactionAmount));
            var NovSale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 11).Sum(x => x.TransactionAmount));
            var DecSale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 12).Sum(x => x.TransactionAmount));
            //Refund
            var JanRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 01).Sum(x => x.TransactionAmount));
            var FebRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 02).Sum(x => x.TransactionAmount));
            var MarRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 03).Sum(x => x.TransactionAmount));
            var AprRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 04).Sum(x => x.TransactionAmount));
            var MayRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 05).Sum(x => x.TransactionAmount));
            var JunRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 06).Sum(x => x.TransactionAmount));
            var JulRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 07).Sum(x => x.TransactionAmount));
            var AugRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 08).Sum(x => x.TransactionAmount));
            var SepRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 09).Sum(x => x.TransactionAmount));
            var OctRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 10).Sum(x => x.TransactionAmount));
            var NovRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 11).Sum(x => x.TransactionAmount));
            var DecRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 12).Sum(x => x.TransactionAmount));
            if (Period == 0)//yearly
            {
                chart.series.Add(new Series()
                {
                    name = "Sales",
                    data = new List<Data>() {
                        new Data(){ name="Jan", y=JanSale-JanRefund },
                        new Data(){ name="Feb", y=FebSale-FebRefund },
                        new Data(){ name="Mar", y=MarSale-MarRefund },
                        new Data(){ name="Apr", y=AprSale-AprRefund },
                        new Data(){ name="May", y=MaySale-MayRefund },
                        new Data(){ name="Jun", y=JunSale-JunRefund },
                        new Data(){ name="Jul", y=JulSale-JulRefund },
                        new Data(){ name="Aug", y=AugSale-AugRefund },
                        new Data(){ name="Sep", y=SepSale-SepRefund },
                        new Data(){ name="Oct", y=OctSale-OctRefund },
                        new Data(){ name="Nov", y=NovSale-NovRefund },
                        new Data(){ name="Dec", y=DecSale-DecRefund }

                    }

                });
            }
            else
            {
                if (Period == 1)
                {
                    var sale1 = db.PaymentTransaction.Where(x => x.intent == "sale" && x.paymentID != null && x.CheckoutId != null && x.CheckoutId != 0 && x.Date == DateTime.Now.Date).Sum(x => x.TransactionAmount);
                    var refund1 = db.PaymentTransaction.Where(x => x.intent == "refund" && x.ReturnId != null && x.Date == DateTime.Now.Date).Sum(x => x.TransactionAmount);
                    var today = Convert.ToDecimal(sale1 - refund1);
                    // var data1 = db.Checkouts.Where(x => x.IsActive == true && x.IsPaid == true && x.Date.Date == DateTime.Now.Date).ToList();
                    // var today = data1.Sum(x => x.TransactionAmount);
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
                    var sale1 = db.PaymentTransaction.Where(x => x.intent == "sale" && x.paymentID != null && x.CheckoutId != null && x.CheckoutId != 0 && x.Date == DateTime.Now.Date).ToList();
                    var refund1 = db.PaymentTransaction.Where(x => x.intent == "refund" && x.ReturnId != null && x.Date == DateTime.Now.Date).ToList();
                    var saleamount = sale1.Where(x => x.Date >= DateTime.Now.AddHours(-1)).Sum(x => x.TransactionAmount);
                    var refundamount = refund1.Where(x => x.Date >= DateTime.Now.AddHours(-1)).Sum(x => x.TransactionAmount);

                    var today = Convert.ToDecimal(saleamount - refundamount);
                    //var data1 = db.Checkouts.Where(x => x.IsActive == true && x.IsPaid == true && x.Date.Date == DateTime.Now.Date).ToList();
                    //var today = data1.Where(x => x.Date >= DateTime.Now.AddHours(-1)).Sum(x => x.TransactionAmount);
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
        [Route("getLifeTimeSales")]
        public IActionResult getLifeTimeSales()
        {
            var sale = db.PaymentTransaction.Where(x => x.intent == "sale" && x.paymentID != null && x.CheckoutId != null && x.CheckoutId != 0).Sum(x => x.TransactionAmount);
            var refund = db.PaymentTransaction.Where(x => x.intent == "refund" && x.ReturnId != null).Sum(x => x.TransactionAmount);
            var data = sale - refund;
            return Ok(data);
        }
        [HttpGet]
        [Route("avgOrder")]
        public IActionResult avgOrder()
        {
            var sale = db.PaymentTransaction.Where(x => x.intent == "sale" && x.paymentID != null && x.CheckoutId != null && x.CheckoutId != 0);
            var sumsale = sale.Sum(x => x.TransactionAmount);
            var count = db.PaymentTransaction.Where(x => x.CheckoutId != null && x.CheckoutId != 0).Count();

            var refund = db.PaymentTransaction.Where(x => x.intent == "refund" && x.ReturnId != null);
            var sumrefund = refund.Sum(x => x.TransactionAmount);
            decimal data = Convert.ToDecimal((sumsale - sumrefund) / count);
            return Ok(Math.Round(data, 2));
        }

        [HttpGet]
        [Route("latest5Orders")]
        public IActionResult Latest5Orders()
        {
            var data = db.Checkouts.Where(x => x.IsActive == true).OrderByDescending(x => x.Id).Take(5).Include(x => x.CheckoutItems).Include(x => x.User).ToList();
            var list = new List<LatestOrders>();
            if (data != null)
            {
                foreach (var d in data)
                {
                    var model = new LatestOrders();
                    model.Id = d.Id;
                    model.items = d.CheckoutItems.Count();
                    model.Total = d.TotalAmount;
                    model.CustomerName = d.User?.FirstName ?? "" + " " + d.User?.LastName ?? "";
                    if (model.CustomerName == " ")
                    {
                        model.CustomerName = d.User?.DisplayName ?? "";
                    }
                    if(model.CustomerName!="")
                    list.Add(model);
                }
            }
            return Ok(list);

        }

        [HttpGet]
        [Route("MostSearchedKeywords")]
        public IActionResult MostSearchedKeywords()
        {
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "bar";
            chart.title.text = "";
            chart.colors = new List<string>() { "#77cc15" };
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Number Of Users" } });
            chart.xAxis.Add(new XAxis() { title = new Title() { text = "Search Keywords" } });

            var result = db.SearchTerm.Where(x => x.Name != null && x.IsActive == true).OrderByDescending(x => x.UserCount).Take(5).ToList();
            var tempList = new List<Data>();


            foreach (var item in result)
            {
                var temp = new Data();
                temp.name = item.Name.ToString();
                temp.y = Convert.ToDecimal(item.UserCount);
                tempList.Add(temp);
            }

            chart.series.Add(new Series()
            {
                name = "Most Searched Keywords",
                data = tempList
            });

            return Ok(chart);
        }

        [HttpGet]
        [Route("MostSearchedProducts")]
        public IActionResult MostSearchedProducts()
        {
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "bar";
            chart.title.text = "";
            chart.colors = new List<string>() { "#77cc15" };
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Number Of Users" } });
            chart.xAxis.Add(new XAxis() { title = new Title() { text = "Products" } });

            var result = db.SearchTerm.Where(x => x.Name != null && x.IsActive == true && x.ProductCount != 0)
                .OrderByDescending(x => x.UserCount).Take(5).ToList();

            var tempList = new List<Data>();

            var productList = db.Products.Where(x => x.IsActive == true && x.Name != null).ToList();

            foreach (var item in result)
            {
                var temp = new Data();

                var product = productList.Where(x => x.Name.Contains(item.Name)).FirstOrDefault();

                if (product != null)
                {
                    temp.name = product.Name;
                    temp.y = Convert.ToDecimal(item.UserCount);
                    tempList.Add(temp);
                }
            }

            chart.series.Add(new Series()
            {
                name = "Most Searched Products",
                data = tempList
            });

            return Ok(chart);
        }

        [HttpGet]
        [Route("getBestSellers")]
        public IActionResult getBestSellers()
        {
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "column";
            chart.title.text = "";
            chart.colors = new List<string>() { "#77cc15" };
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Quantity" } });
            chart.xAxis.Add(new XAxis() { title = new Title() { text = "Products" } });

            var result = db.CheckoutItems.Include(x => x.ProductVariantDetail.Product).ToList();

            var xray = result.GroupBy(x => x.ProductVariantDetail.Product.Name).Select(g => new
            {
                Name = g.Key,
                Quantity = g.Sum(x => x.Quantity)
            }).OrderByDescending(x => x.Quantity).Take(5).ToList();

            var tempList = new List<Data>();

            foreach (var item in xray)
            {
                var temp = new Data();
                temp.name = item.Name;
                temp.y = Convert.ToDecimal(item.Quantity);
                tempList.Add(temp);
            }

            chart.series.Add(new Series()
            {
                name = "Best Selling Products",
                data = tempList
            });

            return Ok(chart);

        }

        [HttpGet]
        [Route("getSalesAndRefund")]
        public IActionResult getSalesAndRefund()
        {
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "pie";
            chart.title.text = "";
            chart.colors = new List<string>() { "#77cc15", "#FB1926" };
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Sales" } });
            chart.xAxis.Add(new XAxis() { title = new Title() { text = "Refund" } });

            var totalSales = db.CheckoutItems.Sum(x => x.Quantity);
            var totalRefunds = db.Return.Sum(x => x.Quantity);
            var realSales = totalSales - totalRefunds;

            chart.series.Add(new Series()
            {
                name = "Sales And Refunds",
                data = new List<Data>()
                        {
                            new Data() { name = "Sales", y = realSales },
                            new Data() { name = "Refunds", y = totalRefunds },
                        },
                colorByPoint = true
            });



            return Ok(chart);
        }

        [HttpGet]
        [Route("getNewCustomers")]
        public IActionResult getNewCustomers(int year)
        {
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "line";
            chart.title.text = "";
            chart.colors = new List<string>() { "#77cc15" };
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Amount" } });
            chart.xAxis.Add(new XAxis());

            var userList = db.Users.Where(x => x.DateTime.Value.Year == year && x.RoleId == 1).ToList();

            var JanUsers = userList.Where(x => x.DateTime.Value.Month == 01).Count();
            var FebUsers = userList.Where(x => x.DateTime.Value.Month == 02).Count();
            var MarUsers = userList.Where(x => x.DateTime.Value.Month == 03).Count();
            var AprUsers = userList.Where(x => x.DateTime.Value.Month == 04).Count();
            var MayUsers = userList.Where(x => x.DateTime.Value.Month == 05).Count();
            var JunUsers = userList.Where(x => x.DateTime.Value.Month == 06).Count();
            var JulUsers = userList.Where(x => x.DateTime.Value.Month == 07).Count();
            var AugUsers = userList.Where(x => x.DateTime.Value.Month == 08).Count();
            var SepUsers = userList.Where(x => x.DateTime.Value.Month == 09).Count();
            var OctUsers = userList.Where(x => x.DateTime.Value.Month == 10).Count();
            var NovUsers = userList.Where(x => x.DateTime.Value.Month == 11).Count();
            var DecUsers = userList.Where(x => x.DateTime.Value.Month == 12).Count();

            chart.series.Add(new Series()
            {
                name = "New Customers",
                data = new List<Data>() {
                    new Data() { name = "Jan", y = JanUsers },
                    new Data() { name = "Feb", y = FebUsers },
                    new Data() { name = "Mar", y = MarUsers },
                    new Data() { name = "Apr", y = AprUsers },
                    new Data() { name = "May", y = MayUsers },
                    new Data() { name = "Jun", y = JunUsers },
                    new Data() { name = "Jul", y = JulUsers },
                    new Data() { name = "Aug", y = AugUsers },
                    new Data() { name = "Sep", y = SepUsers },
                    new Data() { name = "Oct", y = OctUsers },
                    new Data() { name = "Nov", y = NovUsers },
                    new Data() { name = "Dec", y = DecUsers }
                }
            });

            return Ok(chart);
        }

        [HttpGet]
        [Route("getFailedTransactions")]
        public IActionResult getFailedTransactions()
        {
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "pie";
            chart.title.text = "";
            chart.colors = new List<string>() { "#77cc15", "#FB1926", "#FFE033", "#FF7A05" };
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Approved or Accredited" } });
            chart.xAxis.Add(new XAxis() { title = new Title() { text = "Failed" } });

            var transactionList = db.PaymentTransaction.ToList();

            var failedTransactions = transactionList.Where(x => x.StatusDetail != "approved" && x.StatusDetail != "accredited").Count();
            var successTransactions = transactionList.Where(x => x.StatusDetail == "approved" || x.StatusDetail == "accredited").Count();
            var onHoldTransactions = transactionList.Where(x => x.StatusDetail == "onhold").Count();
          //  var fraudTransactions = transactionList.Where(x => x.StatusDetail == "fraud").Count();


            chart.series.Add(new Series()
            {
                name = "Transactions",
                data = new List<Data>()
                        {
                            new Data() { name = "Approved", y = successTransactions },
                            new Data() { name = "Failed", y = failedTransactions },
                            new Data() { name = "On Hold", y = onHoldTransactions },
                            //new Data() { name = "Fraud", y = fraudTransactions }
                        },
                colorByPoint = true
            });

            return Ok(chart);
        }

        [HttpGet]
        [Route("getTopCustomers")]
        public IActionResult getTopCustomers()
        {
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "line";
            chart.title.text = "";
            chart.colors = new List<string>() { "#77cc15", "#FB1926" };
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Purchases" } });
            chart.xAxis.Add(new XAxis() { title = new Title() { text = "Customers" } });

            var result = db.CheckoutItems.Include(x => x.User).Where(x => x.User.FirstName != null).ToList();

            var xtt = result;

            var xray = result.GroupBy(x => x.User.FirstName).Select(g => new
            {
                Name = g.Key,
                Quantity = g.Sum(x => x.Quantity)
            }).OrderByDescending(x => x.Quantity).Take(5).ToList();

            var tempList = new List<Data>();

            foreach (var item in xray)
            {
                var temp = new Data();
                temp.name = item.Name;
                temp.y = Convert.ToDecimal(item.Quantity);
                tempList.Add(temp);
            }

            chart.series.Add(new Series()
            {
                name = "Customers With Most Purchases",
                data = tempList
            });

            return Ok(chart);

        }

        [HttpGet]
        [Route("numOfUsersNow")]
        public IActionResult numOfUsers()
        {
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "line";
            chart.title.text = "";
            chart.colors = new List<string>() { "#77cc15" };
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Number Of Users" } });
            chart.xAxis.Add(new XAxis());

            var data = db.UserLogs.ToList();
            var userList = data.Where(x => x.LogInDate >= DateTime.Now.AddHours(-1)).Count();

            chart.series.Add(new Series()
            {
                name = "Users",
                data = new List<Data>()
                        {
                            new Data() { name = "Last one Hour", y = userList },
                        }
            });

            return Ok(chart);

        }

        [HttpGet]
        [Route("numOfUsersToday")]
        public IActionResult numOfUsersToday()
        {
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "line";
            chart.title.text = "";
            chart.colors = new List<string>() { "#77cc15" };
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Number Of Users" } });
            chart.xAxis.Add(new XAxis());

            var data = db.UserLogs.ToList();

            var numOfHours = DateTime.Now.AddHours(-1).Hour;
            List<Data> tempList = new List<Data>();

            for (int i = numOfHours; i > 0; i--)
            {
                var temp = new Data();

                temp.name = DateTime.Now.AddHours(-i).Hour.ToString();
                temp.y = data.Where(x => x.LogInDate >= DateTime.Now.AddHours(-i)).Count();

                tempList.Add(temp);
            }

            chart.series.Add(new Series()
            {
                name = "Hours",
                data = tempList
            });

            return Ok(chart);
        }

        [HttpGet]
        [Route("getUserActivity")]
        public IActionResult getUserActivity()
        {
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "line";
            chart.title.text = "";
            chart.colors = new List<string>() { "#77cc15" };
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Number Of Users" } });
            chart.xAxis.Add(new XAxis() { title = new Title() { text = "Pages" } });

            var dataList = db.UserLogs.Include(x => x.Page).ToList();

            var xray = dataList.GroupBy(x => x.Page.Name).Select(g => new
            {
                Name = g.Key,
                Users = g.Count()
            }).Take(5).ToList();

            var sortedXray = xray.OrderByDescending(x => x.Users).ToList();

            var tempList = new List<Data>();


            foreach (var item in sortedXray)
            {
                var temp = new Data();
                temp.name = item.Name;
                temp.y = Convert.ToDecimal(item.Users);
                tempList.Add(temp);
            }

            chart.series.Add(new Series()
            {
                name = "User Activity",
                data = tempList
            });

            return Ok(chart);
        }
        [HttpGet]
        [Route("lastSearchTerms")]
        public IActionResult lastsearch()
        {
            try
            {
                var data = db.SearchTerm.Where(x => x.IsActive == true && x.Name != null).OrderByDescending(x=>x.Id).Take(5).ToList();
                return Ok(data);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        [Route("topSearchedItems")]
        public IActionResult lastsearch1()
        {
            try
            {
                var data = db.SearchTerm.Where(x => x.IsActive == true && x.Name!=null).OrderByDescending(x => x.UserCount).Take(5).ToList();
                return Ok(data);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}