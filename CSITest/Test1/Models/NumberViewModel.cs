using System.ComponentModel;

namespace Test1.Models
{
    public class NumberViewModel
    {
        public int Id { get; set; }
        [DisplayName("Number List")]
        public string NumberList { get; set; }
        [DisplayName("Sorted By")]
        public string SortType { get; set; }
        [DisplayName("Process Time")]
        public TimeSpan TimeTaken { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
