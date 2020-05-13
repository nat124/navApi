using Localdb;
using Microsoft.Extensions.Options;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TestCore.DHLHepler;
using static TestCore.FedExHelper.fedexRateRequest;

namespace TestCore.FedExHelper
{
    public static class commonHelperFedex
    {
        internal static string fillXmlForQuote(capabilityRequestModel requestModel, IOptions<AppSettings> _settings, PistisContext db, ProductVariantDetail productData, User vendorData)
        {
            var rateRequest = CreateRateRequest(requestModel, _settings, db, productData, vendorData);

            var result = "";

            XmlSerializer serialization = new XmlSerializer(typeof(Envelope));
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UnicodeEncoding(false, false);
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serialization.Serialize(xmlWriter, rateRequest);
                }
                result = textWriter.ToString();
            }


            var reqData = result.Split('\n');

            reqData[0] = "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"http://fedex.com/ws/rate/v24\">\r\n";
            reqData[1] = "<SOAP-ENV:Body>\r\n";
            reqData[2] = "<RateRequest>\r\n";
            reqData[reqData.Length - 3] = "</RateRequest>\r\n";
            reqData[reqData.Length - 2] = "</SOAP-ENV:Body>\r\n";
            reqData[reqData.Length - 1] = "</SOAP-ENV:Envelope>\r\n";
            result = string.Join("", reqData);
            return result;
        }


        public static Envelope CreateRateRequest(capabilityRequestModel requestModel, IOptions<AppSettings> _settings, PistisContext db, ProductVariantDetail productData, User vendorData)
        {
            try
            {
                var request = new Envelope();
                request.Body = new EnvelopeBody();
                request.Body.RateRequest = new RateRequest();

                request.Body.RateRequest.WebAuthenticationDetail = new RateRequestWebAuthenticationDetail();
                request.Body.RateRequest.WebAuthenticationDetail.UserCredential = new RateRequestWebAuthenticationDetailUserCredential();
                request.Body.RateRequest.WebAuthenticationDetail.UserCredential.Key = _settings.Value.FedExUserId;
                request.Body.RateRequest.WebAuthenticationDetail.UserCredential.Password = _settings.Value.FedExPassword;

                request.Body.RateRequest.ClientDetail = new RateRequestClientDetail();
                request.Body.RateRequest.ClientDetail.AccountNumber = (_settings.Value.FedExAccountNumber);
                request.Body.RateRequest.ClientDetail.MeterNumber = (_settings.Value.FedExMeterNumber);

                request.Body.RateRequest.TransactionDetail = new RateRequestTransactionDetail();
                request.Body.RateRequest.TransactionDetail.CustomerTransactionId = "ENVIO_NACIONAL";

                request.Body.RateRequest.Version = new RateRequestVersion();
                request.Body.RateRequest.Version.ServiceId = "crs";
                request.Body.RateRequest.Version.Major = 24;
                request.Body.RateRequest.Version.Intermediate = 0;
                request.Body.RateRequest.Version.Minor = 0;

                request.Body.RateRequest.ReturnTransitAndCommit = true;

                request.Body.RateRequest.RequestedShipment = new RateRequestRequestedShipment();
                request.Body.RateRequest.RequestedShipment.ShipTimestamp = Convert.ToDateTime(DateTime.Now);
                request.Body.RateRequest.RequestedShipment.DropoffType = "REGULAR_PICKUP";
                request.Body.RateRequest.RequestedShipment.PackagingType = "YOUR_PACKAGING";
                request.Body.RateRequest.RequestedShipment.PreferredCurrency = "MXN";

                request.Body.RateRequest.RequestedShipment.Shipper = new RateRequestRequestedShipmentShipper();

                request.Body.RateRequest.RequestedShipment.Shipper.Contact = new RateRequestRequestedShipmentShipperContact();
                request.Body.RateRequest.RequestedShipment.Shipper.Contact.PersonName = "Pranjal shirivastva";
                request.Body.RateRequest.RequestedShipment.Shipper.Contact.CompanyName = "Pistis";
                request.Body.RateRequest.RequestedShipment.Shipper.Contact.PhoneNumber = "";
                request.Body.RateRequest.RequestedShipment.Shipper.Contact.EMailAddress = "panjaltou3@gmail.com";

                request.Body.RateRequest.RequestedShipment.Shipper.Address = new RateRequestRequestedShipmentShipperAddress();
                request.Body.RateRequest.RequestedShipment.Shipper.Address.StreetLines = new string[1];
                request.Body.RateRequest.RequestedShipment.Shipper.Address.StreetLines[0] = "";
                request.Body.RateRequest.RequestedShipment.Shipper.Address.City = "CDMX";
                request.Body.RateRequest.RequestedShipment.Shipper.Address.CountryCode = "MX";
                request.Body.RateRequest.RequestedShipment.Shipper.Address.PostalCode = "11290";
                //request.Body.RateRequest.RequestedShipment.Shipper.Address.StateOrProvinceCode = "DF";

                request.Body.RateRequest.RequestedShipment.Recipient = new RateRequestRequestedShipmentRecipient();
                request.Body.RateRequest.RequestedShipment.Recipient.Contact = new RateRequestRequestedShipmentRecipientContact();
                //request.Body.RateRequest.RequestedShipment.Recipient.Contact.PersonName = "NOMBRE DE DESTINATARIO";
                //request.Body.RateRequest.RequestedShipment.Recipient.Contact.CompanyName = "COMPANIA SI APLICA";
                //request.Body.RateRequest.RequestedShipment.Recipient.Contact.PhoneNumber = 1236547891;
                //request.Body.RateRequest.RequestedShipment.Recipient.Contact.EMailAddress = "destinatario@destinatario.com";


                request.Body.RateRequest.RequestedShipment.Recipient.Address = new RateRequestRequestedShipmentRecipientAddress();
                //request.Body.RateRequest.RequestedShipment.Recipient.Address.StreetLines = new string[2];
                //request.Body.RateRequest.RequestedShipment.Recipient.Address.StreetLines[0] = "CALLE Y NUMERO";
                //request.Body.RateRequest.RequestedShipment.Recipient.Address.StreetLines[1] = "COLONIA";
                request.Body.RateRequest.RequestedShipment.Recipient.Address.PostalCode = requestModel.Postalcode;
                request.Body.RateRequest.RequestedShipment.Recipient.Address.CountryCode = "MX";
                //request.Body.RateRequest.RequestedShipment.Recipient.Address.StateOrProvinceCode = "DF";
                //request.Body.RateRequest.RequestedShipment.Recipient.Address.City = ;

                request.Body.RateRequest.RequestedShipment.ShippingChargesPayment = new RateRequestRequestedShipmentShippingChargesPayment();
                request.Body.RateRequest.RequestedShipment.ShippingChargesPayment.PaymentType = "SENDER";
                request.Body.RateRequest.RequestedShipment.ShippingChargesPayment.Payor = new RateRequestRequestedShipmentShippingChargesPaymentPayor();
                request.Body.RateRequest.RequestedShipment.ShippingChargesPayment.Payor.ResponsibleParty = new RateRequestRequestedShipmentShippingChargesPaymentPayorResponsibleParty();
                request.Body.RateRequest.RequestedShipment.ShippingChargesPayment.Payor.ResponsibleParty.AccountNumber = (_settings.Value.FedExAccountNumber);

                request.Body.RateRequest.RequestedShipment.RateRequestTypes = "LIST";
                request.Body.RateRequest.RequestedShipment.PackageCount = 1;

                request.Body.RateRequest.RequestedShipment.RequestedPackageLineItems = new RateRequestRequestedShipmentRequestedPackageLineItems();
                request.Body.RateRequest.RequestedShipment.RequestedPackageLineItems.SequenceNumber = 1;
                request.Body.RateRequest.RequestedShipment.RequestedPackageLineItems.GroupNumber = 1;
                request.Body.RateRequest.RequestedShipment.RequestedPackageLineItems.GroupPackageCount = 1;
                request.Body.RateRequest.RequestedShipment.RequestedPackageLineItems.Weight = new RateRequestRequestedShipmentRequestedPackageLineItemsWeight();


                request.Body.RateRequest.RequestedShipment.RequestedPackageLineItems.Weight.Units = "KG";
                request.Body.RateRequest.RequestedShipment.RequestedPackageLineItems.Weight.Value = 1;

                request.Body.RateRequest.RequestedShipment.RequestedPackageLineItems.Dimensions = new RateRequestRequestedShipmentRequestedPackageLineItemsDimensions();
                request.Body.RateRequest.RequestedShipment.RequestedPackageLineItems.Dimensions.Length = 56;
                request.Body.RateRequest.RequestedShipment.RequestedPackageLineItems.Dimensions.Width = 58;
                request.Body.RateRequest.RequestedShipment.RequestedPackageLineItems.Dimensions.Height = 58;
                request.Body.RateRequest.RequestedShipment.RequestedPackageLineItems.Dimensions.Units = "CM";

                request.Body.RateRequest.RequestedShipment.RequestedPackageLineItems.ContentRecords = new RateRequestRequestedShipmentRequestedPackageLineItemsContentRecords();
                request.Body.RateRequest.RequestedShipment.RequestedPackageLineItems.ContentRecords.Description = "ENVIO DE PRUEBA";

                return request;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static HttpWebRequest createFedexSoapRequest()
        {
            //Making Web Request 
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"https://ws.fedex.com:443/web-services");
            Req.ContentType = "text/xml;charset=\"utf-8\"";
            Req.Accept = "text/xml";
            Req.Method = "POST";
            return Req;
        }
    }
}
