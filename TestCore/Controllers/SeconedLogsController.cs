using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Localdb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using MoreLinq;

namespace TestCore.Controllers
{
    [Microsoft.AspNetCore.Cors.EnableCors("EnableCORS")]
    [Route("api/newdash")]
    [ApiController]
    public class SeconedLogsController :  ControllerBase
    {
        private readonly PistisContext db;


        public SeconedLogsController(PistisContext pistis)
        {
            db = pistis;
        }
        [HttpGet]
        [Route("getFailedTransactions")]
        public IActionResult getFailedTransactions()
        {
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "pie";
            chart.title.text = "";
            chart.chart.height = 600;
            chart.chart.width = 500;
            chart.colors = new List<string>() { "#77cc15", "#FB1926", "#FFE033", "#FF7A05" ,"#fe5722","#ff9902","#bb8800","#3eb943","#4cb050","#cddc39","#3f51b5","#2095f","#9c28b1","#cc29ea"};
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Approved or Accredited" } });
            chart.xAxis.Add(new XAxis() { title = new Title() { text = "Failed" } });

            var logs = db.Log.Where(x => x.PageUrl != null).ToList();

            var homepage = logs.Where(x => x.PageUrl == "home page").Count();
            var checkout = logs.Where(x => x.PageUrl.Trim().ToLower() == "/checkout-process/checkout".Trim().ToLower()).Count();
            var productDetail = logs.Where(x=>x.PageUrl.Contains("product-details")).Count();
            var mycart = logs.Where(x => x.PageUrl.Contains("mycart")).Count();
            var productCatelogue = logs.Where(x => x.PageUrl.Contains("productcatalogue")).Count();
            var wishlist = logs.Where(x => x.PageUrl.Contains("wishlist")).Count();
            var CompareProducts = logs.Where(x => x.PageUrl.Contains("CompareProducts")).Count();
            var header = logs.Where(x => x.PageUrl.Contains("header")).Count();
            var footer = logs.Where(x => x.PageUrl.Contains("footer")).Count();
            var profile = logs.Where(x => x.PageUrl.Contains("profile")).Count();
            var AllNotifications = logs.Where(x => x.PageUrl.Contains("AllNotifications")).Count();
            var confirmation = logs.Where(x => x.PageUrl.Contains("confirmation")).Count();
            chart.series.Add(new Series()
            {
                name = "Request,Response & Errors",
                data = new List<Data>()
                        {
                            new Data() { name = "home page", y = homepage },
                          new Data() { name = "ProductDetails", y = productDetail },
                          new Data() { name = "Checkout", y = checkout },
                          new Data() { name = "mycart", y = mycart },
                          new Data() { name = "productcatalogue", y = productCatelogue },
                          new Data() { name = "wishlist", y = wishlist },
                          new Data() { name = "CompareProducts", y = CompareProducts },
                          new Data() { name = "header", y = header },
                          new Data() { name = "footer", y = footer },
                          new Data() { name = "profile", y = profile },
                          new Data() { name = "AllNotifications", y = AllNotifications },
                          new Data() { name = "confirmation", y = confirmation },
                           // new Data() { name = "On Hold", y = onHoldTransactions },
                            //new Data() { name = "Fraud", y = fraudTransactions }
                        },
                colorByPoint = true
            });

            return Ok(chart);
        }
        [HttpGet]
        [Route("getFailedMobileTransactions")]
        public IActionResult getFailedMobileTransactions()
        {
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "pie";
            chart.title.text = "";
            chart.chart.height = 600;
            chart.chart.width = 500;
            chart.colors = new List<string>() { "#77cc15", "#FB1926", "#FFE033", "#FF7A05", "#fe5722", "#ff9902", "#bb8800", "#3eb943", "#4cb050", "#cddc39", "#3f51b5", "#2095f", "#9c28b1", "#cc29ea" };
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Approved or Accredited" } });
            chart.xAxis.Add(new XAxis() { title = new Title() { text = "Failed" } });

            var logs = db.Log.Where(x => x.PageUrl != null && x.AppVersion!="Web").ToList();

            var homepage = logs.Where(x => x.PageUrl == "home page").Count();
            var checkout = logs.Where(x => x.PageUrl.Trim().ToLower() == "/checkout-process/checkout".Trim().ToLower()).Count();
            var productDetail = logs.Where(x => x.PageUrl.Contains("product-details")).Count();
            var mycart = logs.Where(x => x.PageUrl.Contains("mycart")).Count();
            var productCatelogue = logs.Where(x => x.PageUrl.Contains("productcatalogue")).Count();
            var wishlist = logs.Where(x => x.PageUrl.Contains("wishlist")).Count();
            var CompareProducts = logs.Where(x => x.PageUrl.Contains("CompareProducts")).Count();
            var header = logs.Where(x => x.PageUrl.Contains("header")).Count();
            var footer = logs.Where(x => x.PageUrl.Contains("footer")).Count();
            var profile = logs.Where(x => x.PageUrl.Contains("profile")).Count();
            var AllNotifications = logs.Where(x => x.PageUrl.Contains("AllNotifications")).Count();
            var confirmation = logs.Where(x => x.PageUrl.Contains("confirmation")).Count();
            chart.series.Add(new Series()
            {
                name = "Request,Response & Errors",
                data = new List<Data>()
                        {
                            new Data() { name = "home page", y = homepage },
                          new Data() { name = "ProductDetails", y = productDetail },
                          new Data() { name = "Checkout", y = checkout },
                          new Data() { name = "mycart", y = mycart },
                          new Data() { name = "productcatalogue", y = productCatelogue },
                          new Data() { name = "wishlist", y = wishlist },
                          new Data() { name = "CompareProducts", y = CompareProducts },
                          new Data() { name = "header", y = header },
                          new Data() { name = "footer", y = footer },
                          new Data() { name = "profile", y = profile },
                          new Data() { name = "AllNotifications", y = AllNotifications },
                          new Data() { name = "confirmation", y = confirmation },
                           // new Data() { name = "On Hold", y = onHoldTransactions },
                            //new Data() { name = "Fraud", y = fraudTransactions }
                        },
                colorByPoint = true
            });

            return Ok(chart);
        }
        [HttpGet]
        [Route("getFilterFailedTransactions")]
        public IActionResult getFailedTransactions(string date,int UserId)
        {
            var changeIntoDate = Convert.ToDateTime(date);
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "pie";
            chart.title.text = "";
            chart.chart.height = 700;
            chart.chart.width = 700;
            chart.colors = new List<string>() { "#782f85", "#FB1926", "#FFE033", "#FF7A05", "#fe5722", "#ff9902", "#bb8800", "#3eb943", "#4cb050", "#cddc39", "#3f51b5", "#2095f", "#9c28b1", "#cc29ea" };
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Approved or Accredited" } });
            chart.xAxis.Add(new XAxis() { title = new Title() { text = "Failed" } });

            var logs = db.Log.Where(x => x.PageUrl != null).ToList();
            logs = logs.Where(x => x.ActionDateTime.Value.Date == changeIntoDate.Date).ToList();
            if (logs != null)
            {
                logs = logs.Where(x => x.UserId == UserId).ToList();
            }
            var homepage = logs.Where(x => x.PageUrl == "home page").Count();
            var checkout = logs.Where(x => x.PageUrl.Trim().ToLower() == "/checkout-process/checkout".Trim().ToLower()).Count();
            var productDetail = logs.Where(x => x.PageUrl.Contains("product-details")).Count();
            var mycart = logs.Where(x => x.PageUrl.Contains("mycart")).Count();
            var productCatelogue = logs.Where(x => x.PageUrl.Contains("productcatalogue")).Count();
            var wishlist = logs.Where(x => x.PageUrl.Contains("wishlist")).Count();
            var CompareProducts = logs.Where(x => x.PageUrl.Contains("CompareProducts")).Count();
            var header = logs.Where(x => x.PageUrl.Contains("header")).Count();
            var footer = logs.Where(x => x.PageUrl.Contains("footer")).Count();
            var profile = logs.Where(x => x.PageUrl.Contains("profile")).Count();
            var AllNotifications = logs.Where(x => x.PageUrl.Contains("AllNotifications")).Count();
            var confirmation = logs.Where(x => x.PageUrl.Contains("confirmation")).Count();
            chart.series.Add(new Series()
            {
                name = "Request,Response & Errors",
                data = new List<Data>()
                        {
                            new Data() { name = "home page", y = homepage },
                          new Data() { name = "ProductDetails", y = productDetail },
                          new Data() { name = "Checkout", y = checkout },
                          new Data() { name = "Mycart", y = mycart },
                          new Data() { name = "ProductCatelogue", y = productCatelogue },
                          new Data() { name = "Wishlist", y = wishlist },
                          new Data() { name = "CompareProducts", y = CompareProducts },
                          new Data() { name = "Header", y = header },
                          new Data() { name = "Footer", y = footer },
                          new Data() { name = "Profile", y = profile },
                          new Data() { name = "AllNotifications", y = AllNotifications },
                          new Data() { name = "confirmation", y = AllNotifications },
                           // new Data() { name = "On Hold", y = onHoldTransactions },
                            //new Data() { name = "Fraud", y = fraudTransactions }
                        },
                colorByPoint = true
            });

            return Ok(chart);
        }
        [HttpGet]
        [Route("getMObileFailedTransactions")]
        public IActionResult getMObileFailedTransactions(string date, int UserId)
        {
            var changeIntoDate = Convert.ToDateTime(date);
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "pie";
            chart.title.text = "";
            chart.chart.height = 700;
            chart.chart.width = 700;
            chart.colors = new List<string>() { "#782f85", "#FB1926", "#FFE033", "#FF7A05", "#fe5722", "#ff9902", "#bb8800", "#3eb943", "#4cb050", "#cddc39", "#3f51b5", "#2095f", "#9c28b1", "#cc29ea" };
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Approved or Accredited" } });
            chart.xAxis.Add(new XAxis() { title = new Title() { text = "Failed" } });

            var logs = db.Log.Where(x => x.PageUrl != null && x.AppVersion!="Web").ToList();
            logs = logs.Where(x => x.ActionDateTime.Value.Date == changeIntoDate.Date).ToList();
            if (logs != null)
            {
                logs = logs.Where(x => x.UserId == UserId).ToList();
            }
            var homepage = logs.Where(x => x.PageUrl == "home page").Count();
            var checkout = logs.Where(x => x.PageUrl.Trim().ToLower() == "/checkout-process/checkout".Trim().ToLower()).Count();
            var productDetail = logs.Where(x => x.PageUrl.Contains("product-details")).Count();
            var mycart = logs.Where(x => x.PageUrl.Contains("mycart")).Count();
            var productCatelogue = logs.Where(x => x.PageUrl.Contains("productcatalogue")).Count();
            var wishlist = logs.Where(x => x.PageUrl.Contains("wishlist")).Count();
            var CompareProducts = logs.Where(x => x.PageUrl.Contains("CompareProducts")).Count();
            var header = logs.Where(x => x.PageUrl.Contains("header")).Count();
            var footer = logs.Where(x => x.PageUrl.Contains("footer")).Count();
            var profile = logs.Where(x => x.PageUrl.Contains("profile")).Count();
            var AllNotifications = logs.Where(x => x.PageUrl.Contains("AllNotifications")).Count();
            var confirmation = logs.Where(x => x.PageUrl.Contains("confirmation")).Count();
            chart.series.Add(new Series()
            {
                name = "Request,Response & Errors",
                data = new List<Data>()
                        {
                            new Data() { name = "home page", y = homepage },
                          new Data() { name = "ProductDetails", y = productDetail },
                          new Data() { name = "Checkout", y = checkout },
                          new Data() { name = "Mycart", y = mycart },
                          new Data() { name = "ProductCatelogue", y = productCatelogue },
                          new Data() { name = "Wishlist", y = wishlist },
                          new Data() { name = "CompareProducts", y = CompareProducts },
                          new Data() { name = "Header", y = header },
                          new Data() { name = "Footer", y = footer },
                          new Data() { name = "Profile", y = profile },
                          new Data() { name = "AllNotifications", y = AllNotifications },
                          new Data() { name = "confirmation", y = AllNotifications },
                           // new Data() { name = "On Hold", y = onHoldTransactions },
                            //new Data() { name = "Fraud", y = fraudTransactions }
                        },
                colorByPoint = true
            });

            return Ok(chart);
        }
        [HttpGet]
        [Route("getFilterAccordingRequest")]
        public IActionResult getFilterAccordingRequest(int req)
            {

            HighChartModel chart = new HighChartModel();
            chart.chart.type = "pie";
            chart.title.text = "";
            chart.chart.height = 600;
            chart.chart.width = 500;
            chart.colors = new List<string>() { "#77cc15", "#FB1926", "#FFE033", "#FF7A05", "#fe5722", "#ff9902", "#bb8800", "#3eb943", "#4cb050", "#cddc39", "#3f51b5", "#2095f", "#9c28b1", "#cc29ea" };
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Approved or Accredited" } });
            chart.xAxis.Add(new XAxis() { title = new Title() { text = "Failed" } });

            var logs = db.Log.Where(x => x.PageUrl != null).ToList();
            if (req == 3)
                logs = logs.Where(x => x.LogtypeId == 1).ToList();
            else if (req == 2)
                logs = logs.Where(x => x.LogtypeId == 2).ToList();
            else if (req == 1)
                logs = logs.Where(x => x.LogtypeId == 3).ToList();


            var homepage = logs.Where(x => x.PageUrl == "home page").Count();
            var checkout = logs.Where(x => x.PageUrl.Trim().ToLower() == "/checkout-process/checkout".Trim().ToLower()).Count();
            var productDetail = logs.Where(x => x.PageUrl.Contains("product-details")).Count();
            var mycart = logs.Where(x => x.PageUrl.Contains("mycart")).Count();
            var productCatelogue = logs.Where(x => x.PageUrl.Contains("productcatalogue")).Count();
            var wishlist = logs.Where(x => x.PageUrl.Contains("wishlist")).Count();
            var CompareProducts = logs.Where(x => x.PageUrl.Contains("CompareProducts")).Count();
            var header = logs.Where(x => x.PageUrl.Contains("header")).Count();
            var footer = logs.Where(x => x.PageUrl.Contains("footer")).Count();
            var profile = logs.Where(x => x.PageUrl.Contains("profile")).Count();
            var AllNotifications = logs.Where(x => x.PageUrl.Contains("AllNotifications")).Count();
            var confirmation = logs.Where(x => x.PageUrl.Contains("confirmation")).Count();
            if (req == 3)
            {
                chart.series.Add(new Series()
                {
                    name = "Request",
                    data = new List<Data>()
                        {
                            new Data() { name = "HomePage", y = homepage },
                          new Data() { name = "ProductDetails", y = productDetail },
                          new Data() { name = "Checkout", y = checkout },
                          new Data() { name = "Mycart", y = mycart },
                          new Data() { name = "ProductCatelogue", y = productCatelogue },
                          new Data() { name = "Wishlist", y = wishlist },
                          new Data() { name = "CompareProducts", y = CompareProducts },
                          new Data() { name = "Header", y = header },
                          new Data() { name = "Footer", y = footer },
                          new Data() { name = "Profile", y = profile },
                          new Data() { name = "AllNotifications", y = AllNotifications },
                          new Data() { name = "confirmation", y = AllNotifications },
                           // new Data() { name = "On Hold", y = onHoldTransactions },
                            //new Data() { name = "Fraud", y = fraudTransactions }
                        },
                    colorByPoint = true
                });
            }else if (req == 1)
            {
                chart.series.Add(new Series()
                {
                    name = "Response",
                    data = new List<Data>()
                        {
                            new Data() { name = "HomePage", y = homepage },
                          new Data() { name = "ProductDetails", y = productDetail },
                          new Data() { name = "Checkout", y = checkout },
                          new Data() { name = "Mycart", y = mycart },
                          new Data() { name = "ProductCatelogue", y = productCatelogue },
                          new Data() { name = "Wishlist", y = wishlist },
                          new Data() { name = "CompareProducts", y = CompareProducts },
                          new Data() { name = "Header", y = header },
                          new Data() { name = "Footer", y = footer },
                          new Data() { name = "Profile", y = profile },
                          new Data() { name = "AllNotifications", y = AllNotifications },
                          new Data() { name = "confirmation", y = AllNotifications },
                           // new Data() { name = "On Hold", y = onHoldTransactions },
                            //new Data() { name = "Fraud", y = fraudTransactions }
                        },
                    colorByPoint = true
                });

            }else
            {
                chart.series.Add(new Series()
                {
                    name = "Error",
                    data = new List<Data>()
                        {
                            new Data() { name = "HomePage", y = homepage },
                          new Data() { name = "ProductDetails", y = productDetail },
                          new Data() { name = "Checkout", y = checkout },
                          new Data() { name = "Mycart", y = mycart },
                          new Data() { name = "ProductCatelogue", y = productCatelogue },
                          new Data() { name = "Wishlist", y = wishlist },
                          new Data() { name = "CompareProducts", y = CompareProducts },
                          new Data() { name = "Header", y = header },
                          new Data() { name = "Footer", y = footer },
                          new Data() { name = "Profile", y = profile },
                          new Data() { name = "AllNotifications", y = AllNotifications },
                          new Data() { name = "confirmation", y = AllNotifications },
                           // new Data() { name = "On Hold", y = onHoldTransactions },
                            //new Data() { name = "Fraud", y = fraudTransactions }
                        },
                    colorByPoint = true
                });

            }

            return Ok(chart);
        }
        [HttpGet]
        [Route("getFilterAccordingRequestMobile")]
        public IActionResult getFilterAccordingRequestMObile(int req)
        {

            HighChartModel chart = new HighChartModel();
            chart.chart.type = "pie";
            chart.title.text = "";
            chart.chart.height = 600;
            chart.chart.width = 500;
            chart.colors = new List<string>() { "#77cc15", "#FB1926", "#FFE033", "#FF7A05", "#fe5722", "#ff9902", "#bb8800", "#3eb943", "#4cb050", "#cddc39", "#3f51b5", "#2095f", "#9c28b1", "#cc29ea" };
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Approved or Accredited" } });
            chart.xAxis.Add(new XAxis() { title = new Title() { text = "Failed" } });

            var logs = db.Log.Where(x => x.PageUrl != null && x.AppVersion!="Web").ToList();
            if (req == 3)
                logs = logs.Where(x => x.LogtypeId == 1).ToList();
            else if (req == 2)
                logs = logs.Where(x => x.LogtypeId == 2).ToList();
            else if (req == 1)
                logs = logs.Where(x => x.LogtypeId == 3).ToList();


            var homepage = logs.Where(x => x.PageUrl == "home page").Count();
            var checkout = logs.Where(x => x.PageUrl.Trim().ToLower() == "/checkout-process/checkout".Trim().ToLower()).Count();
            var productDetail = logs.Where(x => x.PageUrl.Contains("product-details")).Count();
            var mycart = logs.Where(x => x.PageUrl.Contains("mycart")).Count();
            var productCatelogue = logs.Where(x => x.PageUrl.Contains("productcatalogue")).Count();
            var wishlist = logs.Where(x => x.PageUrl.Contains("wishlist")).Count();
            var CompareProducts = logs.Where(x => x.PageUrl.Contains("CompareProducts")).Count();
            var header = logs.Where(x => x.PageUrl.Contains("header")).Count();
            var footer = logs.Where(x => x.PageUrl.Contains("footer")).Count();
            var profile = logs.Where(x => x.PageUrl.Contains("profile")).Count();
            var AllNotifications = logs.Where(x => x.PageUrl.Contains("AllNotifications")).Count();
            var confirmation = logs.Where(x => x.PageUrl.Contains("confirmation")).Count();
            if (req == 3)
            {
                chart.series.Add(new Series()
                {
                    name = "Request",
                    data = new List<Data>()
                        {
                            new Data() { name = "HomePage", y = homepage },
                          new Data() { name = "ProductDetails", y = productDetail },
                          new Data() { name = "Checkout", y = checkout },
                          new Data() { name = "Mycart", y = mycart },
                          new Data() { name = "ProductCatelogue", y = productCatelogue },
                          new Data() { name = "Wishlist", y = wishlist },
                          new Data() { name = "CompareProducts", y = CompareProducts },
                          new Data() { name = "Header", y = header },
                          new Data() { name = "Footer", y = footer },
                          new Data() { name = "Profile", y = profile },
                          new Data() { name = "AllNotifications", y = AllNotifications },
                          new Data() { name = "confirmation", y = AllNotifications },
                           // new Data() { name = "On Hold", y = onHoldTransactions },
                            //new Data() { name = "Fraud", y = fraudTransactions }
                        },
                    colorByPoint = true
                });
            }
            else if (req == 1)
            {
                chart.series.Add(new Series()
                {
                    name = "Response",
                    data = new List<Data>()
                        {
                            new Data() { name = "HomePage", y = homepage },
                          new Data() { name = "ProductDetails", y = productDetail },
                          new Data() { name = "Checkout", y = checkout },
                          new Data() { name = "Mycart", y = mycart },
                          new Data() { name = "ProductCatelogue", y = productCatelogue },
                          new Data() { name = "Wishlist", y = wishlist },
                          new Data() { name = "CompareProducts", y = CompareProducts },
                          new Data() { name = "Header", y = header },
                          new Data() { name = "Footer", y = footer },
                          new Data() { name = "Profile", y = profile },
                          new Data() { name = "AllNotifications", y = AllNotifications },
                          new Data() { name = "confirmation", y = AllNotifications },
                           // new Data() { name = "On Hold", y = onHoldTransactions },
                            //new Data() { name = "Fraud", y = fraudTransactions }
                        },
                    colorByPoint = true
                });

            }
            else
            {
                chart.series.Add(new Series()
                {
                    name = "Error",
                    data = new List<Data>()
                        {
                            new Data() { name = "HomePage", y = homepage },
                          new Data() { name = "ProductDetails", y = productDetail },
                          new Data() { name = "Checkout", y = checkout },
                          new Data() { name = "Mycart", y = mycart },
                          new Data() { name = "ProductCatelogue", y = productCatelogue },
                          new Data() { name = "Wishlist", y = wishlist },
                          new Data() { name = "CompareProducts", y = CompareProducts },
                          new Data() { name = "Header", y = header },
                          new Data() { name = "Footer", y = footer },
                          new Data() { name = "Profile", y = profile },
                          new Data() { name = "AllNotifications", y = AllNotifications },
                          new Data() { name = "confirmation", y = AllNotifications },
                           // new Data() { name = "On Hold", y = onHoldTransactions },
                            //new Data() { name = "Fraud", y = fraudTransactions }
                        },
                    colorByPoint = true
                });

            }

            return Ok(chart);
        }

