using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface IVotingService
    {
        void Create(VotingEntity votingEntity);
        void Update(VotingEntity votingEntity);

        IEnumerable<VotingEntity> GetAll();
        double GetRatingForPhoto(int photoId);
        int GetRatingForPhotoUser(int photoId, int userId);

    }
}
