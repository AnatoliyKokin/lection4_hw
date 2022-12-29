

namespace lection4_hw.Services.Abstractions;

public interface IDbService<T>
{
    Task<List<T>> GetAllAsync();
}