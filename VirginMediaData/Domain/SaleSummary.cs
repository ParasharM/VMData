namespace VirginMediaData.Domain
{
	public class SaleSummary
	{
		public string Type { get; set; } = string.Empty;
		public string Label { get; set; } = string.Empty;
		public decimal TotalSales { get; set; }
		public decimal TotalUnits { get; set; }
	}
}
