using Localdb;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TestCore.DHLHepler;
//using static TestCore.capabilityRequest;
using static TestCore.DHLHepler.ProductsQuote;
using static TestCore.DHLHepler.TrackingRequestAllCheckPoints;
using static TestCore.shipValidateRequest;

namespace TestCore.DHLHepler
{
    public static class commonHelperDHL
    {
        //public static string fillXmlForCapability(capabilityRequestModel requestModel, string _siteId, string _sitePassword, PistisContext db)
        //{
        //    //var productData = db.ProductVariantDetails.Where(b => b.IsActive == true && b.Id == requestModel.variantDetailId).Include(b => b.Product).FirstOrDefault();
        //    //if (productData == null)
        //    //    return null;
        //    XmlSerializer serialization = new XmlSerializer(typeof(DCTRequest));
        //    DCTRequest dcr = new DCTRequest();

        //    dcr.GetQuote = new GetQuote();
        //    dcr.GetQuote.Request = new GetQuoteRequest();
        //    dcr.GetQuote.Request.ServiceHeader = new GetQuoteRequestServiceHeader();
        //    dcr.GetQuote.From = new GetQuoteFrom();
        //    dcr.GetQuote.BkgDetails = new GetQuoteBkgDetails();
        //    dcr.GetQuote.BkgDetails.Pieces = new GetQuoteBkgDetailsPieces();
        //    dcr.GetQuote.BkgDetails.Pieces.Piece = new GetQuoteBkgDetailsPiecesPiece();
        //    dcr.GetQuote.To = new GetQuoteTO();

        //    dcr.GetQuote.Request.ServiceHeader.SiteID = _siteId;
        //    dcr.GetQuote.Request.ServiceHeader.Password = _sitePassword;

        //    dcr.GetQuote.From.CountryCode = "MX";
        //    dcr.GetQuote.From.Postalcode = 15700;

        //    dcr.GetQuote.BkgDetails.PaymentCountryCode = "MX";
        //    dcr.GetQuote.BkgDetails.Date = DateTime.UtcNow;
        //    dcr.GetQuote.BkgDetails.ReadyTime = "PT10H21M";
        //    dcr.GetQuote.BkgDetails.DimensionUnit = "CM";
        //    dcr.GetQuote.BkgDetails.WeightUnit = "KG";
        //    dcr.GetQuote.BkgDetails.Pieces.Piece.PieceID = 1;
        //    dcr.GetQuote.BkgDetails.Pieces.Piece.PackageTypeCode = "FLY";
        //    dcr.GetQuote.BkgDetails.Pieces.Piece.Height = 0;
        //    dcr.GetQuote.BkgDetails.Pieces.Piece.Depth = 0;
        //    dcr.GetQuote.BkgDetails.Pieces.Piece.Width = 0;
        //    dcr.GetQuote.BkgDetails.Pieces.Piece.Weight = 6;

        //    dcr.GetQuote.To.CountryCode = requestModel.CountryCode;
        //    dcr.GetQuote.To.Postalcode = requestModel.Postalcode;


        //    var result = "";

        //    XmlWriterSettings settings = new XmlWriterSettings();
        //    settings.Encoding = new UnicodeEncoding(false, false);
        //    settings.Indent = true;
        //    settings.OmitXmlDeclaration = true;

        //    using (StringWriter textWriter = new StringWriter())
        //    {
        //        using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
        //        {
        //            serialization.Serialize(xmlWriter, dcr);
        //        }
        //        result = textWriter.ToString();
        //    }

        //    return result;
        //}

