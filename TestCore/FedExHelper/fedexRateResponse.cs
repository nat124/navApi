using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore.FedExHelper
{
    public class fedexRateResponse
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
        public partial class Envelope
        {

            private object headerField;

            private EnvelopeBody bodyField;

            /// <remarks/>
            public object Header
            {
                get
                {
                    return this.headerField;
                }
                set
                {
                    this.headerField = value;
                }
            }

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

            private RateReply rateReplyField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://fedex.com/ws/rate/v24")]
            public RateReply RateReply
            {
                get
                {
                    return this.rateReplyField;
                }
                set
                {
                    this.rateReplyField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://fedex.com/ws/rate/v24", IsNullable = false)]
        public partial class RateReply
        {

            private string highestSeverityField;

            private RateReplyNotifications notificationsField;

            private RateReplyTransactionDetail transactionDetailField;

            private RateReplyVersion versionField;

            private RateReplyRateReplyDetails[] rateReplyDetailsField;

            /// <remarks/>
            public string HighestSeverity
            {
                get
                {
                    return this.highestSeverityField;
                }
                set
                {
                    this.highestSeverityField = value;
                }
            }

            /// <remarks/>
            public RateReplyNotifications Notifications
            {
                get
                {
                    return this.notificationsField;
                }
                set
                {
                    this.notificationsField = value;
                }
            }

            /// <remarks/>
            public RateReplyTransactionDetail TransactionDetail
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
            public RateReplyVersion Version
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
            [System.Xml.Serialization.XmlElementAttribute("RateReplyDetails")]
            public RateReplyRateReplyDetails[] RateReplyDetails
            {
                get
                {
                    return this.rateReplyDetailsField;
                }
                set
                {
                    this.rateReplyDetailsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyNotifications
        {

            private string severityField;

            private string sourceField;

            private string codeField;

            private string messageField;

            private string localizedMessageField;

            /// <remarks/>
            public string Severity
            {
                get
                {
                    return this.severityField;
                }
                set
                {
                    this.severityField = value;
                }
            }

            /// <remarks/>
            public string Source
            {
                get
                {
                    return this.sourceField;
                }
                set
                {
                    this.sourceField = value;
                }
            }

            /// <remarks/>
            public string Code
            {
                get
                {
                    return this.codeField;
                }
                set
                {
                    this.codeField = value;
                }
            }

            /// <remarks/>
            public string Message
            {
                get
                {
                    return this.messageField;
                }
                set
                {
                    this.messageField = value;
                }
            }

            /// <remarks/>
            public string LocalizedMessage
            {
                get
                {
                    return this.localizedMessageField;
                }
                set
                {
                    this.localizedMessageField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyTransactionDetail
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
        public partial class RateReplyVersion
        {

            private string serviceIdField;

            private string majorField;

            private string stringermediateField;

            private string minorField;

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
            public string Major
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
            public string Intermediate
            {
                get
                {
                    return this.stringermediateField;
                }
                set
                {
                    this.stringermediateField = value;
                }
            }

            /// <remarks/>
            public string Minor
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
        public partial class RateReplyRateReplyDetails
        {

            private string serviceTypeField;

            private RateReplyRateReplyDetailsServiceDescription serviceDescriptionField;

            private string packagingTypeField;

            private string deliveryStationField;

            private string deliveryDayOfWeekField;

            private System.DateTime deliveryTimestampField;

            private RateReplyRateReplyDetailsCommitDetails commitDetailsField;

            private string destinationAirportIdField;

            private bool ineligibleForMoneyBackGuaranteeField;

            private string originServiceAreaField;

            private string destinationServiceAreaField;

            private string signatureOptionField;

            private string actualRateTypeField;

            private RateReplyRateReplyDetailsRatedShipmentDetails[] ratedShipmentDetailsField;

            /// <remarks/>
            public string ServiceType
            {
                get
                {
                    return this.serviceTypeField;
                }
                set
                {
                    this.serviceTypeField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsServiceDescription ServiceDescription
            {
                get
                {
                    return this.serviceDescriptionField;
                }
                set
                {
                    this.serviceDescriptionField = value;
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
            public string DeliveryStation
            {
                get
                {
                    return this.deliveryStationField;
                }
                set
                {
                    this.deliveryStationField = value;
                }
            }

            /// <remarks/>
            public string DeliveryDayOfWeek
            {
                get
                {
                    return this.deliveryDayOfWeekField;
                }
                set
                {
                    this.deliveryDayOfWeekField = value;
                }
            }

            /// <remarks/>
            public System.DateTime DeliveryTimestamp
            {
                get
                {
                    return this.deliveryTimestampField;
                }
                set
                {
                    this.deliveryTimestampField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsCommitDetails CommitDetails
            {
                get
                {
                    return this.commitDetailsField;
                }
                set
                {
                    this.commitDetailsField = value;
                }
            }

            /// <remarks/>
            public string DestinationAirportId
            {
                get
                {
                    return this.destinationAirportIdField;
                }
                set
                {
                    this.destinationAirportIdField = value;
                }
            }

            /// <remarks/>
            public bool IneligibleForMoneyBackGuarantee
            {
                get
                {
                    return this.ineligibleForMoneyBackGuaranteeField;
                }
                set
                {
                    this.ineligibleForMoneyBackGuaranteeField = value;
                }
            }

            /// <remarks/>
            public string OriginServiceArea
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
            public string DestinationServiceArea
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
            public string SignatureOption
            {
                get
                {
                    return this.signatureOptionField;
                }
                set
                {
                    this.signatureOptionField = value;
                }
            }

            /// <remarks/>
            public string ActualRateType
            {
                get
                {
                    return this.actualRateTypeField;
                }
                set
                {
                    this.actualRateTypeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("RatedShipmentDetails")]
            public RateReplyRateReplyDetailsRatedShipmentDetails[] RatedShipmentDetails
            {
                get
                {
                    return this.ratedShipmentDetailsField;
                }
                set
                {
                    this.ratedShipmentDetailsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsServiceDescription
        {

            private string serviceTypeField;

            private string codeField;

            private string descriptionField;

            private string astraDescriptionField;

            /// <remarks/>
            public string ServiceType
            {
                get
                {
                    return this.serviceTypeField;
                }
                set
                {
                    this.serviceTypeField = value;
                }
            }

            /// <remarks/>
            public string Code
            {
                get
                {
                    return this.codeField;
                }
                set
                {
                    this.codeField = value;
                }
            }

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

            /// <remarks/>
            public string AstraDescription
            {
                get
                {
                    return this.astraDescriptionField;
                }
                set
                {
                    this.astraDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsCommitDetails
        {

            private string serviceTypeField;

            private RateReplyRateReplyDetailsCommitDetailsServiceDescription serviceDescriptionField;

            private RateReplyRateReplyDetailsCommitDetailsDerivedOriginDetail derivedOriginDetailField;

            private RateReplyRateReplyDetailsCommitDetailsDerivedDestinationDetail derivedDestinationDetailField;

            private System.DateTime commitTimestampField;

            private string dayOfWeekField;

            private string destinationServiceAreaField;

            private string brokerToDestinationDaysField;

            private string documentContentField;

            /// <remarks/>
            public string ServiceType
            {
                get
                {
                    return this.serviceTypeField;
                }
                set
                {
                    this.serviceTypeField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsCommitDetailsServiceDescription ServiceDescription
            {
                get
                {
                    return this.serviceDescriptionField;
                }
                set
                {
                    this.serviceDescriptionField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsCommitDetailsDerivedOriginDetail DerivedOriginDetail
            {
                get
                {
                    return this.derivedOriginDetailField;
                }
                set
                {
                    this.derivedOriginDetailField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsCommitDetailsDerivedDestinationDetail DerivedDestinationDetail
            {
                get
                {
                    return this.derivedDestinationDetailField;
                }
                set
                {
                    this.derivedDestinationDetailField = value;
                }
            }

            /// <remarks/>
            public System.DateTime CommitTimestamp
            {
                get
                {
                    return this.commitTimestampField;
                }
                set
                {
                    this.commitTimestampField = value;
                }
            }

            /// <remarks/>
            public string DayOfWeek
            {
                get
                {
                    return this.dayOfWeekField;
                }
                set
                {
                    this.dayOfWeekField = value;
                }
            }

            /// <remarks/>
            public string DestinationServiceArea
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
            public string BrokerToDestinationDays
            {
                get
                {
                    return this.brokerToDestinationDaysField;
                }
                set
                {
                    this.brokerToDestinationDaysField = value;
                }
            }

            /// <remarks/>
            public string DocumentContent
            {
                get
                {
                    return this.documentContentField;
                }
                set
                {
                    this.documentContentField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsCommitDetailsServiceDescription
        {

            private string serviceTypeField;

            private string codeField;

            private string descriptionField;

            private string astraDescriptionField;

            /// <remarks/>
            public string ServiceType
            {
                get
                {
                    return this.serviceTypeField;
                }
                set
                {
                    this.serviceTypeField = value;
                }
            }

            /// <remarks/>
            public string Code
            {
                get
                {
                    return this.codeField;
                }
                set
                {
                    this.codeField = value;
                }
            }

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

            /// <remarks/>
            public string AstraDescription
            {
                get
                {
                    return this.astraDescriptionField;
                }
                set
                {
                    this.astraDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsCommitDetailsDerivedOriginDetail
        {

            private string countryCodeField;

            private string stateOrProvinceCodeField;

            private string postalCodeField;

            private string serviceAreaField;

            private string locationIdField;

            private string locationNumberField;

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
            public string ServiceArea
            {
                get
                {
                    return this.serviceAreaField;
                }
                set
                {
                    this.serviceAreaField = value;
                }
            }

            /// <remarks/>
            public string LocationId
            {
                get
                {
                    return this.locationIdField;
                }
                set
                {
                    this.locationIdField = value;
                }
            }

            /// <remarks/>
            public string LocationNumber
            {
                get
                {
                    return this.locationNumberField;
                }
                set
                {
                    this.locationNumberField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsCommitDetailsDerivedDestinationDetail
        {

            private string countryCodeField;

            private string stateOrProvinceCodeField;

            private string postalCodeField;

            private string serviceAreaField;

            private string locationIdField;

            private string locationNumberField;

            private string airportIdField;

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
            public string ServiceArea
            {
                get
                {
                    return this.serviceAreaField;
                }
                set
                {
                    this.serviceAreaField = value;
                }
            }

            /// <remarks/>
            public string LocationId
            {
                get
                {
                    return this.locationIdField;
                }
                set
                {
                    this.locationIdField = value;
                }
            }

            /// <remarks/>
            public string LocationNumber
            {
                get
                {
                    return this.locationNumberField;
                }
                set
                {
                    this.locationNumberField = value;
                }
            }

            /// <remarks/>
            public string AirportId
            {
                get
                {
                    return this.airportIdField;
                }
                set
                {
                    this.airportIdField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsRatedShipmentDetails
        {

            private RateReplyRateReplyDetailsRatedShipmentDetailsEffectiveNetDiscount effectiveNetDiscountField;

            private RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetail shipmentRateDetailField;

            /// <remarks/>
            public RateReplyRateReplyDetailsRatedShipmentDetailsEffectiveNetDiscount EffectiveNetDiscount
            {
                get
                {
                    return this.effectiveNetDiscountField;
                }
                set
                {
                    this.effectiveNetDiscountField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetail ShipmentRateDetail
            {
                get
                {
                    return this.shipmentRateDetailField;
                }
                set
                {
                    this.shipmentRateDetailField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsEffectiveNetDiscount
        {

            private string currencyField;

            private decimal amountField;

            /// <remarks/>
            public string Currency
            {
                get
                {
                    return this.currencyField;
                }
                set
                {
                    this.currencyField = value;
                }
            }

            /// <remarks/>
            public decimal Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetail
        {

            private string rateTypeField;

            private string rateScaleField;

            private string rateZoneField;

            private string pricingCodeField;

            private string ratedWeightMethodField;

            private RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailCurrencyExchangeRate currencyExchangeRateField;

            private string dimDivisorField;

            private string dimDivisorTypeField;

            private decimal fuelSurchargePercentField;

            private RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalBillingWeight totalBillingWeightField;

            private RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalDimWeight totalDimWeightField;

            private RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalBaseCharge totalBaseChargeField;

            private RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalFreightDiscounts totalFreightDiscountsField;

            private RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalNetFreight totalNetFreightField;

            private RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalSurcharges totalSurchargesField;

            private RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalNetFedExCharge totalNetFedExChargeField;

            private RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalTaxes totalTaxesField;

            private RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalNetCharge totalNetChargeField;

            private RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalRebates totalRebatesField;

            private RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalDutiesAndTaxes totalDutiesAndTaxesField;

            private RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalAncillaryFeesAndTaxes totalAncillaryFeesAndTaxesField;

            private RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalDutiesTaxesAndFees totalDutiesTaxesAndFeesField;

            private RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalNetChargeWithDutiesAndTaxes totalNetChargeWithDutiesAndTaxesField;

            private RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailFreightDiscounts[] freightDiscountsField;

            private RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailSurcharges surchargesField;

            private RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTaxes taxesField;

            /// <remarks/>
            public string RateType
            {
                get
                {
                    return this.rateTypeField;
                }
                set
                {
                    this.rateTypeField = value;
                }
            }

            /// <remarks/>
            public string RateScale
            {
                get
                {
                    return this.rateScaleField;
                }
                set
                {
                    this.rateScaleField = value;
                }
            }

            /// <remarks/>
            public string RateZone
            {
                get
                {
                    return this.rateZoneField;
                }
                set
                {
                    this.rateZoneField = value;
                }
            }

            /// <remarks/>
            public string PricingCode
            {
                get
                {
                    return this.pricingCodeField;
                }
                set
                {
                    this.pricingCodeField = value;
                }
            }

            /// <remarks/>
            public string RatedWeightMethod
            {
                get
                {
                    return this.ratedWeightMethodField;
                }
                set
                {
                    this.ratedWeightMethodField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailCurrencyExchangeRate CurrencyExchangeRate
            {
                get
                {
                    return this.currencyExchangeRateField;
                }
                set
                {
                    this.currencyExchangeRateField = value;
                }
            }

            /// <remarks/>
            public string DimDivisor
            {
                get
                {
                    return this.dimDivisorField;
                }
                set
                {
                    this.dimDivisorField = value;
                }
            }

            /// <remarks/>
            public string DimDivisorType
            {
                get
                {
                    return this.dimDivisorTypeField;
                }
                set
                {
                    this.dimDivisorTypeField = value;
                }
            }

            /// <remarks/>
            public decimal FuelSurchargePercent
            {
                get
                {
                    return this.fuelSurchargePercentField;
                }
                set
                {
                    this.fuelSurchargePercentField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalBillingWeight TotalBillingWeight
            {
                get
                {
                    return this.totalBillingWeightField;
                }
                set
                {
                    this.totalBillingWeightField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalDimWeight TotalDimWeight
            {
                get
                {
                    return this.totalDimWeightField;
                }
                set
                {
                    this.totalDimWeightField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalBaseCharge TotalBaseCharge
            {
                get
                {
                    return this.totalBaseChargeField;
                }
                set
                {
                    this.totalBaseChargeField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalFreightDiscounts TotalFreightDiscounts
            {
                get
                {
                    return this.totalFreightDiscountsField;
                }
                set
                {
                    this.totalFreightDiscountsField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalNetFreight TotalNetFreight
            {
                get
                {
                    return this.totalNetFreightField;
                }
                set
                {
                    this.totalNetFreightField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalSurcharges TotalSurcharges
            {
                get
                {
                    return this.totalSurchargesField;
                }
                set
                {
                    this.totalSurchargesField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalNetFedExCharge TotalNetFedExCharge
            {
                get
                {
                    return this.totalNetFedExChargeField;
                }
                set
                {
                    this.totalNetFedExChargeField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalTaxes TotalTaxes
            {
                get
                {
                    return this.totalTaxesField;
                }
                set
                {
                    this.totalTaxesField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalNetCharge TotalNetCharge
            {
                get
                {
                    return this.totalNetChargeField;
                }
                set
                {
                    this.totalNetChargeField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalRebates TotalRebates
            {
                get
                {
                    return this.totalRebatesField;
                }
                set
                {
                    this.totalRebatesField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalDutiesAndTaxes TotalDutiesAndTaxes
            {
                get
                {
                    return this.totalDutiesAndTaxesField;
                }
                set
                {
                    this.totalDutiesAndTaxesField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalAncillaryFeesAndTaxes TotalAncillaryFeesAndTaxes
            {
                get
                {
                    return this.totalAncillaryFeesAndTaxesField;
                }
                set
                {
                    this.totalAncillaryFeesAndTaxesField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalDutiesTaxesAndFees TotalDutiesTaxesAndFees
            {
                get
                {
                    return this.totalDutiesTaxesAndFeesField;
                }
                set
                {
                    this.totalDutiesTaxesAndFeesField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalNetChargeWithDutiesAndTaxes TotalNetChargeWithDutiesAndTaxes
            {
                get
                {
                    return this.totalNetChargeWithDutiesAndTaxesField;
                }
                set
                {
                    this.totalNetChargeWithDutiesAndTaxesField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("FreightDiscounts")]
            public RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailFreightDiscounts[] FreightDiscounts
            {
                get
                {
                    return this.freightDiscountsField;
                }
                set
                {
                    this.freightDiscountsField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailSurcharges Surcharges
            {
                get
                {
                    return this.surchargesField;
                }
                set
                {
                    this.surchargesField = value;
                }
            }

            /// <remarks/>
            public RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTaxes Taxes
            {
                get
                {
                    return this.taxesField;
                }
                set
                {
                    this.taxesField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailCurrencyExchangeRate
        {

            private string fromCurrencyField;

            private string stringoCurrencyField;

            private decimal rateField;

            /// <remarks/>
            public string FromCurrency
            {
                get
                {
                    return this.fromCurrencyField;
                }
                set
                {
                    this.fromCurrencyField = value;
                }
            }

            /// <remarks/>
            public string IntoCurrency
            {
                get
                {
                    return this.stringoCurrencyField;
                }
                set
                {
                    this.stringoCurrencyField = value;
                }
            }

            /// <remarks/>
            public decimal Rate
            {
                get
                {
                    return this.rateField;
                }
                set
                {
                    this.rateField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalBillingWeight
        {

            private string unitsField;

            private decimal valueField;

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
            public decimal Value
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
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalDimWeight
        {

            private string unitsField;

            private decimal valueField;

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
            public decimal Value
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
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalBaseCharge
        {

            private string currencyField;

            private decimal amountField;

            /// <remarks/>
            public string Currency
            {
                get
                {
                    return this.currencyField;
                }
                set
                {
                    this.currencyField = value;
                }
            }

            /// <remarks/>
            public decimal Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalFreightDiscounts
        {

            private string currencyField;

            private decimal amountField;

            /// <remarks/>
            public string Currency
            {
                get
                {
                    return this.currencyField;
                }
                set
                {
                    this.currencyField = value;
                }
            }

            /// <remarks/>
            public decimal Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalNetFreight
        {

            private string currencyField;

            private decimal amountField;

            /// <remarks/>
            public string Currency
            {
                get
                {
                    return this.currencyField;
                }
                set
                {
                    this.currencyField = value;
                }
            }

            /// <remarks/>
            public decimal Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalSurcharges
        {

            private string currencyField;

            private decimal amountField;

            /// <remarks/>
            public string Currency
            {
                get
                {
                    return this.currencyField;
                }
                set
                {
                    this.currencyField = value;
                }
            }

            /// <remarks/>
            public decimal Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalNetFedExCharge
        {

            private string currencyField;

            private decimal amountField;

            /// <remarks/>
            public string Currency
            {
                get
                {
                    return this.currencyField;
                }
                set
                {
                    this.currencyField = value;
                }
            }

            /// <remarks/>
            public decimal Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalTaxes
        {

            private string currencyField;

            private decimal amountField;

            /// <remarks/>
            public string Currency
            {
                get
                {
                    return this.currencyField;
                }
                set
                {
                    this.currencyField = value;
                }
            }

            /// <remarks/>
            public decimal Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalNetCharge
        {

            private string currencyField;

            private decimal amountField;

            /// <remarks/>
            public string Currency
            {
                get
                {
                    return this.currencyField;
                }
                set
                {
                    this.currencyField = value;
                }
            }

            /// <remarks/>
            public decimal Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalRebates
        {

            private string currencyField;

            private decimal amountField;

            /// <remarks/>
            public string Currency
            {
                get
                {
                    return this.currencyField;
                }
                set
                {
                    this.currencyField = value;
                }
            }

            /// <remarks/>
            public decimal Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalDutiesAndTaxes
        {

            private string currencyField;

            private decimal amountField;

            /// <remarks/>
            public string Currency
            {
                get
                {
                    return this.currencyField;
                }
                set
                {
                    this.currencyField = value;
                }
            }

            /// <remarks/>
            public decimal Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalAncillaryFeesAndTaxes
        {

            private string currencyField;

            private decimal amountField;

            /// <remarks/>
            public string Currency
            {
                get
                {
                    return this.currencyField;
                }
                set
                {
                    this.currencyField = value;
                }
            }

            /// <remarks/>
            public decimal Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalDutiesTaxesAndFees
        {

            private string currencyField;

            private decimal amountField;

            /// <remarks/>
            public string Currency
            {
                get
                {
                    return this.currencyField;
                }
                set
                {
                    this.currencyField = value;
                }
            }

            /// <remarks/>
            public decimal Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTotalNetChargeWithDutiesAndTaxes
        {

            private string currencyField;

            private decimal amountField;

            /// <remarks/>
            public string Currency
            {
                get
                {
                    return this.currencyField;
                }
                set
                {
                    this.currencyField = value;
                }
            }

            /// <remarks/>
            public decimal Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailFreightDiscounts
        {

            private string rateDiscountTypeField;

            private string descriptionField;

            private RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailFreightDiscountsAmount amountField;

            private decimal percentField;

            /// <remarks/>
            public string RateDiscountType
            {
                get
                {
                    return this.rateDiscountTypeField;
                }
                set
                {
                    this.rateDiscountTypeField = value;
                }
            }

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

            /// <remarks/>
            public RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailFreightDiscountsAmount Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }

            /// <remarks/>
            public decimal Percent
            {
                get
                {
                    return this.percentField;
                }
                set
                {
                    this.percentField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailFreightDiscountsAmount
        {

            private string currencyField;

            private decimal amountField;

            /// <remarks/>
            public string Currency
            {
                get
                {
                    return this.currencyField;
                }
                set
                {
                    this.currencyField = value;
                }
            }

            /// <remarks/>
            public decimal Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailSurcharges
        {

            private string surchargeTypeField;

            private string descriptionField;

            private RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailSurchargesAmount amountField;

            /// <remarks/>
            public string SurchargeType
            {
                get
                {
                    return this.surchargeTypeField;
                }
                set
                {
                    this.surchargeTypeField = value;
                }
            }

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

            /// <remarks/>
            public RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailSurchargesAmount Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailSurchargesAmount
        {

            private string currencyField;

            private decimal amountField;

            /// <remarks/>
            public string Currency
            {
                get
                {
                    return this.currencyField;
                }
                set
                {
                    this.currencyField = value;
                }
            }

            /// <remarks/>
            public decimal Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTaxes
        {

            private string taxTypeField;

            private string descriptionField;

            private RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTaxesAmount amountField;

            /// <remarks/>
            public string TaxType
            {
                get
                {
                    return this.taxTypeField;
                }
                set
                {
                    this.taxTypeField = value;
                }
            }

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

            /// <remarks/>
            public RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTaxesAmount Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://fedex.com/ws/rate/v24")]
        public partial class RateReplyRateReplyDetailsRatedShipmentDetailsShipmentRateDetailTaxesAmount
        {

            private string currencyField;

            private decimal amountField;

            /// <remarks/>
            public string Currency
            {
                get
                {
                    return this.currencyField;
                }
                set
                {
                    this.currencyField = value;
                }
            }

            /// <remarks/>
            public decimal Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }
        }

    }
}
