using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore.DHLHepler
{
    public class AllTrackResponse
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
        public partial class TrackingResponse
        {

            private Response responseField;

            private AWBInfo aWBInfoField;

            private string languageCodeField;

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
            public AWBInfo AWBInfo
            {
                get
                {
                    return this.aWBInfoField;
                }
                set
                {
                    this.aWBInfoField = value;
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
        public partial class AWBInfo
        {

            private ulong aWBNumberField;

            private AWBInfoStatus statusField;

            private AWBInfoShipmentInfo shipmentInfoField;

            /// <remarks/>
            public ulong AWBNumber
            {
                get
                {
                    return this.aWBNumberField;
                }
                set
                {
                    this.aWBNumberField = value;
                }
            }

            /// <remarks/>
            public AWBInfoStatus Status
            {
                get
                {
                    return this.statusField;
                }
                set
                {
                    this.statusField = value;
                }
            }

            /// <remarks/>
            public AWBInfoShipmentInfo ShipmentInfo
            {
                get
                {
                    return this.shipmentInfoField;
                }
                set
                {
                    this.shipmentInfoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class AWBInfoStatus
        {

            private string actionStatusField;

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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class AWBInfoShipmentInfo
        {

            private AWBInfoShipmentInfoOriginServiceArea originServiceAreaField;

            private AWBInfoShipmentInfoDestinationServiceArea destinationServiceAreaField;

            private string shipperNameField;

            private uint shipperAccountNumberField;

            private string consigneeNameField;

            private System.DateTime shipmentDateField;

            private byte piecesField;

            private decimal weightField;

            private string weightUnitField;

            private string globalProductCodeField;

            private string shipmentDescField;

            private string dlvyNotificationFlagField;

            private AWBInfoShipmentInfoShipper shipperField;

            private AWBInfoShipmentInfoConsignee consigneeField;

            private AWBInfoShipmentInfoShipperReference shipperReferenceField;

            private AWBInfoShipmentInfoShipmentEvent shipmentEventField;

            /// <remarks/>
            public AWBInfoShipmentInfoOriginServiceArea OriginServiceArea
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
            public AWBInfoShipmentInfoDestinationServiceArea DestinationServiceArea
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
            public string ShipperName
            {
                get
                {
                    return this.shipperNameField;
                }
                set
                {
                    this.shipperNameField = value;
                }
            }

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
            public string ConsigneeName
            {
                get
                {
                    return this.consigneeNameField;
                }
                set
                {
                    this.consigneeNameField = value;
                }
            }

            /// <remarks/>
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
            public byte Pieces
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
            public string ShipmentDesc
            {
                get
                {
                    return this.shipmentDescField;
                }
                set
                {
                    this.shipmentDescField = value;
                }
            }

            /// <remarks/>
            public string DlvyNotificationFlag
            {
                get
                {
                    return this.dlvyNotificationFlagField;
                }
                set
                {
                    this.dlvyNotificationFlagField = value;
                }
            }

            /// <remarks/>
            public AWBInfoShipmentInfoShipper Shipper
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
            public AWBInfoShipmentInfoConsignee Consignee
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
            public AWBInfoShipmentInfoShipperReference ShipperReference
            {
                get
                {
                    return this.shipperReferenceField;
                }
                set
                {
                    this.shipperReferenceField = value;
                }
            }

            /// <remarks/>
            public AWBInfoShipmentInfoShipmentEvent ShipmentEvent
            {
                get
                {
                    return this.shipmentEventField;
                }
                set
                {
                    this.shipmentEventField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class AWBInfoShipmentInfoOriginServiceArea
        {

            private string serviceAreaCodeField;

            private string descriptionField;

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

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class AWBInfoShipmentInfoDestinationServiceArea
        {

            private string serviceAreaCodeField;

            private string descriptionField;

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

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class AWBInfoShipmentInfoShipper
        {

            private string cityField;

            private string countryCodeField;

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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class AWBInfoShipmentInfoConsignee
        {

            private string cityField;

            private string divisionCodeField;

            private string countryCodeField;

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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class AWBInfoShipmentInfoShipperReference
        {

            private uint referenceIDField;

            /// <remarks/>
            public uint ReferenceID
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
        public partial class AWBInfoShipmentInfoShipmentEvent
        {

            private System.DateTime dateField;

            private System.DateTime timeField;

            private AWBInfoShipmentInfoShipmentEventServiceEvent serviceEventField;

            private object signatoryField;

            private AWBInfoShipmentInfoShipmentEventServiceArea serviceAreaField;

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
            [System.Xml.Serialization.XmlElementAttribute(DataType = "time")]
            public System.DateTime Time
            {
                get
                {
                    return this.timeField;
                }
                set
                {
                    this.timeField = value;
                }
            }

            /// <remarks/>
            public AWBInfoShipmentInfoShipmentEventServiceEvent ServiceEvent
            {
                get
                {
                    return this.serviceEventField;
                }
                set
                {
                    this.serviceEventField = value;
                }
            }

            /// <remarks/>
            public object Signatory
            {
                get
                {
                    return this.signatoryField;
                }
                set
                {
                    this.signatoryField = value;
                }
            }

            /// <remarks/>
            public AWBInfoShipmentInfoShipmentEventServiceArea ServiceArea
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class AWBInfoShipmentInfoShipmentEventServiceEvent
        {

            private string eventCodeField;

            private string descriptionField;

            /// <remarks/>
            public string EventCode
            {
                get
                {
                    return this.eventCodeField;
                }
                set
                {
                    this.eventCodeField = value;
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class AWBInfoShipmentInfoShipmentEventServiceArea
        {

            private string serviceAreaCodeField;

            private string descriptionField;

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
