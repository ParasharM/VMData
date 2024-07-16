using VirginMediaData.Domain;

namespace VirginMediaData.Models
{
	public class SalesViewModel
	{
		public IEnumerable<SalesInfo> Sales { get; set; } = new List<SalesInfo>();
		public int Page { get; set; }
		public int TotalPages { get; set; }
		public int TotalRecords {  get; set; }
	}
}
