namespace VirginMediaData.Domain
{
	public class SalesInfo
	{
		public string Segment { get; set; } = string.Empty;
		public string Country { get; set; } = string.Empty;
		public string Product { get; set; } = string.Empty;
		public string Discount { get; set; } = string.Empty;
		public decimal UnitsSold { get; set; }
		public  decimal ManufacturingPrice { get; set; }
	    public decimal SalePrice { get; set; }
		public DateTime Date { get; set; }
	}
}
