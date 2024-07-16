using System.Collections.Generic;
using VirginMediaData.Domain;
using System.Globalization;

namespace VirginMediaData.Services
{
    public interface IImportService
	{
		IEnumerable<SalesInfo> ImportCSVData(string filePath);
	}
}
