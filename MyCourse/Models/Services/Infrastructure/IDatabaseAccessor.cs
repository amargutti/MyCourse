using System.Data;

namespace MyCourse.Models.Services.Infrastructure
{
    public interface IDatabaseAccessor
    {
        Task<int> CommandAsync(FormattableString command);
        Task<DataSet> QueryAsync(FormattableString query);
        Task<T> QueryScalarAsync<T>(FormattableString query);
    }
}
