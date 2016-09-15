using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Mappers;
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

        /// <summary>
        /// Method Create create role entity.
        /// </summary>
        /// <param name="dalRole">New role entity for create.</param>
        public void Create(DalRole dalRole)
        {
            var role = new Role()
            {
                Name = dalRole.Name
            };

            context.Set<Role>().Add(role);
        }

        /// <summary>
        /// Method Delete delete role. 
        /// </summary>
        /// <param name="dalRole">Entity that need delete.</param>
        public void Delete(DalRole dalRole)
        {
            var role = new Role()
            {
                Name = dalRole.Name
            };

            context.Set<Role>().Remove(role);
        }

        /// <summary>
        /// Method GetAll return all roles.
        /// </summary>
        /// <returns>Lists of roles entity.</returns>
        public IEnumerable<DalRole> GetAll()
        {
            return context.Set<Role>().Select(role => new DalRole()
            {
                Id = role.RoleId,
                Name = role.Name
            });
        }

        /// <summary>
        /// Method GetById return DalRole entity by id.
        /// </summary>
        /// <param name="id">Id of role entity.</param>
        /// <returns>DalRole entity by id.</returns>
        public DalRole GetById(int id)
        {
            return GetAll().SingleOrDefault(role => role.Id == id);
        }

        /// <summary>
        /// Method GetByPredicate return DalRole by predicate.
        /// </summary>
        /// <param name="f">Predicate.</param>
        /// <returns>Return DalRole by predicate f.</returns>
        public DalRole GetByPredicate(Expression<Func<DalRole, bool>> f)
        {
            Role item = context.Set<Role>().SingleOrDefault(f.ConvertExpressionRole());

            return new DalRole { Id = item.RoleId, Name = item.Name };
        }

        /// <summary>
        /// Method Update update exists role.
        /// </summary>
        /// <param name="dalPhoto">DalRole that need update.</param>
        public void Update(DalRole entity)
        {
            var actualRole = GetById(entity.Id);
            Role updatedRole = new Role()
            {
                RoleId = actualRole.Id,
                Name = actualRole.Name
            };
            context.Set<Role>().Attach(updatedRole);
            var role = context.Entry(updatedRole);
            role.Property(r => r.Name).IsModified = true;
            context.SaveChanges();
        }

        #endregion
    }
}
