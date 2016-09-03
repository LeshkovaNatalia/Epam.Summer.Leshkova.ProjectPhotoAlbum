using DAL.Interface.DTO;
using DAL.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Data.Entity;
using ORM;

namespace DAL.Concrete
{
    public class RoleRepository : IRepository<DalRole>
    {
        #region Fields
        private readonly DbContext context;
        #endregion

        #region Ctors
        public RoleRepository(DbContext uow)
        {
            this.context = uow;
        }
        #endregion

        #region Public Methods

        public void Create(DalRole entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(DalRole entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DalRole> GetAll()
        {
            return context.Set<Role>().Select(role => new DalRole()
            {
                Id = role.RoleId,
                Name = role.Name
            });
        }

        public DalRole GetById(int key)
        {
            throw new NotImplementedException();
        }

        public DalRole GetByPredicate(Expression<Func<DalRole, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Update(DalRole entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
