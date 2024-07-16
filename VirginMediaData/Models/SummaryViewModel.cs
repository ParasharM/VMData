using VirginMediaData.Domain;

namespace VirginMediaData.Models
{
    public class SummaryViewModel
    {
        public IEnumerable<SaleSummary> SaleSummaries { get; set; } = Enumerable.Empty<SaleSummary>();
    }
}
