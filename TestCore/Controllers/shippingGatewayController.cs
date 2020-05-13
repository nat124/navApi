using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Localdb;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;
using TestCore.DHLHepler;
using TestCore.Helper;
using static TestCore.DHLHepler.AllTrackResponse;
using static TestCore.DHLHepler.DHLPickupResponse;
using static TestCore.DHLHepler.QuoteResponse;
using TestCore.Extension_Method;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using TestCore.FedExHelper;
//using static TestCore.DHLHepler.responseCapability;

namespace TestCore.Controllers
{
    [Route("api/shippingGateway")]
    [EnableCors("EnableCORS")]
    [ApiController]
    public class shippingGatewayController : ControllerBase
    {
        private readonly PistisContext db;
        private IOptions<AppSettings> _settings;
        public shippingGatewayController(PistisContext pistis, IOptions<AppSettings> settings)
        {
            db = pistis;
            _settings = settings;
        }

        //[HttpGet]
        //[Route("GetQuotesCapabilitiesString")]
        //public IActionResult GetQuotesCapabilitiesString()
        //{
        //    capabilityRequestModel requestModel = new capabilityRequestModel();
        //    requestModel.CountryCode = "MX";
        //    requestModel.Postalcode = 15810;
        //    requestModel.productId = 33;
        //    requestModel.quantity = 2;
        //    requestModel.variantDetailId = 84;
        //    var DHLResponse = "";
        //    try
        //    {
        //        DHLResponse = returnString(requestModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex);
        //    }
        //    return Ok(DHLResponse);
        //}

        //private string returnString(capabilityRequestModel requestModel)
        //{
        //    try
        //    {
        //        if (requestModel != null)
        //        {
        //            _settings.Value.DHLId = "DHLMexico2";
        //            _settings.Value.DHLPassword = "gW8xKmuj6a3Ps";
        //            var requestedData = commonHelperDHL.fillXmlForQuote(requestModel, _settings.Value.DHLId, _settings.Value.DHLPassword, db);
        //            HttpWebRequest request = CreateSOAPWebRequest();
        //            var reqData = requestedData.Split('\n');
        //            //reqData[0] = "<p:DCTRequest xmlns:p=\"http://www.dhl.com\" xmlns:p1=\"http://www.dhl.com/datatypes\" xmlns:p2=\"http://www.dhl.com/DCTRequestdatatypes\" schemaVersion=\"2.0\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.dhl.com DCT-req.xsd \">\r\n";
        //            //reqData[reqData.Length - 1] = "\r</p:DCTRequest>";
        //            //requestedData = string.Join("", reqData);
        //            return reqData[0];
        //        }
        //        else
        //        {
        //            return "Requested model is null";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.StackTrace.ToString();
        //    }
        //}


