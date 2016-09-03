using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalVoting : IEntity
    {
        public int UserId { get; set; }
        public int Rating { get; set; }
        public int PhotoId { get; set; }

        public int Id
        {
            get
            {
                return 0;
            }
            set { Id = 0; }
        }
    }
}
