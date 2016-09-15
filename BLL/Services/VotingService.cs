using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using DAL.Concrete;
using DAL.Interface.DTO;
using Logger;
//using NLog;

namespace BLL.Services
{
    public class VotingService : IVotingService
    {
        #region Fields
        private readonly IUnitOfWork uow;
        private readonly IVotingRepository votingRepository;
        private readonly ILogger logger;        
        #endregion

        #region Ctors
        public VotingService(IUnitOfWork uow, IVotingRepository repository)
        {
            this.uow = uow;
            this.votingRepository = repository;
            logger = GlobalLogger.Logger;       
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Method Create create voting entity.
        /// </summary>
        /// <param name="votingEntity">New voting entity for create.</param>
        public void Create(VotingEntity votingEntity)
        {
            try {
                votingRepository.Create(votingEntity.ToDalVoting());
                uow.Commit();
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage("Create new Vote was failed.", this), ex);
            }
        }

        /// <summary>
        /// Method GetRatingForPhoto return summary rating for photo.
        /// </summary>
        /// <param name="photoId">Summary rating for photo by it's id.</param>
        /// <returns>Summary rating for photo by id.</returns>
        public double GetRatingForPhoto(int photoId)
        {
            try {
                return votingRepository.GetRatingForPhoto(photoId);
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage("Get rating for photo was failed.", this), ex);
            }

            return default(double);
        }

        /// <summary>
        /// Method GetRatingForPhotoUser return rating for photo by.
        /// </summary>
        /// <param name="photoId">Photo id.</param>
        /// <param name="userId">User id.</param>
        /// <returns>Returns rating for photo by user.</returns>
        public int GetRatingForPhotoUser(int photoId, int userId)
        {
            try {
                return votingRepository.GetRatingForPhotoUser(photoId, userId);
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage("Get rating for photo by user was failed.", this), ex);
            }

            return default(int);
        }

        /// <summary>
        /// Method GetAll return all votings.
        /// </summary>
        /// <returns>Lists of votings entity.</returns>
        public IEnumerable<VotingEntity> GetAll()
        {
            try {
                return votingRepository.GetAll().Select(vote => vote.ToBllVoting());
            }
            catch(Exception ex) { 
                logger.Error(logger.GetMessage("Get all votes was failed.", this), ex);
            }

            return null;
         }

        /// <summary>
        /// Method Update update exists voting.
        /// </summary>
        /// <param name="votingEntity">VotingEntity that need update.</param>
        public void Update(VotingEntity votingEntity)
        {
            try {
                votingRepository.Update(votingEntity.ToDalVoting());
                uow.Commit();
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage("Update vote for photo was failed.", this), ex);
            }            
        }

        /// <summary>
        /// Method return count of votes users.
        /// </summary>
        /// <param name="photoId">Photo id.</param>
        /// <returns>Number of votes.</returns>
        public int GetRaters(int photoId)
        {
            try {
                int raters = 0;
                var votes = votingRepository.GetAll().Select(vote => new VotingEntity
                {
                    UserId = vote.UserId,
                    PhotoId = vote.PhotoId,
                    Rating = vote.Rating
                });

                if (votes != null)
                    foreach (var vote in votes)
                        if (vote.PhotoId == photoId)
                            raters++;

                return raters;
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage(string.Format("Get raters for photo by photo with id {0} was failed.", photoId), this), ex);
            }
            
            return default(int);
        }

        #endregion
    }
}
