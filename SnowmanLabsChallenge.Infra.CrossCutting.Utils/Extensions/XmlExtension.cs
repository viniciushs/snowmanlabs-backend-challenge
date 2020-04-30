namespace System
{
    using System.Security;
    using System.Xml;
    using System.Xml.Linq;

    public static class XmlExtensions
    {
        /// <summary>
        ///     Converts the string to XDocument.
        /// </summary>
        /// <param name="str">
        ///     The string to be converted.
        /// </param>
        public static XDocument ToXDocument(this string str)
        {
            var doc = str.ToXmlDocument();
            return doc.ToXDocument();
        }

        /// <summary>
        ///     Converts the XmlDocument to XDocument.
        /// </summary>
        /// <param name="document">
        ///     The document to be converted.
        /// </param>
        public static XDocument ToXDocument(this XmlDocument document)
        {
            if (document == null)
            {
                return new XDocument();
            }

            return document.ToXDocument(LoadOptions.None);
        }

        /// <summary>
        ///     Converts the XmlDocument to XDocument.
        /// </summary>
        /// <param name="document">
        ///     The document to be converted.
        /// </param>
        /// <param name="options">
        ///     The convert options.
        /// </param>
        public static XDocument ToXDocument(this XmlDocument document, LoadOptions options)
        {
            if (document == null)
            {
                return new XDocument();
            }

            using (var reader = new XmlNodeReader(document))
            {
                return XDocument.Load(reader, options);
            }
        }

        /// <summary>
        ///     Converts the XDocument to XmlDocument.
        /// </summary>
        /// <param name="document">
        ///     The document to be converted.
        /// </param>
        public static XmlDocument ToXmlDocument(this XDocument document)
        {
            if (document == null)
            {
                return new XmlDocument();
            }

            var xmlDocument = new XmlDocument();
            using (var xmlReader = document.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }

            return xmlDocument;
        }

        /// <summary>
        ///     Converts the string to XmlDocument.
        /// </summary>
        /// <param name="str">
        ///     The string to be converted.
        /// </param>
        public static XmlDocument ToXmlDocument(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return new XmlDocument();
            }

            var doc = new XmlDocument();
            var xml = SecurityElement.Escape(str);

            if (!string.IsNullOrEmpty(xml))
            {
                xml = xml.Replace("\r", string.Empty)
                         .Replace("\n", string.Empty)
                         .Replace("&lt;", "<")
                         .Replace("&gt;", ">")
                         .RegexReplace("(<\\?xml version=(.)*\\?>)+", string.Empty)
                         .RegexReplace("(>[\\s]*<)+", "><")
                         .Trim();

                doc.LoadXml(xml);
            }

            return doc;
        }
    }
}
