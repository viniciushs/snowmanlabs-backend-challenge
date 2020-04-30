using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SnowmanLabsChallenge.Infra.CrossCutting.Utils.Tests.Extensions
{
    [TestClass]
    public class StringExtensionsTests
    {
        private readonly string Text = "Lorem ipSUM dOloR sit AMET. 123";

        [TestMethod]
        [Owner("Vinicius Haninec Silva")]
        public void ToCamelCaseTest()
        {
            var text = this.Text;
            var camelCaseFormat = text.ToCamelCase();
            Assert.AreEqual("LoremIpsumDolorSitAmet.123", camelCaseFormat);
        }

        [TestMethod]
        [Owner("Vinicius Haninec Silva")]
        public void ToTitleCaseTest()
        {
            var text = this.Text;
            var titleCaseFormat = text.ToTitleCase();
            Assert.AreEqual("Lorem Ipsum Dolor Sit Amet. 123", titleCaseFormat);
        }

        [TestMethod]
        [Owner("Vinicius Haninec Silva")]
        public void ToLowerCaseTest()
        {
            var text = this.Text;
            var lowerCaseFormat = text.ToLowerCase();
            Assert.AreEqual("lorem ipsum dolor sit amet. 123", lowerCaseFormat);
        }

        [TestMethod]
        [Owner("Vinicius Haninec Silva")]
        public void ToUpperCaseTest()
        {
            var text = this.Text;
            var upperCaseFormat = text.ToUpperCase();
            Assert.AreEqual("LOREM IPSUM DOLOR SIT AMET. 123", upperCaseFormat);
        }

        [TestMethod]
        [Owner("Vinicius Haninec Silva")]
        public void RegexReplaceTest()
        {
            var text = "Lorem   ipSUM      dOloR    sit               AMET.    123";
            var withoutMultiplesSpaces = text.RegexReplace(@"\s+", " ");
            Assert.AreEqual("Lorem ipSUM dOloR sit AMET. 123", withoutMultiplesSpaces);

            var withoutSpaces = text.RegexReplace(@"\s+", string.Empty);
            Assert.AreEqual("LoremipSUMdOloRsitAMET.123", withoutSpaces);

            var withoutNumbers = text.RegexReplace(@"[0-9]+", string.Empty);
            Assert.AreEqual("Lorem   ipSUM      dOloR    sit               AMET.    ", withoutNumbers);

            var multiplesRegex = text.RegexReplace(@"\s+", string.Empty)     // Without spaces
                                     .RegexReplace(@"[0-9]+", string.Empty); // Without numbers
            Assert.AreEqual("LoremipSUMdOloRsitAMET.", multiplesRegex);
        }

        [TestMethod]
        [Owner("Vinicius Haninec Silva")]
        public void ReplaceNonDigitsTest()
        {
            var text = "123.456.789-99";
            var onlyDigits = text.ReplaceNonDigits();
            Assert.AreEqual("12345678999", onlyDigits);

            var nonDigitsTurnIntoAChar = text.ReplaceNonDigits("A");
            Assert.AreEqual("123A456A789A99", nonDigitsTurnIntoAChar);
        }
    }
}
