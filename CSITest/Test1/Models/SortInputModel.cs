using System.ComponentModel.DataAnnotations;

namespace Test1.Models
{
	public class SortInputModel
	{
		[Required]
		public string Numbers { get; set; }
		[Required]
		public string SortBy { get; set; }
	}
}
