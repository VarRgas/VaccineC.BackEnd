using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("userId")]
        public Guid UserId { get; set; }

        [Column("message", TypeName = "varchar(255)")]
        public string Message { get; set; }

        [Column("messageType", TypeName = "varchar(1)")]
        public string MessageType { get; set; }

        [Column("situation", TypeName = "varchar(1)")]
        public string Situation { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        public Notification(Guid id, Guid userId, string message, string messageType, string situation, DateTime register)
        {
            ID = id;
            UserId = userId;
            Message = message;
            MessageType = messageType;
            Situation = situation;
            Register = register;
        }

        public Notification() { 
        }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }

        public void SetMessage(string message)
        {
            Message = message;
        }

        public void SetMessageType(string messageType)
        {
            MessageType = messageType;
        }

        public void SetSituation(string situation)
        {
            Situation = situation;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }

    }
}
