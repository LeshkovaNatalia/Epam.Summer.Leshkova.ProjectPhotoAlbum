using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IVotingRepository : IRepository<DalVoting>
    {
        double GetRatingForPhoto(int photoId);
        int GetRatingForPhotoUser(int photoId, int userId);
    }
}
