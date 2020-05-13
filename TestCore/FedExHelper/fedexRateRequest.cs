using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore.FedExHelper
{
    public class fedexRateRequest
    {
        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
        public partial class Envelope
        {

            private EnvelopeBody bodyField;

            /// <remarks/>
            public EnvelopeBody Body
            {
                get
                {
                    return this.bodyField;
                }
                set
                {
                    this.bodyField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public partial class EnvelopeBody
        {

            private RateRequest rateRequestField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://fedex.com/ws/rate/v24")]
            public RateRequest RateRequest
            {
                get
                {
                    return this.rateRequestField;
                }
                set
                {
                    this.rateRequestField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://fedex.com/ws/rate/v24", IsNullable = false)]
        public partial class RateRequest
        {

            private RateRequestWebAuthenticationDetail webAuthenticationDetailField;

            private RateRequestClientDetail clientDetailField;

            private RateRequestTransactionDetail transactionDetailField;

            private RateRequestVersion versionField;

            private bool returnTransitAndCommitField;

            private RateRequestRequestedShipment requestedShipmentField;

            /// <remarks/>
            public RateRequestWebAuthenticationDetail WebAuthenticationDetail
            {
                get
                {
                    return this.webAuthenticationDetailField;
                }
                set
                {
                    this.webAuthenticationDetailField = value;
                }
            }

            /// <remarks/>
            public RateRequestClientDetail ClientDetail
            {
                get
                {
                    return this.clientDetailField;
                }
                set
                {
                    this.clientDetailField = value;
                }
            }

            /// <remarks/>
            public RateRequestTransactionDetail TransactionDetail
            {
                get
                {
                    return this.transactionDetailField;
                }
                set
                {
                    this.transactionDetailField = value;
                }
            }

            /// <remarks/>
            public RateRequestVersion Version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }

            /// <remarks/>
            public bool ReturnTransitAndCommit
            {
                get
                {
                    return this.returnTransitAndCommitField;
                }
                set
                {
                    this.returnTransitAndCommitField = value;
                }
            }

            /// <remarks/>
            public RateRequestRequestedShipment RequestedShipment
            {
                get
                {
                    return this.requestedShipmentField;
                }
                set
                {
                    this.requestedShipmentField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateRequestWebAuthenticationDetail
        {

            private RateRequestWebAuthenticationDetailUserCredential userCredentialField;

            /// <remarks/>
            public RateRequestWebAuthenticationDetailUserCredential UserCredential
            {
                get
                {
                    return this.userCredentialField;
                }
                set
                {
                    this.userCredentialField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateRequestWebAuthenticationDetailUserCredential
        {

            private string keyField;

            private string passwordField;

            /// <remarks/>
            public string Key
            {
                get
                {
                    return this.keyField;
                }
                set
                {
                    this.keyField = value;
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateRequestClientDetail
        {

            private string accountNumberField;

            private string meterNumberField;

            /// <remarks/>
            public string AccountNumber
            {
                get
                {
                    return this.accountNumberField;
                }
                set
                {
                    this.accountNumberField = value;
                }
            }

            /// <remarks/>
            public string MeterNumber
            {
                get
                {
                    return this.meterNumberField;
                }
                set
                {
                    this.meterNumberField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateRequestTransactionDetail
        {

            private string customerTransactionIdField;

            /// <remarks/>
            public string CustomerTransactionId
            {
                get
                {
                    return this.customerTransactionIdField;
                }
                set
                {
                    this.customerTransactionIdField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateRequestVersion
        {

            private string serviceIdField;

            private byte majorField;

            private byte intermediateField;

            private byte minorField;

            /// <remarks/>
            public string ServiceId
            {
                get
                {
                    return this.serviceIdField;
                }
                set
                {
                    this.serviceIdField = value;
                }
            }

            /// <remarks/>
            public byte Major
            {
                get
                {
                    return this.majorField;
                }
                set
                {
                    this.majorField = value;
                }
            }

            /// <remarks/>
            public byte Intermediate
            {
                get
                {
                    return this.intermediateField;
                }
                set
                {
                    this.intermediateField = value;
                }
            }

            /// <remarks/>
            public byte Minor
            {
                get
                {
                    return this.minorField;
                }
                set
                {
                    this.minorField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateRequestRequestedShipment
        {

            private System.DateTime shipTimestampField;

            private string dropoffTypeField;

            private string packagingTypeField;

            private string preferredCurrencyField;

            private RateRequestRequestedShipmentShipper shipperField;

            private RateRequestRequestedShipmentRecipient recipientField;

            private RateRequestRequestedShipmentShippingChargesPayment shippingChargesPaymentField;

            private string rateRequestTypesField;

            private byte packageCountField;

            private RateRequestRequestedShipmentRequestedPackageLineItems requestedPackageLineItemsField;

            /// <remarks/>
            public System.DateTime ShipTimestamp
            {
                get
                {
                    return this.shipTimestampField;
                }
                set
                {
                    this.shipTimestampField = value;
                }
            }

            /// <remarks/>
            public string DropoffType
            {
                get
                {
                    return this.dropoffTypeField;
                }
                set
                {
                    this.dropoffTypeField = value;
                }
            }

            /// <remarks/>
            public string PackagingType
            {
                get
                {
                    return this.packagingTypeField;
                }
                set
                {
                    this.packagingTypeField = value;
                }
            }

            /// <remarks/>
            public string PreferredCurrency
            {
                get
                {
                    return this.preferredCurrencyField;
                }
                set
                {
                    this.preferredCurrencyField = value;
                }
            }

            /// <remarks/>
            public RateRequestRequestedShipmentShipper Shipper
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
            public RateRequestRequestedShipmentRecipient Recipient
            {
                get
                {
                    return this.recipientField;
                }
                set
                {
                    this.recipientField = value;
                }
            }

            /// <remarks/>
            public RateRequestRequestedShipmentShippingChargesPayment ShippingChargesPayment
            {
                get
                {
                    return this.shippingChargesPaymentField;
                }
                set
                {
                    this.shippingChargesPaymentField = value;
                }
            }

            /// <remarks/>
            public string RateRequestTypes
            {
                get
                {
                    return this.rateRequestTypesField;
                }
                set
                {
                    this.rateRequestTypesField = value;
                }
            }

            /// <remarks/>
            public byte PackageCount
            {
                get
                {
                    return this.packageCountField;
                }
                set
                {
                    this.packageCountField = value;
                }
            }

            /// <remarks/>
            public RateRequestRequestedShipmentRequestedPackageLineItems RequestedPackageLineItems
            {
                get
                {
                    return this.requestedPackageLineItemsField;
                }
                set
                {
                    this.requestedPackageLineItemsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateRequestRequestedShipmentShipper
        {

            private RateRequestRequestedShipmentShipperContact contactField;

            private RateRequestRequestedShipmentShipperAddress addressField;

            /// <remarks/>
            public RateRequestRequestedShipmentShipperContact Contact
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

            /// <remarks/>
            public RateRequestRequestedShipmentShipperAddress Address
            {
                get
                {
                    return this.addressField;
                }
                set
                {
                    this.addressField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateRequestRequestedShipmentShipperContact
        {

            private string personNameField;

            private string companyNameField;

            private string phoneNumberField;

            private string eMailAddressField;

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
            public string PhoneNumber
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

            /// <remarks/>
            public string EMailAddress
            {
                get
                {
                    return this.eMailAddressField;
                }
                set
                {
                    this.eMailAddressField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateRequestRequestedShipmentShipperAddress
        {

            private string[] streetLinesField;

            private string cityField;

            private string stateOrProvinceCodeField;

            private string postalCodeField;

            private string countryCodeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("StreetLines")]
            public string[] StreetLines
            {
                get
                {
                    return this.streetLinesField;
                }
                set
                {
                    this.streetLinesField = value;
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
            public string StateOrProvinceCode
            {
                get
                {
                    return this.stateOrProvinceCodeField;
                }
                set
                {
                    this.stateOrProvinceCodeField = value;
                }
            }

            /// <remarks/>
            public string PostalCode
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateRequestRequestedShipmentRecipient
        {

            private RateRequestRequestedShipmentRecipientContact contactField;

            private RateRequestRequestedShipmentRecipientAddress addressField;

            /// <remarks/>
            public RateRequestRequestedShipmentRecipientContact Contact
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

            /// <remarks/>
            public RateRequestRequestedShipmentRecipientAddress Address
            {
                get
                {
                    return this.addressField;
                }
                set
                {
                    this.addressField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateRequestRequestedShipmentRecipientContact
        {

            private string personNameField;

            private string companyNameField;

            private string phoneNumberField;

            private string eMailAddressField;

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
            public string PhoneNumber
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

            /// <remarks/>
            public string EMailAddress
            {
                get
                {
                    return this.eMailAddressField;
                }
                set
                {
                    this.eMailAddressField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateRequestRequestedShipmentRecipientAddress
        {

            private string[] streetLinesField;

            private string cityField;

            private string stateOrProvinceCodeField;

            private string postalCodeField;

            private string countryCodeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("StreetLines")]
            public string[] StreetLines
            {
                get
                {
                    return this.streetLinesField;
                }
                set
                {
                    this.streetLinesField = value;
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
            public string StateOrProvinceCode
            {
                get
                {
                    return this.stateOrProvinceCodeField;
                }
                set
                {
                    this.stateOrProvinceCodeField = value;
                }
            }

            /// <remarks/>
            public string PostalCode
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateRequestRequestedShipmentShippingChargesPayment
        {

            private string paymentTypeField;

            private RateRequestRequestedShipmentShippingChargesPaymentPayor payorField;

            /// <remarks/>
            public string PaymentType
            {
                get
                {
                    return this.paymentTypeField;
                }
                set
                {
                    this.paymentTypeField = value;
                }
            }

            /// <remarks/>
            public RateRequestRequestedShipmentShippingChargesPaymentPayor Payor
            {
                get
                {
                    return this.payorField;
                }
                set
                {
                    this.payorField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateRequestRequestedShipmentShippingChargesPaymentPayor
        {

            private RateRequestRequestedShipmentShippingChargesPaymentPayorResponsibleParty responsiblePartyField;

            /// <remarks/>
            public RateRequestRequestedShipmentShippingChargesPaymentPayorResponsibleParty ResponsibleParty
            {
                get
                {
                    return this.responsiblePartyField;
                }
                set
                {
                    this.responsiblePartyField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateRequestRequestedShipmentShippingChargesPaymentPayorResponsibleParty
        {

            private string accountNumberField;

            /// <remarks/>
            public string AccountNumber
            {
                get
                {
                    return this.accountNumberField;
                }
                set
                {
                    this.accountNumberField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateRequestRequestedShipmentRequestedPackageLineItems
        {

            private byte sequenceNumberField;

            private byte groupNumberField;

            private byte groupPackageCountField;

            private RateRequestRequestedShipmentRequestedPackageLineItemsWeight weightField;

            private RateRequestRequestedShipmentRequestedPackageLineItemsDimensions dimensionsField;

            private RateRequestRequestedShipmentRequestedPackageLineItemsContentRecords contentRecordsField;

            /// <remarks/>
            public byte SequenceNumber
            {
                get
                {
                    return this.sequenceNumberField;
                }
                set
                {
                    this.sequenceNumberField = value;
                }
            }

            /// <remarks/>
            public byte GroupNumber
            {
                get
                {
                    return this.groupNumberField;
                }
                set
                {
                    this.groupNumberField = value;
                }
            }

            /// <remarks/>
            public byte GroupPackageCount
            {
                get
                {
                    return this.groupPackageCountField;
                }
                set
                {
                    this.groupPackageCountField = value;
                }
            }

            /// <remarks/>
            public RateRequestRequestedShipmentRequestedPackageLineItemsWeight Weight
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
            public RateRequestRequestedShipmentRequestedPackageLineItemsDimensions Dimensions
            {
                get
                {
                    return this.dimensionsField;
                }
                set
                {
                    this.dimensionsField = value;
                }
            }

            /// <remarks/>
            public RateRequestRequestedShipmentRequestedPackageLineItemsContentRecords ContentRecords
            {
                get
                {
                    return this.contentRecordsField;
                }
                set
                {
                    this.contentRecordsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateRequestRequestedShipmentRequestedPackageLineItemsWeight
        {

            private string unitsField;

            private byte valueField;

            /// <remarks/>
            public string Units
            {
                get
                {
                    return this.unitsField;
                }
                set
                {
                    this.unitsField = value;
                }
            }

            /// <remarks/>
            public byte Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateRequestRequestedShipmentRequestedPackageLineItemsDimensions
        {

            private byte lengthField;

            private byte widthField;

            private byte heightField;

            private string unitsField;

            /// <remarks/>
            public byte Length
            {
                get
                {
                    return this.lengthField;
                }
                set
                {
                    this.lengthField = value;
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
            public string Units
            {
                get
                {
                    return this.unitsField;
                }
                set
                {
                    this.unitsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateRequestRequestedShipmentRequestedPackageLineItemsContentRecords
        {

            private string descriptionField;

            /// <remarks/>
            public string Description
            {
                get
                {
                    return this.descriptionField;
                }
                set
                {
                    this.descriptionField = value;
                }
            }
        }


    }
}
