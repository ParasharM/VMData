using Microsoft.AspNetCore.Mvc.RazorPages;
using VirginMediaData.Domain;

namespace VirginMediaData.Services
{
    public interface IDataRepository
	{
		IQueryable<SalesInfo> GetAllSales();
        IEnumerable<SaleSummary> UnitSalesByMetric(string metric);
	}
}
