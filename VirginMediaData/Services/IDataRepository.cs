using Microsoft.AspNetCore.Mvc.RazorPages;
using VirginMediaData.Domain;

namespace VirginMediaData.Services
{
    public interface IDataRepository
	{
		IQueryable<SalesInfo> GetAllSales();
		IEnumerable<SaleSummary> UnitSalesByCountry();
		IEnumerable<SaleSummary> UnitSalesBySegment();
		IEnumerable<SaleSummary> UnitSalesByProduct();
	}
}
