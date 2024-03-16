using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test1.Models.DBEntties
{
    public class Numbers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string NumberList { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string SortType { get; set; }
               
        public TimeSpan TimeTaken { get; set; }

        public DateTime CreatedAt {  get; set; }
    }
}
