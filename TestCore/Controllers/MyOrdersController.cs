using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Localdb;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Models;
using Microsoft.EntityFrameworkCore;
using TestCore.Extension_Method;
using TestCore.Helper;
using Microsoft.AspNetCore.Authorization;

namespace TestCore.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [EnableCors("EnableCORS")]
    public class MyOrdersController : ControllerBase
    {
        private readonly PistisContext db;
        public MyOrdersController(PistisContext pistis)
        {
            db = pistis;
        }
        [HttpGet]
        [Route("userOrders")]
      // [Authorize(Roles = "Admin,Customer")]
        public IActionResult CheckOutByCustomer(int CustomerId, string IpAddress, int? orderId = 0)
        {
            var checkouts = new List<Checkout>();
            try
            {
                if (CustomerId > 0)
                    checkouts = db.Checkouts.Where(x => x.UserId == CustomerId && x.IsActive == true)
                        .Include(x => x.CheckoutItems)
                        .Include(x => x.User).ToList();
                //else
                //{
                //    checkouts = db.Checkouts.Where(x => x.IpAddress == IpAddress && x.IsActive == true).Include(x => x.CheckoutItems).OrderByDescending(x=>x.Id).ToList();
                //}
                var productvarientdetails= db.ProductVariantDetails
                                .Where(x => x.IsActive == true)
                                .Include(x => x.Product)
                                .Include(x => x.ProductVariantOptions)
                                .Include(x => x.ProductImages).ToList();
                if (orderId > 0)
                    checkouts = checkouts.Where(b => b.Id == orderId).ToList();
                foreach (var checkout in checkouts)
                {

                    foreach (var c in checkout?.CheckoutItems.Where(m => m.IsActive == true))
                    {
                        c.ProductVariantDetail = new ProductVariantDetail();
                        if (c.ProductVariantDetailId > 0)
                        {
                            c.ProductVariantDetail = productvarientdetails
                                .Where(x=> x.Id == c.ProductVariantDetailId).
                                FirstOrDefault();
                        }
                    }
                }
                var checkoutitems = new List<CheckOut>();
                var vendors = db.Users.Where(b => b.IsActive == true).ToList();
                var trackorders = db.TrackOrders.ToList();
                var products = db.Products.Where(x => x.IsActive == true).ToList();
                var varientoptions = db.VariantOptions.Where(x => x.IsActive == true).Include(x => x.Variant).ToList();
                var return1= db.Return.Where(x => x.IsActive == true).ToList();
                foreach (var checkout in checkouts)
                {
                    foreach (var c in checkout?.CheckoutItems.Where(x => x.IsActive == true))
                    {

                        var model = new CheckOut();
                        if (model.SellerName == null && c?.ProductVariantDetail?.Product?.VendorId > 0)
                        {
                            var vendor = vendors.Where(v => v.Id == c.ProductVariantDetail.Product.VendorId && v.RoleId == (int)RoleType.Vendor).FirstOrDefault();
                            if (vendor != null)
                                model.SellerName = vendor.FirstName + " " + vendor.LastName;
                        }
                        model.AdditionalCost = checkout.AdditionalCost;
                        model.Id = checkout.Id;
                        model.OrderStatus = trackorders.Where(x => x.OrderId == checkout.Id).FirstOrDefault()?.Status;
                        if (c.ProductVariantDetail?.ProductImages.Count>0 || c.ProductVariantDetail?.ProductImages!=null)
                        {
                            model.Image = c.ProductVariantDetail?.ProductImages
                            .Where(x => x.IsDefault == true && x.IsActive == true)
                            .FirstOrDefault() == null ? c.ProductVariantDetail?.ProductImages
                            .Where(x => x.IsActive == true).FirstOrDefault()
                            .ImagePath : c.ProductVariantDetail?.ProductImages
                            .Where(x => x.IsDefault == true && x.IsActive == true)
                            .FirstOrDefault().ImagePath;
                        }
                        model.IpAddress = checkout.IpAddress;
                        if (c.ProductVariantDetail != null)
                        {
                            model.ProductId = products.Where(x => x.Id == c.ProductVariantDetail.ProductId).Select(x => x.Id).FirstOrDefault();
                            //     model.IsConvertToCheckout = checkout.IsConvertToCheckout;
                            if (c.ProductVariantDetail.Product?.Name != null)
                                model.Name = c.ProductVariantDetail.Product.Name;

                        }
                        model.OrderDate = checkout.CheckoutDate;
                        model.OrderNumber = checkout.InvoiceNumber;
                        model.TotalAmount = checkout.TotalAmount;
                        model.UserId = checkout.UserId;
                        model.ProductVariantDetailId = c.ProductVariantDetailId;
                        if (c.ProductVariantDetail != null)
                            foreach (var v in c.ProductVariantDetail?.ProductVariantOptions.Where(x => x.IsActive == true))
                            {
                                var variantid = v.VariantOptionId;
                                var variantop = new VariantOption();
                                variantop = varientoptions.Where(x => x.IsActive == true && x.Id == variantid).FirstOrDefault().RemoveReferences();
                                var variantmodel = new VariantOptionModel();
                                variantmodel.Id = variantop.Id;
                                variantmodel.Name = variantop.Name;
                                variantmodel.VariantId = variantop.VariantId;
                                if (variantop.Variant?.Name != null)
                                    variantmodel.varientName = variantop.Variant.Name;
                                model.VariantOptions.Add(variantmodel);
                            }
                        model.SellingPrice = c.UnitPrice * c.Quantity;
                        model.Discount = Convert.ToInt32(c.Discount + c.DealDiscount);
                        // var PriceAfterDiscount = c.UnitPrice - (c.UnitPrice * model.Discount / 100);
                        // model.PriceAfterDiscount = PriceAfterDiscount * c.Quantity;
                        model.PriceAfterDiscount = c.Amount/c.Quantity;
                        model.Quantity = c.Quantity;
                        model.Amount = model.PriceAfterDiscount ;
                        model.TotalAmount = checkout.TotalAmount + checkout.ShippingPrice;
                        model.checkoutItemId = c.Id;
                        var data = return1.Where(x => x.CheckoutItemId == c.Id ).FirstOrDefault();
                        if (data != null)
                            model.IsReturned = true;
                        else
                            model.IsReturned = false;
                        checkoutitems.Add(model);
                    }
                }
                var items = checkoutitems.OrderByDescending(x => x.Id).GroupBy(x => x.OrderNumber).ToList();
                
                    return Ok(items);

            }
            catch (Exception ex)
            {

                throw ex;
            }

            //  checkoutitems;






        }
        [HttpGet]
        [Route("getOrderDetails")]
        public IActionResult actionResult(string First, string scnd, int page, int pageSize)
        {
            var orderList = new List<OrderDeatils>();
            var skipData = pageSize * (page - 1);
            try
            {
                var data = new List<PaymentTransaction>();
                if (First != null && scnd != null)
                {
                    var first = Convert.ToDateTime(First);
                    var Seconed = Convert.ToDateTime(scnd);
                    data = db.PaymentTransaction.Where(x => x.Date.Value.Year >= first.Year&&x.Date.Value.Month>=first.Month && x.Date.Value.Day>=first.Day && x.Date.Value.Year <= Seconed.Year && x.Date.Value.Month<=Seconed.Month && x.Date.Value.Day<=Seconed.Day)
                    .Include(x => x.Checkout.User)
                         .Include(x => x.Checkout.CheckoutItems)

                    .OrderByDescending(x => x.Id)
                    .ToList();
                }
                else
                {
                    data = db.PaymentTransaction
                         .Include(x => x.Checkout)
                         .Include(x=>x.Checkout.User)
                         .Include(x => x.Checkout.CheckoutItems)
                   .OrderByDescending(x => x.Id)
                   .ToList();
                }

                var billingaddress = db.BillingAddress.Where(x => x.IsActive == true).ToList();
                var shippingaddress = db.Shipping.Where(x => x.IsActive == true).ToList();
                var users = db.Users.Where(x => x.IsActive == true && x.RoleId == 2).ToList();
                var trackorders = db.TrackOrders.Where(x => x.IsActive == true).ToList();
               // var paymentMode = db.PaymentModes.Where(x => x.IsActive == true).ToList();
                var varinatDetails = db.ProductVariantDetails.Where(v => v.IsActive == true).Include(v => v.Product).ToList();
                foreach (var item in data)
                {
                    if (item.Checkout != null)
                    {
                        var order = new OrderDeatils();
                        order.PaymentTransactionId = item.Id;
                        order.OrederNo = item.Checkout.InvoiceNumber;
                        order.PurchasedOn = item.Date?.ToShortDateString();
                        if (item.Checkout.BillingAddressId > 0)
                            order.billtoName = billingaddress.Where(x => x.Id == item.Checkout?.BillingAddressId).FirstOrDefault()?.Name;
                        if (order.billtoName == null)
                            order.billtoName = "";
                        if (item.Checkout.ShippingId > 0)
                            order.ShiptoName = shippingaddress.Where(x => x.Id == item.Checkout?.ShippingId).FirstOrDefault()?.Name;
                        if (order.ShiptoName == null)
                            order.ShiptoName = "";
                        order.PurchasePrice = item.Checkout?.TotalAmount + item.Checkout?.ShippingPrice;
                        order.BasePrice = item.Checkout?.TotalAmount ;
                        order.Quantity = item.Checkout?.CheckoutItems.Count();
                        var variant = varinatDetails.Where(v => v.Id == item.Checkout?.CheckoutItems.Select(x => x.ProductVariantDetailId).FirstOrDefault()).FirstOrDefault();
                        var venderId = variant?.Product?.VendorId;
                        if (venderId > 0)
                        {
                            order.VenderName = users.Where(x => x.Id == variant?.Product?.VendorId).FirstOrDefault()?.FirstName;
                            if (order.VenderName == null)
                                order.VenderName = "";
                            if (order.VenderName != null)
                                order.VenderId = venderId;

                        }
                        order.TrackingNumber = trackorders.Where(x => x.OrderId == item.Id).FirstOrDefault()?.TrackId;
                        if (order.TrackingNumber == null)
                            order.TrackingNumber = "";

                        order.OrderStatus = trackorders.Where(x => x.OrderId == item.CheckoutId).FirstOrDefault()?.Status;
                        if (order.OrderStatus == null)
                            order.OrderStatus = "";
                        order.ShippedBy = item.Checkout.ShippingType;
                        if (order.ShippedBy == null)
                            order.ShippedBy = "";
                        order.PaymentMethod = item.PaymentMethod;
                        order.UserEmailId = item.Checkout?.User?.FirstName;
                        orderList.Add(order);
                    }
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }

                var response = new
            {
                data = orderList.OrderByDescending(x=>x.OrederNo).Skip(skipData).Take(pageSize).ToList(),
                count = orderList.Count
            };

            return Ok(response);
        }

        //[HttpGet]
        //[Route("getOrderDetails")]
        //public IActionResult actionResult(string First, string scnd, int page, int pageSize)
        //{
        //    var orderList = new List<OrderDeatils>();
        //    var skipData = pageSize * (page - 1);
        //    try
        //    {
        //        var data = new List<Models.Checkout>();
        //        if (First != null && scnd != null)
        //        {
        //            var first = Convert.ToDateTime(First);
        //            var Seconed = Convert.ToDateTime(scnd);
        //            //data = db.PaymentTransaction.Where(x => x.Date.Value.Date >= first.Date && x.Date.Value.Date <= Seconed.Date)
        //            //.Include(x => x.Checkout.User)
        //            //     .Include(x => x.Checkout.CheckoutItems)

        //            //.OrderByDescending(x => x.Id)
        //            //.ToList();
        //            data = db.Checkouts.Where(x => x.IsActive == true && x.CheckoutDate.Date >= first.Date && x.CheckoutDate.Date <= Seconed.Date)
        //            .Include(x => x.CheckoutItems)
        //            .Include(x => x.User)
        //            .OrderByDescending(x => x.Id)
        //            .ToList();
        //        }
        //        else
        //        {
        //            // data = db.PaymentTransaction
        //            //      .Include(x => x.Checkout.User)
        //            //      .Include(x => x.Checkout.CheckoutItems)
        //            //.OrderByDescending(x => x.Id)
        //            //.ToList();
        //            data = db.Checkouts.Where(x => x.IsActive == true)
        //            .Include(x => x.CheckoutItems)
        //            .Include(x => x.User)
        //            .Include(x => x.CheckoutItems)
        //            .ToList();
        //        }

        //        var billingaddress = db.BillingAddress.Where(x => x.IsActive == true).ToList();
        //        var shippingaddress = db.Shipping.Where(x => x.IsActive == true).ToList();
        //        var users = db.Users.Where(x => x.IsActive == true && x.RoleId == 2).ToList();
        //        var trackorders = db.TrackOrders.Where(x => x.IsActive == true).ToList();
        //        var paymentMode = db.PaymentModes.Where(x => x.IsActive == true).ToList();
        //        var varinatDetails = db.ProductVariantDetails.Where(v => v.IsActive == true).Include(v => v.Product).ToList();
        //        foreach (var item in data)
        //        {
        //            var order = new OrderDeatils();
        //            order.OrederNo = item.InvoiceNumber;
        //            order.PurchasedOn = item.CheckoutDate.ToShortDateString();
        //            if (item.BillingAddressId > 0)
        //                order.billtoName = billingaddress.Where(x => x.Id == item.BillingAddressId).FirstOrDefault()?.Name;
        //            if (order.billtoName == null)
        //                order.billtoName = "";
        //            if (item.ShippingId > 0)
        //                order.ShiptoName = shippingaddress.Where(x => x.Id == item.ShippingId).FirstOrDefault()?.Name;
        //            if (order.ShiptoName == null)
        //                order.ShiptoName = "";
        //            order.PurchasePrice = item.TotalAmount;
        //            order.BasePrice = item.TotalAmount - item.ShippingPrice;
        //            order.Quantity = item.CheckoutItems.Count();
        //            var variant = varinatDetails.Where(v => v.Id == item.CheckoutItems.Select(x => x.ProductVariantDetailId).FirstOrDefault()).FirstOrDefault();
        //            var venderId = variant?.Product?.VendorId;
        //            if (venderId > 0)
        //            {
        //                order.VenderName = users.Where(x => x.Id == variant?.Product?.VendorId).FirstOrDefault()?.FirstName;
        //                if (order.VenderName == null)
        //                    order.VenderName = "";
        //                if (order.VenderName != null)
        //                    order.VenderId = venderId;

        //            }
        //            order.TrackingNumber = trackorders.Where(x => x.OrderId == item.Id).FirstOrDefault()?.TrackId;
        //            if (order.TrackingNumber == null)
        //                order.TrackingNumber = "";
        //            //if (TrackId !=null)
        //            // order.TrackingNumber = TrackId;
        //            order.OrderStatus = trackorders.Where(x => x.OrderId == item.Id).FirstOrDefault()?.Status;
        //            if (order.OrderStatus == null)
        //                order.OrderStatus = "";
        //            order.ShippedBy = item.ShippingType;
        //            if (order.ShippedBy == null)
        //                order.ShippedBy = "";
        //            order.PaymentMethod = paymentMode.Where(x => x.Id == item.PaymentModeId).FirstOrDefault()?.Name;
        //            if (order.PaymentMethod == null)
        //                order.PaymentMethod = "";
        //            order.UserEmailId = item.User?.FirstName;
        //            orderList.Add(order);

        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    //orderList = orderList.Skip(skipData).Take(pageSize).ToList();
        //    //foreach (var d in orderList)
        //    //{
        //    //    d.PaymentMethod=
        //    //}
        //    var response = new
        //    {
        //        data = orderList.Skip(skipData).Take(pageSize).ToList(),
        //        count = orderList.Count
        //    };

        //    return Ok(response);
        //}
        [HttpGet]
        [Route("filtergetOrderDetails")]
        public IActionResult actionResult1(string First, string scnd, int page, int pageSize)
        {
            var orderList = new List<OrderDeatils>();
            var skipData = pageSize * (page - 1);
            try
            {
                if (First != null && scnd != null)
                {
                    var first = Convert.ToDateTime(First);
                    var Seconed = Convert.ToDateTime(scnd);
                    var data = db.Checkouts.Where(x => x.IsActive == true && x.CheckoutDate.Date >= first.Date && x.CheckoutDate.Date <= Seconed.Date)
                    .Include(x => x.CheckoutItems)
                    .Include(x => x.User)
                    .OrderByDescending(x => x.Id)
                    .ToList();
                    var billingaddress = db.BillingAddress.Where(x => x.IsActive == true).ToList();
                    var shippingaddress = db.Shipping.Where(x => x.IsActive == true).ToList();
                    var users = db.Users.Where(x => x.IsActive == true && x.RoleId == 2).ToList();
                    var trackorders = db.TrackOrders.Where(x => x.IsActive == true).ToList();
                    var paymentMode = db.PaymentModes.Where(x => x.IsActive == true).ToList();
                    foreach (var item in data)
                    {
                        var order = new OrderDeatils();
                        order.OrederNo = item.InvoiceNumber;
                        order.PurchasedOn = item.CheckoutDate.ToShortDateString();
                        if (item.BillingAddressId > 0)
                            order.billtoName = billingaddress.Where(x => x.Id == item.BillingAddressId).FirstOrDefault()?.Name;
                        if (order.billtoName == null)
                            order.billtoName = "";
                        if (item.ShippingId > 0)
                            order.ShiptoName = shippingaddress.Where(x => x.Id == item.ShippingId).FirstOrDefault()?.Name;
                        if (order.ShiptoName == null)
                            order.ShiptoName = "";
                        order.PurchasePrice = item.TotalAmount;
                        order.BasePrice = item.TotalAmount - item.ShippingPrice;
                        order.Quantity = item.CheckoutItems.Count();
                        var venderId = item.CheckoutItems.Select(x => x.VendorId).FirstOrDefault();
                        if (venderId > 0)
                        {
                            order.VenderName = users.Where(x => x.Id == venderId).FirstOrDefault()?.FirstName;
                            if (order.VenderName == null)
                                order.VenderName = "";
                            if (order.VenderName != null)
                                order.VenderId = venderId;

                        }
                        order.TrackingNumber = trackorders.Where(x => x.OrderId == item.Id).FirstOrDefault()?.TrackId;
                        if (order.TrackingNumber == null)
                            order.TrackingNumber = "";
                        //if (TrackId !=null)
                        // order.TrackingNumber = TrackId;
                        order.OrderStatus = trackorders.Where(x => x.OrderId == item.Id).FirstOrDefault()?.Status;
                        if (order.OrderStatus == null)
                            order.OrderStatus = "";
                        order.ShippedBy = item.ShippingType;
                        if (order.ShippedBy == null)
                            order.ShippedBy = "";
                        order.PaymentMethod = paymentMode.Where(x => x.Id == item.PaymentModeId).FirstOrDefault()?.Name;
                        if (order.PaymentMethod == null)
                            order.PaymentMethod = "";
                        order.UserEmailId = item.User?.FirstName;
                        orderList.Add(order);

                    }
                    //var data1 = from orders in db.Checkouts
                    // join items in db.CheckoutItems on orders.Id equals items.CheckoutId
                    // join Users in db.Users on orders.UserId equals Users.Id
                    // join track in db.TrackOrders on orders.Id equals track.OrderId
                    // select orders;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            var response = new
            {
                data = orderList.Skip(page).Take(pageSize).ToList(),
                count = orderList.Count
            };
            return Ok(response);
        }
        [HttpGet]
        [Route("filtergetOrderDetails2")]
        public IActionResult actionResult1(int page, int pageSize, string DOY)
        {
            var skipData = pageSize * (page - 1);
            var orderList = new List<OrderDeatils>();
            var data = new List<Models.Checkout>();
            try
            {
                if (DOY != null)
                {
                    var first = DateTime.Now;
                    if (DOY == "day")
                    {
                        data = db.Checkouts.Where(x => x.IsActive == true && x.CheckoutDate.Date.Day == first.Day)
                        .Include(x => x.CheckoutItems)
                        .Include(x => x.User)
                        .ToList();
                    }
                    else if (DOY == "year")
                    {
                        data = db.Checkouts.Where(x => x.IsActive == true && x.CheckoutDate.Date.Year == first.Year)
                        .Include(x => x.CheckoutItems)
                        .Include(x => x.User)
                        .ToList();
                    }
                    else if (DOY == "month")
                    {
                        data = db.Checkouts.Where(x => x.IsActive == true && x.CheckoutDate.Date.Month == first.Month)
                        .Include(x => x.CheckoutItems)
                        .Include(x => x.User)
                        .OrderByDescending(x => x.Id)
                        .ToList();
                    }

                    var billingaddress = db.BillingAddress.Where(x => x.IsActive == true).ToList();
                    var shippingaddress = db.Shipping.Where(x => x.IsActive == true).ToList();
                    var users = db.Users.Where(x => x.IsActive == true && x.RoleId == 2).ToList();
                    var trackorders = db.TrackOrders.Where(x => x.IsActive == true).ToList();
                    var paymentMode = db.PaymentModes.Where(x => x.IsActive == true).ToList();
                    foreach (var item in data)
                    {
                        var order = new OrderDeatils();
                        order.OrederNo = item.InvoiceNumber;
                        order.PurchasedOn = item.CheckoutDate.ToShortDateString();
                        if (item.BillingAddressId > 0)
                            order.billtoName = billingaddress.Where(x => x.Id == item.BillingAddressId).FirstOrDefault()?.Name;
                        if (order.billtoName == null)
                            order.billtoName = "";
                        if (item.ShippingId > 0)
                            order.ShiptoName = shippingaddress.Where(x => x.Id == item.ShippingId).FirstOrDefault()?.Name;
                        if (order.ShiptoName == null)
                            order.ShiptoName = "";
                        order.PurchasePrice = item.TotalAmount;
                        order.BasePrice = item.TotalAmount - item.ShippingPrice;
                        order.Quantity = item.CheckoutItems.Count();
                        var venderId = item.CheckoutItems.Select(x => x.VendorId).FirstOrDefault();
                        if (venderId > 0)
                        {
                            order.VenderName = users.Where(x => x.Id == venderId).FirstOrDefault()?.FirstName;
                            if (order.VenderName == null)
                                order.VenderName = "";
                            if (order.VenderName != null)
                                order.VenderId = venderId;

                        }
                        order.TrackingNumber = trackorders.Where(x => x.OrderId == item.Id).FirstOrDefault()?.TrackId;
                        if (order.TrackingNumber == null)
                            order.TrackingNumber = "";
                        //if (TrackId !=null)
                        // order.TrackingNumber = TrackId;
                        order.OrderStatus = trackorders.Where(x => x.OrderId == item.Id).FirstOrDefault()?.Status;
                        if (order.OrderStatus == null)
                            order.OrderStatus = "";
                        order.ShippedBy = item.ShippingType;
                        if (order.ShippedBy == null)
                            order.ShippedBy = "";
                        order.PaymentMethod = paymentMode.Where(x => x.Id == item.PaymentModeId).FirstOrDefault()?.Name;
                        if (order.PaymentMethod == null)
                            order.PaymentMethod = "";
                        order.UserEmailId = item.User?.FirstName;
                        orderList.Add(order);

                    }
                    //var data1 = from orders in db.Checkouts
                    // join items in db.CheckoutItems on orders.Id equals items.CheckoutId
                    // join Users in db.Users on orders.UserId equals Users.Id
                    // join track in db.TrackOrders on orders.Id equals track.OrderId
                    // select orders;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            var response = new
            {
                data = orderList.Skip(page).Take(pageSize).ToList(),
                cout = orderList.Count
            };

            return Ok(response);
        }



        [HttpGet]
        [Route("getOrderById")]
        public IActionResult GetOrderById(int orderno)
        {
            //var orderList = new List<OrderDeatils>();
            var order = new InvoiceDetails();

            try
            {
                var item = db.PaymentTransaction.Where(x => x.Id == orderno)
                         .Include(x => x.Checkout)
                         .Include(x => x.Checkout.User)
                         .Include(x => x.Checkout.CheckoutItems)
                         .FirstOrDefault();


                var billingaddress = db.BillingAddress.ToList();
                var shippingaddress = db.Shipping.ToList();
                var users = db.Users.Where(x =>x.RoleId == 2).ToList();
                var trackorders = db.TrackOrders.Where(x => x.IsActive == true).ToList();
                var paymentMode = db.PaymentModes.Where(x => x.IsActive == true).ToList();

                order.OrderNo = item.Checkout.InvoiceNumber;
                order.PurchasedOn = item.Date?.ToShortDateString();
                order.DeliveryDate = item.Checkout.DeliveryDate.ToShortDateString();
                if (item.Checkout.BillingAddressId > 0)
                {
                    var data = billingaddress.Where(x => x.Id == item.Checkout.BillingAddressId).FirstOrDefault();
                    if (data != null)
                    {
                        order.billtoName = data?.Name;
                        order.billAddress = data?.Street +  (data?.OutsideNumber != "" ? " " : "")+data?.OutsideNumber + (data?.InteriorNumber != "" ? " " : "")+data?.InteriorNumber;
                        order.billStreets = data?.Street1 + (data?.Street2 != "" ? " " : "") + data.Street2;
                        order.billColony = data?.Colony ?? "";
                        order.billCity = data?.City??"";
                        order.billState = data?.State;
                        order.billPin = data?.Pincode;
                        order.billLandmark = data?.LandMark ?? "";
                        order.billPhoneNo = data?.PhoneNo;

                    }

                }
                if (order.billtoName == null)
                    order.billtoName = "";
                if (item.Checkout.ShippingId > 0)
                {
                    var ship = shippingaddress.Where(x => x.Id == item.Checkout.ShippingId ).FirstOrDefault();
                    order.ShiptoName = ship?.Name;
                    order.ShipAddress = ship?.Street+(ship?.OutsideNumber != "" ? " " : "") + ship?.OutsideNumber + (ship?.InteriorNumber != "" ? " " : "") + ship?.InteriorNumber;
                    order.ShipStreets = ship?.Street1 + (ship?.Street2 != "" ? " " : "") + ship.Street2;
                    order.ShipColony = ship?.Colony??"";
                    order.ShipCity = ship?.City??"";
                    //order.ShipCountry = ship?.Country?.Name;
                    order.ShipState = ship?.StateName;
                    order.ShipPin = ship?.Pincode;
                    order.ShipLandmark = ship?.LandMark??"";
                    order.ShipPhoneNo = ship?.PhoneNo;
                }
                if (order.ShiptoName == null)
                    order.ShiptoName = "";
                order.PurchasePrice = item.Checkout.TotalAmount + item.Checkout.ShippingPrice;
                
                order.BasePrice = item.Checkout.TotalAmount ;
                order.ShippingPrice = item.Checkout.ShippingPrice;
                order.Quantity = item.Checkout.CheckoutItems.Count();
                var venderId = item.Checkout.CheckoutItems.Select(x => x.VendorId).FirstOrDefault();
                if (venderId > 0)
                {
                    order.VenderName = users.Where(x => x.Id == venderId).FirstOrDefault()?.FirstName;
                    if (order.VenderName == null)
                        order.VenderName = "";
                    if (order.VenderName != null)
                        order.VenderId = venderId;

                }
                order.TrackingNumber = trackorders.Where(x => x.OrderId == item.Id).FirstOrDefault()?.TrackId;
                if (order.TrackingNumber == null)
                    order.TrackingNumber = "";
                //if (TrackId !=null)
                // order.TrackingNumber = TrackId;
                order.OrderStatus = trackorders.Where(x => x.OrderId == item.Id).FirstOrDefault()?.Status;
                if (order.OrderStatus == null)
                    order.OrderStatus = "";
                order.ShippedBy = item.Checkout.ShippingType;
                if (order.ShippedBy == null)
                    order.ShippedBy = "";
                order.PaymentMethod = item.PaymentMethod;
                order.UserEmailId = item.Checkout.User?.Email??"";
                //orderList.Add(order);
                order.items = db.CheckoutItems.Where(x => x.CheckoutId == item.CheckoutId && x.IsActive == true).Include(x => x.ProductVariantDetail.Product).ToList();
                foreach (var i in order.items)
                {
                    i.ProductVariantDetail.Product.ProductVariantDetails = null;
                    i.Checkout = null;
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(order);
        }
    }
    public class OrderDeatils
    {
        public int PaymentTransactionId { get; set; }
        public string OrederNo { get; set; }
        public string PurchasedOn { get; set; }
        public Nullable<Decimal> BasePrice { get; set; }
        public string billtoName { get; set; }
        public string ShiptoName { get; set; }
        public Nullable<Decimal> PurchasePrice { get; set; }
        public string OrderStatus { get; set; }
        public string ShippedBy { get; set; }
        public string PaymentMethod { get; set; }
        public int? Quantity { get; set; }
        public int? VenderId { get; set; }
        public string VenderName { get; set; }
        public string TrackingNumber { get; set; }
        public string UserEmailId { get; set; }

    }
}