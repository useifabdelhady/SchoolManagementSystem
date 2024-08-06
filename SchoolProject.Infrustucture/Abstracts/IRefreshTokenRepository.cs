using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrustucture.InfrustuctureBases;

namespace SchoolProject.Infrustucture.Abstracts
{
    public interface IRefreshTokenRepository : IGenericRepositoryAsync<UserRefreshToken>
    {
    }
}