        [HttpGet]
        [Route("getHomeActions")]
        public IActionResult getBestSellers(string PageName)
        {
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "column";
            chart.title.text = "";
            chart.chart.height = 600;
            chart.chart.width = 500;
            // chart.colors = new List<string>() { "#77cc15" };
            chart.colors = new List<string>() { "#77cc15", "#FB1926", "#FFE033", "#FF7A05", "#fe5722", "#ff9902", "#bb8800", "#3eb943", "#4cb050", "#cddc39", "#3f51b5", "#2095f", "#9c28b1", "#cc29ea" };

            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Request,Response and Errors" } });
            chart.xAxis.Add(new XAxis() { title = new Title() { text = "Home Page" } });
            var result = new List<Log>();
            result = db.Log.Where(x => x.PageUrl != null && x.Action!=null).ToList();
           if(PageName == "Checkout")
            {
                result = result.Where(x => x.PageUrl== "/checkout-process/checkout").ToList();

            }
            else if(PageName== "ProductDetails")
            {
                result = result.Where(x => x.PageUrl.Contains("product-details")).ToList();
            }
            else
            {
                result = result.Where(x => x.PageUrl.Contains(PageName)).ToList();
            }

            var actions = result.DistinctBy(x => x.Action).Select(x => x.Action).ToList();

            var tempList = new List<Data>();

            foreach (var item in actions)
            {
                var temp = new Data();
                temp.name = item;
                temp.y = Convert.ToDecimal(result.Where(x=>x.Action==item).Count());
                tempList.Add(temp);
            }

            chart.series.Add(new Series()
            {
                name = "Page logs",
                data = tempList
            });

            return Ok(chart);

        }
        [HttpGet]
        [Route("getHomeMobileActions")]
        public IActionResult getHomeMobileActions(string PageName)
        {
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "column";
            chart.title.text = "";
            chart.chart.height = 600;
            chart.chart.width = 500;
            // chart.colors = new List<string>() { "#77cc15" };
            chart.colors = new List<string>() { "#77cc15", "#FB1926", "#FFE033", "#FF7A05", "#fe5722", "#ff9902", "#bb8800", "#3eb943", "#4cb050", "#cddc39", "#3f51b5", "#2095f", "#9c28b1", "#cc29ea" };

            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Request,Response and Errors" } });
            chart.xAxis.Add(new XAxis() { title = new Title() { text = "Home Page" } });
            var result = new List<Log>();
            if (PageName == "Checkout")
            {
                result = db.Log.Where(x => x.PageUrl == "/checkout-process/checkout" && x.AppVersion!="Web").ToList();

            }
            else if (PageName == "ProductDetails")
            {
                result = db.Log.Where(x => x.PageUrl.Contains("product-details") && x.AppVersion != "Web").ToList();
            }
            else
            {
                result = db.Log.Where(x => x.PageUrl.Contains(PageName) && x.AppVersion != "Web").ToList();
            }

            var actions = result.DistinctBy(x => x.Action).Select(x => x.Action).ToList();

            var tempList = new List<Data>();

            foreach (var item in actions)
            {
                var temp = new Data();
                temp.name = item;
                temp.y = Convert.ToDecimal(result.Where(x => x.Action == item).Count());
                tempList.Add(temp);
            }

            chart.series.Add(new Series()
            {
                name = "Page logs",
                data = tempList
            });

            return Ok(chart);

        }
        [HttpGet]
        [Route("getFilterAccordingRequestandpage")]
        public IActionResult getFilterAccordingRequestandpage(int req, string PageName)
        {
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "column";
            chart.title.text = "";
            chart.chart.height = 600;
            chart.chart.width = 500;
            // chart.colors = new List<string>() { "#77cc15" };
            chart.colors = new List<string>() { "#77cc15", "#FB1926", "#FFE033", "#FF7A05", "#fe5722", "#ff9902", "#bb8800", "#3eb943", "#4cb050", "#cddc39", "#3f51b5", "#2095f", "#9c28b1", "#cc29ea" };

            chart.xAxis.Add(new XAxis() { title = new Title() { text = "Home Page" } });
            var result = new List<Log>();
            var logs = db.Log.Where(x => x.PageUrl != null).ToList();

            if (PageName == "Checkout")
            {
                result = logs.Where(x => x.PageUrl == "/checkout-process/checkout").ToList();

            }
            else if (PageName == "ProductDetails")
            {
                result = logs.Where(x => x.PageUrl.Contains("product-details")).ToList();
            }
            else
            {
                result = logs.Where(x => x.PageUrl.Contains(PageName)).ToList();
            }

            if (req == 3)
            {
                result = result.Where(x => x.LogtypeId == 1).ToList();
                chart.yAxis.Add(new YAxis() { title = new Title() { text = "Response" } });

            }
            else if (req == 2)
            {
                result = result.Where(x => x.LogtypeId == 2).ToList();
                chart.yAxis.Add(new YAxis() { title = new Title() { text = "Error" } });

            }
            else if (req == 1)
            {
                result = result.Where(x => x.LogtypeId == 3).ToList();
                chart.yAxis.Add(new YAxis() { title = new Title() { text = "Request" } });

            }


            var actions = result.DistinctBy(x => x.Action).Select(x => x.Action).ToList();

            var tempList = new List<Data>();

            foreach (var item in actions)
            {
                var temp = new Data();
                temp.name = item;
                temp.y = Convert.ToDecimal(result.Where(x => x.Action == item).Count());
                tempList.Add(temp);
            }

            chart.series.Add(new Series()
            {
                name = "Page logs",
                data = tempList
            });

            return Ok(chart);

        }
        [HttpGet]
        [Route("getFilterAccordingRequestandpageMobile")]
        public IActionResult getFilterAccordingRequestandpageMobile(int req, string PageName)
        {
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "column";
            chart.title.text = "";
            chart.chart.height = 600;
            chart.chart.width = 500;
            // chart.colors = new List<string>() { "#77cc15" };
            chart.colors = new List<string>() { "#77cc15", "#FB1926", "#FFE033", "#FF7A05", "#fe5722", "#ff9902", "#bb8800", "#3eb943", "#4cb050", "#cddc39", "#3f51b5", "#2095f", "#9c28b1", "#cc29ea" };

            chart.xAxis.Add(new XAxis() { title = new Title() { text = "Home Page" } });
            var result = new List<Log>();
         var   logs = db.Log.Where(x => x.PageUrl != null && x.AppVersion!="Web").ToList();

            if (PageName == "Checkout")
            {
                result = logs.Where(x => x.PageUrl == "/checkout-process/checkout").ToList();

            }
            else if (PageName == "ProductDetails")
            {
                result = logs.Where(x => x.PageUrl.Contains("product-details")).ToList();
            }
            else
            {
                result = logs.Where(x => x.PageUrl.Contains(PageName)).ToList();
            }

            if (req == 3)
            {
                result = result.Where(x => x.LogtypeId == 1).ToList();
                chart.yAxis.Add(new YAxis() { title = new Title() { text = "Response" } });

            }
            else if (req == 2)
            {
                result = result.Where(x => x.LogtypeId == 2).ToList();
                chart.yAxis.Add(new YAxis() { title = new Title() { text = "Error" } });

            }
            else if (req == 1)
            {
                result = result.Where(x => x.LogtypeId == 3).ToList();
                chart.yAxis.Add(new YAxis() { title = new Title() { text = "Request" } });

            }


            var actions = result.DistinctBy(x => x.Action).Select(x => x.Action).ToList();

            var tempList = new List<Data>();

            foreach (var item in actions)
            {
                var temp = new Data();
                temp.name = item;
                temp.y = Convert.ToDecimal(result.Where(x => x.Action == item).Count());
                tempList.Add(temp);
            }

            chart.series.Add(new Series()
            {
                name = "Page logs",
                data = tempList
            });

            return Ok(chart);

        }
        [HttpGet]
        [Route("getPageDateFilter")]
        public IActionResult getPageDateFilter(string date, int UserId, string PageName)
        {
            var changeIntoDate = Convert.ToDateTime(date);
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "column";
            chart.title.text = "";
            chart.chart.height = 600;
            chart.chart.width = 500;
            // chart.colors = new List<string>() { "#77cc15" };
            chart.colors = new List<string>() { "#77cc15", "#FB1926", "#FFE033", "#FF7A05", "#fe5722", "#ff9902", "#bb8800", "#3eb943", "#4cb050", "#cddc39", "#3f51b5", "#2095f", "#9c28b1", "#cc29ea" };
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Request,Response,Error" } });

            chart.xAxis.Add(new XAxis() { title = new Title() { text = "Home Page" } });
            var result = new List<Log>();
            var logs = db.Log.Where(x => x.PageUrl != null && x.ActionDateTime.Value.Date == changeIntoDate.Date && x.UserId == UserId).ToList();

            if (PageName == "Checkout")
            {
                result = logs.Where(x => x.PageUrl == "/checkout-process/checkout").ToList();

            }
            else if (PageName == "ProductDetails")
            {
                result = logs.Where(x => x.PageUrl.Contains("product-details")).ToList();
            }
            else
            {
                result = logs.Where(x => x.PageUrl.Contains(PageName)).ToList();
            }




            var actions = result.DistinctBy(x => x.Action).Select(x => x.Action).ToList();

            var tempList = new List<Data>();

            foreach (var item in actions)
            {
                var temp = new Data();
                temp.name = item;
                temp.y = Convert.ToDecimal(result.Where(x => x.Action == item).Count());
                tempList.Add(temp);
            }

            chart.series.Add(new Series()
            {
                name = "Page logs",
                data = tempList
            });

            return Ok(chart);
        }
        [HttpGet]
        [Route("getPageDateMobileFilter")]
        public IActionResult getMobilePageDateFilter(string date, int UserId,string PageName)
        {
            var changeIntoDate = Convert.ToDateTime(date);
            HighChartModel chart = new HighChartModel();
            chart.chart.type = "column";
            chart.title.text = "";
            chart.chart.height = 600;
            chart.chart.width = 500;
            // chart.colors = new List<string>() { "#77cc15" };
            chart.colors = new List<string>() { "#77cc15", "#FB1926", "#FFE033", "#FF7A05", "#fe5722", "#ff9902", "#bb8800", "#3eb943", "#4cb050", "#cddc39", "#3f51b5", "#2095f", "#9c28b1", "#cc29ea" };
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Request,Response,Error" } });

            chart.xAxis.Add(new XAxis() { title = new Title() { text = "Home Page" } });
            var result = new List<Log>();
            var logs = db.Log.Where(x => x.PageUrl != null && x.ActionDateTime.Value.Date == changeIntoDate.Date && x.UserId==UserId && x.AppVersion!="Web").ToList();

            if (PageName == "Checkout")
            {
                result = logs.Where(x => x.PageUrl == "/checkout-process/checkout").ToList();

            }
            else if (PageName == "ProductDetails")
            {
                result = logs.Where(x => x.PageUrl.Contains("product-details")).ToList();
            }
            else
            {
                result = logs.Where(x => x.PageUrl.Contains(PageName)).ToList();
            }

          


            var actions = result.DistinctBy(x => x.Action).Select(x => x.Action).ToList();

            var tempList = new List<Data>();

            foreach (var item in actions)
            {
                var temp = new Data();
                temp.name = item;
                temp.y = Convert.ToDecimal(result.Where(x => x.Action == item).Count());
                tempList.Add(temp);
            }

            chart.series.Add(new Series()
            {
                name = "Page logs",
                data = tempList
            });

            return Ok(chart);
        }
        [HttpGet]
        [Route("getActionData")]
        public IActionResult getActionData(string Pagename, string ActionName)
        {
            try
            {
                var contains = Pagename;
                var list = new List<Log>();
                var data = new List<Log>();
                //   var data = db.Log.Where(x => x.PageUrl.Contains("mycart")).Include(x => x.User).GroupBy(x => x.IpAddress != null);
                if (contains == "profile")
                {
                    data = db.Log.Where(x => x.PageUrl == "profile").Include(x => x.User).ToList();
                }
                else
                {
                    data = db.Log.Where(x => x.PageUrl.Contains(contains)).Include(x => x.User).ToList();
                }
                data = data.Where(x => x.Action == ActionName).ToList();
                //  data = data.GroupBy(x => x.IpAddress );
                var groupedData = data.GroupBy(x => x.IpAddress != null);
                foreach (var items in groupedData)
                {
                    foreach (var item in items)
                    {
                        list.Add(item);
                    }
                }

                var filterList = list.Where(x => x.Guid != null).ToList();
                var action = from c in filterList
                             group c by new
                             {
                                 c.Action,
                                 c.Guid
                             } into gcs
                             select gcs;
                list = new List<Log>();
                foreach (var items in action)
                {

                    foreach (var item in items)
                    {
                        list.Add(item);
                    }
                }
                foreach (var item in list)
                {
                    if (item.Result.Contains("Request"))
                        item.Result = "1";
                    if (item.Result.Contains("Error"))
                        item.Result = "Error";

                }

                return Ok(list);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("getMobileActionData")]
        public IActionResult getMobileActionData(string Pagename, string ActionName)
        {
            try
            {
                var contains = Pagename;
                var list = new List<Log>();
                var data = new List<Log>();
                //   var data = db.Log.Where(x => x.PageUrl.Contains("mycart")).Include(x => x.User).GroupBy(x => x.IpAddress != null);
                if (contains == "profile")
                {
                    data = db.Log.Where(x => x.PageUrl == "profile" && x.AppVersion!="Web").Include(x => x.User).ToList();
                }
                else
                {
                    data = db.Log.Where(x => x.PageUrl.Contains(contains) && x.AppVersion != "Web").Include(x => x.User).ToList();
                }
                data = data.Where(x => x.Action == ActionName).ToList();
                //  data = data.GroupBy(x => x.IpAddress );
                var groupedData = data.GroupBy(x => x.IpAddress != null);
                foreach (var items in groupedData)
                {
                    foreach (var item in items)
                    {
                        list.Add(item);
                    }
                }

                var filterList = list.Where(x => x.Guid != null).ToList();
                var action = from c in filterList
                             group c by new
                             {
                                 c.Action,
                                 c.Guid
                             } into gcs
                             select gcs;
                list = new List<Log>();
                foreach (var items in action)
                {

                    foreach (var item in items)
                    {
                        list.Add(item);
                    }
                }
                foreach (var item in list)
                {
                    if (item.Result.Contains("Request"))
                        item.Result = "1";
                    if (item.Result.Contains("Error"))
                        item.Result = "Error";

                }

                return Ok(list);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("filterUserLogs")]
        public List<Log> tracking(string val,string actionName, string date, int UserId)
        {
            try
            {
                var changeIntoDate = Convert.ToDateTime(date);
                var contains = val;
                var list = new List<Log>();
                var data = new List<Log>();
                //   var data = db.Log.Where(x => x.PageUrl.Contains("mycart")).Include(x => x.User).GroupBy(x => x.IpAddress != null);
                if (contains == "profile")
                {
                    data = db.Log.Where(x => x.PageUrl == "profile" && x.ActionDateTime.Value.Date == changeIntoDate.Date && x.UserId == UserId).Include(x => x.User).ToList();
                }
                else
                {
                    data = db.Log.Where(x => x.PageUrl.Contains(contains) && x.ActionDateTime.Value.Date == changeIntoDate.Date && x.UserId == UserId).Include(x => x.User).ToList();
                }
                data = data.Where(x => x.Action == actionName).ToList();
                //  data = data.GroupBy(x => x.IpAddress );
                var groupedData = data.GroupBy(x => x.IpAddress != null);
                foreach (var items in groupedData)
                {
                    foreach (var item in items)
                    {
                        list.Add(item);
                    }
                }

                var filterList = list.Where(x => x.Guid != null).ToList();
                var action = from c in filterList
                             group c by new
                             {
                                 c.Action,
                                 c.Guid
                             } into gcs
                             select gcs;
                list = new List<Log>();
                foreach (var items in action)
                {

                    foreach (var item in items)
                    {
                        list.Add(item);
                    }
                }
                foreach (var item in list)
                {
                    if (item.Result.Contains("Request"))
                        item.Result = "1";
                    if (item.Result.Contains("Error"))
                        item.Result = "Error";

                }

                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("filterMObileUserLogs")]
        public List<Log> tracking1(string val, string actionName, string date, int UserId)
        {
            try
            {
                var changeIntoDate = Convert.ToDateTime(date);
                var contains = val;
                var list = new List<Log>();
                var data = new List<Log>();
                //   var data = db.Log.Where(x => x.PageUrl.Contains("mycart")).Include(x => x.User).GroupBy(x => x.IpAddress != null);
                if (contains == "profile")
                {
                    data = db.Log.Where(x => x.PageUrl == "profile" && x.ActionDateTime.Value.Date == changeIntoDate.Date && x.UserId == UserId).Include(x => x.User).ToList();
                }
                else
                {
                    data = db.Log.Where(x => x.PageUrl.Contains(contains) && x.ActionDateTime.Value.Date == changeIntoDate.Date && x.UserId == UserId).Include(x => x.User).ToList();
                }
                data = data.Where(x => x.Action == actionName &&  x.AppVersion!="Web").ToList();
                //  data = data.GroupBy(x => x.IpAddress );
                var groupedData = data.GroupBy(x => x.IpAddress != null);
                foreach (var items in groupedData)
                {
                    foreach (var item in items)
                    {
                        list.Add(item);
                    }
                }

                var filterList = list.Where(x => x.Guid != null).ToList();
                var action = from c in filterList
                             group c by new
                             {
                                 c.Action,
                                 c.Guid
                             } into gcs
                             select gcs;
                list = new List<Log>();
                foreach (var items in action)
                {

                    foreach (var item in items)
                    {
                        list.Add(item);
                    }
                }
                foreach (var item in list)
                {
                    if (item.Result.Contains("Request"))
                        item.Result = "1";
                    if (item.Result.Contains("Error"))
                        item.Result = "Error";

                }

                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}