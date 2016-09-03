﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class UserEntity
    {
        #region Ctors
        public UserEntity()
        {
            Roles = new HashSet<RoleEntity>();
        }
        #endregion

        #region Properties

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public DateTime CreatedOn { get; set; }
        public byte[] Photo { get; set; }
        public ICollection<RoleEntity> Roles { get; set; }

        #endregion
    }
}
