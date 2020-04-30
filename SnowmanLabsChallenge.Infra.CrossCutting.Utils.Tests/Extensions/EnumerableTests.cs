using SnowmanLabsChallenge.Infra.CrossCutting.Utils.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SnowmanLabsChallenge.Infra.CrossCutting.Utils.Tests.Extensions
{
    [TestClass]
    public class EnumerableTests
    {
        [TestMethod]
        [Owner("Vinicius Haninec Silva")]
        public void ToCsvTest()
        {
            var objects = new List<ExempleDataObject>
            {
                new ExempleDataObject(1, 1, "Description 1", new ComplexDataObject("Code 1", "Complex Description 1"), new DateTime(2020, 02, 01), true),
                new ExempleDataObject(2, null, "Description 2", new ComplexDataObject("Code 2", "Complex Description 2"), new DateTime(2020, 02, 02), true),
                new ExempleDataObject(3, null, "Description 3", new ComplexDataObject("Code 3", "Complex Description 3"), new DateTime(2020, 02, 03), false),
                new ExempleDataObject(4, 4, "Description 4", new ComplexDataObject("Code 4", "Complex Description 4"), new DateTime(2020, 02, 04), true),
            };

            var csvResult = @"Id,IdNullable,Description,Date,Active
1,1,Description 1,01/02/2020 00:00:00,True
2,,Description 2,02/02/2020 00:00:00,True
3,,Description 3,03/02/2020 00:00:00,False
4,4,Description 4,04/02/2020 00:00:00,True
";

            var csv = objects.ToCsv();
            Assert.AreEqual(csv, csvResult);
        }
    }

    public class ExempleDataObject
    {
        public ExempleDataObject(int id, int? idNullable, string description, ComplexDataObject complexDataObject, DateTime date, bool active)
        {
            this.Id = id;
            this.IdNullable = idNullable;
            this.ComplexDataObject = complexDataObject;
            this.Description = description;
            this.Date = date;
            this.Active = active;
        }

        public int Id { get; set; }

        public int? IdNullable { get; set; }

        public ComplexDataObject ComplexDataObject { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public bool Active { get; set; }
    }

    public class ComplexDataObject
    {
        public ComplexDataObject(string code, string description)
        {
            this.Code = code;
            this.Description = description;
        }

        public string Code { get; set; }
        public string Description { get; set; }
    }

}
