using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
//    public class capabilityRequest
//    {

//        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
//        /// <remarks/>
//        [System.SerializableAttribute()]
//        [System.ComponentModel.DesignerCategoryAttribute("code")]
//        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
//        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
//        public partial class DCTRequest
//        {

//            private GetCapability getCapabilityField;

//            /// <remarks/>
//            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
//            public GetCapability GetCapability
//            {
//                get
//                {
//                    return this.getCapabilityField;
//                }
//                set
//                {
//                    this.getCapabilityField = value;
//                }
//            }
//        }

//        /// <remarks/>
//        [System.SerializableAttribute()]
//        [System.ComponentModel.DesignerCategoryAttribute("code")]
//        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
//        public partial class GetCapability
//        {

//            private GetCapabilityRequest requestField;

//            private GetCapabilityFrom fromField;

//            private GetCapabilityBkgDetails bkgDetailsField;

//            private GetCapabilityTO toField;

//            /// <remarks/>
//            public GetCapabilityRequest Request
//            {
//                get
//                {
//                    return this.requestField;
//                }
//                set
//                {
//                    this.requestField = value;
//                }
//            }

//            /// <remarks/>
//            public GetCapabilityFrom From
//            {
//                get
//                {
//                    return this.fromField;
//                }
//                set
//                {
//                    this.fromField = value;
//                }
//            }

//            /// <remarks/>
//            public GetCapabilityBkgDetails BkgDetails
//            {
//                get
//                {
//                    return this.bkgDetailsField;
//                }
//                set
//                {
//                    this.bkgDetailsField = value;
//                }
//            }

//            /// <remarks/>
//            public GetCapabilityTO To
//            {
//                get
//                {
//                    return this.toField;
//                }
//                set
//                {
//                    this.toField = value;
//                }
//            }
//        }

//        /// <remarks/>
//        [System.SerializableAttribute()]
//        [System.ComponentModel.DesignerCategoryAttribute("code")]
//        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//        public partial class GetCapabilityRequest
//        {

//            private GetCapabilityRequestServiceHeader serviceHeaderField;

//            /// <remarks/>
//            public GetCapabilityRequestServiceHeader ServiceHeader
//            {
//                get
//                {
//                    return this.serviceHeaderField;
//                }
//                set
//                {
//                    this.serviceHeaderField = value;
//                }
//            }
//        }

//        /// <remarks/>
//        [System.SerializableAttribute()]
//        [System.ComponentModel.DesignerCategoryAttribute("code")]
//        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//        public partial class GetCapabilityRequestServiceHeader
//        {

//            private string siteIDField;

//            private string passwordField;

//            /// <remarks/>
//            public string SiteID
//            {
//                get
//                {
//                    return this.siteIDField;
//                }
//                set
//                {
//                    this.siteIDField = value;
//                }
//            }

//            /// <remarks/>
//            public string Password
//            {
//                get
//                {
//                    return this.passwordField;
//                }
//                set
//                {
//                    this.passwordField = value;
//                }
//            }
//        }

//        /// <remarks/>
//        [System.SerializableAttribute()]
//        [System.ComponentModel.DesignerCategoryAttribute("code")]
//        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//        public partial class GetCapabilityFrom
//        {

//            private string countryCodeField;

//            private ushort postalcodeField;

//            /// <remarks/>
//            public string CountryCode
//            {
//                get
//                {
//                    return this.countryCodeField;
//                }
//                set
//                {
//                    this.countryCodeField = value;
//                }
//            }

//            /// <remarks/>
//            public ushort Postalcode
//            {
//                get
//                {
//                    return this.postalcodeField;
//                }
//                set
//                {
//                    this.postalcodeField = value;
//                }
//            }
//        }

//        /// <remarks/>
//        [System.SerializableAttribute()]
//        [System.ComponentModel.DesignerCategoryAttribute("code")]
//        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//        public partial class GetCapabilityBkgDetails
//        {

//            private string paymentCountryCodeField;

//            private System.DateTime dateField;

//            private string readyTimeField;

//            private string dimensionUnitField;

//            private string weightUnitField;

//            private GetCapabilityBkgDetailsPieces piecesField;

//            private string isDutiableField;

