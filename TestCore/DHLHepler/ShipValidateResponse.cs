using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore.DHLHepler
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
    public partial class ShipmentValidateResponse
    {

        private Response responseField;

        private Note noteField;

        private uint airwayBillNumberField;

        private string billingCodeField;

        private string currencyCodeField;

        private string courierMessageField;

        private DestinationServiceArea destinationServiceAreaField;

        private OriginServiceArea originServiceAreaField;

        private string ratedField;

        private string weightUnitField;

        private byte chargeableWeightField;

        private decimal dimensionalWeightField;

        private string countryCodeField;

        private Barcodes barcodesField;

        private byte pieceField;

        private string contentsField;

        private Reference referenceField;

        private Consignee consigneeField;

        private Shipper shipperField;

        private uint customerIDField;

        private System.DateTime shipmentDateField;

        private string globalProductCodeField;

        private SpecialService specialServiceField;

        private Billing billingField;

        private string dHLRoutingCodeField;

        private string dHLRoutingDataIdField;

        private string productContentCodeField;

        private string productShortNameField;

        private string internalServiceCodeField;

        private object deliveryDateCodeField;

        private object deliveryTimeCodeField;

        private PiecesPiece[] piecesField;

        private LabelImage labelImageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public Response Response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public Note Note
        {
            get
            {
                return this.noteField;
            }
            set
            {
                this.noteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public uint AirwayBillNumber
        {
            get
            {
                return this.airwayBillNumberField;
            }
            set
            {
                this.airwayBillNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public string BillingCode
        {
            get
            {
                return this.billingCodeField;
            }
            set
            {
                this.billingCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public string CurrencyCode
        {
            get
            {
                return this.currencyCodeField;
            }
            set
            {
                this.currencyCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public string CourierMessage
        {
            get
            {
                return this.courierMessageField;
            }
            set
            {
                this.courierMessageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public DestinationServiceArea DestinationServiceArea
        {
            get
            {
                return this.destinationServiceAreaField;
            }
            set
            {
                this.destinationServiceAreaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public OriginServiceArea OriginServiceArea
        {
            get
            {
                return this.originServiceAreaField;
            }
            set
            {
                this.originServiceAreaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public string Rated
        {
            get
            {
                return this.ratedField;
            }
            set
            {
                this.ratedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public string WeightUnit
        {
            get
            {
                return this.weightUnitField;
            }
            set
            {
                this.weightUnitField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public byte ChargeableWeight
        {
            get
            {
                return this.chargeableWeightField;
            }
            set
            {
                this.chargeableWeightField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public decimal DimensionalWeight
        {
            get
            {
                return this.dimensionalWeightField;
            }
            set
            {
                this.dimensionalWeightField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public string CountryCode
        {
            get
            {
                return this.countryCodeField;
            }
            set
            {
                this.countryCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public Barcodes Barcodes
        {
            get
            {
                return this.barcodesField;
            }
            set
            {
                this.barcodesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public byte Piece
        {
            get
            {
                return this.pieceField;
            }
            set
            {
                this.pieceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public string Contents
        {
            get
            {
                return this.contentsField;
            }
            set
            {
                this.contentsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public Reference Reference
        {
            get
            {
                return this.referenceField;
            }
            set
            {
                this.referenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public Consignee Consignee
        {
            get
            {
                return this.consigneeField;
            }
            set
            {
                this.consigneeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public Shipper Shipper
        {
            get
            {
                return this.shipperField;
            }
            set
            {
                this.shipperField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public uint CustomerID
        {
            get
            {
                return this.customerIDField;
            }
            set
            {
                this.customerIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "", DataType = "date")]
        public System.DateTime ShipmentDate
        {
            get
            {
                return this.shipmentDateField;
            }
            set
            {
                this.shipmentDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public string GlobalProductCode
        {
            get
            {
                return this.globalProductCodeField;
            }
            set
            {
                this.globalProductCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public SpecialService SpecialService
        {
            get
            {
                return this.specialServiceField;
            }
            set
            {
                this.specialServiceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public Billing Billing
        {
            get
            {
                return this.billingField;
            }
            set
            {
                this.billingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public string DHLRoutingCode
        {
            get
            {
                return this.dHLRoutingCodeField;
            }
            set
            {
                this.dHLRoutingCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public string DHLRoutingDataId
        {
            get
            {
                return this.dHLRoutingDataIdField;
            }
            set
            {
                this.dHLRoutingDataIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public string ProductContentCode
        {
            get
            {
                return this.productContentCodeField;
            }
            set
            {
                this.productContentCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public string ProductShortName
        {
            get
            {
                return this.productShortNameField;
            }
            set
            {
                this.productShortNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public string InternalServiceCode
        {
            get
            {
                return this.internalServiceCodeField;
            }
            set
            {
                this.internalServiceCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public object DeliveryDateCode
        {
            get
            {
                return this.deliveryDateCodeField;
            }
            set
            {
                this.deliveryDateCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public object DeliveryTimeCode
        {
            get
            {
                return this.deliveryTimeCodeField;
            }
            set
            {
                this.deliveryTimeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Namespace = "")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Piece", IsNullable = false)]
        public PiecesPiece[] Pieces
        {
            get
            {
                return this.piecesField;
            }
            set
            {
                this.piecesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public LabelImage LabelImage
        {
            get
            {
                return this.labelImageField;
            }
            set
            {
                this.labelImageField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Response
    {

        private ResponseServiceHeader serviceHeaderField;

        /// <remarks/>
        public ResponseServiceHeader ServiceHeader
        {
            get
            {
                return this.serviceHeaderField;
            }
            set
            {
                this.serviceHeaderField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseServiceHeader
    {

        private System.DateTime messageTimeField;

        private string messageReferenceField;

        private string siteIDField;

        /// <remarks/>
        public System.DateTime MessageTime
        {
            get
            {
                return this.messageTimeField;
            }
            set
            {
                this.messageTimeField = value;
            }
        }

        /// <remarks/>
        public string MessageReference
        {
            get
            {
                return this.messageReferenceField;
            }
            set
            {
                this.messageReferenceField = value;
            }
        }

        /// <remarks/>
        public string SiteID
        {
            get
            {
                return this.siteIDField;
            }
            set
            {
                this.siteIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Note
    {

        private string actionNoteField;

        /// <remarks/>
        public string ActionNote
        {
            get
            {
                return this.actionNoteField;
            }
            set
            {
                this.actionNoteField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class DestinationServiceArea
    {

        private string serviceAreaCodeField;

        private string facilityCodeField;

        private string inboundSortCodeField;

        /// <remarks/>
        public string ServiceAreaCode
        {
            get
            {
                return this.serviceAreaCodeField;
            }
            set
            {
                this.serviceAreaCodeField = value;
            }
        }

        /// <remarks/>
        public string FacilityCode
        {
            get
            {
                return this.facilityCodeField;
            }
            set
            {
                this.facilityCodeField = value;
            }
        }

        /// <remarks/>
        public string InboundSortCode
        {
            get
            {
                return this.inboundSortCodeField;
            }
            set
            {
                this.inboundSortCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class OriginServiceArea
    {

        private string serviceAreaCodeField;

        private string outboundSortCodeField;

        /// <remarks/>
        public string ServiceAreaCode
        {
            get
            {
                return this.serviceAreaCodeField;
            }
            set
            {
                this.serviceAreaCodeField = value;
            }
        }

        /// <remarks/>
        public string OutboundSortCode
        {
            get
            {
                return this.outboundSortCodeField;
            }
            set
            {
                this.outboundSortCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Barcodes
    {

        private string aWBBarCodeField;

        private string originDestnBarcodeField;

        private string clientIDBarCodeField;

        private string dHLRoutingBarCodeField;

        /// <remarks/>
        public string AWBBarCode
        {
            get
            {
                return this.aWBBarCodeField;
            }
            set
            {
                this.aWBBarCodeField = value;
            }
        }

        /// <remarks/>
        public string OriginDestnBarcode
        {
            get
            {
                return this.originDestnBarcodeField;
            }
            set
            {
                this.originDestnBarcodeField = value;
            }
        }

        /// <remarks/>
        public string ClientIDBarCode
        {
            get
            {
                return this.clientIDBarCodeField;
            }
            set
            {
                this.clientIDBarCodeField = value;
            }
        }

        /// <remarks/>
        public string DHLRoutingBarCode
        {
            get
            {
                return this.dHLRoutingBarCodeField;
            }
            set
            {
                this.dHLRoutingBarCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Reference
    {

        private string referenceIDField;

        /// <remarks/>
        public string ReferenceID
        {
            get
            {
                return this.referenceIDField;
            }
            set
            {
                this.referenceIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Consignee
    {

        private string companyNameField;

        private string[] addressLineField;

        private string cityField;

        private string divisionField;

        private uint postalCodeField;

        private string countryCodeField;

        private string countryNameField;

        private ConsigneeContact contactField;

        /// <remarks/>
        public string CompanyName
        {
            get
            {
                return this.companyNameField;
            }
            set
            {
                this.companyNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AddressLine")]
        public string[] AddressLine
        {
            get
            {
                return this.addressLineField;
            }
            set
            {
                this.addressLineField = value;
            }
        }

        /// <remarks/>
        public string City
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }

        /// <remarks/>
        public string Division
        {
            get
            {
                return this.divisionField;
            }
            set
            {
                this.divisionField = value;
            }
        }

        /// <remarks/>
        public uint PostalCode
        {
            get
            {
                return this.postalCodeField;
            }
            set
            {
                this.postalCodeField = value;
            }
        }

        /// <remarks/>
        public string CountryCode
        {
            get
            {
                return this.countryCodeField;
            }
            set
            {
                this.countryCodeField = value;
            }
        }

        /// <remarks/>
        public string CountryName
        {
            get
            {
                return this.countryNameField;
            }
            set
            {
                this.countryNameField = value;
            }
        }

        /// <remarks/>
        public ConsigneeContact Contact
        {
            get
            {
                return this.contactField;
            }
            set
            {
                this.contactField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ConsigneeContact
    {

        private string personNameField;

        private ulong phoneNumberField;

        /// <remarks/>
        public string PersonName
        {
            get
            {
                return this.personNameField;
            }
            set
            {
                this.personNameField = value;
            }
        }

        /// <remarks/>
        public ulong PhoneNumber
        {
            get
            {
                return this.phoneNumberField;
            }
            set
            {
                this.phoneNumberField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Shipper
    {

        private uint shipperIDField;

        private string companyNameField;

        private uint registeredAccountField;

        private string addressLineField;

        private string cityField;

        private string divisionField;

        private string divisionCodeField;

        private ushort postalCodeField;

        private string countryCodeField;

        private string countryNameField;

        private ShipperContact contactField;

        /// <remarks/>
        public uint ShipperID
        {
            get
            {
                return this.shipperIDField;
            }
            set
            {
                this.shipperIDField = value;
            }
        }

        /// <remarks/>
        public string CompanyName
        {
            get
            {
                return this.companyNameField;
            }
            set
            {
                this.companyNameField = value;
            }
        }

        /// <remarks/>
        public uint RegisteredAccount
        {
            get
            {
                return this.registeredAccountField;
            }
            set
            {
                this.registeredAccountField = value;
            }
        }

        /// <remarks/>
        public string AddressLine
        {
            get
            {
                return this.addressLineField;
            }
            set
            {
                this.addressLineField = value;
            }
        }

        /// <remarks/>
        public string City
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }

        /// <remarks/>
        public string Division
        {
            get
            {
                return this.divisionField;
            }
            set
            {
                this.divisionField = value;
            }
        }

        /// <remarks/>
        public string DivisionCode
        {
            get
            {
                return this.divisionCodeField;
            }
            set
            {
                this.divisionCodeField = value;
            }
        }

        /// <remarks/>
        public ushort PostalCode
        {
            get
            {
                return this.postalCodeField;
            }
            set
            {
                this.postalCodeField = value;
            }
        }

        /// <remarks/>
        public string CountryCode
        {
            get
            {
                return this.countryCodeField;
            }
            set
            {
                this.countryCodeField = value;
            }
        }

        /// <remarks/>
        public string CountryName
        {
            get
            {
                return this.countryNameField;
            }
            set
            {
                this.countryNameField = value;
            }
        }

        /// <remarks/>
        public ShipperContact Contact
        {
            get
            {
                return this.contactField;
            }
            set
            {
                this.contactField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ShipperContact
    {

        private string personNameField;

        private uint phoneNumberField;

        /// <remarks/>
        public string PersonName
        {
            get
            {
                return this.personNameField;
            }
            set
            {
                this.personNameField = value;
            }
        }

        /// <remarks/>
        public uint PhoneNumber
        {
            get
            {
                return this.phoneNumberField;
            }
            set
            {
                this.phoneNumberField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class SpecialService
    {

        private string specialServiceTypeField;

        private string specialServiceDescField;

        /// <remarks/>
        public string SpecialServiceType
        {
            get
            {
                return this.specialServiceTypeField;
            }
            set
            {
                this.specialServiceTypeField = value;
            }
        }

        /// <remarks/>
        public string SpecialServiceDesc
        {
            get
            {
                return this.specialServiceDescField;
            }
            set
            {
                this.specialServiceDescField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Billing
    {

        private uint shipperAccountNumberField;

        private string shippingPaymentTypeField;

        private uint billingAccountNumberField;

        private string dutyPaymentTypeField;

        /// <remarks/>
        public uint ShipperAccountNumber
        {
            get
            {
                return this.shipperAccountNumberField;
            }
            set
            {
                this.shipperAccountNumberField = value;
            }
        }

        /// <remarks/>
        public string ShippingPaymentType
        {
            get
            {
                return this.shippingPaymentTypeField;
            }
            set
            {
                this.shippingPaymentTypeField = value;
            }
        }

        /// <remarks/>
        public uint BillingAccountNumber
        {
            get
            {
                return this.billingAccountNumberField;
            }
            set
            {
                this.billingAccountNumberField = value;
            }
        }

        /// <remarks/>
        public string DutyPaymentType
        {
            get
            {
                return this.dutyPaymentTypeField;
            }
            set
            {
                this.dutyPaymentTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PiecesPiece
    {

        private byte pieceNumberField;

        private decimal depthField;

        private decimal widthField;

        private decimal heightField;

        private decimal weightField;

        private string packageTypeField;

        private decimal dimWeightField;

        private string dataIdentifierField;

        private string licensePlateField;

        private string licensePlateBarCodeField;

        /// <remarks/>
        public byte PieceNumber
        {
            get
            {
                return this.pieceNumberField;
            }
            set
            {
                this.pieceNumberField = value;
            }
        }

        /// <remarks/>
        public decimal Depth
        {
            get
            {
                return this.depthField;
            }
            set
            {
                this.depthField = value;
            }
        }

        /// <remarks/>
        public decimal Width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }

        /// <remarks/>
        public decimal Height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }

        /// <remarks/>
        public decimal Weight
        {
            get
            {
                return this.weightField;
            }
            set
            {
                this.weightField = value;
            }
        }

        /// <remarks/>
        public string PackageType
        {
            get
            {
                return this.packageTypeField;
            }
            set
            {
                this.packageTypeField = value;
            }
        }

        /// <remarks/>
        public decimal DimWeight
        {
            get
            {
                return this.dimWeightField;
            }
            set
            {
                this.dimWeightField = value;
            }
        }

        /// <remarks/>
        public string DataIdentifier
        {
            get
            {
                return this.dataIdentifierField;
            }
            set
            {
                this.dataIdentifierField = value;
            }
        }

        /// <remarks/>
        public string LicensePlate
        {
            get
            {
                return this.licensePlateField;
            }
            set
            {
                this.licensePlateField = value;
            }
        }

        /// <remarks/>
        public string LicensePlateBarCode
        {
            get
            {
                return this.licensePlateBarCodeField;
            }
            set
            {
                this.licensePlateBarCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class LabelImage
    {

        private string outputFormatField;

        private string outputImageField;

        /// <remarks/>
        public string OutputFormat
        {
            get
            {
                return this.outputFormatField;
            }
            set
            {
                this.outputFormatField = value;
            }
        }

        /// <remarks/>
        public string OutputImage
        {
            get
            {
                return this.outputImageField;
            }
            set
            {
                this.outputImageField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Pieces
    {

        private PiecesPiece[] pieceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Piece")]
        public PiecesPiece[] Piece
        {
            get
            {
                return this.pieceField;
            }
            set
            {
                this.pieceField = value;
            }
        }
    }


}
