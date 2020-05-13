using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    public class shipValidateRequest
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
        public partial class ShipmentValidateRequest
        {

            private Request requestField;

            private string requestedPickupTimeField;

            private string newShipperField;

            private string languageCodeField;

            private string piecesEnabledField;

            private Billing billingField;

            private Consignee consigneeField;

            private Reference referenceField;

            private ShipmentDetails shipmentDetailsField;

            private Shipper shipperField;

            private string eProcShipField;

            private string labelImageFormatField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public Request Request
            {
                get
                {
                    return this.requestField;
                }
                set
                {
                    this.requestField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public string RequestedPickupTime
            {
                get
                {
                    return this.requestedPickupTimeField;
                }
                set
                {
                    this.requestedPickupTimeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public string NewShipper
            {
                get
                {
                    return this.newShipperField;
                }
                set
                {
                    this.newShipperField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public string LanguageCode
            {
                get
                {
                    return this.languageCodeField;
                }
                set
                {
                    this.languageCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public string PiecesEnabled
            {
                get
                {
                    return this.piecesEnabledField;
                }
                set
                {
                    this.piecesEnabledField = value;
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
            public ShipmentDetails ShipmentDetails
            {
                get
                {
                    return this.shipmentDetailsField;
                }
                set
                {
                    this.shipmentDetailsField = value;
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
            public string EProcShip
            {
                get
                {
                    return this.eProcShipField;
                }
                set
                {
                    this.eProcShipField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public string LabelImageFormat
            {
                get
                {
                    return this.labelImageFormatField;
                }
                set
                {
                    this.labelImageFormatField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class Request
        {

            private RequestServiceHeader serviceHeaderField;

            /// <remarks/>
            public RequestServiceHeader ServiceHeader
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
        public partial class RequestServiceHeader
        {

            private string siteIDField;

            private string passwordField;

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

            /// <remarks/>
            public string Password
            {
                get
                {
                    return this.passwordField;
                }
                set
                {
                    this.passwordField = value;
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
        public partial class ShipmentDetails
        {

            private byte numberOfPiecesField;

            private ShipmentDetailsPiece[] piecesField;

            private byte weightField;

            private string weightUnitField;

            private string globalProductCodeField;

            private string localProductCodeField;

            private System.DateTime dateField;

            private string contentsField;

            private string doorToField;

            private string dimensionUnitField;

            private string isDutiableField;

            private string currencyCodeField;

            /// <remarks/>
            public byte NumberOfPieces
            {
                get
                {
                    return this.numberOfPiecesField;
                }
                set
                {
                    this.numberOfPiecesField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Piece", IsNullable = false)]
            public ShipmentDetailsPiece[] Pieces
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
            public byte Weight
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
            public string LocalProductCode
            {
                get
                {
                    return this.localProductCodeField;
                }
                set
                {
                    this.localProductCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
            public System.DateTime Date
            {
                get
                {
                    return this.dateField;
                }
                set
                {
                    this.dateField = value;
                }
            }

            /// <remarks/>
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
            public string DoorTo
            {
                get
                {
                    return this.doorToField;
                }
                set
                {
                    this.doorToField = value;
                }
            }

            /// <remarks/>
            public string DimensionUnit
            {
                get
                {
                    return this.dimensionUnitField;
                }
                set
                {
                    this.dimensionUnitField = value;
                }
            }

            /// <remarks/>
            public string IsDutiable
            {
                get
                {
                    return this.isDutiableField;
                }
                set
                {
                    this.isDutiableField = value;
                }
            }

            /// <remarks/>
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class ShipmentDetailsPiece
        {

            private byte pieceIDField;

            private string packageTypeField;

            private byte weightField;

            private byte widthField;

            private byte heightField;

            private byte depthField;

            /// <remarks/>
            public byte PieceID
            {
                get
                {
                    return this.pieceIDField;
                }
                set
                {
                    this.pieceIDField = value;
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
            public byte Weight
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
            public byte Width
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
            public byte Height
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
            public byte Depth
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



    }
}
