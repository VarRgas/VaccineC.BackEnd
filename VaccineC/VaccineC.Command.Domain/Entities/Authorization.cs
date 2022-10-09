using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class Authorization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("authorizationNumber")]
        public int AuthorizationNumber { get; set; }

        [Column("userId")]
        public Guid UserId { get; set; }

        [Column("authorizationDate", TypeName = "date")]
        public DateTime AuthorizationDate { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        [Column("borrowerPersonId")]
        public Guid BorrowerPersonId { get; set; }

        [Column("situation", TypeName = "varchar(1)")]
        public string Situation { get; set; }

        [Column("typeOfService", TypeName = "varchar(1)")]
        public string TypeOfService { get; set; }

        [Column("notify", TypeName = "varchar(1)")]
        public string Notify { get; set; }

        [Column("eventId")]
        public Guid EventId { get; set; }

        [Column("budgetProductId")]
        public Guid BudgetProductId { get; set; }

        public Authorization(Guid id, Guid userId, Guid eventId, Guid budgetProductId, Guid borrowerPersonId, int authorizationNumber, string situation, string typeOfService, string notify, DateTime authorizationDate, DateTime register)
        {
            ID = id;
            UserId = userId;
            EventId = eventId;
            BudgetProductId = budgetProductId;
            BorrowerPersonId = borrowerPersonId;
            AuthorizationNumber = authorizationNumber;
            Situation = situation;
            TypeOfService = typeOfService;
            Notify = notify;
            AuthorizationDate = authorizationDate;
            Register = register;
        }

        public Authorization()
        {

        }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }

        public void SetEventId(Guid eventId)
        {
            EventId = eventId;
        }

        public void SetBudgetProductId(Guid budgetProductId)
        {
            BudgetProductId = budgetProductId;
        }

        public void SetBorrowerPersonId(Guid borrowerPersonId)
        {
            BorrowerPersonId = borrowerPersonId;
        }

        public void SetAuthorizationNumber(int authorizationNumber)
        {
            AuthorizationNumber = authorizationNumber;
        }

        public void SetSituation(string situation)
        {
            Situation = situation;
        }
        
        public void SetTypeOfService(string typeOfService)
        {
            TypeOfService = typeOfService;
        }

        public void SetNotify(string notify)
        {
            Notify = notify;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }

        public void SetAuthorizationDate(DateTime authorizationDate)
        {
            AuthorizationDate = authorizationDate;
        }
    }
}
