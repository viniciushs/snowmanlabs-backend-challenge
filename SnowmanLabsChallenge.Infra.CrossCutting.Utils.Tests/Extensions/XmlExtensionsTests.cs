using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnowmanLabsChallenge.Infra.CrossCutting.Utils.Tests.Extensions
{
    [TestClass]
    public class XmlExtensionsTests
    {
        private readonly string _xmlText = @"
            <?xml version='1.0' encoding='UTF-8'?>
            <source>
                <publisher>Test</publisher>
                <publisherurl>http://www.test.com?id=1&a=test's&b=3</publisherurl>
                <lastBuildDate>12/12/2018</lastBuildDate>
                <totalresults>19</totalresults>
                <pagenumber>2</pagenumber>
                <pagesize>20</pagesize>
            </source>
        ";

        [TestMethod]
        public void StringToXmlDocumentTest()
        {
            var xmlDocument = this._xmlText.ToXmlDocument();

            var root = xmlDocument.DocumentElement;
            Assert.AreEqual(root.Name, "source");

            var nodes = root.ChildNodes;

            var publisherNode = nodes[0];
            Assert.AreEqual(publisherNode.Name, "publisher");
            Assert.AreEqual(publisherNode.InnerText, "Test");

            var publisherurlNode = nodes[1];
            Assert.AreEqual(publisherurlNode.Name, "publisherurl");
            Assert.AreEqual(publisherurlNode.InnerText, "http://www.test.com?id=1&a=test's&b=3");

            var lastBuildDateNode = nodes[2];
            Assert.AreEqual(lastBuildDateNode.Name, "lastBuildDate");
            Assert.AreEqual(lastBuildDateNode.InnerText, "12/12/2018");

            var totalresultsNode = nodes[3];
            Assert.AreEqual(totalresultsNode.Name, "totalresults");
            Assert.AreEqual(totalresultsNode.InnerText, "19");

            var pagenumberNode = nodes[4];
            Assert.AreEqual(pagenumberNode.Name, "pagenumber");
            Assert.AreEqual(pagenumberNode.InnerText, "2");

            var pagesizeNode = nodes[5];
            Assert.AreEqual(pagesizeNode.Name, "pagesize");
            Assert.AreEqual(pagesizeNode.InnerText, "20");
        }

        [TestMethod]
        public void StringToXDocumentTest()
        {
            var xDocument = this._xmlText.ToXDocument();

            var root = xDocument.Element("source");
            var publisherNode = root.Elements().FirstOrDefault(x => x.Name == "publisher");
            Assert.AreEqual(publisherNode.Value, "Test");

            var publisherurlNode = root.Elements().FirstOrDefault(x => x.Name == "publisherurl");
            Assert.AreEqual(publisherurlNode.Value, "http://www.test.com?id=1&a=test's&b=3");

            var lastBuildDateNode = root.Elements().FirstOrDefault(x => x.Name == "lastBuildDate");
            Assert.AreEqual(lastBuildDateNode.Value, "12/12/2018");

            var totalresultsNode = root.Elements().FirstOrDefault(x => x.Name == "totalresults");
            Assert.AreEqual(totalresultsNode.Value, "19");

            var pagenumberNode = root.Elements().FirstOrDefault(x => x.Name == "pagenumber");
            Assert.AreEqual(pagenumberNode.Value, "2");

            var pagesizeNode = root.Elements().FirstOrDefault(x => x.Name == "pagesize");
            Assert.AreEqual(pagesizeNode.Value, "20");
        }

        [TestMethod]
        public void XmlDocumentToXDocumentTest()
        {
            var xmlDocument = this._xmlText.ToXmlDocument();
            var xDocument = xmlDocument.ToXDocument();

            var root = xDocument.Element("source");
            var publisherNode = root.Elements().FirstOrDefault(x => x.Name == "publisher");
            Assert.AreEqual(publisherNode.Value, "Test");

            var publisherurlNode = root.Elements().FirstOrDefault(x => x.Name == "publisherurl");
            Assert.AreEqual(publisherurlNode.Value, "http://www.test.com?id=1&a=test's&b=3");

            var lastBuildDateNode = root.Elements().FirstOrDefault(x => x.Name == "lastBuildDate");
            Assert.AreEqual(lastBuildDateNode.Value, "12/12/2018");

            var totalresultsNode = root.Elements().FirstOrDefault(x => x.Name == "totalresults");
            Assert.AreEqual(totalresultsNode.Value, "19");

            var pagenumberNode = root.Elements().FirstOrDefault(x => x.Name == "pagenumber");
            Assert.AreEqual(pagenumberNode.Value, "2");

            var pagesizeNode = root.Elements().FirstOrDefault(x => x.Name == "pagesize");
            Assert.AreEqual(pagesizeNode.Value, "20");
        }

        [TestMethod]
        public void XDocumentToXmlDocumentTest()
        {
            var xDocument = this._xmlText.ToXDocument();
            var xmlDocument = xDocument.ToXmlDocument();

            var root = xmlDocument.DocumentElement;
            Assert.AreEqual(root.Name, "source");

            var nodes = root.ChildNodes;

            var publisherNode = nodes[0];
            Assert.AreEqual(publisherNode.Name, "publisher");
            Assert.AreEqual(publisherNode.InnerText, "Test");

            var publisherurlNode = nodes[1];
            Assert.AreEqual(publisherurlNode.Name, "publisherurl");
            Assert.AreEqual(publisherurlNode.InnerText, "http://www.test.com?id=1&a=test's&b=3");

            var lastBuildDateNode = nodes[2];
            Assert.AreEqual(lastBuildDateNode.Name, "lastBuildDate");
            Assert.AreEqual(lastBuildDateNode.InnerText, "12/12/2018");

            var totalresultsNode = nodes[3];
            Assert.AreEqual(totalresultsNode.Name, "totalresults");
            Assert.AreEqual(totalresultsNode.InnerText, "19");

            var pagenumberNode = nodes[4];
            Assert.AreEqual(pagenumberNode.Name, "pagenumber");
            Assert.AreEqual(pagenumberNode.InnerText, "2");

            var pagesizeNode = nodes[5];
            Assert.AreEqual(pagesizeNode.Name, "pagesize");
            Assert.AreEqual(pagesizeNode.InnerText, "20");
        }
    }
}
