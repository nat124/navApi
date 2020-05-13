using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore.DHLHepler
{
    //public class responseCapability
    //{
    //    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
    //    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
    //    public partial class DCTResponse
    //    {

    //        private GetQuoteResponse GetQuoteResponseField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
    //        public GetQuoteResponse GetQuoteResponse
    //        {
    //            get
    //            {
    //                return this.GetQuoteResponseField;
    //            }
    //            set
    //            {
    //                this.GetQuoteResponseField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    //    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    //    public partial class GetQuoteResponse
    //    {

    //        private GetQuoteResponseResponse responseField;

    //        private GetQuoteResponseBkgDetails bkgDetailsField;

    //        private GetQuoteResponseSrv[] srvsField;

    //        /// <remarks/>
    //        public GetQuoteResponseResponse Response
    //        {
    //            get
    //            {
    //                return this.responseField;
    //            }
    //            set
    //            {
    //                this.responseField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public GetQuoteResponseBkgDetails BkgDetails
    //        {
    //            get
    //            {
    //                return this.bkgDetailsField;
    //            }
    //            set
    //            {
    //                this.bkgDetailsField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlArrayItemAttribute("Srv", IsNullable = false)]
    //        public GetQuoteResponseSrv[] Srvs
    //        {
    //            get
    //            {
    //                return this.srvsField;
    //            }
    //            set
    //            {
    //                this.srvsField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    //    public partial class GetQuoteResponseResponse
    //    {

    //        private GetQuoteResponseResponseServiceHeader serviceHeaderField;

    //        /// <remarks/>
    //        public GetQuoteResponseResponseServiceHeader ServiceHeader
    //        {
    //            get
    //            {
    //                return this.serviceHeaderField;
    //            }
    //            set
    //            {
    //                this.serviceHeaderField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    //    public partial class GetQuoteResponseResponseServiceHeader
    //    {

    //        private System.DateTime messageTimeField;

    //        private string siteIDField;

    //        /// <remarks/>
    //        public System.DateTime MessageTime
    //        {
    //            get
    //            {
    //                return this.messageTimeField;
    //            }
    //            set
    //            {
    //                this.messageTimeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public string SiteID
    //        {
    //            get
    //            {
    //                return this.siteIDField;
    //            }
    //            set
    //            {
    //                this.siteIDField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    //    public partial class GetQuoteResponseBkgDetails
    //    {

    //        private GetQuoteResponseBkgDetailsOriginServiceArea originServiceAreaField;

    //        private GetQuoteResponseBkgDetailsDestinationServiceArea destinationServiceAreaField;

    //        private GetQuoteResponseBkgDetailsQtdShp[] qtdShpField;

    //        /// <remarks/>
    //        public GetQuoteResponseBkgDetailsOriginServiceArea OriginServiceArea
    //        {
    //            get
    //            {
    //                return this.originServiceAreaField;
    //            }
    //            set
    //            {
    //                this.originServiceAreaField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public GetQuoteResponseBkgDetailsDestinationServiceArea DestinationServiceArea
    //        {
    //            get
    //            {
    //                return this.destinationServiceAreaField;
    //            }
    //            set
    //            {
    //                this.destinationServiceAreaField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlElementAttribute("QtdShp")]
    //        public GetQuoteResponseBkgDetailsQtdShp[] QtdShp
    //        {
    //            get
    //            {
    //                return this.qtdShpField;
    //            }
    //            set
    //            {
    //                this.qtdShpField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    //    public partial class GetQuoteResponseBkgDetailsOriginServiceArea
    //    {

    //        private string facilityCodeField;

    //        private string serviceAreaCodeField;

    //        /// <remarks/>
    //        public string FacilityCode
    //        {
    //            get
    //            {
    //                return this.facilityCodeField;
    //            }
    //            set
    //            {
    //                this.facilityCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public string ServiceAreaCode
    //        {
    //            get
    //            {
    //                return this.serviceAreaCodeField;
    //            }
    //            set
    //            {
    //                this.serviceAreaCodeField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    //    public partial class GetQuoteResponseBkgDetailsDestinationServiceArea
    //    {

    //        private string facilityCodeField;

    //        private string serviceAreaCodeField;

    //        /// <remarks/>
    //        public string FacilityCode
    //        {
    //            get
    //            {
    //                return this.facilityCodeField;
    //            }
    //            set
    //            {
    //                this.facilityCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public string ServiceAreaCode
    //        {
    //            get
    //            {
    //                return this.serviceAreaCodeField;
    //            }
    //            set
    //            {
    //                this.serviceAreaCodeField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    //    public partial class GetQuoteResponseBkgDetailsQtdShp
    //    {

    //        private string globalProductCodeField;

    //        private string localProductCodeField;

    //        private string productShortNameField;

    //        private string localProductNameField;

    //        private string networkTypeCodeField;

    //        private string pOfferedCustAgreementField;

    //        private string transIndField;

    //        private System.DateTime pickupDateField;

    //        private string pickupCutoffTimeField;

    //        private string bookingTimeField;

    //        private byte totalTransitDaysField;

    //        private byte pickupPostalLocAddDaysField;

    //        private byte deliveryPostalLocAddDaysField;

    //        private object pickupNonDHLCourierCodeField;

    //        private object deliveryNonDHLCourierCodeField;

    //        private System.DateTime deliveryDateField;

    //        private string deliveryTimeField;

    //        private decimal dimensionalWeightField;

    //        private string weightUnitField;

    //        private byte pickupDayOfWeekNumField;

    //        private byte destinationDayOfWeekNumField;

    //        /// <remarks/>
    //        public string GlobalProductCode
    //        {
    //            get
    //            {
    //                return this.globalProductCodeField;
    //            }
    //            set
    //            {
    //                this.globalProductCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public string LocalProductCode
    //        {
    //            get
    //            {
    //                return this.localProductCodeField;
    //            }
    //            set
    //            {
    //                this.localProductCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public string ProductShortName
    //        {
    //            get
    //            {
    //                return this.productShortNameField;
    //            }
    //            set
    //            {
    //                this.productShortNameField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public string LocalProductName
    //        {
    //            get
    //            {
    //                return this.localProductNameField;
    //            }
    //            set
    //            {
    //                this.localProductNameField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public string NetworkTypeCode
    //        {
    //            get
    //            {
    //                return this.networkTypeCodeField;
    //            }
    //            set
    //            {
    //                this.networkTypeCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public string POfferedCustAgreement
    //        {
    //            get
    //            {
    //                return this.pOfferedCustAgreementField;
    //            }
    //            set
    //            {
    //                this.pOfferedCustAgreementField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public string TransInd
    //        {
    //            get
    //            {
    //                return this.transIndField;
    //            }
    //            set
    //            {
    //                this.transIndField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
    //        public System.DateTime PickupDate
    //        {
    //            get
    //            {
    //                return this.pickupDateField;
    //            }
    //            set
    //            {
    //                this.pickupDateField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
    //        public string PickupCutoffTime
    //        {
    //            get
    //            {
    //                return this.pickupCutoffTimeField;
    //            }
    //            set
    //            {
    //                this.pickupCutoffTimeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
    //        public string BookingTime
    //        {
    //            get
    //            {
    //                return this.bookingTimeField;
    //            }
    //            set
    //            {
    //                this.bookingTimeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public byte TotalTransitDays
    //        {
    //            get
    //            {
    //                return this.totalTransitDaysField;
    //            }
    //            set
    //            {
    //                this.totalTransitDaysField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public byte PickupPostalLocAddDays
    //        {
    //            get
    //            {
    //                return this.pickupPostalLocAddDaysField;
    //            }
    //            set
    //            {
    //                this.pickupPostalLocAddDaysField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public byte DeliveryPostalLocAddDays
    //        {
    //            get
    //            {
    //                return this.deliveryPostalLocAddDaysField;
    //            }
    //            set
    //            {
    //                this.deliveryPostalLocAddDaysField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public object PickupNonDHLCourierCode
    //        {
    //            get
    //            {
    //                return this.pickupNonDHLCourierCodeField;
    //            }
    //            set
    //            {
    //                this.pickupNonDHLCourierCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public object DeliveryNonDHLCourierCode
    //        {
    //            get
    //            {
    //                return this.deliveryNonDHLCourierCodeField;
    //            }
    //            set
    //            {
    //                this.deliveryNonDHLCourierCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
    //        public System.DateTime DeliveryDate
    //        {
    //            get
    //            {
    //                return this.deliveryDateField;
    //            }
    //            set
    //            {
    //                this.deliveryDateField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
    //        public string DeliveryTime
    //        {
    //            get
    //            {
    //                return this.deliveryTimeField;
    //            }
    //            set
    //            {
    //                this.deliveryTimeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public decimal DimensionalWeight
    //        {
    //            get
    //            {
    //                return this.dimensionalWeightField;
    //            }
    //            set
    //            {
    //                this.dimensionalWeightField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public string WeightUnit
    //        {
    //            get
    //            {
    //                return this.weightUnitField;
    //            }
    //            set
    //            {
    //                this.weightUnitField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public byte PickupDayOfWeekNum
    //        {
    //            get
    //            {
    //                return this.pickupDayOfWeekNumField;
    //            }
    //            set
    //            {
    //                this.pickupDayOfWeekNumField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public byte DestinationDayOfWeekNum
    //        {
    //            get
    //            {
    //                return this.destinationDayOfWeekNumField;
    //            }
    //            set
    //            {
    //                this.destinationDayOfWeekNumField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    //    public partial class GetQuoteResponseSrv
    //    {

    //        private string globalProductCodeField;

    //        private GetQuoteResponseSrvMrkSrv mrkSrvField;

    //        /// <remarks/>
    //        public string GlobalProductCode
    //        {
    //            get
    //            {
    //                return this.globalProductCodeField;
    //            }
    //            set
    //            {
    //                this.globalProductCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public GetQuoteResponseSrvMrkSrv MrkSrv
    //        {
    //            get
    //            {
    //                return this.mrkSrvField;
    //            }
    //            set
    //            {
    //                this.mrkSrvField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    //    public partial class GetQuoteResponseSrvMrkSrv
    //    {

    //        private string localProductCodeField;

    //        private string productShortNameField;

    //        private string localProductNameField;

    //        private string productDescField;

    //        private string networkTypeCodeField;

    //        private string pOfferedCustAgreementField;

    //        private string transIndField;

    //        /// <remarks/>
    //        public string LocalProductCode
    //        {
    //            get
    //            {
    //                return this.localProductCodeField;
    //            }
    //            set
    //            {
    //                this.localProductCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public string ProductShortName
    //        {
    //            get
    //            {
    //                return this.productShortNameField;
    //            }
    //            set
    //            {
    //                this.productShortNameField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public string LocalProductName
    //        {
    //            get
    //            {
    //                return this.localProductNameField;
    //            }
    //            set
    //            {
    //                this.localProductNameField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public string ProductDesc
    //        {
    //            get
    //            {
    //                return this.productDescField;
    //            }
    //            set
    //            {
    //                this.productDescField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public string NetworkTypeCode
    //        {
    //            get
    //            {
    //                return this.networkTypeCodeField;
    //            }
    //            set
    //            {
    //                this.networkTypeCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public string POfferedCustAgreement
    //        {
    //            get
    //            {
    //                return this.pOfferedCustAgreementField;
    //            }
    //            set
    //            {
    //                this.pOfferedCustAgreementField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public string TransInd
    //        {
    //            get
    //            {
    //                return this.transIndField;
    //            }
    //            set
    //            {
    //                this.transIndField = value;
    //            }
    //        }
    //    }

    //}

}
