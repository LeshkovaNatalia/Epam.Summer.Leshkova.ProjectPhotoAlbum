using DAL.Interface.DTO;
using DAL.Interface.Repository;
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

        public void Delete(DalVoting entity)
        {
            throw new NotImplementedException();
        }

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

        public DalVoting GetById(int key)
        {
            throw new NotImplementedException();
        }

        public DalVoting GetByPredicate(Expression<Func<DalVoting, bool>> f)
        {
            throw new NotImplementedException();
        }

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
