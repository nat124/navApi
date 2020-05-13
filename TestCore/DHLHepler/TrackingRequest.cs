using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore.DHLHepler
{

    public class TrackingRequestAllCheckPoints
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
        public partial class KnownTrackingRequest
        {

            private Request requestField;

            private string languageCodeField;

            private uint aWBNumberField;

            private string levelOfDetailsField;

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
            public uint AWBNumber
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
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public string LevelOfDetails
            {
                get
                {
                    return this.levelOfDetailsField;
                }
                set
                {
                    this.levelOfDetailsField = value;
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

            private System.DateTime messageTimeField;

            private string messageReferenceField;

            private string siteIDField;

            private string passwordField;

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
            [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
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

    }

}
