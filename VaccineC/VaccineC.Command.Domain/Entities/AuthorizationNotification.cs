using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class AuthorizationNotification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("authorizationId")]
        public Guid AuthorizationId { get; set; }

        [Column("eventId")]
        public Guid EventId { get; set; }

        [Column("personPhone", TypeName = "varchar(20)")]
        public string PersonPhone { get; set; }

        [Column("message", TypeName = "varchar(160)")]
        public string Message { get; set; }

        [Column("sendDate", TypeName = "date")]
        public DateTime SendDate { get; set; }

        [Column("sendHour", TypeName = "time")]
        public TimeSpan SendHour { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        [Column("returnId", TypeName = "varchar(100)")]
        public string? ReturnId { get; set; }

        public AuthorizationNotification(Guid id, Guid authorizationId, Guid eventId, string personPhone, string message, DateTime sendDate, TimeSpan sendHour, DateTime register, string? returnId)
        {
            ID = id;
            AuthorizationId = authorizationId;
            EventId = eventId;
            PersonPhone = personPhone;
            Message = message;
            SendDate = sendDate;
            SendHour = sendHour;
            Register = register;
            ReturnId = returnId;    
        }

        public AuthorizationNotification()
        {

        }
        
        public void setAuthorizationId(Guid authorizationId)
        {
            AuthorizationId = authorizationId;
        }

        public void setEventId(Guid eventId)
        {
            EventId = eventId;
        }

        public void setPersonPhone(string personPhone)
        {
            PersonPhone = personPhone;
        }

        public void setMessage(string message)
        {
            Message = message;
        }

        public void setSendDate(DateTime sendDate)
        {
            SendDate = sendDate;
        }

        public void setSendHour(TimeSpan sendHour)
        {
            SendHour = sendHour;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }

        public void SetReturnId(string? returnId)
        {
            ReturnId = returnId;
        }
    }
}
