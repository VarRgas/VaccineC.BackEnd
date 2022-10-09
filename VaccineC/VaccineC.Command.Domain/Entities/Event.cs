using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("userId")]
        public Guid UserId { get; set; }

        [Column("situation", TypeName = "varchar(1)")]
        public string Situation { get; set; }

        [Column("concluded", TypeName = "varchar(1)")]
        public string Concluded { get; set; }

        [Column("startDate", TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column("endDate", TypeName = "date")]
        public DateTime EndDate { get; set; }

        [Column("startTime", TypeName = "time")]
        public TimeSpan StartTime { get; set; }

        [Column("endTime", TypeName = "time")]
        public TimeSpan EndTime { get; set; }

        [Column("details", TypeName = "varchar(1000)")]
        public string? Details { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        public Event(Guid id, Guid userId, string situation, string concluded, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime, string? details, DateTime register)
        {
            ID = id;
            UserId = userId;
            Situation = situation;
            Concluded = concluded;
            StartDate = startDate;
            EndDate = endDate;
            StartTime = startTime;
            EndTime = endTime;
            Details = details;
            Register = register;
        }

        public Event()
        {

        }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }

        public void SetSituation(string situation)
        {
            Situation = situation;
        }

        public void SetConcluded(string concluded)
        {
            Concluded = concluded;
        }

        public void SetStartDate(DateTime startDate)
        {
            StartDate = startDate;
        }

        public void SetEndDate(DateTime endDate)
        {
            EndDate = endDate;
        }

        public void SetStartTime(TimeSpan startTime)
        {
            StartTime = startTime;
        }

        public void SetEndTime(TimeSpan endTime)
        {
            EndTime = endTime;
        }
        public void SetDetails(string details)
        {
            Details = details;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }
    }
}
