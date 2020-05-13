using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Localdb;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using TestCore.Helper;
using TestCore.Extension_Method;

namespace TestCore.Controllers
{
    [Route("api/cart")]
    [ApiController]
    [EnableCors("EnableCORS")]
    public class CartController : ControllerBase
    {
        private readonly PistisContext db;
        CategoryCommission obj;
        public CartController(PistisContext pistis)
        {
            db = pistis;
            obj = new CategoryCommission(db);
        }
        [HttpPost]
        [Route("addToCart")]
        public IActionResult AddToCart([FromBody] Cart pro )
        {
           
            var model = new Cart();
            var cart = new Cart();

            //model.AdditionalCost=pro.
            model.IsActive = true;
            model.IsConvertToCheckout = false;
            model.OrderDate = DateTime.Now;
           // model.TotalAmount = pro.TotalAmount;
            if (pro.UserId > 0)
            {
                model.UserId = pro.UserId;
                cart = db.Carts.Where(x => x.UserId == pro.UserId && x.IsActive == true && x.IsConvertToCheckout == false).FirstOrDefault();
            }
            else
            {
                cart = db.Carts.Where(x => x.IpAddress == pro.IpAddress &&x.UserId==null && x.IsActive == true && x.IsConvertToCheckout==false).FirstOrDefault();

            }

            model.IpAddress = pro.IpAddress;
            //model.VendorId
            try
            {
                if (cart == null)
                {
                    db.Carts.Add(model);
                    db.SaveChanges();
                    model.OrderNumber = "ORD-" + CommonFunctions.RandCode(model.Id);
                    db.SaveChanges();
                    //Addto cartitem
                    var item = new CartItem();
                    item.IsActive = true;
                    item.ProductVariantDetailId = pro.CartItems[0].ProductVariantDetailId;
                    
                    item.Quantity = pro.CartItems[0].Quantity==0?1:pro.CartItems[0].Quantity;
                    
                    item.UnitId = 1;
                    item.VendorId = pro.CartItems[0].VendorId;
                    item.CartId = model.Id;
                    db.CartItems.Add(item);
                    db.SaveChanges();

                    ////TotalAmount withCommission in cart
                    //var cartdata = db.Carts.Where(x => x.Id == model.Id).FirstOrDefault();
                    //cartdata.TotalAmount = item.Amount;
                    //db.SaveChanges();
                }
                else
                {//update
                    model.Id = cart.Id;
                    var cartitem = db.CartItems.Where(x => x.CartId == model.Id && x.ProductVariantDetailId == pro.CartItems[0].ProductVariantDetailId && x.IsActive == true).FirstOrDefault();
                    if (cartitem == null)
                    {
                        //Addto cartitem
                        var item = new CartItem();
                        item.IsActive = true;
                        item.ProductVariantDetailId = pro.CartItems[0].ProductVariantDetailId;
                        item.Quantity = pro.CartItems[0].Quantity==0?1:pro.CartItems[0].Quantity;
                        item.UnitId = 1;
                        item.VendorId = pro.CartItems[0].VendorId;
                        item.CartId = model.Id;
                        item.ShipmentVendor = pro.CartItems[0].ShipmentVendor;
                        db.CartItems.Add(item);
                        db.SaveChanges();
                        //TotalAmount withCommission in cart
                        //var cartdata = db.Carts.Where(x => x.Id == model.Id).FirstOrDefault();
                        //cartdata.TotalAmount = item.Amount;
                        //db.SaveChanges();
                    }
                    else
                    {
                        var now = pro.CartItems[0].Quantity == 0 ? 1 : pro.CartItems[0].Quantity;
                        var quantity = cartitem.Quantity + now;
                        cartitem.Quantity = quantity;
                        //var pricewithcommission = cartitem.UnitPrice + cartitem.Commission;
                        //var discountedPrice = pricewithcommission - (pricewithcommission * cartitem.Discount / 100);
                        //cartitem.Amount = discountedPrice * quantity;
                        db.Entry(cartitem).State = EntityState.Modified;
                        db.SaveChanges();


                    }
                    //cart.TotalAmount = db.CartItems.Where(x => x.CartId == cart.Id && x.IsActive == true).AsNoTracking().ToList().Sum(x => x.Amount);
                    cart.IpAddress = model.IpAddress;
                    cart.OrderDate = DateTime.Now;
                    db.Entry(cart).State = EntityState.Modified;
                    db.SaveChanges();
                }
                
                return Ok(pro.CartItems[0].ShipmentVendor??false);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return Ok();
            }
        }

