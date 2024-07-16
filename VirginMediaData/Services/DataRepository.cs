using VirginMediaData.Domain;

namespace VirginMediaData.Services
{
    public class DataRepository : IDataRepository
	{
		private readonly IImportService _importService;
		private IEnumerable<SalesInfo> _salesInfos;

        public DataRepository(IImportService importService, IConfiguration config)
        {
			ArgumentNullException.ThrowIfNull(importService, nameof(importService));
			ArgumentNullException.ThrowIfNull(config, nameof(config));

			string csvPath = config["CSVPath"] ?? throw new ArgumentNullException("CSVPath not found");

            _importService = importService;
			_salesInfos = _importService.ImportCSVData(csvPath) ?? throw new InvalidDataException("Sales Info is null");
        }

        public IQueryable<SalesInfo> GetAllSales()
		{
			return _salesInfos.AsQueryable();
		}

		public IEnumerable<SaleSummary> UnitSalesByCountry()
		{
            return _salesInfos.GroupBy(s => s.Country)
                    .Select(g => new SaleSummary
					{
						Label = g.Key,
						TotalUnits = g.Sum(u => u.UnitsSold),
						TotalSales = g.Sum(s => s.SalePrice),
						Type = "Country"
					});
		}

		public IEnumerable<SaleSummary> UnitSalesByProduct()
		{
			return _salesInfos.GroupBy(s => s.Product)
					.Select(g => new SaleSummary { 
						Type = "Product", 
						Label = g.Key, 
						TotalSales = g.Sum(s => s.SalePrice), 
						TotalUnits = g.Sum(s => s.UnitsSold)
					});
		}

		public IEnumerable<SaleSummary> UnitSalesBySegment()
		{
			return _salesInfos.GroupBy(s => s.Segment)
					.Select(g => new SaleSummary { Type = "Segment",
						Label = g.Key,
						TotalSales = g.Sum(s => s.SalePrice),
						TotalUnits = g.Sum(s => s.UnitsSold)
					});
		}
	}
}