        public static string fillXmlForQuote(capabilityRequestModel requestModel, string _siteId, string _sitePassword, PistisContext db,ProductVariantDetail productData, User vendorData)
        {
            int Qtd = 2;

            if (productData == null)
                return null;

            XmlSerializer serialization = new XmlSerializer(typeof(DCTRequest));
            DCTRequest dcr = new DCTRequest();

            dcr.GetQuote = new GetQuote();
            dcr.GetQuote.Request = new GetQuoteRequest();
            dcr.GetQuote.Request.ServiceHeader = new GetQuoteRequestServiceHeader();
            dcr.GetQuote.Request.MetaData = new GetQuoteRequestMetaData();
            dcr.GetQuote.From = new GetQuoteFrom();
            dcr.GetQuote.BkgDetails = new GetQuoteBkgDetails();
            dcr.GetQuote.BkgDetails.Pieces = new GetQuoteBkgDetailsPieces();
            dcr.GetQuote.BkgDetails.Pieces.Piece = new GetQuoteBkgDetailsPiecesPiece();
            dcr.GetQuote.BkgDetails.QtdShp = new GetQuoteBkgDetailsQtdShpExChrg[Qtd];
            dcr.GetQuote.To = new GetQuoteTO();
            dcr.GetQuote.Dutiable = new GetQuoteDutiable();

            dcr.GetQuote.Request.ServiceHeader.MessageTime = DateTime.UtcNow;
            dcr.GetQuote.Request.ServiceHeader.MessageReference = "de28a010e5d441d8a5dfb3d2cadd99a8";
            dcr.GetQuote.Request.ServiceHeader.SiteID = _siteId;
            dcr.GetQuote.Request.ServiceHeader.Password = _sitePassword;

            dcr.GetQuote.Request.MetaData.SoftwareName = "3PV";
            dcr.GetQuote.Request.MetaData.SoftwareVersion = 1;

            dcr.GetQuote.From.CountryCode = "MX";
            dcr.GetQuote.From.Postalcode = "11290";

            dcr.GetQuote.BkgDetails.PaymentCountryCode = "MX";
            dcr.GetQuote.BkgDetails.Date = DateTime.UtcNow;
            dcr.GetQuote.BkgDetails.ReadyTime = "PT10H21M";
            dcr.GetQuote.BkgDetails.DimensionUnit = "CM";
            dcr.GetQuote.BkgDetails.WeightUnit = "KG";
            dcr.GetQuote.BkgDetails.Pieces.Piece.PieceID = 1;
            //dcr.GetQuote.BkgDetails.Pieces.Piece.PackageTypeCode = "FLY";
            dcr.GetQuote.BkgDetails.Pieces.Piece.Height = 0;
            dcr.GetQuote.BkgDetails.Pieces.Piece.Depth = 0;
            dcr.GetQuote.BkgDetails.Pieces.Piece.Width = 0;
            dcr.GetQuote.BkgDetails.Pieces.Piece.Weight = (1 * requestModel.quantity);

            dcr.GetQuote.BkgDetails.PaymentAccountNumber = 980039904;
            dcr.GetQuote.BkgDetails.IsDutiable = "N";

            for (int i = 0; i < Qtd; i++)
            {
                dcr.GetQuote.BkgDetails.QtdShp[i] = new GetQuoteBkgDetailsQtdShpExChrg();
                dcr.GetQuote.BkgDetails.QtdShp[i].SpecialServiceType = "OSINFO";
                if (i > 0)
                    dcr.GetQuote.BkgDetails.QtdShp[i].SpecialServiceType = "TK";
                dcr.GetQuote.BkgDetails.QtdShp.Append(dcr.GetQuote.BkgDetails.QtdShp[i]);
            }


            dcr.GetQuote.To.CountryCode = requestModel.CountryCode;
            dcr.GetQuote.To.Postalcode = requestModel.Postalcode;

            dcr.GetQuote.Dutiable.DeclaredCurrency = "USD";
            dcr.GetQuote.Dutiable.DeclaredValue = 83;

            var result = "";

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UnicodeEncoding(false, false);
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serialization.Serialize(xmlWriter, dcr);
                }
                result = textWriter.ToString();
            }

