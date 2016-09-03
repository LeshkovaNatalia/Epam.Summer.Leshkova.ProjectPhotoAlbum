using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalPhoto : IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}
