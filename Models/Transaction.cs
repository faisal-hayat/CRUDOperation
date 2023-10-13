using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_Operations.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        
        [Column(TypeName = "nvarchar(12)")]
        public string AccountNumber { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string BeneficiaryName { get; set; }
        
        [Column(TypeName = "nvarchar(100)")]
        public string BankName { get; set; }
        [Column(TypeName = "nvarchar(11)")]
        public string SWIFTCode { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
