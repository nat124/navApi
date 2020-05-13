using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore.DHLHepler
{
    public class QuoteResponse
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
        public partial class DCTResponse
        {

            private GetQuoteResponse getQuoteResponseField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public GetQuoteResponse GetQuoteResponse
            {
                get
                {
                    return this.getQuoteResponseField;
                }
                set
                {
                    this.getQuoteResponseField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class GetQuoteResponse
        {

            private GetQuoteResponseResponse responseField;

            private GetQuoteResponseQtdShp[] bkgDetailsField;

            private GetQuoteResponseSrv[] srvsField;

            private GetQuoteResponseNote noteField;

            /// <remarks/>
            public GetQuoteResponseResponse Response
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
            [System.Xml.Serialization.XmlArrayItemAttribute("QtdShp", IsNullable = false)]
            public GetQuoteResponseQtdShp[] BkgDetails
            {
                get
                {
                    return this.bkgDetailsField;
                }
                set
                {
                    this.bkgDetailsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Srv", IsNullable = false)]
            public GetQuoteResponseSrv[] Srvs
            {
                get
                {
                    return this.srvsField;
                }
                set
                {
                    this.srvsField = value;
                }
            }

            /// <remarks/>
            public GetQuoteResponseNote Note
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteResponseResponse
        {

            private GetQuoteResponseResponseServiceHeader serviceHeaderField;

            /// <remarks/>
            public GetQuoteResponseResponseServiceHeader ServiceHeader
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
        public partial class GetQuoteResponseResponseServiceHeader
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
        public partial class GetQuoteResponseQtdShp
        {

            private GetQuoteResponseQtdShpOriginServiceArea originServiceAreaField;

            private GetQuoteResponseQtdShpDestinationServiceArea destinationServiceAreaField;

            private string globalProductCodeField;

            private string localProductCodeField;

            private string productShortNameField;

            private string localProductNameField;

            private string networkTypeCodeField;

            private string pOfferedCustAgreementField;

            private string transIndField;

            private System.DateTime pickupDateField;

            private string pickupCutoffTimeField;

            private string bookingTimeField;

            private string currencyCodeField;

            private decimal exchangeRateField;

            private decimal weightChargeField;

            private decimal weightChargeTaxField;

            private byte totalTransitDaysField;

            private byte pickupPostalLocAddDaysField;

            private byte deliveryPostalLocAddDaysField;

            private object pickupNonDHLCourierCodeField;

            private object deliveryNonDHLCourierCodeField;

            private GetQuoteResponseQtdShpDeliveryDate deliveryDateField;

            private string deliveryTimeField;

            private decimal dimensionalWeightField;

            private string weightUnitField;

            private byte pickupDayOfWeekNumField;

            private byte destinationDayOfWeekNumField;

            private decimal quotedWeightField;

            private string quotedWeightUOMField;

            private GetQuoteResponseQtdShpQtdShpExChrg[] qtdShpExChrgField;

            private System.DateTime pricingDateField;

            private decimal shippingChargeField;

            private decimal totalTaxAmountField;

            private GetQuoteResponseQtdShpQtdSInAdCur[] qtdSInAdCurField;

            private System.DateTime pickupWindowEarliestTimeField;

            private System.DateTime pickupWindowLatestTimeField;

            private string bookingCutoffOffsetField;

            /// <remarks/>
            public GetQuoteResponseQtdShpOriginServiceArea OriginServiceArea
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
            public GetQuoteResponseQtdShpDestinationServiceArea DestinationServiceArea
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
            public string LocalProductName
            {
                get
                {
                    return this.localProductNameField;
                }
                set
                {
                    this.localProductNameField = value;
                }
            }

            /// <remarks/>
            public string NetworkTypeCode
            {
                get
                {
                    return this.networkTypeCodeField;
                }
                set
                {
                    this.networkTypeCodeField = value;
                }
            }

            /// <remarks/>
            public string POfferedCustAgreement
            {
                get
                {
                    return this.pOfferedCustAgreementField;
                }
                set
                {
                    this.pOfferedCustAgreementField = value;
                }
            }

            /// <remarks/>
            public string TransInd
            {
                get
                {
                    return this.transIndField;
                }
                set
                {
                    this.transIndField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
            public System.DateTime PickupDate
            {
                get
                {
                    return this.pickupDateField;
                }
                set
                {
                    this.pickupDateField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
            public string PickupCutoffTime
            {
                get
                {
                    return this.pickupCutoffTimeField;
                }
                set
                {
                    this.pickupCutoffTimeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
            public string BookingTime
            {
                get
                {
                    return this.bookingTimeField;
                }
                set
                {
                    this.bookingTimeField = value;
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

            /// <remarks/>
            public decimal ExchangeRate
            {
                get
                {
                    return this.exchangeRateField;
                }
                set
                {
                    this.exchangeRateField = value;
                }
            }

            /// <remarks/>
            public decimal WeightCharge
            {
                get
                {
                    return this.weightChargeField;
                }
                set
                {
                    this.weightChargeField = value;
                }
            }

            /// <remarks/>
            public decimal WeightChargeTax
            {
                get
                {
                    return this.weightChargeTaxField;
                }
                set
                {
                    this.weightChargeTaxField = value;
                }
            }

            /// <remarks/>
            public byte TotalTransitDays
            {
                get
                {
                    return this.totalTransitDaysField;
                }
                set
                {
                    this.totalTransitDaysField = value;
                }
            }

            /// <remarks/>
            public byte PickupPostalLocAddDays
            {
                get
                {
                    return this.pickupPostalLocAddDaysField;
                }
                set
                {
                    this.pickupPostalLocAddDaysField = value;
                }
            }

            /// <remarks/>
            public byte DeliveryPostalLocAddDays
            {
                get
                {
                    return this.deliveryPostalLocAddDaysField;
                }
                set
                {
                    this.deliveryPostalLocAddDaysField = value;
                }
            }

            /// <remarks/>
            public object PickupNonDHLCourierCode
            {
                get
                {
                    return this.pickupNonDHLCourierCodeField;
                }
                set
                {
                    this.pickupNonDHLCourierCodeField = value;
                }
            }

            /// <remarks/>
            public object DeliveryNonDHLCourierCode
            {
                get
                {
                    return this.deliveryNonDHLCourierCodeField;
                }
                set
                {
                    this.deliveryNonDHLCourierCodeField = value;
                }
            }

            /// <remarks/>
            public GetQuoteResponseQtdShpDeliveryDate DeliveryDate
            {
                get
                {
                    return this.deliveryDateField;
                }
                set
                {
                    this.deliveryDateField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
            public string DeliveryTime
            {
                get
                {
                    return this.deliveryTimeField;
                }
                set
                {
                    this.deliveryTimeField = value;
                }
            }

            /// <remarks/>
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
            public byte PickupDayOfWeekNum
            {
                get
                {
                    return this.pickupDayOfWeekNumField;
                }
                set
                {
                    this.pickupDayOfWeekNumField = value;
                }
            }

            /// <remarks/>
            public byte DestinationDayOfWeekNum
            {
                get
                {
                    return this.destinationDayOfWeekNumField;
                }
                set
                {
                    this.destinationDayOfWeekNumField = value;
                }
            }

            /// <remarks/>
            public decimal QuotedWeight
            {
                get
                {
                    return this.quotedWeightField;
                }
                set
                {
                    this.quotedWeightField = value;
                }
            }

            /// <remarks/>
            public string QuotedWeightUOM
            {
                get
                {
                    return this.quotedWeightUOMField;
                }
                set
                {
                    this.quotedWeightUOMField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("QtdShpExChrg")]
            public GetQuoteResponseQtdShpQtdShpExChrg[] QtdShpExChrg
            {
                get
                {
                    return this.qtdShpExChrgField;
                }
                set
                {
                    this.qtdShpExChrgField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
            public System.DateTime PricingDate
            {
                get
                {
                    return this.pricingDateField;
                }
                set
                {
                    this.pricingDateField = value;
                }
            }

            /// <remarks/>
            public decimal ShippingCharge
            {
                get
                {
                    return this.shippingChargeField;
                }
                set
                {
                    this.shippingChargeField = value;
                }
            }

            /// <remarks/>
            public decimal TotalTaxAmount
            {
                get
                {
                    return this.totalTaxAmountField;
                }
                set
                {
                    this.totalTaxAmountField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("QtdSInAdCur")]
            public GetQuoteResponseQtdShpQtdSInAdCur[] QtdSInAdCur
            {
                get
                {
                    return this.qtdSInAdCurField;
                }
                set
                {
                    this.qtdSInAdCurField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "time")]
            public System.DateTime PickupWindowEarliestTime
            {
                get
                {
                    return this.pickupWindowEarliestTimeField;
                }
                set
                {
                    this.pickupWindowEarliestTimeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "time")]
            public System.DateTime PickupWindowLatestTime
            {
                get
                {
                    return this.pickupWindowLatestTimeField;
                }
                set
                {
                    this.pickupWindowLatestTimeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
            public string BookingCutoffOffset
            {
                get
                {
                    return this.bookingCutoffOffsetField;
                }
                set
                {
                    this.bookingCutoffOffsetField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteResponseQtdShpOriginServiceArea
        {

            private string facilityCodeField;

            private string serviceAreaCodeField;

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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteResponseQtdShpDestinationServiceArea
        {

            private string facilityCodeField;

            private string serviceAreaCodeField;

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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteResponseQtdShpDeliveryDate
        {

            private string deliveryTypeField;

            private string dlvyDateTimeField;

            private string deliveryDateTimeOffsetField;

            /// <remarks/>
            public string DeliveryType
            {
                get
                {
                    return this.deliveryTypeField;
                }
                set
                {
                    this.deliveryTypeField = value;
                }
            }

            /// <remarks/>
            public string DlvyDateTime
            {
                get
                {
                    return this.dlvyDateTimeField;
                }
                set
                {
                    this.dlvyDateTimeField = value;
                }
            }

            /// <remarks/>
            public string DeliveryDateTimeOffset
            {
                get
                {
                    return this.deliveryDateTimeOffsetField;
                }
                set
                {
                    this.deliveryDateTimeOffsetField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteResponseQtdShpQtdShpExChrg
        {

            private string specialServiceTypeField;

            private string localServiceTypeField;

            private string globalServiceNameField;

            private string localServiceTypeNameField;

            private string sOfferedCustAgreementField;

            private string chargeCodeTypeField;

            private string currencyCodeField;

            private decimal chargeValueField;

            private GetQuoteResponseQtdShpQtdShpExChrgQtdSExtrChrgInAdCur[] qtdSExtrChrgInAdCurField;

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
            public string LocalServiceType
            {
                get
                {
                    return this.localServiceTypeField;
                }
                set
                {
                    this.localServiceTypeField = value;
                }
            }

            /// <remarks/>
            public string GlobalServiceName
            {
                get
                {
                    return this.globalServiceNameField;
                }
                set
                {
                    this.globalServiceNameField = value;
                }
            }

            /// <remarks/>
            public string LocalServiceTypeName
            {
                get
                {
                    return this.localServiceTypeNameField;
                }
                set
                {
                    this.localServiceTypeNameField = value;
                }
            }

            /// <remarks/>
            public string SOfferedCustAgreement
            {
                get
                {
                    return this.sOfferedCustAgreementField;
                }
                set
                {
                    this.sOfferedCustAgreementField = value;
                }
            }

            /// <remarks/>
            public string ChargeCodeType
            {
                get
                {
                    return this.chargeCodeTypeField;
                }
                set
                {
                    this.chargeCodeTypeField = value;
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

            /// <remarks/>
            public decimal ChargeValue
            {
                get
                {
                    return this.chargeValueField;
                }
                set
                {
                    this.chargeValueField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("QtdSExtrChrgInAdCur")]
            public GetQuoteResponseQtdShpQtdShpExChrgQtdSExtrChrgInAdCur[] QtdSExtrChrgInAdCur
            {
                get
                {
                    return this.qtdSExtrChrgInAdCurField;
                }
                set
                {
                    this.qtdSExtrChrgInAdCurField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteResponseQtdShpQtdShpExChrgQtdSExtrChrgInAdCur
        {

            private decimal chargeValueField;

            private decimal chargeTaxRateField;

            private bool chargeTaxRateFieldSpecified;

            private string currencyCodeField;

            private string currencyRoleTypeCodeField;

            /// <remarks/>
            public decimal ChargeValue
            {
                get
                {
                    return this.chargeValueField;
                }
                set
                {
                    this.chargeValueField = value;
                }
            }

            /// <remarks/>
            public decimal ChargeTaxRate
            {
                get
                {
                    return this.chargeTaxRateField;
                }
                set
                {
                    this.chargeTaxRateField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool ChargeTaxRateSpecified
            {
                get
                {
                    return this.chargeTaxRateFieldSpecified;
                }
                set
                {
                    this.chargeTaxRateFieldSpecified = value;
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

            /// <remarks/>
            public string CurrencyRoleTypeCode
            {
                get
                {
                    return this.currencyRoleTypeCodeField;
                }
                set
                {
                    this.currencyRoleTypeCodeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteResponseQtdShpQtdSInAdCur
        {

            private decimal exchangeRateField;

            private bool exchangeRateFieldSpecified;

            private string currencyCodeField;

            private string currencyRoleTypeCodeField;

            private decimal weightChargeField;

            private decimal totalAmountField;

            private decimal totalTaxAmountField;

            private decimal weightChargeTaxField;

            /// <remarks/>
            public decimal ExchangeRate
            {
                get
                {
                    return this.exchangeRateField;
                }
                set
                {
                    this.exchangeRateField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool ExchangeRateSpecified
            {
                get
                {
                    return this.exchangeRateFieldSpecified;
                }
                set
                {
                    this.exchangeRateFieldSpecified = value;
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

            /// <remarks/>
            public string CurrencyRoleTypeCode
            {
                get
                {
                    return this.currencyRoleTypeCodeField;
                }
                set
                {
                    this.currencyRoleTypeCodeField = value;
                }
            }

            /// <remarks/>
            public decimal WeightCharge
            {
                get
                {
                    return this.weightChargeField;
                }
                set
                {
                    this.weightChargeField = value;
                }
            }

            /// <remarks/>
            public decimal TotalAmount
            {
                get
                {
                    return this.totalAmountField;
                }
                set
                {
                    this.totalAmountField = value;
                }
            }

            /// <remarks/>
            public decimal TotalTaxAmount
            {
                get
                {
                    return this.totalTaxAmountField;
                }
                set
                {
                    this.totalTaxAmountField = value;
                }
            }

            /// <remarks/>
            public decimal WeightChargeTax
            {
                get
                {
                    return this.weightChargeTaxField;
                }
                set
                {
                    this.weightChargeTaxField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteResponseSrv
        {

            private string globalProductCodeField;

            private GetQuoteResponseSrvMrkSrv[] mrkSrvField;

            private GetQuoteResponseSrvSBTP sBTPField;

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
            [System.Xml.Serialization.XmlElementAttribute("MrkSrv")]
            public GetQuoteResponseSrvMrkSrv[] MrkSrv
            {
                get
                {
                    return this.mrkSrvField;
                }
                set
                {
                    this.mrkSrvField = value;
                }
            }

            /// <remarks/>
            public GetQuoteResponseSrvSBTP SBTP
            {
                get
                {
                    return this.sBTPField;
                }
                set
                {
                    this.sBTPField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteResponseSrvMrkSrv
        {

            private string localServiceTypeField;

            private string globalServiceNameField;

            private string localServiceTypeNameField;

            private string sOfferedCustAgreementField;

            private string chargeCodeTypeField;

            private string mrkSrvIndField;

            private string localProductCodeField;

            private string productShortNameField;

            private string localProductNameField;

            private string productDescField;

            private string networkTypeCodeField;

            private string pOfferedCustAgreementField;

            private string transIndField;

            private string localProductCtryCdField;

            private string globalServiceTypeField;

            private string localServiceNameField;

            /// <remarks/>
            public string LocalServiceType
            {
                get
                {
                    return this.localServiceTypeField;
                }
                set
                {
                    this.localServiceTypeField = value;
                }
            }

            /// <remarks/>
            public string GlobalServiceName
            {
                get
                {
                    return this.globalServiceNameField;
                }
                set
                {
                    this.globalServiceNameField = value;
                }
            }

            /// <remarks/>
            public string LocalServiceTypeName
            {
                get
                {
                    return this.localServiceTypeNameField;
                }
                set
                {
                    this.localServiceTypeNameField = value;
                }
            }

            /// <remarks/>
            public string SOfferedCustAgreement
            {
                get
                {
                    return this.sOfferedCustAgreementField;
                }
                set
                {
                    this.sOfferedCustAgreementField = value;
                }
            }

            /// <remarks/>
            public string ChargeCodeType
            {
                get
                {
                    return this.chargeCodeTypeField;
                }
                set
                {
                    this.chargeCodeTypeField = value;
                }
            }

            /// <remarks/>
            public string MrkSrvInd
            {
                get
                {
                    return this.mrkSrvIndField;
                }
                set
                {
                    this.mrkSrvIndField = value;
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
            public string LocalProductName
            {
                get
                {
                    return this.localProductNameField;
                }
                set
                {
                    this.localProductNameField = value;
                }
            }

            /// <remarks/>
            public string ProductDesc
            {
                get
                {
                    return this.productDescField;
                }
                set
                {
                    this.productDescField = value;
                }
            }

            /// <remarks/>
            public string NetworkTypeCode
            {
                get
                {
                    return this.networkTypeCodeField;
                }
                set
                {
                    this.networkTypeCodeField = value;
                }
            }

            /// <remarks/>
            public string POfferedCustAgreement
            {
                get
                {
                    return this.pOfferedCustAgreementField;
                }
                set
                {
                    this.pOfferedCustAgreementField = value;
                }
            }

            /// <remarks/>
            public string TransInd
            {
                get
                {
                    return this.transIndField;
                }
                set
                {
                    this.transIndField = value;
                }
            }

            /// <remarks/>
            public string LocalProductCtryCd
            {
                get
                {
                    return this.localProductCtryCdField;
                }
                set
                {
                    this.localProductCtryCdField = value;
                }
            }

            /// <remarks/>
            public string GlobalServiceType
            {
                get
                {
                    return this.globalServiceTypeField;
                }
                set
                {
                    this.globalServiceTypeField = value;
                }
            }

            /// <remarks/>
            public string LocalServiceName
            {
                get
                {
                    return this.localServiceNameField;
                }
                set
                {
                    this.localServiceNameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteResponseSrvSBTP
        {

            private GetQuoteResponseSrvSBTPVldSrvComb[] prodField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("VldSrvComb", IsNullable = false)]
            public GetQuoteResponseSrvSBTPVldSrvComb[] Prod
            {
                get
                {
                    return this.prodField;
                }
                set
                {
                    this.prodField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteResponseSrvSBTPVldSrvComb
        {

            private string specialServiceTypeField;

            private string localServiceTypeField;

            private GetQuoteResponseSrvSBTPVldSrvCombCombRSrv combRSrvField;

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
            public string LocalServiceType
            {
                get
                {
                    return this.localServiceTypeField;
                }
                set
                {
                    this.localServiceTypeField = value;
                }
            }

            /// <remarks/>
            public GetQuoteResponseSrvSBTPVldSrvCombCombRSrv CombRSrv
            {
                get
                {
                    return this.combRSrvField;
                }
                set
                {
                    this.combRSrvField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteResponseSrvSBTPVldSrvCombCombRSrv
        {

            private object restrictedSpecialServiceTypeField;

            private object restrictedLocalServiceTypeField;

            /// <remarks/>
            public object RestrictedSpecialServiceType
            {
                get
                {
                    return this.restrictedSpecialServiceTypeField;
                }
                set
                {
                    this.restrictedSpecialServiceTypeField = value;
                }
            }

            /// <remarks/>
            public object RestrictedLocalServiceType
            {
                get
                {
                    return this.restrictedLocalServiceTypeField;
                }
                set
                {
                    this.restrictedLocalServiceTypeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteResponseNote
        {

            private string actionStatusField;

            private GetQuoteResponseNoteCondition conditionField;

            /// <remarks/>
            public string ActionStatus
            {
                get
                {
                    return this.actionStatusField;
                }
                set
                {
                    this.actionStatusField = value;
                }
            }

            /// <remarks/>
            public GetQuoteResponseNoteCondition Condition
            {
                get
                {
                    return this.conditionField;
                }
                set
                {
                    this.conditionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteResponseNoteCondition
        {

            private uint conditionCodeField;

            private string conditionDataField;

            /// <remarks/>
            public uint ConditionCode
            {
                get
                {
                    return this.conditionCodeField;
                }
                set
                {
                    this.conditionCodeField = value;
                }
            }

            /// <remarks/>
            public string ConditionData
            {
                get
                {
                    return this.conditionDataField;
                }
                set
                {
                    this.conditionDataField = value;
                }
            }
        }


    }
}
