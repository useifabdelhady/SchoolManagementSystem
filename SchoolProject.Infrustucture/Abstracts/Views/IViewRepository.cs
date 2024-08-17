using SchoolProject.Infrustucture.InfrustuctureBases;

namespace SchoolProject.Infrustucture.Abstracts.Views
{
    public interface IViewRepository<T> : IGenericRepositoryAsync<T> where T : class
    {
    }
}