        int getparentCat(int Id)
        {
            var data = db.ProductCategories.Where(x => x.IsActive == true && x.Id == Id).FirstOrDefault().RemoveReferences();
            if (data.ParentId == null)
                return data.Id;
            else
                return getparentCat(Convert.ToInt32(data.ParentId));
        }
         int GetCommissionByCategoryId(int id)
        {
            var data = obj.CategoryCommissions.Where(x => x.IsActive == true && x.ProductCategoryId == id).FirstOrDefault();
            if (data != null)
                return data.Commission;
            else
                return 0;
        }
        [HttpGet]
        [Route("getCartByCustomer")]
        public List<GetCart> GetCartByCustomer(int CustomerId, string ip)
        {
            var carts = new List<Cart>();
            var cart = new Cart();
            var vendors = db.Users.Where(b => b.IsActive == true).ToList();

            if (CustomerId > 0)
                carts = db.Carts.Where(x => (x.UserId == CustomerId ||(x.UserId==null && x.IpAddress==ip)) && x.IsActive == true && x.IsConvertToCheckout == false).Include(x => x.CartItems).ToList();
            else
            {
                carts = db.Carts.Where(x => x.IpAddress == ip && x.IsActive == true && x.UserId == null && x.IsConvertToCheckout == false).Include(x => x.CartItems).ToList();
            }
            //to add all cartitems in first cart
            if (carts.Count() > 1)
            {
                cart = carts[0];
                for(int i=1;i<carts.Count();i++)
                {
                   var items= db.CartItems.Where(x => x.CartId == carts[i].Id && x.CartId!= cart.Id && x.IsActive == true).ToList();
                    foreach(var it in items)
                    {
                        
                        if(cart.CartItems.Any(x=>x.ProductVariantDetailId==it.ProductVariantDetailId && x.IsActive==true))
                        {
                            cart.CartItems.Where(x => x.ProductVariantDetailId == it.ProductVariantDetailId && x.IsActive == true).FirstOrDefault().Quantity += it.Quantity;
                            it.IsActive = false;
                        }
                        it.CartId = cart.Id;
                        db.Entry(it).State = EntityState.Modified;

                    }
                    try
                    {
                        db.SaveChanges();
                        //var allcarts = db.Carts.Where(x => x.IsActive == true && x.CartItems.Count() == 0).Include(x=>x.CartItems).ToList();
                        ////allcarts.RemoveAll(x => x.Id > 0);
                        //if (allcarts.Count() > 0)
                        //{
                        //    foreach(var a in allcarts)
                        //    db.Entry(a).State = EntityState.Deleted;
                        //    db.SaveChanges();
                        //}
                    }
                    catch(Exception ex)
                    { }
                }
            }
            else if(carts.Count()==1)
                cart = carts[0];

            foreach (var c in cart.CartItems.Where(m => m.IsActive == true))
            {
                c.ProductVariantDetail = db.ProductVariantDetails.Where(x => x.IsActive == true && x.Id == c.ProductVariantDetailId)
                .Include(x => x.Product)
                .Include(x => x.ProductVariantOptions)
                .Include(x => x.ProductImages).FirstOrDefault().RemoveReferences();
            }
            var cartitems = new List<GetCart>();
            var newitems1 = cart.CartItems.Where(x => x.IsActive == true).ToList();
            var newitems = newitems1.Where(x => x.ShipmentVendor == true).ToList();
            if (newitems.Count() == 0)
                newitems = newitems1;
            //newitems = DealHelper.calculateDeal(newitems, db);
            foreach (var c in newitems)
            {
                var model = new GetCart();
                model.AdditionalCost = cart.AdditionalCost;
                model.ProductId = Convert.ToInt32(c.ProductVariantDetail?.ProductId);
                model.Id = cart.Id;
                var productimages = c.ProductVariantDetail?.ProductImages.Where(x => x.IsActive == true).ToList();
                if (productimages?.Count > 0)
                { 
                    var image="";
                    if (c.ProductVariantDetail?.ProductImages?.Where(x => x.IsDefault == true && x.IsActive == true).FirstOrDefault()!=null)
                    {
                    image = c.ProductVariantDetail.ProductImages.Where(x => x.IsDefault == true && x.IsActive == true).FirstOrDefault().ImagePath150x150 == null ?
                       c.ProductVariantDetail.ProductImages.Where(x => x.IsDefault == true && x.IsActive == true).FirstOrDefault().ImagePath :
                       c.ProductVariantDetail.ProductImages.Where(x => x.IsDefault == true && x.IsActive == true).FirstOrDefault().ImagePath150x150;

                    }
                    else
                    if (c.ProductVariantDetail.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath150x150 == null)
                    {
                        image = c.ProductVariantDetail?.ProductImages?.Where(x =>  x.IsActive == true).FirstOrDefault().ImagePath;
                    }
                    else
                    {
                        image = c.ProductVariantDetail?.ProductImages?.Where(x =>  x.IsActive == true).FirstOrDefault().ImagePath150x150;

                    }

                    if (image != null)
                    {
                        model.Image150 = image;
                    }
                }
                model.InStock = Convert.ToInt32(c.ProductVariantDetail?.InStock);
                model.SellingPrice = Convert.ToDecimal(c.ProductVariantDetail?.Price);
                var p = Convert.ToInt32(db.ProductVariantDetails.Where(x => x.Id == c.ProductVariantDetailId).Include(x => x.Product.ProductCategory).FirstOrDefault().Product?.ProductCategory?.ParentId);
                var catid = getparentCat(p);
                model.Commission = GetCommissionByCategoryId(catid);
                model.Discount = Convert.ToInt32(c.ProductVariantDetail?.Discount);
             
               
                model.IpAddress = cart.IpAddress;
                model.IsConvertToCheckout = cart.IsConvertToCheckout;
                model.Name = c.ProductVariantDetail?.Product?.Name;
                model.ShipmentVendor = c.ProductVariantDetail?.Product?.ShipmentVendor??false;
                model.ShipmentCost = c.ProductVariantDetail?.Product?.ShipmentCost??0;
                model.ShipmentTime = c.ProductVariantDetail?.Product?.ShipmentTime??0;
                model.OrderDate = cart.OrderDate;
                model.OrderNumber = cart.OrderNumber;
                model.UserId = cart.UserId;
                model.ProductVariantDetailId = c.ProductVariantDetailId;
                if (c.ProductVariantDetail?.ProductVariantOptions != null)
                {
                    foreach (var v in c.ProductVariantDetail?.ProductVariantOptions.Where(x => x.IsActive == true))
                    {
                        var variantid = v.VariantOptionId;
                        var variantop = new VariantOption();
                        variantop = db.VariantOptions.Where(x => x.IsActive == true && x.Id == variantid).Include(x => x.Variant).FirstOrDefault().RemoveReferences();
                        var variantmodel = new VariantOptionModel();
                        variantmodel.Id = variantop.Id;
                        variantmodel.Name = variantop.Name;
                        variantmodel.VariantId = variantop.VariantId;
                        variantmodel.varientName = variantop.Variant?.Name;
                        model.VariantOptions.Add(variantmodel);
                    }
                }
                model.Quantity = c.Quantity;
                model.CartItemId = c.Id;
                if (c.ProductVariantDetail?.Product.VendorId > 0)
                {
                    var vendor = vendors.Where(v => v.Id == c.ProductVariantDetail?.Product?.VendorId && v.RoleId == (int)RoleType.Vendor).FirstOrDefault();
                    if (vendor != null)
                    {
                        model.VendorName = vendor.DisplayName;
                        model.VendorId = vendor.Id;
                    }
                }
                cartitems.Add(model);
            }
            
            cartitems = DealHelper.calculateDeal(cartitems, db);
            cartitems = PriceIncrementHelper.calculatePrice(cartitems, db);
            return cartitems;

        }
        [HttpGet]
        [Route("getCartByCustomerpromo")]
        public List<GetCart> GetCartByCustomerPromo(int CustomerId, string ip)
        {
            var carts = new List<Cart>();
            var cart = new Cart();
            var vendors = db.Users.Where(b => b.IsActive == true).ToList();

            if (CustomerId > 0)
                carts = db.Carts.Where(x => x.UserId == CustomerId && x.IsActive == true && x.IsConvertToCheckout == false).Include(x => x.CartItems).ToList();
            //else
            //{
            //    carts = db.Carts.Where(x => x.IpAddress == ip && x.IsActive == true && x.UserId == null && x.IsConvertToCheckout == false).Include(x => x.CartItems).ToList();
            //}
            //to add all cartitems in first cart
            if (carts.Count() > 1)
                {
                    cart = carts[0];
                    for (int i = 1; i < carts.Count(); i++)
                    {
                        var items = db.CartItems.Where(x => x.CartId == carts[i].Id && x.CartId != cart.Id && x.IsActive == true).ToList();
                        foreach (var it in items)
                        {
                            it.CartId = cart.Id;
                        }
                        try
                        {
                            db.Entry(items).State = EntityState.Modified;
                            db.SaveChanges();
                            var allcarts = db.Carts.Where(x => x.IsActive == true && x.CartItems.Count() == 0).Include(x => x.CartItems).ToList();
                            //allcarts.RemoveAll(x => x.Id > 0);
                            db.Entry(allcarts).State = EntityState.Deleted;
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        { }
                    }
                }
                else if (carts.Count() == 1)
                    cart = carts[0];


                foreach (var c in cart.CartItems.Where(m => m.IsActive == true))
                {
                    c.ProductVariantDetail = db.ProductVariantDetails.Where(x => x.IsActive == true && x.Id == c.ProductVariantDetailId)
                    .Include(x => x.Product)
                    .Include(x => x.ProductVariantOptions)
                    .Include(x => x.ProductImages).FirstOrDefault().RemoveReferences();
                }


                var cartitems = new List<GetCart>();
                var newitems = cart.CartItems.Where(x => x.IsActive == true).ToList();
                //newitems = DealHelper.calculateDeal(newitems, db);
                foreach (var c in newitems)
                {
                    var model = new GetCart();
                    model.AdditionalCost = cart.AdditionalCost;
                    model.ProductId = Convert.ToInt32(c.ProductVariantDetail?.ProductId);
                    model.Id = cart.Id;
                    if (c.ProductVariantDetail?.ProductImages.Count > 0)
                    {
                        model.Image150 = c.ProductVariantDetail?.ProductImages.Where(x => x.IsDefault == true && x.IsActive == true).FirstOrDefault() == null ? 
                        (c.ProductVariantDetail.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath150x150 ==null? c.ProductVariantDetail.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath: c.ProductVariantDetail.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath150x150)
                        :( c.ProductVariantDetail.ProductImages.Where(x => x.IsDefault == true && x.IsActive == true).FirstOrDefault().ImagePath150x150==null? c.ProductVariantDetail.ProductImages.Where(x => x.IsDefault == true && x.IsActive == true).FirstOrDefault().ImagePath: c.ProductVariantDetail.ProductImages.Where(x => x.IsDefault == true && x.IsActive == true).FirstOrDefault().ImagePath150x150);

                    }
                    model.InStock = Convert.ToInt32(c.ProductVariantDetail?.InStock);
                    model.SellingPrice = Convert.ToInt32(c.ProductVariantDetail.Price);
                    var p = Convert.ToInt32(db.ProductVariantDetails.Where(x => x.Id == c.ProductVariantDetailId).Include(x => x.Product.ProductCategory).FirstOrDefault().Product?.ProductCategory?.ParentId);
                    var catid = getparentCat(p);
                    model.Commission = GetCommissionByCategoryId(catid);
                    model.Discount = Convert.ToInt32(c.ProductVariantDetail.Discount);
                    model.IpAddress = cart.IpAddress;
                    model.IsConvertToCheckout = cart.IsConvertToCheckout;
                    model.Name = c.ProductVariantDetail?.Product?.Name;
                    model.OrderDate = cart.OrderDate;
                    model.OrderNumber = cart.OrderNumber;
                    model.UserId = cart.UserId;
                    model.ProductVariantDetailId = c.ProductVariantDetailId;
                    if (c.ProductVariantDetail?.ProductVariantOptions != null)
                    {
                        foreach (var v in c.ProductVariantDetail?.ProductVariantOptions.Where(x => x.IsActive == true))
                        {
                            var variantid = v.VariantOptionId;
                            var variantop = new VariantOption();
                            variantop = db.VariantOptions.Where(x => x.IsActive == true && x.Id == variantid).Include(x => x.Variant).FirstOrDefault().RemoveReferences();
                            var variantmodel = new VariantOptionModel();
                            variantmodel.Id = variantop.Id;
                            variantmodel.Name = variantop.Name;
                            variantmodel.VariantId = variantop.VariantId;
                            variantmodel.varientName = variantop.Variant?.Name;
                            model.VariantOptions.Add(variantmodel);
                        }
                    }
                    model.Quantity = c.Quantity;
                    model.CartItemId = c.Id;
                    if (c.ProductVariantDetail?.Product.VendorId > 0)
                    {
                        var vendor = vendors.Where(v => v.Id == c.ProductVariantDetail?.Product?.VendorId && v.RoleId == (int)RoleType.Vendor).FirstOrDefault();
                        if (vendor != null)
                            model.VendorName = vendor.DisplayName;
                    }
                    cartitems.Add(model);
                }

                cartitems = DealHelper.calculateDeal(cartitems, db);
            cartitems = PriceIncrementHelper.calculatePrice(cartitems, db);
            cartitems = SpinnerPromotion(cartitems, CustomerId);
                return cartitems;

            
            var getCart = new List<GetCart>();
            return getCart;
        }

        [HttpGet]
        [Route("removeItem")]
        public bool RemoveItem(int id)
        {
            var data = db.CartItems.Where(x => x.Id == id && x.IsActive == true).AsNoTracking().FirstOrDefault();
            if (data != null)
            {
                data.IsActive = false;
                db.Entry(data).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    //var cart = db.Carts.Where(x => x.Id == data.CartId && x.IsActive == true).AsNoTracking().FirstOrDefault();
                    //cart.TotalAmount = db.CartItems.Where(x => x.CartId == cart.Id && x.IsActive == true).AsNoTracking().ToList().Sum(x => x.Amount);
                    //db.Entry(cart).State = EntityState.Modified;
                   // db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }

        [HttpGet]
        [Route("updateQuantity")]
        public bool UpdateQuantity(int quantity,int Id)
        {
            var data = db.CartItems.Where(x => x.Id == Id && x.IsActive == true).AsNoTracking().FirstOrDefault().RemoveReferences();
            data.Quantity = quantity;
            //var pricewithcommission = data.UnitPrice + data.Commission;
            //var discountedPrice = pricewithcommission - (pricewithcommission * data.Discount / 100);
            //data.Amount = discountedPrice * quantity;
            db.Entry(data).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                //var cart = db.Carts.Where(x => x.Id == data.CartId && x.IsActive == true).AsNoTracking().FirstOrDefault();
                //cart.TotalAmount = db.CartItems.Where(x => x.CartId == cart.Id && x.IsActive == true).AsNoTracking().ToList().Sum(x => x.Amount);
               // db.Entry(cart).State = EntityState.Modified;
                //db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        [HttpGet]
        [Route("updateUser")]
        public bool UpdateUser(int userid, string ipaddress)
        {
            var data = db.Carts.Where(x => x.IpAddress == ipaddress && x.IsActive == true && x.IsConvertToCheckout == false && (x.UserId == null || x.UserId == userid)).FirstOrDefault();
            if (data != null)
                data.UserId = userid;
            try
            {
                db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<GetCart> SpinnerPromotion(List<GetCart> productVariants, int CustomerId)
        {
            var Thridlevel = new List<ProductCategory>();
            var match = new List<ProductVariantDetail>();
            var productvarientdetailList = db.ProductVariantDetails.Where(x => x.IsActive == true).ToList();

            int promotionVarientDetailId = 0;
            var spinnerUserData = db.SpinUserData.Where(x => x.UserId == CustomerId).FirstOrDefault();
            var SpinnerPromotionData = db.SpinnerPromotion.Where(x => x.Id == spinnerUserData.SpinnerPromotionId).FirstOrDefault();
            var allCategories = db.ProductCategories.Where(x => x.IsActive == true).ToList();
           


            if (SpinnerPromotionData.CategoryId > 0)
            {
                if (SpinnerPromotionData.ProductCategoryId > 0)
                {
                    if (SpinnerPromotionData.ProductId > 0)
                    {
                        promotionVarientDetailId =Convert.ToInt32(SpinnerPromotionData.ProductId);
                        productVariants = productVariants.Where(x => x.ProductVariantDetailId == promotionVarientDetailId).ToList();
                        
                        foreach (var item in productVariants)
                        {
                            if (item.Quantity <= SpinnerPromotionData.MaxQty)
                            {
                                item.Discount += Convert.ToInt32(SpinnerPromotionData.DiscountPercentage);

                                var PriceAfterDiscount = item.SellingPrice - (item.SellingPrice * item.Discount / 100);
                              item.PriceAfterDiscount=  Convert.ToDecimal(PriceAfterDiscount.ToString("#.00"));
                                item.Amount = item.PriceAfterDiscount * item.Quantity;
                            }
                        }
                        return productVariants;
                    }

                    var PromoitionProductCategoryId = db.ProductCategories.Where(x => x.Id == SpinnerPromotionData.ProductCategoryId).ToList();
                    foreach (var Subchild in PromoitionProductCategoryId)
                    {
                        if (allCategories.Any(x => x.Id == Subchild.Id))
                        {
                            Thridlevel.Add(Subchild);
                        }
                        else
                            Thridlevel.AddRange(allCategories.Where(x => x.ParentId == Subchild.Id).ToList());
                    }
                    if (Thridlevel != null)
                    {
                        var filter = new List<GetCart>();
                        foreach (var item in productVariants)
                        {
                            var productId = productvarientdetailList.Where(x => x.Id == item.ProductVariantDetailId).FirstOrDefault().ProductId;
                            var ProductCategoryId = db.Products.Where(x => x.Id == productId).FirstOrDefault().ProductCategoryId;
                            var SingleThridlevel = Thridlevel.Where(x => x.Id == ProductCategoryId).FirstOrDefault();
                            if (SingleThridlevel != null)
                            {
                                if (item.Quantity <= SpinnerPromotionData.MaxQty)
                                {
                                    item.Discount += Convert.ToInt32(SpinnerPromotionData.DiscountPercentage);
                                    item.PriceAfterDiscount = item.SellingPrice - (item.SellingPrice * item.Discount / 100);
                                    item.Amount = item.PriceAfterDiscount * item.Quantity;
                                    filter.Add(item);
                                }
                            }

                        }
                        //  match= productVariants.Where(p => Thridlevel.All(x => x.Id == p.Product.ProductCategoryId)).ToList();
                        return filter;
                    }


                }


                var seconedlevel = allCategories.Where(x => x.ParentId == SpinnerPromotionData.CategoryId).ToList();
                foreach (var Subchild in seconedlevel)
                {
                    if (allCategories.Any(x => x.Id == Subchild.Id))
                    {
                        Thridlevel.Add(Subchild);
                    }
                    else
                        Thridlevel.AddRange(allCategories.Where(x => x.ParentId == Subchild.Id).ToList());


                }
                var lastlevel = new List<ProductCategory>();
                foreach (var item in Thridlevel)
                {
                    if (!allCategories.Any(x => x.ParentId == item.Id))
                    {
                        lastlevel.Add(item);
                    }
                    else
                        lastlevel.AddRange(allCategories.Where(x => x.ParentId == item.Id).ToList());
                }
                if (lastlevel != null)
                {
                    var filter = new List<GetCart>();
                    foreach (var item in productVariants)
                    {
                        var productId = productvarientdetailList.Where(x => x.Id == item.ProductVariantDetailId).FirstOrDefault().ProductId;
                        var ProductCategoryId = db.Products.Where(x => x.Id == productId).FirstOrDefault().ProductCategoryId;
                        var SingleThridlevel = lastlevel.Where(x => x.Id == ProductCategoryId).FirstOrDefault();
                        if (SingleThridlevel != null)
                        {
                            if (item.Quantity <= SpinnerPromotionData.MaxQty)
                            {
                                item.Discount += Convert.ToInt32(SpinnerPromotionData.DiscountPercentage);
                                item.PriceAfterDiscount = item.SellingPrice - (item.SellingPrice * item.Discount / 100);
                                item.Amount = item.PriceAfterDiscount * item.Quantity;
                                filter.Add(item);
                            }
                        }

                    }
                    //  match= productVariants.Where(p => Thridlevel.All(x => x.Id == p.Product.ProductCategoryId)).ToList();
                    return filter;
                }

            }
            return productVariants;
        }
        [HttpGet]
        [Route("placeOrderChecking")]
        public IActionResult placeOrderChecking(int id)
        {
            var data = db.CartItems.Where(x => x.IsActive == true && x.CartId == id).Include(x=>x.ProductVariantDetail).ToList();
            var result = new List<CartStock>();
            foreach(var d in data)
            {
                var r = new CartStock();
                if(d.Quantity>d.ProductVariantDetail.InStock)
                {
                    r.Id = d.CartId;
                    r.CartItemId = d.Id;
                    r.IsStockAvailable = false;
                    r.MaxStock = d.ProductVariantDetail.InStock;
                }
                else
                {
                    r.Id = d.CartId;
                    r.CartItemId = d.Id;
                    r.IsStockAvailable = true;
                    r.MaxStock = d.ProductVariantDetail.InStock;
                }
                result.Add(r);
            }
            return Ok(result);
        }
    }
}