using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore.DHLHepler
{
    public class DHLPickupResponse
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
        public partial class PickupResponse
        {

            private Response responseField;

            private Note noteField;

            private uint confirmationNumberField;

            private string readyByTimeField;

            private System.DateTime nextPickupDateField;

            private string originSvcAreaField;

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
            public uint ConfirmationNumber
            {
                get
                {
                    return this.confirmationNumberField;
                }
                set
                {
                    this.confirmationNumberField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public string ReadyByTime
            {
                get
                {
                    return this.readyByTimeField;
                }
                set
                {
                    this.readyByTimeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "", DataType = "date")]
            public System.DateTime NextPickupDate
            {
                get
                {
                    return this.nextPickupDateField;
                }
                set
                {
                    this.nextPickupDateField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public string OriginSvcArea
            {
                get
                {
                    return this.originSvcAreaField;
                }
                set
                {
                    this.originSvcAreaField = value;
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


    }
}
