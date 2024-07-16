using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirginMediaData.Controllers;
using VirginMediaData.Domain;
using VirginMediaData.Services;

namespace Virgin.Tests
{
    public class RepositoryTests
    {
        private Mock<IImportService> _importServiceMock;
        private Mock<IConfiguration> _configurationMock;
        private List<SalesInfo> _salesInfo;
        [SetUp]
        public void Setup()
        {
            _importServiceMock = new Mock<IImportService>();
            _configurationMock = new Mock<IConfiguration>();
            _salesInfo = new List<SalesInfo>();

        }

        [Test]
        public void Repository_Throws_ArgumentNullException_When_ImportService_Is_Null()
        {
            //arrange
            Action sut = () => {
                _ = new DataRepository(null, It.IsAny<IConfiguration>());
            };

            //assert
            Assert.Throws<ArgumentNullException>(() => sut()).Message.Contains("importService");
        }

        [Test]
        public void Repository_Throws_ArgumentNullException_When_Configuration_Is_Null()
        {
            //arrange
            Action sut = () => {
                _ = new DataRepository(_importServiceMock.Object, null);
            };

            Assert.Throws<ArgumentNullException>(() => sut());
        }

        [Test]
        public void Repository_Throws_ArgumentNullException_When_CSVPath_Is_Null()
        {
            //arrange
            _configurationMock.Setup(x => x["CSVPath"]).Returns((string)null);

            Action sut = () => {
                _ = new DataRepository(_importServiceMock.Object, _configurationMock.Object);
            };

            //assert
            Assert.Throws<ArgumentNullException>(() => sut());
        }

        [Test]
        public void Repository_Throws_InvalidDataException_When_ImportCSV_Returns_Null()
        {
            //arrange
            _configurationMock.SetupGet(x => x[It.Is<string>(s => s == "CSVPath")]).Returns("c:\\temp\\data.csv");
            _importServiceMock.Setup(x => x.ImportCSVData(It.IsAny<string>())).Returns((IEnumerable<SalesInfo>)null);

            Action sut = () => {
                _ = new DataRepository(_importServiceMock.Object, _configurationMock.Object);
            };

            //assert
            Assert.Throws<InvalidDataException>(() => sut());
        }

        [Test]
        public void GetAll_Returns_All_Data()
        {
            //arrange
            _configurationMock.SetupGet(x => x[It.Is<string>(s => s == "CSVPath")]).Returns("c:\\temp\\data.csv");
            _importServiceMock.Setup(x => x.ImportCSVData(It.IsAny<string>())).Returns(GetMockSalesData());

            var sut = new DataRepository(_importServiceMock.Object, _configurationMock.Object);

            //act
            var expected = sut.GetAllSales();

            //assert
            Assert.True(expected.Count() == 6);
        }

        [Test]
        public void UnitSalesByCountry_Returns_Data_Grouped_By_Country()
        {
            //arrange
            _configurationMock.SetupGet(x => x[It.Is<string>(s => s == "CSVPath")]).Returns("c:\\temp\\data.csv");
            _importServiceMock.Setup(x => x.ImportCSVData(It.IsAny<string>())).Returns(GetMockSalesData());

            var sut = new DataRepository(_importServiceMock.Object, _configurationMock.Object);

            //act
            var groupedByCountry = sut.UnitSalesByCountry();

            //assert
            Assert.True(groupedByCountry.Count() == 3);
        }

        [Test]
        public void UnitSalesByProduct_Returns_Data_Grouped_By_Product()
        {
            //arrange
            _configurationMock.SetupGet(x => x[It.Is<string>(s => s == "CSVPath")]).Returns("c:\\temp\\data.csv");
            _importServiceMock.Setup(x => x.ImportCSVData(It.IsAny<string>())).Returns(GetMockSalesData());

            var sut = new DataRepository(_importServiceMock.Object, _configurationMock.Object);

            //act
            var groupedByProduct = sut.UnitSalesByProduct();

            //assert
            Assert.True(groupedByProduct.Count() == 2);
        }

        [Test]
        public void UnitSalesBySegment_Returns_Data_Grouped_By_Segment()
        {
            //arrange
            _configurationMock.SetupGet(x => x[It.Is<string>(s => s == "CSVPath")]).Returns("c:\\temp\\data.csv");
            _importServiceMock.Setup(x => x.ImportCSVData(It.IsAny<string>())).Returns(GetMockSalesData());

            var sut = new DataRepository(_importServiceMock.Object, _configurationMock.Object);

            //act
            var groupedByProduct = sut.UnitSalesBySegment();

            //assert
            Assert.True(groupedByProduct.Count() == 4);
        }

        private IEnumerable<SalesInfo> GetMockSalesData()
        {
            return new List<SalesInfo>() {
                new SalesInfo{ Segment = "Government", Country = "UK", Product = "Widget1", Discount = "None", UnitsSold = 1000, SalePrice = 10.00M, ManufacturingPrice = 5.00M, Date = DateTime.Now},
                new SalesInfo{ Segment = "Commercial", Country = "USA", Product = "Widget2", Discount = "None", UnitsSold = 2000, SalePrice = 10.00M, ManufacturingPrice = 5.00M, Date = DateTime.Now},
                new SalesInfo{ Segment = "Charity", Country = "UK", Product = "Widget1", Discount = "None", UnitsSold = 500, SalePrice = 10.00M, ManufacturingPrice = 5.00M, Date = DateTime.Now},
                new SalesInfo{ Segment = "Government", Country = "USA", Product = "Widget2", Discount = "None", UnitsSold = 1000, SalePrice = 10.00M, ManufacturingPrice = 5.00M, Date = DateTime.Now},
                new SalesInfo{ Segment = "Government", Country = "Japan", Product = "Widget1", Discount = "None", UnitsSold = 1000, SalePrice = 10.00M, ManufacturingPrice = 5.00M, Date = DateTime.Now},
                new SalesInfo{ Segment = "Black Ops", Country = "Japan", Product = "Widget1", Discount = "None", UnitsSold = 1000, SalePrice = 10.00M, ManufacturingPrice = 5.00M, Date = DateTime.Now},
            };
        }

    }
}
