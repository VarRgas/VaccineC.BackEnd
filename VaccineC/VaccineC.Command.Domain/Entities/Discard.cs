using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class Discard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("productSummaryBatchId")]
        public Guid ProductSummaryBatchId { get; set; }

        [Column("userId")]
        public Guid UserId { get; set; }

        [Column("batch", TypeName = "varchar(255)")]
        public string Batch { get; set; }

        [Column("discardedUnits", TypeName = "int")]
        public int DiscardedUnits { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        public Discard(Guid id, Guid productSummaryBatchId, Guid userId, string batch, int discardedUnits, DateTime register)
        {
            ID = id;
            ProductSummaryBatchId = productSummaryBatchId;
            UserId = userId;
            Batch = batch;
            DiscardedUnits = discardedUnits;
            Register = register;
        }
        
        public Discard()
        {

        }

        public void SetProductSummaryBatchId(Guid productSummaryBatchId)
        {
            ProductSummaryBatchId = productSummaryBatchId;
        }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }

        public void SetBatch(string batch)
        {
            Batch = batch;
        }

        public void SetDiscardedUnits(int discardedUnits)
        {
            DiscardedUnits = discardedUnits;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }
    }
}
