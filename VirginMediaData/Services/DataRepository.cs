using System.Security.Policy;
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

		public IEnumerable<SaleSummary> UnitSalesByMetric(string metric)
		{
			var query = _salesInfos.AsQueryable();
			IQueryable<IGrouping<string, SalesInfo>> groupedQuery;

			switch(metric)
			{
				case Constants.Country:
					groupedQuery = query.GroupBy(s => s.Country);
					break;
				case Constants.Product:
                    groupedQuery = query.GroupBy(s => s.Product);
					break;
				case Constants.Segment:
					groupedQuery = query.GroupBy(s => s.Segment);
					break;
				default:
					metric = Constants.Country;
					groupedQuery = query.GroupBy(s => s.Country);
					break;
			}

			return groupedQuery.Select(g => new SaleSummary
            {
                Label = g.Key,
                TotalUnits = g.Sum(u => u.UnitsSold),
                TotalSales = g.Sum(s => s.SalePrice),
                Type = metric
            }).ToList();
        }
	}
}
