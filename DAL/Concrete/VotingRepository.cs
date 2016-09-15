using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;
using ORM;

namespace DAL.Concrete
{
    public class VotingRepository : IVotingRepository
    {
        #region Fields
        private readonly DbContext context;
        #endregion

        #region Ctors
        public VotingRepository(DbContext dbContext)
        {
            this.context = dbContext;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Method Create create voting entity.
        /// </summary>
        /// <param name="dalVoting">New voting entity for create.</param>
        public void Create(DalVoting dalVoting)
        {
            var voting = new Voting()
            {
                UserId = dalVoting.UserId,
                PhotoId = dalVoting.PhotoId,
                Rating = dalVoting.Rating
            };

            context.Set<Voting>().Add(voting);
        }

        /// <summary>
        /// Method GetRatingForPhoto return summary rating for photo.
        /// </summary>
        /// <param name="photoId">Summary rating for photo by it's id.</param>
        /// <returns>Summary rating for photo by id.</returns>
        public double GetRatingForPhoto(int photoId)
        {
            IEnumerable<DalVoting> votings = context.Set<Voting>().Select(vote => new DalVoting()
            {
                UserId = vote.UserId,
                PhotoId = vote.PhotoId,
                Rating = vote.Rating
            }).Where(votes => votes.PhotoId == photoId);

            int sum = 0;

            foreach (var vote in votings)
                sum += vote.Rating;

            if(votings.Count() != 0)
                return (double)sum / votings.Count();

            return sum;
        }

        /// <summary>
        /// Method Delete delete voting entity.
        /// </summary>
        /// <param name="entity">DalVoting entity.</param>
        public void Delete(DalVoting entity)
        {
            var vote = new Voting()
            {
                PhotoId = entity.PhotoId,
                UserId = entity.UserId
            };

            context.Set<Voting>().Remove(vote);
        }

        /// <summary>
        /// Method GetAll return all votings.
        /// </summary>
        /// <returns>Lists of votings entity.</returns>
        public IEnumerable<DalVoting> GetAll()
        {
            IEnumerable<DalVoting> votes = context.Set<Voting>().Select(vote => new DalVoting()
            {
                PhotoId = vote.PhotoId,
                UserId = vote.UserId,
                Rating = vote.Rating
            });

            return votes;
        }

        /// <summary>
        /// Method GetById return DalVoting entity by id.
        /// </summary>
        /// <param name="id">Id of voting entity.</param>
        /// <returns>DalVoting entity by id.</returns>
        public DalVoting GetById(int id)
        {
            return GetAll().SingleOrDefault(vote => vote.Id == id);
        }

        /// <summary>
        /// Method GetByPredicate return DalVoting by predicate.
        /// </summary>
        /// <param name="f">Predicate.</param>
        /// <returns>Return DalVoting by predicate f.</returns>
        public DalVoting GetByPredicate(Expression<Func<DalVoting, bool>> f)
        {
            Voting item = context.Set<Voting>().SingleOrDefault(f.ConvertExpressionVote());

            return new DalVoting {
                UserId = item.UserId,
                PhotoId = item.PhotoId,
                Rating = item.Rating
            };
        }

        /// <summary>
        /// Method Update update exists voting.
        /// </summary>
        /// <param name="dalVoting">DalVoting that need update.</param>
        public void Update(DalVoting dalVoting)
        {
            var vote = context.Set<Voting>().Where(photo => photo.PhotoId == dalVoting.PhotoId).Where(user => user.UserId == dalVoting.UserId).FirstOrDefault();

            Voting original = new Voting()
            {
                PhotoId = vote.PhotoId,
                UserId = vote.UserId,
                Rating = vote.Rating
            };

            context.Set<Voting>().Attach(original);

            vote.Rating = dalVoting.Rating;
            context.SaveChanges();            
        }

        /// <summary>
        /// Method GetRatingForPhotoUser return rating for photo by.
        /// </summary>
        /// <param name="photoId">Photo id.</param>
        /// <param name="userId">User id.</param>
        /// <returns>Returns rating for photo by user.</returns>
        public int GetRatingForPhotoUser(int photoId, int userId)
        {
            var voting = context.Set<Voting>().Select(vote => new DalVoting()
            {
                UserId = vote.UserId,
                PhotoId = vote.PhotoId,
                Rating = vote.Rating
            }).Where(votes => votes.PhotoId == photoId && votes.UserId == userId).FirstOrDefault();

            if(voting != null)
                return voting.Rating;
            return default(int);
        }

        #endregion
    }
}
