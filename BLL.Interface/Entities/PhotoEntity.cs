using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class PhotoEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}
