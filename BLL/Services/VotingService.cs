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

        #endregion
    }
}