//            /// <remarks/>
//            public string PaymentCountryCode
//            {
//                get
//                {
//                    return this.paymentCountryCodeField;
//                }
//                set
//                {
//                    this.paymentCountryCodeField = value;
//                }
//            }

//            /// <remarks/>
//            [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
//            public System.DateTime Date
//            {
//                get
//                {
//                    return this.dateField;
//                }
//                set
//                {
//                    this.dateField = value;
//                }
//            }

//            /// <remarks/>
//            [System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
//            public string ReadyTime
//            {
//                get
//                {
//                    return this.readyTimeField;
//                }
//                set
//                {
//                    this.readyTimeField = value;
//                }
//            }

//            /// <remarks/>
//            public string DimensionUnit
//            {
//                get
//                {
//                    return this.dimensionUnitField;
//                }
//                set
//                {
//                    this.dimensionUnitField = value;
//                }
//            }

//            /// <remarks/>
//            public string WeightUnit
//            {
//                get
//                {
//                    return this.weightUnitField;
//                }
//                set
//                {
//                    this.weightUnitField = value;
//                }
//            }

//            /// <remarks/>
//            public GetCapabilityBkgDetailsPieces Pieces
//            {
//                get
//                {
//                    return this.piecesField;
//                }
//                set
//                {
//                    this.piecesField = value;
//                }
//            }

//            /// <remarks/>
//            public string IsDutiable
//            {
//                get
//                {
//                    return this.isDutiableField;
//                }
//                set
//                {
//                    this.isDutiableField = value;
//                }
//            }
//        }

//        /// <remarks/>
//        [System.SerializableAttribute()]
//        [System.ComponentModel.DesignerCategoryAttribute("code")]
//        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//        public partial class GetCapabilityBkgDetailsPieces
//        {

//            private GetCapabilityBkgDetailsPiecesPiece pieceField;

//            /// <remarks/>
//            public GetCapabilityBkgDetailsPiecesPiece Piece
//            {
//                get
//                {
//                    return this.pieceField;
//                }
//                set
//                {
//                    this.pieceField = value;
//                }
//            }
//        }

//        /// <remarks/>
//        [System.SerializableAttribute()]
//        [System.ComponentModel.DesignerCategoryAttribute("code")]
//        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//        public partial class GetCapabilityBkgDetailsPiecesPiece
//        {

//            private byte pieceIDField;

//            private string packageTypeCodeField;

//            private byte heightField;

//            private byte depthField;

//            private byte widthField;

//            private decimal weightField;

//            /// <remarks/>
//            public byte PieceID
//            {
//                get
//                {
//                    return this.pieceIDField;
//                }
//                set
//                {
//                    this.pieceIDField = value;
//                }
//            }

//            /// <remarks/>
//            public string PackageTypeCode
//            {
//                get
//                {
//                    return this.packageTypeCodeField;
//                }
//                set
//                {
//                    this.packageTypeCodeField = value;
//                }
//            }

//            /// <remarks/>
//            public byte Height
//            {
//                get
//                {
//                    return this.heightField;
//                }
//                set
//                {
//                    this.heightField = value;
//                }
//            }

//            /// <remarks/>
//            public byte Depth
//            {
//                get
//                {
//                    return this.depthField;
//                }
//                set
//                {
//                    this.depthField = value;
//                }
//            }

//            /// <remarks/>
//            public byte Width
//            {
//                get
//                {
//                    return this.widthField;
//                }
//                set
//                {
//                    this.widthField = value;
//                }
//            }

//            /// <remarks/>
//            public decimal Weight
//            {
//                get
//                {
//                    return this.weightField;
//                }
//                set
//                {
//                    this.weightField = value;
//                }
//            }
//        }

//        /// <remarks/>
//        [System.SerializableAttribute()]
//        [System.ComponentModel.DesignerCategoryAttribute("code")]
//        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//        public partial class GetCapabilityTO
//        {

//            private string countryCodeField;

//            private int postalcodeField;

//            /// <remarks/>
//            public string CountryCode
//            {
//                get
//                {
//                    return this.countryCodeField;
//                }
//                set
//                {
//                    this.countryCodeField = value;
//                }
//            }

//            /// <remarks/>
//            public int Postalcode
//            {
//                get
//                {
//                    return this.postalcodeField;
//                }
//                set
//                {
//                    this.postalcodeField = value;
//                }
//            }
//        }


//    }
}
