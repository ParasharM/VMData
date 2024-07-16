using VirginMediaData.Domain;
using System.Text.RegularExpressions;

namespace VirginMediaData.Services
{
    public class ImportService : IImportService
	{
		private const string DELIMITER = ",";
		public IEnumerable<SalesInfo> ImportCSVData(string filePath)
		{
			ArgumentNullException.ThrowIfNullOrEmpty(filePath, nameof(filePath));

			if (!File.Exists(filePath)) {
				throw new FileNotFoundException($"Error: {filePath} not found");
			}

			var lines = File.ReadAllLines(filePath, System.Text.Encoding.ASCII);

			return lines.Skip(1).Select(x => ParseSalesInfo(x));
		}

		private SalesInfo ParseSalesInfo(string row)
		{
			var columns = row.Split(DELIMITER);

			if (columns.Length < 8)
			{
				throw new Exception("Parsing error. Column length not expected");
			}


			//clean up csv data, replace non decimal chars with empty
			string mfPrice = Regex.Replace(columns[(int)DataColumns.ManufacturingPrice], "[^0-9.]", string.Empty);
			string slPrice = Regex.Replace(columns[(int)DataColumns.SalePrice], "[^0-9.]", string.Empty);
			string unitsSold = Regex.Replace(columns[(int)DataColumns.UnitsSold], "[^0-9.]", string.Empty);

			
			string productName = columns[(int)DataColumns.Product];
			string segmentName = columns[(int)DataColumns.Segment];
			string discountBand = columns[(int)DataColumns.DiscountBand];
            string countryName = columns[(int)DataColumns.Country];

			segmentName = string.IsNullOrWhiteSpace(segmentName) ? "MISSING VALUE" : segmentName;
            productName = string.IsNullOrWhiteSpace(productName) ? "MISSING VALUE" : productName;
            countryName = string.IsNullOrWhiteSpace(countryName) ? "MISSING VALUE" : countryName;
            discountBand = string.IsNullOrWhiteSpace(discountBand) ? "MISSING VALUE" : discountBand;


            decimal manufacturingPrice = 0.00M;
			decimal salePrice = 0.00M;
			decimal soldUnits = 0.00M;
			DateTime saleDate = DateTime.Now;

			//try to convert values
			decimal.TryParse(mfPrice, out manufacturingPrice);
			decimal.TryParse(slPrice, out salePrice);
			decimal.TryParse(unitsSold, out soldUnits);
			DateTime.TryParse(columns[(int)DataColumns.Date], out saleDate);

			return new SalesInfo
			{
				Segment = segmentName,
				Country = countryName,
				Product = productName,
				Discount = discountBand,
				UnitsSold = soldUnits,
				ManufacturingPrice = manufacturingPrice,
				SalePrice = salePrice,
				Date = saleDate
			};
		}
	}
}
