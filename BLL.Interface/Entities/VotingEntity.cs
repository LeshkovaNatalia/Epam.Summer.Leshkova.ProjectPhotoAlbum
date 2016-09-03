using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class VotingEntity
    {
        public int UserId { get; set; }
        public int PhotoId { get; set; }
        public int Rating { get; set; }
    }
}
