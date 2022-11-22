using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class CompanyParameter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("companyId")]
        public Guid CompanyId { get; set; }

        [Column("defaultPaymentFormId")]
        public Guid? DefaultPaymentFormId { get; set; }

        [Column("applicationTimePerMinute", TypeName = "int")]
        public int ApplicationTimePerMinute { get; set; }

        [Column("maximumDaysBudgetValidity", TypeName = "int")]
        public int MaximumDaysBudgetValidity { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        [Column("startTime", TypeName = "time(0)")]
        public TimeSpan StartTime { get; set; }

        [Column("finalTime", TypeName = "time(0)")]
        public TimeSpan FinalTime { get; set; }

        public CompanyParameter(Guid id, Guid companyId, Guid? defaultPaymentFormId, int applicationTimePerMinute, int maximumDaysBudgetValidity, TimeSpan startTime, TimeSpan finalTime, DateTime register)
        {
            ID = id;
            CompanyId = companyId;
            DefaultPaymentFormId = defaultPaymentFormId;
            ApplicationTimePerMinute = applicationTimePerMinute;
            MaximumDaysBudgetValidity = maximumDaysBudgetValidity;
            StartTime = startTime;
            FinalTime = finalTime;
            Register = register;
        }
        public CompanyParameter()
        {

        }

        public void SetCompanyId(Guid companyID)
        {
            CompanyId = companyID;
        }

        public void SetDefaultPaymentFormId(Guid? defaultPaymentFormId)
        {
            DefaultPaymentFormId = defaultPaymentFormId;
        }

        public void SetApplicationTimePerMinute(int applicationTimePerMinute)
        {
            ApplicationTimePerMinute = applicationTimePerMinute;
        }

        public void SetMaximumDaysBudgetValidity(int maximumDaysBudgetValidity)
        {
            MaximumDaysBudgetValidity = maximumDaysBudgetValidity;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }

        public void setStartTime(TimeSpan startTime)
        {
            StartTime = startTime;
        }

        public void setFinalTime(TimeSpan finalTime)
        {
            FinalTime = finalTime;
        }

    }
}

