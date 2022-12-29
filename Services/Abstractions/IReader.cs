
namespace lection4_hw.Services.Abstractions;


public interface IReader<T>
{
    T? Read(string path);
}