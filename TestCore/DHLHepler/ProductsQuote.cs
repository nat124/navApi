using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore.DHLHepler
{
    public class ProductsQuote
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
        public partial class DCTRequest
        {

            private GetQuote getQuoteField;

            private decimal schemaVersionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public GetQuote GetQuote
            {
                get
                {
                    return this.getQuoteField;
                }
                set
                {
                    this.getQuoteField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal schemaVersion
            {
                get
                {
                    return this.schemaVersionField;
                }
                set
                {
                    this.schemaVersionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class GetQuote
        {

            private GetQuoteRequest requestField;

            private GetQuoteFrom fromField;

            private GetQuoteBkgDetails bkgDetailsField;

            private GetQuoteTO toField;

            private GetQuoteDutiable dutiableField;

            /// <remarks/>
            public GetQuoteRequest Request
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
            public GetQuoteFrom From
            {
                get
                {
                    return this.fromField;
                }
                set
                {
                    this.fromField = value;
                }
            }

            /// <remarks/>
            public GetQuoteBkgDetails BkgDetails
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
            public GetQuoteTO To
            {
                get
                {
                    return this.toField;
                }
                set
                {
                    this.toField = value;
                }
            }

            /// <remarks/>
            public GetQuoteDutiable Dutiable
            {
                get
                {
                    return this.dutiableField;
                }
                set
                {
                    this.dutiableField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteRequest
        {

            private GetQuoteRequestServiceHeader serviceHeaderField;

            private GetQuoteRequestMetaData metaDataField;

            /// <remarks/>
            public GetQuoteRequestServiceHeader ServiceHeader
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

            /// <remarks/>
            public GetQuoteRequestMetaData MetaData
            {
                get
                {
                    return this.metaDataField;
                }
                set
                {
                    this.metaDataField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteRequestServiceHeader
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

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteRequestMetaData
        {

            private string softwareNameField;

            private decimal softwareVersionField;

            /// <remarks/>
            public string SoftwareName
            {
                get
                {
                    return this.softwareNameField;
                }
                set
                {
                    this.softwareNameField = value;
                }
            }

            /// <remarks/>
            public decimal SoftwareVersion
            {
                get
                {
                    return this.softwareVersionField;
                }
                set
                {
                    this.softwareVersionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteFrom
        {

            private string countryCodeField;

            private string postalcodeField;

            private string cityField;

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
            public string Postalcode
            {
                get
                {
                    return this.postalcodeField;
                }
                set
                {
                    this.postalcodeField = value;
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteBkgDetails
        {

            private string paymentCountryCodeField;

            private System.DateTime dateField;

            private string readyTimeField;

            private string dimensionUnitField;

            private string weightUnitField;

            private byte numberOfPiecesField;

            private decimal shipmentWeightField;

            private GetQuoteBkgDetailsPieces piecesField;

            private uint paymentAccountNumberField;

            private string isDutiableField;

            private string networkTypeCodeField;

            private GetQuoteBkgDetailsQtdShpExChrg[] qtdShpField;

            /// <remarks/>
            public string PaymentCountryCode
            {
                get
                {
                    return this.paymentCountryCodeField;
                }
                set
                {
                    this.paymentCountryCodeField = value;
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
            [System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
            public string ReadyTime
            {
                get
                {
                    return this.readyTimeField;
                }
                set
                {
                    this.readyTimeField = value;
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
            //public byte NumberOfPieces
            //{
            //    get
            //    {
            //        return this.numberOfPiecesField;
            //    }
            //    set
            //    {
            //        this.numberOfPiecesField = value;
            //    }
            //}

            ///// <remarks/>
            //public decimal ShipmentWeight
            //{
            //    get
            //    {
            //        return this.shipmentWeightField;
            //    }
            //    set
            //    {
            //        this.shipmentWeightField = value;
            //    }
            //}

            /// <remarks/>
            public GetQuoteBkgDetailsPieces Pieces
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
            public uint PaymentAccountNumber
            {
                get
                {
                    return this.paymentAccountNumberField;
                }
                set
                {
                    this.paymentAccountNumberField = value;
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
            [System.Xml.Serialization.XmlArrayItemAttribute("QtdShpExChrg", IsNullable = false)]
            public GetQuoteBkgDetailsQtdShpExChrg[] QtdShp
            {
                get
                {
                    return this.qtdShpField;
                }
                set
                {
                    this.qtdShpField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteBkgDetailsPieces
        {

            private GetQuoteBkgDetailsPiecesPiece pieceField;

            /// <remarks/>
            public GetQuoteBkgDetailsPiecesPiece Piece
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

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteBkgDetailsPiecesPiece
        {

            private byte pieceIDField;

            private byte heightField;

            private byte depthField;

            private byte widthField;

            private decimal weightField;

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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteBkgDetailsQtdShpExChrg
        {

            private string specialServiceTypeField;

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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteTO
        {

            private string countryCodeField;

            private string postalcodeField;

            private string cityField;

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
            public string Postalcode
            {
                get
                {
                    return this.postalcodeField;
                }
                set
                {
                    this.postalcodeField = value;
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GetQuoteDutiable
        {

            private string declaredCurrencyField;

            private decimal declaredValueField;

            /// <remarks/>
            public string DeclaredCurrency
            {
                get
                {
                    return this.declaredCurrencyField;
                }
                set
                {
                    this.declaredCurrencyField = value;
                }
            }

            /// <remarks/>
            public decimal DeclaredValue
            {
                get
                {
                    return this.declaredValueField;
                }
                set
                {
                    this.declaredValueField = value;
                }
            }
        }


    }
}
