using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Localdb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace TestCore.Controllers
{
    [Route("api/shipping")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly PistisContext db;
        public ShippingController(PistisContext pistis)
        {
            db = pistis;
        }

        [HttpGet]
        [Route("getAdressByUser")]
      //  [Authorize(Roles="Admin,Customer")]
        public IActionResult ByUser(int id, string IpAddress)
        {
            try
            {
                var query = new List<Models.Shipping>() ;
                if (id > 0)
                {
                    query = db.Shipping.Where(m => m.IsActive == true && m.UserId == id).ToList();
                    var user = db.Users.Where(x => x.Id == id).FirstOrDefault().FirstName;
                    //if (IpAddress != null && id < 0)
                    //    query = query.Where(v => v.IpAddress == IpAddress);

                    var data = query.Select(b => new ShippingModel
                    {
                        Id = b.Id,
                        Name = b.Name,
                        UserId = b.UserId,
                        IpAddress = b.IpAddress,
                        AlternatePhoneNo = b.AlternatePhoneNo,
                        AddressType = b.AddressType,
                        Address = b.Address,
                        City = b.City,
                        CountryId = b.CountryId,
                        IsActive = b.IsActive,
                        LandMark = b.LandMark,
                        PhoneNo = b.PhoneNo,
                        Pincode = b.Pincode,
                        StateName = b.StateName,
                        IsDefault = b.IsDefault,
                        Street = b.Street,
                        Street1 = b.Street1,
                        Street2 = b.Street2,
                        Colony = b.Colony,
                        InteriorNumber = b.InteriorNumber,
                        OutsideNumber = b.OutsideNumber,
                        //Country = new Country
                        //{
                        //    Id = b.Country.Id,
                        //    Name = b.Country.Name,
                        //},
                        //State = new State
                        //{
                        //    Id = b.State.Id,
                        //    Name = b.State.Name,
                        //}
                    }).ToList();
                    if (data != null)
                        return Ok(data);
                    else
                        return Ok(data);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }
        [HttpPost]
        [Route("addUserAddress1")]
        public IActionResult Add1(ShippingModel5 model)
        {
            var message = 0;
            try
            {
                if (model.Id > 0)
                {
                    var oldData = db.Shipping.Where(v => v.Id == model.Id && v.IsActive == true).FirstOrDefault();
                    if (oldData != null)
                    {
                        oldData.Name = model.Name;
                        oldData.IpAddress = model.IpAddress;
                        oldData.Address = model.Address;
                        oldData.AlternatePhoneNo = model.AlternatePhoneNo;
                        oldData.AddressType = model.AddressType;
                        oldData.City = model.City;
                        oldData.CountryId = model.CountryId;
                        oldData.PhoneNo = model.PhoneNo;
                        oldData.LandMark = model.LandMark;
                        oldData.Pincode = model.Pincode;
                        oldData.IsDefault = false;
                        oldData.StateName = model.StateName;
                        oldData.Street = model.Street;
                        oldData.Street1 = model.Street1;
                        oldData.Street2 = model.Street2;
                        oldData.Colony = model.Colony;
                        oldData.InteriorNumber = model.InteriorNumber;
                        oldData.OutsideNumber = model.OutsideNumber;
                        db.SaveChanges();
                        return Ok(message);

                    }
                    else
                    {
                        Shipping shipData = new Shipping();
                        shipData.Name = model.Name;
                        shipData.UserId = model.UserId;
                        shipData.IpAddress = model.IpAddress;
                        shipData.AddressType = model.AddressType;
                        shipData.City = model.City;
                        shipData.CountryId = model.CountryId;
                        shipData.PhoneNo = model.PhoneNo;
                        shipData.LandMark = model.LandMark;
                        shipData.Pincode = model.Pincode;
                        shipData.StateName = model.StateName;

                        shipData.Street = model.Street;
                        shipData.Street1 = model.Street1;
                        shipData.Street2 = model.Street2;
                        shipData.Colony = model.Colony;
                        shipData.InteriorNumber = model.InteriorNumber;
                        shipData.OutsideNumber = model.OutsideNumber;

                        shipData.IsDefault = false;
                        shipData.IsActive = true;
                        db.Shipping.Add(shipData);
                        db.SaveChanges();
                        message = 1;
                        return Ok(message);
                    }
                }
                else
                {
                    Shipping shipData = new Shipping();
                    shipData.Name = model.Name;
                    shipData.UserId = model.UserId;
                    shipData.IpAddress = model.IpAddress;
                    shipData.AddressType = model.AddressType;
                    shipData.City = model.City;
                    shipData.CountryId = model.CountryId;
                    shipData.PhoneNo = model.PhoneNo;
                    shipData.LandMark = model.LandMark;
                    shipData.Pincode = model.Pincode;
                    shipData.StateName = model.StateName;

                    shipData.Street = model.Street;
                    shipData.Street1 = model.Street1;
                    shipData.Street2 = model.Street2;
                    shipData.Colony = model.Colony;
                    shipData.InteriorNumber = model.InteriorNumber;
                    shipData.OutsideNumber = model.OutsideNumber;

                    shipData.IsDefault = false;
                    shipData.IsActive = true;
                    db.Shipping.Add(shipData);
                    db.SaveChanges();
                    message = 1;
                    return Ok(message);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
            return Ok(message);
        }
        [HttpPost]
        [Route("addUserAddress")]
        public IActionResult Add(ShippingModel5 model)
        {
            try
            {
                Shipping shipData = new Shipping();
                shipData.Name = model.Name;
                shipData.Email = model.Email;
                shipData.UserId = model.UserId;
                shipData.IpAddress = model.IpAddress;
                shipData.Address = model.Address;
                shipData.AlternatePhoneNo = model.AlternatePhoneNo;
                shipData.AddressType = model.AddressType;
                shipData.City = model.City;
                shipData.CountryId = model.CountryId;
                shipData.PhoneNo = model.PhoneNo;
                shipData.LandMark = model.LandMark;
                shipData.Pincode = model.Pincode;
                shipData.StateName = model.StateName;

                shipData.Street = model.Street;
                shipData.Street1 = model.Street1;
                shipData.Street2 = model.Street2;
                shipData.Colony = model.Colony;
                shipData.InteriorNumber = model.InteriorNumber;
                shipData.OutsideNumber = model.OutsideNumber;

                shipData.IsDefault = false;
                shipData.IsActive = true;
                db.Shipping.Add(shipData);
                db.SaveChanges();
                return Ok(shipData);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }

        [HttpPost]
        [Route("addUserAddressGuest")]
        public IActionResult AddGuest(ShippingModel5 model)
        {
            try
            {
                Shipping shipData = new Shipping();
                shipData.Name = model.Name;
                shipData.Email = model.Email;
                shipData.UserId = model.UserId;
                shipData.IpAddress = model.IpAddress;
                shipData.Address = model.Address;
                shipData.AlternatePhoneNo = model.AlternatePhoneNo;
                shipData.AddressType = model.AddressType;
                shipData.City = model.City;
                shipData.CountryId = model.CountryId;
                shipData.PhoneNo = model.PhoneNo;
                shipData.LandMark = model.LandMark;
                shipData.Pincode = model.Pincode;
                shipData.StateName = model.StateName;

                shipData.Street = model.Street;
                shipData.Street1 = model.Street1;
                shipData.Street2 = model.Street2;
                shipData.Colony = model.Colony;
                shipData.InteriorNumber = model.InteriorNumber;
                shipData.OutsideNumber = model.OutsideNumber;

                shipData.IsDefault = true;
                shipData.IsActive = true;
                db.Shipping.Add(shipData);
                db.SaveChanges();
                return Ok(shipData);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }

        [HttpPost]
        [Route("updateUserAddress")]
        public IActionResult Update(ShippingModel5 model)
        {
            try
            {
                var oldData = db.Shipping.Where(v => v.Id == model.Id && v.IsActive == true).FirstOrDefault();
                if (oldData == null)
                    return Ok("User Address Not Found...");
                oldData.Name = model.Name;
                oldData.Email = model.Email;
                oldData.IpAddress = model.IpAddress;
                oldData.Address = model.Address;
                oldData.AlternatePhoneNo = model.AlternatePhoneNo;
                oldData.AddressType = model.AddressType;
                oldData.City = model.City;
                oldData.CountryId = model.CountryId;
                oldData.PhoneNo = model.PhoneNo;
                oldData.LandMark = model.LandMark;
                oldData.Pincode = model.Pincode;
                oldData.IsDefault = false;
                oldData.StateName = model.StateName;
                oldData.Street = model.Street;
                oldData.Street1 = model.Street1;
                oldData.Street2 = model.Street2;
                oldData.Colony = model.Colony;
                oldData.InteriorNumber = model.InteriorNumber;
                oldData.OutsideNumber = model.OutsideNumber;
                db.SaveChanges();
                return Ok(oldData);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }

       
        [HttpGet]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var oldData = db.Shipping.Where(v => v.Id == id && v.IsActive == true).FirstOrDefault();
                if (oldData == null)
                    return Ok("User Address Not Found...");
                oldData.IsActive = false;
                db.SaveChanges();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }

        [HttpGet]
        [Route("getById")]
        public IActionResult getById(int id)
        {
            try
            {
                var ship = db.Shipping.Where(x => x.IsActive == true && x.Id == id).FirstOrDefault();

                var data = db.Shipping.Where(x => x.IsActive == true && x.Id == id)
                .Select(b => new ShippingModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    UserId = b.UserId,
                    IpAddress = b.IpAddress,
                    AddressType = b.AddressType,
                    Address = b.Address,
                    City = b.City,
                    CountryId = b.CountryId,
                    AlternatePhoneNo = b.AlternatePhoneNo,
                    IsActive = b.IsActive,
                    LandMark = b.LandMark,
                    PhoneNo = b.PhoneNo,
                    Pincode = b.Pincode,
    // StateId = b.StateId,
    Street = b.Street,
    StateName=b.StateName,
                    Street1 = b.Street1,
                    Street2 = b.Street2,
                    Colony = b.Colony,
                    InteriorNumber = b.InteriorNumber,
                    OutsideNumber = b.OutsideNumber,
    //Country = new Country
    //{
    // Id = b.Country.Id,
    // Name = b.Country.Name,
    //},
    //State = new State
    //{
    // Id = b.State.Id,
    // Name = b.State.Name,
    //}
}).FirstOrDefault();
                if (data != null)
                {
                    return Ok(data);
                }
                else
                {
                    return Ok("User Not Found");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }


        //---------------------------for billing addresses


        [HttpGet]
        [Route("getBillAdressByUser")]
        public IActionResult billAddressByUser(int id, string IpAddress)
        {
            try
            {
                var query = db.BillingAddress.Where(m => m.IsActive == true);
                var data = new List<ShippingBillAddressModel>();
                if (id > 0)
                {
                    query = query.Where(v => v.UserId == id);

                    //if (IpAddress != null && id < 0)
                    //    query = query.Where(v => v.IpAddress == IpAddress && v.UserId == 0);

                    data = query.Select(b => new ShippingBillAddressModel
                    {
                        Id = b.Id,
                        Name = b.Name,
                        UserId = b.UserId,
                        IpAddress = b.IpAddress,
                        City = b.City,
                        IsActive = b.IsActive,
                        LandMark = b.LandMark,
                        PhoneNo = b.PhoneNo,
                        Pincode = b.Pincode,
                        State = b.State,
                        IsDefault = b.IsDefault,
                        OutsideNumber = b.OutsideNumber,
                        InteriorNumber = b.InteriorNumber,
                        Street = b.Street,
                        Street1 = b.Street1,
                        Street2 = b.Street2,
                        Colony = b.Colony,
                    }).ToList();
                }
                if (data != null)
                    return Ok(data);
                else
                    return Ok(data);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        [Route("addUsersBillAddress")]
        public IActionResult AddBillAddress(ShippingBillAddressModel model)
        {
            try
            {
                BillingAddress shipData = new BillingAddress();
                shipData.Name = model.Name;
                shipData.UserId = model.UserId;
                shipData.IpAddress = model.IpAddress;
                shipData.Street = model.Street;
                shipData.Street1 = model.Street1;
                shipData.Street2 = model.Street2;
                shipData.OutsideNumber = model.OutsideNumber;
                shipData.InteriorNumber = model.InteriorNumber;
                shipData.City = model.City;
                shipData.PhoneNo = model.PhoneNo;
                shipData.LandMark = model.LandMark;
                shipData.Pincode = model.Pincode;
                shipData.State = model.State;
                shipData.Colony = model.Colony;
                shipData.IsDefault = false;
                shipData.IsActive = true;
                db.BillingAddress.Add(shipData);
                db.SaveChanges();
                return Ok(shipData);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }


        [HttpPost]
        [Route("updateUsersBillAdd")]
        public IActionResult UpdateBillAdd(ShippingBillAddressModel model)
        {
            try
            {
                var shipData = db.BillingAddress.Where(v => v.Id == model.Id && v.IsActive == true).FirstOrDefault();
                if (shipData == null)
                    return Ok("User Address Not Found...");
                shipData.Name = model.Name;
                shipData.UserId = model.UserId;
                shipData.IpAddress = model.IpAddress;
                shipData.Street = model.Street;
                shipData.Street1 = model.Street1;
                shipData.Street2 = model.Street2;
                shipData.OutsideNumber = model.OutsideNumber;
                shipData.InteriorNumber = model.InteriorNumber;
                shipData.City = model.City;
                shipData.PhoneNo = model.PhoneNo;
                shipData.LandMark = model.LandMark;
                shipData.Pincode = model.Pincode;
                shipData.State = model.State;
                shipData.Colony = model.Colony;
                db.SaveChanges();
                return Ok(shipData);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }

        [HttpGet]
        [Route("deleteBillAdd")]
        public IActionResult DeleteBillAdd(int id)
        {
            try
            {
                var oldData = db.BillingAddress.Where(v => v.Id == id && v.IsActive == true).FirstOrDefault();
                if (oldData == null)
                    return Ok("User Address Not Found...");
                oldData.IsActive = false;
                db.SaveChanges();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }

        [HttpGet]
        [Route("getBillAddById")]
        public IActionResult getBillAddById(int id)
        {
            try
            {
                var data = db.BillingAddress.Where(x => x.IsActive == true && x.Id == id)
                    .Select(b => new ShippingBillAddressModel
                    {
                        Id = b.Id,
                        Name = b.Name,
                        UserId = b.UserId,
                        IpAddress = b.IpAddress,
                        City = b.City,
                        IsActive = b.IsActive,
                        LandMark = b.LandMark,
                        PhoneNo = b.PhoneNo,
                        Pincode = b.Pincode,
                        State = b.State,
                        IsDefault = b.IsDefault,
                        OutsideNumber = b.OutsideNumber,
                        InteriorNumber = b.InteriorNumber,
                        Street = b.Street,
                        Street1 = b.Street1,
                        Street2 = b.Street2,
                        Colony = b.Colony,
                    }).FirstOrDefault();
                if (data != null)
                    return Ok(data);
                else
                    return Ok("User Not Found");
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }


        [HttpGet]
        [Route("setAsBillAddress")]
        public IActionResult setAsBillAddress(int id)
        {
            try
            {
                var model = db.Shipping.Where(v => v.Id == id).FirstOrDefault();
                var oldBillData = db.BillingAddress.Where(v => v.IsActive == true && v.ShippingId == id).FirstOrDefault();
                if(model != null && oldBillData==null)
                {
                    var billData = new BillingAddress();
                    billData.Name = model.Name;
                    billData.UserId = model.UserId;
                    billData.IpAddress = model.IpAddress;
                    billData.Street = model.Street;
                    billData.Street1 = model.Street1;
                    billData.Street2 = model.Street2;
                    billData.OutsideNumber = model.OutsideNumber;
                    billData.InteriorNumber = model.InteriorNumber;
                    billData.City = model.City;
                    billData.PhoneNo = model.PhoneNo;
                    billData.LandMark = model.LandMark;
                    billData.Pincode = model.Pincode;
                    billData.State = model.StateName;
                    billData.Colony = model.Colony;
                    billData.ShippingId = id;
                    billData.IsActive = true;
                    db.BillingAddress.Add(billData);
                    db.SaveChanges();
                    return Ok(billData);
                }
                else
                    return Ok(oldBillData);
            }
            catch (Exception ex)
            {

                return Ok(ex);
            }

        }
        public class ShippingModel5
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public string IpAddress { get; set; }
            public string Address { get; set; }

            public string Name { get; set; }
            public string PhoneNo { get; set; }
            public string AlternatePhoneNo { get; set; }
            public string Pincode { get; set; }
            public string City { get; set; }
            public string LandMark { get; set; }
            public int CountryId { get; set; }
            public string AddressType { get; set; }
            public bool IsActive { get; set; }
            public bool IsDefault { get; set; }
            public virtual Country Country { get; set; }
            //public virtual State State { get; set; }

            public string State { get; set; }
            public string Street { get; set; }
            public string Street1 { get; set; }
            public string Street2 { get; set; }
            public string OutsideNumber { get; set; }
            public string InteriorNumber { get; set; }
            public string Colony { get; set; }

            //forcheckout page
            public string CountryName { get; set; }
            public string StateName { get; set; }
            public string Email { get; set; }
        }
    }
}