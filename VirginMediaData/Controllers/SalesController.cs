using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using VirginMediaData.Domain;
using VirginMediaData.Models;
using VirginMediaData.Services;

namespace VirginMediaData.Controllers
{
	public class SalesController : Controller
	{
		private readonly IDataRepository _dataService;
		private const int _pageSize = 20;
        public SalesController(IDataRepository dataService)
        {
            ArgumentNullException.ThrowIfNull(dataService, nameof(dataService));

			_dataService = dataService;
        }

        public ActionResult Index(int page = 1)
		{
			var salesQueryable = _dataService.GetAllSales();
			var totalCount = salesQueryable.Count();
			var totalPages = (int)Math.Ceiling((double)totalCount / _pageSize);
			var pagedSales = salesQueryable.Skip((page - 1) * _pageSize).Take(_pageSize);
			var model = new SalesViewModel
			{
				Sales = pagedSales.ToList(),
				Page = page,
				TotalPages = totalPages,
				TotalRecords = totalCount
			};
			return View(model);
		}

	    public ActionResult UnitSales(string group = Constants.Country)
        {

			IEnumerable<SaleSummary> chartData = Enumerable.Empty<SaleSummary>();

			switch (group.ToLower())
			{
				case Constants.Country:
					chartData = _dataService.UnitSalesByMetric(group);
					break;
				case Constants.Product:
					chartData = _dataService.UnitSalesByMetric(group);
					break;
                case Constants.Segment:
                    chartData = _dataService.UnitSalesByMetric(group);
                    break;
				default:
                    chartData = _dataService.UnitSalesByMetric(Constants.Country);
					break;
            }

            SummaryViewModel model = new SummaryViewModel { SaleSummaries = chartData };

            return View(model);
        }
    }
}
