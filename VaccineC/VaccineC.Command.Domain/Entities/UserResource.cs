using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class UserResource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("usersId")]
        public Guid UsersId { get; set; }

        [Column("resourcesId")]
        public Guid ResourcesId { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        public UserResource(Guid id, Guid usersId, Guid resourcesId, DateTime register)
        {
            ID = id;
            UsersId = usersId;
            ResourcesId = resourcesId;
            Register = register;
        }

        public UserResource()
        {
        }

        public void SetUsersId(Guid usersId)
        {
            UsersId = usersId;
        }

        public void SetResourcesId(Guid resourcesId)
        {
            ResourcesId = resourcesId;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }
    }
}