            return result;
        }

        public static string fillXmlForShipValidate(capabilityRequestModel requestModel, string _siteId, string _sitePassword, PistisContext db)
        {
            var addrsline = new string[2]
            {
                "CALLE MARTIN CADENA NUM 202",
                "Col. FULANA"
            };

            var shipperAddrsline = new string[1]
            {
                "GUSTAVO BAZ KM 12.5",
            };

            var NoOfPiece = 2;

            XmlSerializer serialization = new XmlSerializer(typeof(ShipmentValidateRequest));
            ShipmentValidateRequest svr = new ShipmentValidateRequest();


            svr.Request = new shipValidateRequest.Request();
            svr.Request.ServiceHeader = new shipValidateRequest.RequestServiceHeader();
            svr.Billing = new shipValidateRequest.Billing();
            svr.Consignee = new shipValidateRequest.Consignee();
            svr.Consignee.Contact = new shipValidateRequest.ConsigneeContact();
            svr.Reference = new shipValidateRequest.Reference();
            svr.ShipmentDetails = new shipValidateRequest.ShipmentDetails();
            svr.ShipmentDetails.Pieces = new ShipmentDetailsPiece[NoOfPiece];
            svr.Shipper = new shipValidateRequest.Shipper();
            svr.Shipper.Contact = new shipValidateRequest.ShipperContact();

            svr.Request.ServiceHeader.SiteID = _siteId;
            svr.Request.ServiceHeader.Password = _sitePassword;

            svr.RequestedPickupTime = "Y";
            svr.NewShipper = "N";
            svr.LanguageCode = "ES";
            svr.PiecesEnabled = "Y";

            svr.Billing.BillingAccountNumber = 980039904;
            svr.Billing.DutyPaymentType = "S";
            svr.Billing.ShipperAccountNumber = 980039904;
            svr.Billing.ShippingPaymentType = "S";

            svr.Consignee.CompanyName = "MARIA DE JESUS GARCIO OLIVO";
            svr.Consignee.AddressLine = addrsline;
            svr.Consignee.City = "Villahermosa";
            svr.Consignee.Division = "TABASCO";
            svr.Consignee.PostalCode = 86100;
            svr.Consignee.CountryCode = "MX";
            svr.Consignee.CountryName = "Mexico";
            svr.Consignee.Contact.PersonName = "MARIA DE JESUS GARCIA OLIVO";
            svr.Consignee.Contact.PhoneNumber = 9999999999;

            svr.Reference.ReferenceID = "Orden de compra 9999";

            svr.ShipmentDetails.NumberOfPieces = (byte)NoOfPiece;

            for (int i = 0; i < NoOfPiece; i++)
            {
                svr.ShipmentDetails.Pieces[i] = new ShipmentDetailsPiece();
                svr.ShipmentDetails.Pieces[i].PieceID = (byte)(i + 1);
                svr.ShipmentDetails.Pieces[i].PackageType = "CP";
                svr.ShipmentDetails.Pieces[i].Weight = 3;
                svr.ShipmentDetails.Pieces[i].Width = 30;
                svr.ShipmentDetails.Pieces[i].Height = 20;
                svr.ShipmentDetails.Pieces[i].Depth = 30;
                svr.ShipmentDetails.Pieces.Append(svr.ShipmentDetails.Pieces[i]);
            }

            svr.ShipmentDetails.Weight = 6;
            svr.ShipmentDetails.WeightUnit = "K";
            svr.ShipmentDetails.GlobalProductCode = "N";
            svr.ShipmentDetails.LocalProductCode = "N";
            svr.ShipmentDetails.Date = System.DateTime.Now.AddDays(1);
            svr.ShipmentDetails.Contents = "Cosmeticos";
            svr.ShipmentDetails.DoorTo = "DD";
            svr.ShipmentDetails.DimensionUnit = "C";
            svr.ShipmentDetails.IsDutiable = "N";
            svr.ShipmentDetails.CurrencyCode = "MXN";

            svr.Shipper.ShipperID = 988220698;
            svr.Shipper.CompanyName = "MEXICO SA DE CV";
            svr.Shipper.RegisteredAccount = 988127555;
            svr.Shipper.AddressLine = shipperAddrsline.ToString();
            svr.Shipper.City = "Tlalnepantla";
            svr.Shipper.Division = "ESTADO DE MEXICO";
            svr.Shipper.DivisionCode = "MX";
            svr.Shipper.PostalCode = 54010;
            svr.Shipper.CountryCode = "MX";
            svr.Shipper.CountryName = "Mexico";
            svr.Shipper.Contact.PersonName = "OCTAVIO";
            svr.Shipper.Contact.PhoneNumber = 985006;

            svr.EProcShip = "N";
            svr.LabelImageFormat = "ZPL2";

            var result = "";

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UnicodeEncoding(false, false);
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serialization.Serialize(xmlWriter, svr);
                }
                result = textWriter.ToString();
            }
            //var header= "<req:ShipmentValidateRequest xmlns:req="http://www.dhl.com" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://www.dhl.com ship-val-req.xsd">";
            return result;
        }

        public static string fillXmlForPickupRequest(capabilityRequestModel requestModel, string _siteId, string _sitePassword, PistisContext db)
        {
            XmlSerializer serialization = new XmlSerializer(typeof(BookPickupRequest));
            BookPickupRequest bpr = new BookPickupRequest();

            bpr.Request = new Request();
            bpr.Request.ServiceHeader = new RequestServiceHeader();
            bpr.Requestor = new Requestor();
            bpr.Place = new Place();
            bpr.Pickup = new Pickup();
            bpr.Pickup.weight = new PickupWeight();
            bpr.PickupContact = new PickupContact();
            bpr.ShipmentDetails = new ShipmentDetails();
            bpr.ShipmentDetails.Pieces = new ShipmentDetailsPieces();

            bpr.Request.ServiceHeader.SiteID = _siteId;
            bpr.Request.ServiceHeader.Password = _sitePassword;

            bpr.Requestor.AccountType = "D";
            bpr.Requestor.AccountNumber = 980039904;

            bpr.Place.Address1 = "carretera mzt-aeropuerto";
            bpr.Place.Address2 = "parque industrial";
            bpr.Place.City = "Alvaro Obregon";
            bpr.Place.CompanyName = "Comercial Dportenis";
            bpr.Place.CountryCode = "MX";
            bpr.Place.DivisionName = "Distrito Federal";
            bpr.Place.LocationType = "B";
            bpr.Place.PackageLocation = "Recoger en Recepcion";
            bpr.Place.PostalCode = 15700;

            bpr.Pickup.PickupDate = System.DateTime.UtcNow.AddDays(1);
            bpr.Pickup.ReadyByTime = "10:20";
            bpr.Pickup.CloseTime = "18:20";
            bpr.Pickup.Pieces = 1;
            bpr.Pickup.weight.Weight = 5;
            bpr.Pickup.weight.WeightUnit = "K";

            bpr.PickupContact.PersonName = "Dora Arangure";
            bpr.PickupContact.Phone = 4801313131;
            bpr.PickupContact.PhoneExtention = 5678;

            bpr.ShipmentDetails.AccountType = "D";
            bpr.ShipmentDetails.AccountNumber = 980039904;
            bpr.ShipmentDetails.BillToAccountNumber = 980039904;
            bpr.ShipmentDetails.NumberOfPieces = 1;
            bpr.ShipmentDetails.Weight = 5;
            bpr.ShipmentDetails.WeightUnit = "K";
            bpr.ShipmentDetails.DoorTo = "DD";
            bpr.ShipmentDetails.DimensionUnit = "C";
            bpr.ShipmentDetails.Pieces.Weight = 5;
            bpr.ShipmentDetails.Pieces.Width = 2;
            bpr.ShipmentDetails.Pieces.Height = 2;
            bpr.ShipmentDetails.Pieces.Depth = 2;

            var result = "";

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UnicodeEncoding(false, false);
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serialization.Serialize(xmlWriter, bpr);
                }
                result = textWriter.ToString();
            }
            return result;
        }


        public static string fillXmlForShipTracking(trackingRequestModel requestModel, string _siteId, string _sitePassword, PistisContext db, string checkAction)
        {
            uint awbNo = Convert.ToUInt32(requestModel.trackingNumber);

            XmlSerializer serialization = new XmlSerializer(typeof(KnownTrackingRequest));
            KnownTrackingRequest ktr = new KnownTrackingRequest();

            ktr.Request = new TrackingRequestAllCheckPoints.Request();
            ktr.Request.ServiceHeader = new TrackingRequestAllCheckPoints.RequestServiceHeader();

            ktr.Request.ServiceHeader.MessageTime = System.DateTime.Now;
            ktr.Request.ServiceHeader.MessageReference = "1234567890123456789012345678";
            ktr.Request.ServiceHeader.SiteID = _siteId;
            ktr.Request.ServiceHeader.Password = _sitePassword;

            ktr.LanguageCode = "en";
            ktr.AWBNumber = awbNo;

            if (checkAction.ToLower() == "all")
                ktr.LevelOfDetails = "ALL_CHECK_POINTS";
            if (checkAction.ToLower() == "last")
                ktr.LevelOfDetails = "LAST_CHECK_POINT_ONLY";


            var result = "";
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UnicodeEncoding(false, false);
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serialization.Serialize(xmlWriter, ktr);
                }
                result = textWriter.ToString();
            }

            return result;
        }


    }
}
