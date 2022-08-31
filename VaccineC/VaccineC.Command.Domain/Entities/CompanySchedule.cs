using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VaccineC.Command.Domain.Entities
{
    public class CompanySchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("companyId")]
        public Guid CompanyId { get; set; }

        [Column("day", TypeName = "Varchar(3)")]
        public string Day { get; set; }

        [Column("startTime", TypeName = "time")]
        public TimeSpan StartTime { get; set; }

        [Column("finalTime", TypeName = "time")]
        public TimeSpan FinalTime { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        public CompanySchedule(Guid id, Guid companyId, string day, TimeSpan startTime, TimeSpan finalTime, DateTime register)
        {
            ID = id;
            CompanyId = companyId;
            Day = day;
            StartTime = startTime;
            FinalTime = finalTime;
            Register = register;
        }
        public CompanySchedule()
        {

        }

        public void SetCompanyId(Guid companyID)
        {
            CompanyId = companyID;
        }

        public void SetDay(string day)
        {
            Day = day;
        }
        public void SetStartTime(TimeSpan startTime)
        {
            StartTime = startTime;
        }

        public void SetFinalTime(TimeSpan finalTime)
        {
            FinalTime = finalTime;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }
    }
}