        [HttpGet]
        [Route("GetQuotesCapabilities")]
        public IActionResult GetQuotesCapabilities(string countryCode, string postalCode, string productId, string variantId, string quantity, string city)
        {
            city = null;
            var productIds = productId.Split(',');
            var variantIds = (variantId.Split(','));
            var quantities = (quantity.Split(','));

            var productsData = db.ProductVariantDetails.Where(b => b.IsActive == true).Include(b => b.Product).ToList();
            var vendorsData = db.Users.ToList();

            var allPostalCodes = db.PostalCodeMap.Where(v => v.IsActive == true).ToList();

            var DHLResponseList = new List<ShippingResponse>();
            var FedexResponseList = new List<ShippingResponse>();
            var variantsData = new List<ProductVariantDetail>();
            var vendors = new List<User>();

            for (int i = 0; i < quantities.Count(); i++)
            {
                capabilityRequestModel requestModel = new capabilityRequestModel();
                requestModel.CountryCode = countryCode;
                requestModel.Postalcode = postalCode;
                requestModel.productId = Convert.ToInt32(productIds[i]);
                requestModel.quantity = Convert.ToInt32(quantities[i]);
                var vId = Convert.ToInt32(variantIds[i]);
                var productData = productsData.Where(b => b.IsActive == true && b.Id == vId).FirstOrDefault();
                var vendorData = vendorsData.Where(v => v.Id == productData.Product.VendorId).FirstOrDefault();
                if (productData == null)
                    return null;

                requestModel.variantDetailId = vId;
                ShippingResponse DHLResponse1 = new ShippingResponse();
                try
                {
                    DHLResponse1 = checkDHLPrice(requestModel, productData, vendorData);
                    var responseFedex = checkFEDEXPrice(requestModel, productData, vendorData);

                    productData = DealHelper.calculateDealForProductDetailModel(productData, db);
                    productData = PriceIncrementHelper.calculatePriceForProductDetailModel(productData, db);

                    variantsData.Add(productData);
                    vendors.Add(vendorData);
                    DHLResponseList.Add(DHLResponse1);
                    FedexResponseList.Add(responseFedex);
                }
                catch (Exception ex)
                {
                    return Ok(ex);
                }
            }

            if (city == null)
            {
                city = allPostalCodes.Where(v => v.PostalCode == postalCode.ToString()).FirstOrDefault()?.State;
                if (city == null)
                    city = "";
            }

            var samecityRespoonse = new List<ShippingResponse>();
            var diffcityRespoonse = new List<ShippingResponse>();

            var defaultCity = new string[3] { "Ciudad de México".Trim().ToLower(), "cdmx".Trim().ToLower(), "Ciudad de MÃ©xico".Trim().ToLower() };

            //---getting from FEDEX response
            for (int i = 0; i < quantities.Count(); i++)
            {
                //---------------for same cities
                if (FedexResponseList[i].ErrorData != null && FedexResponseList[i].ErrorData.Note == "Failure")
                {
                    return Ok(FedexResponseList[i].ErrorData);
                }
                if (
                    (defaultCity.Contains(vendors[i].City.Trim().ToLower()))
                    && (defaultCity.Contains(city.Trim().ToLower())))
                {
                    //----if cost is greater or equal then 600
                    if (variantsData[i].PriceAfterdiscount * Convert.ToInt32(quantities[i]) >= 600)
                    {
                        if (FedexResponseList[i].SuccessData != null && FedexResponseList[i].SuccessData.Count > 0)
                        {
                            var data = FedexResponseList[i].SuccessData.Where(v => v.DeliveryType.
                             Replace(" ", string.Empty).ToUpper() == FedexDeliveryType.STANDARD_OVERNIGHT.ToString().ToUpper() ||
                             v.DeliveryType.
                             Replace(" ", string.Empty).ToUpper() == FedexDeliveryType.PRIORITY_OVERNIGHT.ToString().ToUpper()
                             ).FirstOrDefault();
                            var response = new ShippingResponse();
                            data.ShipPrice = 0.0;
                            data.DeliveryDate = Convert.ToDateTime(data.DeliveryDate).ToString();
                            response.SuccessData.Add(data);
                            samecityRespoonse.Add(response);
                        }
                    }

                    //----if cost is less  then 600



                    if (variantsData[i].PriceAfterdiscount * Convert.ToInt32(quantities[i]) < 600)
                    {
                        if (FedexResponseList[i].SuccessData != null && FedexResponseList[i].SuccessData.Count > 0)
                        {
                            var data = FedexResponseList[i].SuccessData.Where(v => v.DeliveryType.
                             Replace(" ", string.Empty).ToUpper() == FedexDeliveryType.STANDARD_OVERNIGHT.ToString().ToUpper() ||
                             v.DeliveryType.
                             Replace(" ", string.Empty).ToUpper() == FedexDeliveryType.PRIORITY_OVERNIGHT.ToString().ToUpper()
                             ).ToList().OrderBy(c => c.ShipPrice).ToList();
                            var response = new ShippingResponse();
                            foreach (var d in data)
                            {
                                d.ShipPrice = 55.0;
                                d.DeliveryDate = Convert.ToDateTime(d.DeliveryDate).ToString();
                                response.SuccessData.Add(d);
                            }
                            samecityRespoonse.Add(response);
                        }
                    }
                }

                //---------------for different cities
                else
                {
                    //----if cost is less then 600
                    if (variantsData[i].PriceAfterdiscount * Convert.ToInt32(quantities[i]) < 600)
                    {
                        if (FedexResponseList[i].SuccessData != null)
                        {
                            var responseList = new List<ShippingResponse>();
                            var data1 = FedexResponseList[i].SuccessData.Where(v => v.DeliveryType.
                             Replace(" ", string.Empty).ToUpper() == FedexDeliveryType.FEDEX_EXPRESS_SAVER.ToString().ToUpper()).ToList();
                            var response = new ShippingResponse();
                            response.SuccessData.Add(data1.OrderBy(v => v.ShipPrice).FirstOrDefault());
                            responseList.Add(response);
                            diffcityRespoonse.AddRange(responseList);
                        }
                    }

                    //----if cost is greater or equal then 600
                    if (variantsData[i].PriceAfterdiscount * Convert.ToInt32(quantities[i]) >= 600)
                    {
                        if (FedexResponseList[i].SuccessData != null)
                        {
                            var res = FedexResponseList[i].SuccessData.Where(v => v.DeliveryType.
                             Replace(" ", string.Empty).ToUpper() == FedexDeliveryType.FEDEX_EXPRESS_SAVER.ToString().ToUpper()).OrderBy(b => b.ShipPrice).ToList();

                            var responseList = new List<ShippingResponse>();

                            foreach (var data in res)
                            {
                                var response = new ShippingResponse();
                                //----- calculation over shipping
                                if (!(data.ShipPrice < 200))
                                    data.ShipPrice = data.ShipPrice / 2;
                                else
                                    data.ShipPrice = data.ShipPrice;
                                //------for multiple products items
                                if (Convert.ToInt32(quantities[i]) > 1)
                                    data.ShipPrice = data.ShipPrice * Convert.ToInt32(quantities[i]);

                                response.SuccessData.Add(data);
                                responseList.Add(response);
                            }
                            diffcityRespoonse.AddRange(responseList);

                        }
                    }
                }
            }

            var finaData = new List<ShippingPrice>();

            //---getting from DHL response----------------------------------------------------------------------------------
            for (int i = 0; i < quantities.Count(); i++)
            {
                //---------------for same cities
                if (DHLResponseList[i].ErrorData != null && DHLResponseList[i].ErrorData.Note == "Failure")
                {
                    return Ok(DHLResponseList[i].ErrorData);
                }
                if (
                    (defaultCity.Contains(vendors[i].City.Trim().ToLower()))
                    && (defaultCity.Contains(city.Trim().ToLower())))
                {
                    //----if cost is greater or equal then 600
                    if (variantsData[i].PriceAfterdiscount * Convert.ToInt32(quantities[i]) >= 600)
                    {
                        if (DHLResponseList[i].SuccessData != null && DHLResponseList[i].SuccessData.Count > 0)
                        {
                            var data = DHLResponseList[i].SuccessData.Where(v => v.DeliveryType.
                             Replace(" ", string.Empty).ToUpper() == DHLPriceType.SameDaySprintline.ToString().ToUpper()
                             ).FirstOrDefault();
                            var response = new ShippingResponse();
                            data.ShipPrice = 0.0;
                            data.DeliveryDate = Convert.ToDateTime(data.DeliveryDate).ToString();
                            response.SuccessData.Add(data);
                            samecityRespoonse.Add(response);
                        }
                    }

                    //----if cost is less  then 600
                    if (variantsData[i].PriceAfterdiscount * Convert.ToInt32(quantities[i]) < 600)
                    {
                        if (DHLResponseList[i].SuccessData != null && DHLResponseList[i].SuccessData.Count > 0)
                        {
                            var data = DHLResponseList[i].SuccessData.Where(v => v.DeliveryType.
                             Replace(" ", string.Empty).ToUpper() == DHLPriceType.SameDaySprintline.ToString().ToUpper()
                             ).FirstOrDefault();
                            var response = new ShippingResponse();
                            data.ShipPrice = 55.0;
                            data.DeliveryDate = Convert.ToDateTime(data.DeliveryDate).ToString();
                            response.SuccessData.Add(data);
                            samecityRespoonse.Add(response);
                        }
                    }
                }

                //---------------for different cities
                else
                {
                    //----if cost is less then 600
                    if (variantsData[i].PriceAfterdiscount * Convert.ToInt32(quantities[i]) < 600)
                    {
                        if (DHLResponseList[i].SuccessData != null)
                        {
                            var responseList = new List<ShippingResponse>();
                            var data1 = DHLResponseList[i].SuccessData.Where(v => v.DeliveryType.
                             Replace(" ", string.Empty).ToUpper() != DHLPriceType.SameDaySprintline.ToString().ToUpper()).ToList();
                            var response = new ShippingResponse();
                            response.SuccessData.Add(data1.OrderBy(v => v.ShipPrice).FirstOrDefault());
                            responseList.Add(response);
                            diffcityRespoonse.AddRange(responseList);
                        }
                    }

                    //----if cost is greater or equal then 600
                    if (variantsData[i].PriceAfterdiscount * Convert.ToInt32(quantities[i]) >= 600)
                    {
                        if (DHLResponseList[i].SuccessData != null)
                        {
                            var res = DHLResponseList[i].SuccessData.Where(v => v.DeliveryType.
                             Replace(" ", string.Empty).ToUpper() != DHLPriceType.SameDaySprintline.ToString().ToUpper()).OrderBy(b => b.ShipPrice).ToList();

                            var responseList = new List<ShippingResponse>();

                            foreach (var data in res)
                            {
                                var response = new ShippingResponse();
                                //----- calculation over shipping
                                if (!(data.ShipPrice < 200))
                                    data.ShipPrice = data.ShipPrice / 2;
                                else
                                    data.ShipPrice = data.ShipPrice;
                                //------for multiple products items
                                if (Convert.ToInt32(quantities[i]) > 1)
                                    data.ShipPrice = data.ShipPrice * Convert.ToInt32(quantities[i]);

                                response.SuccessData.Add(data);
                                responseList.Add(response);
                            }
                            diffcityRespoonse.AddRange(responseList);

                        }
                    }
                }
            }

            try
            {
                if (samecityRespoonse.Count > 0 && diffcityRespoonse.Count <= 0)
                {
                    foreach (var item in samecityRespoonse)
                    {
                        foreach (var ele in item.SuccessData)
                        {
                            if (ele.ResponseFrom == "fedex")
                            {
                                //ele.ShipPrice = samecityRespoonse.Sum(v => v.SuccessData.Select(n => n.ShipPrice).FirstOrDefault());
                                ele.DeliveryDate = Convert.ToDateTime(ele.DeliveryDate.ToString()).ToString("MM-dd-yyyy hh:mm");
                                if (!finaData.Any(c => c.DeliveryType == ele.DeliveryType && c.ResponseFrom == ele.ResponseFrom))
                                    finaData.Add(ele);
                            }
                            if (ele.ResponseFrom.ToLower().ToString() == "dhl")
                            {
                                //ele.ShipPrice = samecityRespoonse.Sum(v => v.SuccessData.Select(n => n.ShipPrice).FirstOrDefault());
                                ele.DeliveryDate = Convert.ToDateTime(ele.DeliveryDate.ToString()).ToString("MM-dd-yyyy hh:mm");
                                if (!finaData.Any(c => c.DeliveryType == ele.DeliveryType && c.ResponseFrom == ele.ResponseFrom))
                                    finaData.Add(ele);
                            }
                        }
                    }
                }

                if (diffcityRespoonse.Count > 0)
                {
                    foreach (var item in diffcityRespoonse)
                        finaData.AddRange(item.SuccessData);
                    var priceTotal = finaData.Max(v => v.ShipPrice);
                    var final = finaData.Where(v => v.DeliveryType.
                                  Replace(" ", string.Empty).ToUpper() != DHLPriceType.SameDaySprintline.ToString().ToUpper()).OrderByDescending(b => b.DeliveryDate)
                                 .ToList();
                    finaData = new List<ShippingPrice>();
                    int i = 0;
                    foreach (var item in final)
                    {
                        if (i == 0 && finaData.Count <= 0)
                            item.DeliveryDate = Convert.ToDateTime(item.DeliveryDate.ToString()).AddDays(4).ToString("MM-dd-yyyy hh:mm");
                        else if (i == 1 && item.ShipPrice > finaData[0].ShipPrice)
                            item.DeliveryDate = Convert.ToDateTime(item.DeliveryDate.ToString()).AddDays(3).ToString("MM-dd-yyyy hh:mm");
                        else
                            item.DeliveryDate = Convert.ToDateTime(item.DeliveryDate.ToString()).AddDays(5).ToString("MM-dd-yyyy hh:mm");
                        i++;
                        if (!finaData.Any(c => c.DeliveryType == item.DeliveryType && c.ResponseFrom == item.ResponseFrom))
                            finaData.Add(item);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (variantIds[0] == "1891" && variantIds.Length == 1 && city == "CDMX" &&
                postalCode == Convert.ToString(vendorsData.Where(v => v.PostalCode == "11290" && v.Id == 271).FirstOrDefault().PostalCode))
                foreach (var item in finaData)
                    item.ShipPrice = 0;

            finaData = finaData.OrderBy(c => c.DeliveryDate).Distinct().Take(3).ToList();

            return Ok(finaData);
        }


        private ShippingResponse checkFEDEXPrice(capabilityRequestModel requestModel, ProductVariantDetail productData, User vendorData)
        {
            ShippingResponse Data = new ShippingResponse();
            var data = new List<ShippingPrice>();

            _settings.Value.FedExUserId = "rNosH4RD2BLYvKEY";
            _settings.Value.FedExPassword = "noNEB6eAqLiekzU6zgWc7MfpV";
            _settings.Value.FedExAccountNumber = "900369573";
            _settings.Value.FedExMeterNumber = "250812962";

            fedexRateResponse.Envelope response;

            try
            {
                var ServiceResult = "";
                var soapRequest = commonHelperFedex.createFedexSoapRequest();
                var requestedData = commonHelperFedex.fillXmlForQuote(requestModel, _settings, db, productData, vendorData);

                XmlDocument SOAPReqBody = new XmlDocument();
                SOAPReqBody.LoadXml(requestedData);
                using (Stream stream = soapRequest.GetRequestStream())
                {
                    SOAPReqBody.Save(stream);
                }

                using (WebResponse Serviceres = soapRequest.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                    {
                        ServiceResult = rd.ReadToEnd();
                        var serializer = new XmlSerializer(typeof(fedexRateResponse.Envelope));
                        using (TextReader reader = new StringReader(ServiceResult))
                        {
                            response = (fedexRateResponse.Envelope)serializer.Deserialize(reader);
                        }
                    }
                }
                if (response != null && response.Body != null && response.Body.RateReply != null && response.Body.RateReply.RateReplyDetails != null && response.Body.RateReply.RateReplyDetails.Length > 0)
                {
                    foreach (var item in response.Body.RateReply.RateReplyDetails)
                    {
                        var ship = new ShippingPrice();
                        ship.ResponseFrom = "fedex";
                        ship.DeliveryType = item.CommitDetails.ServiceType;
                        ship.DeliveryDate = item.DeliveryTimestamp.ToString();
                        ship.ShipPrice = Convert.ToDouble(item.RatedShipmentDetails[0].ShipmentRateDetail.TotalNetChargeWithDutiesAndTaxes.Amount);
                        data.Add(ship);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //----------convert data to shipping model

            //if (result.GetQuoteResponse.BkgDetails != null && result.GetQuoteResponse.BkgDetails.Length > 0)
            //{

            //    var data = result.GetQuoteResponse.BkgDetails.Where(v =>
            //    v.ProductShortName.Replace(" ", string.Empty).ToUpper() == DHLPriceType.ExpressDomestic.ToString().ToUpper() ||
            //    v.ProductShortName.Replace(" ", string.Empty).ToUpper() == DHLPriceType.SameDaySprintline.ToString().ToUpper() ||
            //    v.ProductShortName.Replace(" ", string.Empty).ToUpper() == DHLPriceType.EconomySelectDomestic.ToString().ToUpper()
            //    ).Select(c => new ShippingPrice
            //    {
            //        DeliveryDate = c.DeliveryDate.DlvyDateTime,
            //        DeliveryType = c.ProductShortName,
            //        ShipPrice = (double)c.ShippingCharge,
            //        ResponseFrom = "DHL"
            //    }).ToList();
            //    Data.SuccessData.AddRange(data);
            //}
            //else
            //{
            //    ErrorResponse err = new ErrorResponse();
            //    err.Note = result.GetQuoteResponse.Note.ActionStatus;
            //    err.Message = result.GetQuoteResponse.Note.Condition.ConditionData;
            //    Data.ErrorData = err;
            //}

            Data.SuccessData.AddRange(data);
            return Data;
        }




        //[HttpGet]
        //[Route("GetQuotesCapabilities")]
        //public IActionResult GetQuotesCapabilities(string countryCode, int postalCode, int productId, int variantId, int quantity)
        //{
        //    var productData = db.ProductVariantDetails.Where(b => b.IsActive == true && b.Id == variantId).Include(b => b.Product).FirstOrDefault();
        //    if (productData == null)
        //        return null;

        //    capabilityRequestModel requestModel = new capabilityRequestModel();
        //    requestModel.CountryCode = countryCode;
        //    requestModel.Postalcode = postalCode;
        //    requestModel.productId = productId;
        //    requestModel.quantity = quantity;
        //    requestModel.variantDetailId = variantId;
        //    var DHLResponse = new ShippingResponse();
        //    try
        //    {
        //        DHLResponse = checkDHLPrice(requestModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex);
        //    }
        //    var FEDEXResponse = checkFEDEXPrice(requestModel);

        //    //-----------add all responses
        //    var finalData = new object();
        //    var List = new List<ShippingPrice>();

        //    if ((DHLResponse.SuccessData.Count > 0) && (FEDEXResponse.SuccessData.Count > 0))
        //    {
        //        DHLResponse.SuccessData.AddRange(FEDEXResponse.SuccessData);
        //        List = DHLResponse.SuccessData;
        //    }
        //    if ((DHLResponse.SuccessData.Count <= 0 && DHLResponse.ErrorData != null) && (FEDEXResponse.ErrorData != null && FEDEXResponse.SuccessData.Count > 0))
        //    {
        //        List = FEDEXResponse.SuccessData;
        //    }
        //    if ((DHLResponse.SuccessData.Count > 0 && DHLResponse.ErrorData == null) && (FEDEXResponse.SuccessData.Count <= 0 && FEDEXResponse.ErrorData == null))
        //    {
        //        List = DHLResponse.SuccessData;
        //    }
        //    if (List.Count > 0)
        //        finalData = calculateFinalREsults(List, productData.PriceAfterdiscount * quantity);
        //    else
        //    {
        //        if (DHLResponse.ErrorData != null)
        //            finalData = DHLResponse.ErrorData;
        //    }
        //    return Ok(finalData);
        //}


        private object calculateFinalREsults(List<ShippingPrice> allResponse, decimal productPrice)
        {
            var data = new List<ShippingPrice>();
            var config = db.ShippingConfig.Where(v => v.IsActive == true).FirstOrDefault();
            foreach (var item in allResponse)
            {
                if (productPrice > config.MaxOrderPriceForFreeShip)
                    item.ShipPrice = 0;
                if (productPrice < config.MaxOrderPriceForFreeShip && ((item.ShipPrice / config.MyShareOnShipCharge) >= ((double)config.MaxShipPrice)))
                    item.ShipPrice = ((double)config.MaxShipPrice);
                if (productPrice < config.MaxOrderPriceForFreeShip && ((item.ShipPrice / config.MyShareOnShipCharge) < ((double)config.MaxShipPrice)))
                    item.ShipPrice = (item.ShipPrice / config.MyShareOnShipCharge);
                data.Add(item);
            }
            return data.OrderBy(b => b.ShipPrice);
        }

        private ShippingResponse checkFEDEXPrice(capabilityRequestModel requestModel)
        {
            ShippingResponse Data = new ShippingResponse();

            var data = new List<ShippingPrice>();
            //data.Add(new ShippingPrice
            //{
            // DeliveryDate = System.DateTime.Now.AddDays(1).ToString(),
            // DeliveryType = "SAMEDAY SPRINTLINE",
            // ShipPrice = 150,
            // ResponseFrom = "FEDEX"
            //});
            //data.Add(new ShippingPrice
            //{
            // DeliveryDate = System.DateTime.Now.AddDays(1).ToString(),
            // DeliveryType = "EXPRESS DOMESTIC",
            // ShipPrice = 200,
            // ResponseFrom = "FEDEX"
            //});
            //data.Add(new ShippingPrice
            //{
            // DeliveryDate = System.DateTime.Now.AddHours(3).ToString(),
            // DeliveryType = "ECONOMY SELECT DOMESTIC",
            // ShipPrice = 160,
            // ResponseFrom = "FEDEX"
            //});
            Data.SuccessData.AddRange(data);
            return Data;
        }


        private ShippingResponse checkDHLPrice(capabilityRequestModel requestModel, ProductVariantDetail productData, User vendorData)
        {
            ShippingResponse Data = new ShippingResponse();
            DCTResponse result;
            //requestModel.CountryCode = "MX";
            //requestModel.Postalcode = 15810;
            _settings.Value.DHLId = "DHLMexico2";
            _settings.Value.DHLPassword = "gW8xKmuj6a3Ps";
            var ServiceResult = "";
            try
            {
                var requestedData = commonHelperDHL.fillXmlForQuote(requestModel, _settings.Value.DHLId, _settings.Value.DHLPassword, db, productData, vendorData);
                HttpWebRequest request = CreateSOAPWebRequest();
                //var fileName = XElement.Load(@"F:\Karan\26-9-19\PISTISAPI_core\TestCore\XML\Valid11_QuoteRequest_WithValidAccount.xml").ToString();

                var reqData = requestedData.Split('\n');
                //var fileData = fileName.Split('\r');
                reqData[0] = "<p:DCTRequest xmlns:p=\"http://www.dhl.com\" xmlns:p1=\"http://www.dhl.com/datatypes\" xmlns:p2=\"http://www.dhl.com/DCTRequestdatatypes\" schemaVersion=\"2.0\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.dhl.com DCT-req.xsd \">\r\n";
                reqData[reqData.Length - 1] = "</p:DCTRequest>";
                requestedData = string.Join("", reqData);

                XmlDocument SOAPReqBody = new XmlDocument();
                SOAPReqBody.LoadXml(requestedData);
                using (Stream stream = request.GetRequestStream())
                {
                    SOAPReqBody.Save(stream);
                }
                using (WebResponse Serviceres = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                    {
                        ServiceResult = rd.ReadToEnd();
                        var serializer = new XmlSerializer(typeof(DCTResponse));
                        using (TextReader reader = new StringReader(ServiceResult))
                        {
                            result = (DCTResponse)serializer.Deserialize(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //----------convert data to shipping model

            if (result.GetQuoteResponse.BkgDetails != null && result.GetQuoteResponse.BkgDetails.Length > 0)
            {

                var data = result.GetQuoteResponse.BkgDetails.Where(v =>
                v.ProductShortName.Replace(" ", string.Empty).ToUpper() == DHLPriceType.ExpressDomestic.ToString().ToUpper() ||
                v.ProductShortName.Replace(" ", string.Empty).ToUpper() == DHLPriceType.SameDaySprintline.ToString().ToUpper() ||
                v.ProductShortName.Replace(" ", string.Empty).ToUpper() == DHLPriceType.EconomySelectDomestic.ToString().ToUpper()
                ).Select(c => new ShippingPrice
                {
                    DeliveryDate = c.DeliveryDate.DlvyDateTime,
                    DeliveryType = c.ProductShortName,
                    ShipPrice = (double)c.ShippingCharge,
                    ResponseFrom = "DHL"
                }).ToList();
                Data.SuccessData.AddRange(data);
            }
            else
            {
                ErrorResponse err = new ErrorResponse();
                err.Note = result.GetQuoteResponse.Note.ActionStatus;
                err.Message = result.GetQuoteResponse.Note.Condition.ConditionData;
                Data.ErrorData = err;
            }
            return Data;
        }

        [HttpGet]
        [Route("BookPickupRequest")]
        public IActionResult BookPickupRequest()
        {
            PickupResponse result;

            capabilityRequestModel requestModel = new capabilityRequestModel();
            requestModel.CountryCode = "MX";
            requestModel.Postalcode = "15700";
            var ServiceResult = "";
            try
            {
                var requestedData = commonHelperDHL.fillXmlForPickupRequest(requestModel, _settings.Value.DHLId, _settings.Value.DHLPassword, db);
                HttpWebRequest request = CreateSOAPWebRequest();
                var fileName = XElement.Load(@"F:\Karan\26-9-19\PISTISAPI_core\TestCore\XML\BookPickup_Valid1_Cliente.xml").ToString();

                var reqData = requestedData.Split('\r');
                var fileData = fileName.Split('\r');
                reqData[0] = fileData[0];
                reqData[reqData.Length - 1] = fileData[reqData.Length - 1];
                requestedData = string.Join("", reqData);

                XmlDocument SOAPReqBody = new XmlDocument();
                SOAPReqBody.LoadXml(requestedData);
                using (Stream stream = request.GetRequestStream())
                {
                    SOAPReqBody.Save(stream);
                }
                using (WebResponse Serviceres = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                    {
                        ServiceResult = rd.ReadToEnd();
                        var serializer = new XmlSerializer(typeof(PickupResponse));
                        using (TextReader reader = new StringReader(ServiceResult))
                        {
                            result = (PickupResponse)serializer.Deserialize(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
            return Ok(result);
        }


        [HttpGet]
        [Route("ShipValidate")]
        public IActionResult ShipValidate()
        {
            ShipmentValidateResponse result;

            capabilityRequestModel requestModel = new capabilityRequestModel();
            requestModel.CountryCode = "MX";
            requestModel.Postalcode = "15700";
            var ServiceResult = "";
            try
            {
                var requestedData = commonHelperDHL.fillXmlForShipValidate(requestModel, _settings.Value.DHLId, _settings.Value.DHLPassword, db);
                HttpWebRequest request = CreateSOAPWebRequest();
                var fileName = XElement.Load(@"F:\Karan\26-9-19\PISTISAPI_core\TestCore\XML\ShipmentValidateRequest_MX_to_MX_PieceEnabled_With1Pcs_PcsSeg_Termico.xml").ToString();

                var reqData = requestedData.Split('\r');
                var fileData = fileName.Split('\r');
                reqData[0] = fileData[0];
                reqData[reqData.Length - 1] = fileData[reqData.Length - 1];
                requestedData = string.Join("", reqData);

                XmlDocument SOAPReqBody = new XmlDocument();
                SOAPReqBody.LoadXml(requestedData);
                using (Stream stream = request.GetRequestStream())
                {
                    SOAPReqBody.Save(stream);
                }
                using (WebResponse Serviceres = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                    {
                        ServiceResult = rd.ReadToEnd();
                        var serializer = new XmlSerializer(typeof(ShipmentValidateResponse));
                        using (TextReader reader = new StringReader(ServiceResult))
                        {
                            result = (ShipmentValidateResponse)serializer.Deserialize(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
            return Ok(result);
        }

        public TrackingResponse ShipTrack(string trackNumber)
        {
            TrackingResponse result;
            var requestModel = new trackingRequestModel();
            requestModel.CountryCode = "MX";
            requestModel.Postalcode = 15700;
            requestModel.trackingNumber = trackNumber;
            var ServiceResult = "";
            try
            {
                var requestedData = commonHelperDHL.fillXmlForShipTracking(requestModel, _settings.Value.DHLId, _settings.Value.DHLPassword, db, "all");
                HttpWebRequest request = CreateSOAPWebRequest();
                var fileName = XElement.Load(@"F:\Karan\14-10-19\PISTISAPI_core\TestCore\XML\TrackingRequest_SingleAWB_10D.xml");
                //string xmlString = System.IO.File.ReadAllText(fileName);

                XmlDocument SOAPReqBody = new XmlDocument();
                SOAPReqBody.LoadXml(requestedData.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    SOAPReqBody.Save(stream);
                }
                using (WebResponse Serviceres = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                    {
                        ServiceResult = rd.ReadToEnd();
                        var serializer = new XmlSerializer(typeof(TrackingResponse));
                        using (TextReader reader = new StringReader(ServiceResult))
                        {
                            result = (TrackingResponse)serializer.Deserialize(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        public TrackingResponse ShipTrackLastCkheckPoint(string trackNumber)
        {
            TrackingResponse result;
            var requestModel = new trackingRequestModel();
            requestModel.CountryCode = "MX";
            requestModel.Postalcode = 15700;
            requestModel.trackingNumber = trackNumber;
            var ServiceResult = "";
            try
            {
                var requestedData = commonHelperDHL.fillXmlForShipTracking(requestModel, _settings.Value.DHLId, _settings.Value.DHLPassword, db, "last");
                HttpWebRequest request = CreateSOAPWebRequest();
                var fileName = XElement.Load(@"F:\Karan\14-10-19\PISTISAPI_core\TestCore\XML\TrackingRequest_SingleAWB_10DLASTCHECKPOINT.xml").ToString();
                //string xmlString = System.IO.File.ReadAllText(fileName);

                var reqData = requestedData.Split('\n');
                //var fileData = fileName.Split('\r');
                reqData[0] = "<req:KnownTrackingRequest xmlns:req='http://www.dhl.com' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='http://www.dhl.com TrackingRequestKnown.xsd'>\r\n";
                reqData[reqData.Length - 1] = "</req:KnownTrackingRequest>";
                requestedData = string.Join("", reqData);

                XmlDocument SOAPReqBody = new XmlDocument();
                SOAPReqBody.LoadXml(requestedData);
                using (Stream stream = request.GetRequestStream())
                {
                    SOAPReqBody.Save(stream);
                }
                using (WebResponse Serviceres = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                    {
                        ServiceResult = rd.ReadToEnd();
                        var serializer = new XmlSerializer(typeof(TrackingResponse));
                        using (TextReader reader = new StringReader(ServiceResult))
                        {
                            result = (TrackingResponse)serializer.Deserialize(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        public HttpWebRequest CreateSOAPWebRequest()
        {
            //Making Web Request 
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"http://xmlpitest-ea.dhl.com/XMLShippingServlet");
            //SOAPAction 
            //Req.Headers.Add(@"SOAPAction:http://tempuri.org/Addition");
            //Content_type 
            Req.ContentType = "text/xml;charset=\"utf-8\"";
            Req.Accept = "text/xml";
            //HTTP method 
            Req.Method = "POST";
            //return HttpWebRequest 
            return Req;
        }


        //---------------------------------------TRACKING START---------------------------------------------------------

        [HttpGet]
        [Route("TrackOrder")]
        public IActionResult TrackOrder(int orderId, int UserId)
        {
            try
            {
                var data = db.TrackOrders.Where(b => b.OrderId == orderId && b.IsActive == true).Select(n => new TrackOrderModel
                {
                    Id = n.Id,
                    IsActive = n.IsActive,
                    OrderId = n.OrderId,
                    Status = n.Status,
                    TrackId = n.TrackId,
                    UpdatedDate = n.UpdatedDate,
                    OrderDetail = db.Checkouts.Where(v => v.Id == orderId && v.IsActive == true).Include(z => z.User).Include(x => x.CheckoutItems).FirstOrDefault(),
                }).FirstOrDefault();

                if (data != null)
                {
                    //if (data != null && data.TrackId != null)
                    //{
                    // var response = ShipTrack(data.TrackId);
                    // if (response != null && response.AWBInfo != null && response.AWBInfo.Status != null)
                    // data.Status = response.AWBInfo.Status.ActionStatus;
                    //}

                    data.OrderDetail.CheckoutItems = db.CheckoutItems.Where(c => c.CheckoutId == data.OrderId && c.IsActive == true).ToList();
                    data.OrderDetail.User = db.Users.Where(v => v.Id == UserId).FirstOrDefault();

                    foreach (var c in data.OrderDetail.CheckoutItems.Where(m => m.IsActive == true))
                    {
                        c.ProductVariantDetail = new ProductVariantDetail();
                        c.ProductVariantDetail = db.ProductVariantDetails
                        .Where(x => x.IsActive == true && x.Id == c.ProductVariantDetailId)
                        .Include(x => x.Product)
                        .Include(x => x.ProductVariantOptions)
                        .Include(x => x.ProductImages).FirstOrDefault();
                    }

                    var checkout = data.OrderDetail;

                    var checkoutitems = new List<CheckOut>();

                    var vendors = db.Users.Where(b => b.IsActive == true).ToList();
                    var variantOptions = db.VariantOptions.Where(v => v.IsActive == true).Include(x => x.Variant).ToList();
                    var orderStatuses = db.OrderStatus.Where(b => b.IsActive == true).ToList();

                    foreach (var c in data.OrderDetail.CheckoutItems.Where(x => x.IsActive == true))
                    {
                        var model = new CheckOut();
                        if (model.SellerName == null && c.ProductVariantDetail.Product.VendorId > 0)
                        {
                            var vendor = vendors.Where(v => v.Id == c.ProductVariantDetail.Product.VendorId && v.RoleId == (int)RoleType.Vendor).FirstOrDefault();
                            if (vendor != null)
                                model.SellerName = vendor.FirstName + " " + vendor.LastName;
                        }
                        model.AdditionalCost = checkout.AdditionalCost;
                        model.Id = checkout.Id;
                        model.OrderStatus = orderStatuses.Where(x => x.Id == c.OrderStatusId).Select(x => x.Name).FirstOrDefault();
                        if (c.ProductVariantDetail.ProductImages != null)
                        {
                            model.Image = c.ProductVariantDetail.ProductImages
                            .Where(x => x.IsDefault == true && x.IsActive == true)
                            .FirstOrDefault() == null ? c.ProductVariantDetail.ProductImages
                            .Where(x => x.IsActive == true).FirstOrDefault()
                            .ImagePath : c.ProductVariantDetail.ProductImages
                            .Where(x => x.IsDefault == true && x.IsActive == true)
                            .FirstOrDefault().ImagePath;
                        }
                        model.IpAddress = checkout.IpAddress;
                        model.ProductId = c.ProductVariantDetail.ProductId;
                        // model.IsConvertToCheckout = checkout.IsConvertToCheckout;
                        model.Name = c.ProductVariantDetail.Product.Name;
                        model.OrderDate = checkout.CheckoutDate;
                        model.OrderNumber = checkout.InvoiceNumber;
                        model.TotalAmount = checkout.TotalAmount;
                        model.UserId = UserId;
                        model.ProductVariantDetailId = c.ProductVariantDetailId;
                        foreach (var v in c.ProductVariantDetail.ProductVariantOptions.Where(x => x.IsActive == true))
                        {
                            var variantid = v.VariantOptionId;
                            var variantop = new VariantOption();
                            variantop = variantOptions.Where(x => x.IsActive == true && x.Id == variantid
                            && x.Variant.Name.ToLower() != "default").FirstOrDefault();
                            if (variantop != null)
                            {
                                var variantmodel = new VariantOptionModel();
                                variantmodel.Id = variantop.Id;
                                variantmodel.Name = variantop.Name;
                                variantmodel.VariantId = variantop.VariantId;
                                variantmodel.varientName = variantop.Variant.Name;
                                model.VariantOptions.Add(variantmodel);
                            }
                        }
                        model.SellingPrice = c.UnitPrice * c.Quantity;
                        model.Discount = Convert.ToInt32(c.Discount + c.DealDiscount);

                        //      decimal PriceAfterDiscount = c.UnitPrice - (c.Amount * model.Discount / 100);
                        //      model.PriceAfterDiscount = PriceAfterDiscount / c.Quantity;
                        model.PriceAfterDiscount = (c.Amount/c.Quantity);
                        model.Quantity = c.Quantity;
                        model.Amount = c.Amount ;
                        //     model.TotalAmount = checkout.TotalAmount;
                        model.checkoutItemId = c.Id;
                        checkoutitems.Add(model);
                    }
                    var items = checkoutitems.GroupBy(x => x.OrderNumber).ToList();

                    data.OrderDetail.CheckoutItems = new List<CheckoutItem>();
                    data.checkoutItems = checkoutitems;
                    data.OrderDetail.TotalAmount = checkout.TotalAmount + checkout.ShippingPrice;
                    data.DeliveryAddress = db.Shipping.Where(b => b.UserId == UserId && b.IsDefault == true &&
                    b.IsActive == true).FirstOrDefault();
                    return Ok(data);
                }
                else
                    return Ok("404");
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpGet]
        [Route("getByTrackNo")]
        public IActionResult getByTrackNo(string no)
        {
            var orderid = db.TrackOrders.Where(x => x.TrackId == no && x.IsActive == true).FirstOrDefault()?.OrderId;
            return Ok(orderid);
        }

        [HttpGet]
        [Route("getAllOrders")]
        public IActionResult getAllOrders(int page, int pageSize, string search)
        {
            var skipData = pageSize * (page - 1);
            try
            {
                var data = db.TrackOrders.Where(v => v.IsActive == true).Select(n => new TrackOrderModel
                {
                    Id = n.Id,
                    IsActive = n.IsActive,
                    OrderId = n.OrderId,
                    Status = n.Status,
                    TrackId = n.TrackId,
                    UpdatedDate = n.UpdatedDate,
                    OrderDetail = db.Checkouts.Where(v => v.Id == n.Id && v.IsActive == true).Include(z => z.User).Include(x => x.CheckoutItems).FirstOrDefault(),
                }).ToList();

                if (search != null)
                {
                    data = data.Where(c => c.OrderId.ToString().Contains(search)
                    || c.Status.ToLower().Contains(search.ToLower())
                    ).ToList();
                }

                var response = new
                {
                    data = data.Skip(skipData).Take(pageSize).OrderByDescending(c => c.Id).ToList(),
                    count = data.Count
                };

                return Ok(response);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpGet]
        [Route("getById")]
        public IActionResult getById(int id)
        {
            try
            {
                var order = db.TrackOrders.Where(v => v.IsActive == true && v.Id == id).Select(n => new TrackOrderModel
                {
                    Id = n.Id,
                    IsActive = n.IsActive,
                    OrderId = n.OrderId,
                    Status = n.Status,
                    TrackId = n.TrackId,
                    UpdatedDate = n.UpdatedDate,
                    OrderDetail = db.Checkouts.Where(v => v.Id == n.OrderId && v.IsActive == true).Include(z => z.User).Include(x => x.CheckoutItems).FirstOrDefault(),
                }).FirstOrDefault();

                var user = db.Users.Where(v => v.IsActive == true && v.Id == order.OrderDetail.UserId)?.FirstOrDefault();
                var billingAddress = db.BillingAddress.Where(v => v.IsActive == true && v.Id == order.OrderDetail.BillingAddressId)?.FirstOrDefault();
                var shippingAddress = db.Shipping.Where(v => v.IsActive == true && v.Id == order.OrderDetail.ShippingId)?.FirstOrDefault();
                var paymentInfo = "";

                var data = new TackCompositeModel();
                data.TrackOrder = order;
                data.UserInfo = user;
                data.BillingInfo = billingAddress;
                data.ShippingInfo = shippingAddress;

                if (data != null)
                    return Ok(data);
                else
                    return Ok("EmptyOrders");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("trackById")]
        public IActionResult trackById(int id)
        {
            try
            {
                var data = db.TrackOrders.Where(v => v.IsActive == true && v.Id == id).Select(n => new TrackOrderModel
                {
                    Id = n.Id,
                    IsActive = n.IsActive,
                    OrderId = n.OrderId,
                    Status = n.Status,
                    TrackId = n.TrackId,
                    UpdatedDate = n.UpdatedDate,
                }).FirstOrDefault();
                if (data != null)
                    return Ok(data);
                else
                    return Ok("EmptyOrders");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("updateOrder")]
        public IActionResult updateOrder(TrackOrder model)
        {
            try
            {
                var data = db.TrackOrders.Where(x => x.IsActive == true && x.Id == model.Id).FirstOrDefault();
                if (data != null)
                {
                    data.TrackId = model.TrackId;
                    data.Status = "Packed";
                    data.UpdatedDate = System.DateTime.Now;
                    db.SaveChanges();
                    bool status = NotificationHelper.UpdateOrderStatus(data.Status, db, data.OrderId, Convert.ToInt32(db.Checkouts.Where(x => x.Id == data.OrderId).FirstOrDefault().UserId));
                    return Ok(true);
                }
                else
                    return Ok(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------------------------TRACKING END---------------------------------------------------------




        //----------mapping postal codes here temporary




        //[HttpGet]
        //[Route("mapPostalCode")]
        //public IActionResult Images()
        //{
        //    try
        //    {
        //        var allMappedData = db.PostalCodeMap.ToList();

        //        //Read the contents of CSV file.
        //        var csvData = System.IO.File.ReadAllLines(@"F:\Karan\5-11-19\PISTISAPI_core\TestCore\Backend\mx_postal_codes.csv");
        //        for (int i = 1; i < csvData.Length; i++)
        //        {
        //            var modelData = csvData[i].Split(',');
        //            if (modelData == null)
        //                continue;
        //            var model = new PostalCodeMap();
        //            model.PostalCode = Convert.ToString(modelData[0]);
        //            model.PlaceName = Convert.ToString(modelData[1]);
        //            model.State = Convert.ToString(modelData[2]);
        //            model.StateAbbreviation = Convert.ToString(modelData[3]);
        //            model.Latitude = Convert.ToString(modelData[4]);
        //            model.Longitude = Convert.ToString(modelData[5]);
        //            model.IsActive = true;
        //            db.PostalCodeMap.Add(model);
        //            db.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex);
        //    }

        //    return Ok("Mapped successfully!");
        //}

        //private static DataTable GetDataTableFromExcel(string path, bool hasHeader = true)
        //{
        //    using (var pck = new OfficeOpenXml.ExcelPackage())
        //    {
        //        using (var stream = System.IO.File.OpenRead(path))
        //        {
        //            pck.Load(stream);
        //        }
        //        var ws = pck.Workbook.Worksheets.First();
        //        DataTable tbl = new DataTable();
        //        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
        //        {
        //            tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
        //        }
        //        var startRow = hasHeader ? 2 : 1;
        //        for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
        //        {
        //            var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
        //            DataRow row = tbl.Rows.Add();
        //            foreach (var cell in wsRow)
        //            {
        //                row[cell.Start.Column - 1] = cell.Text;
        //            }
        //        }
        //        return tbl;
        //    }

        //}



    }
}