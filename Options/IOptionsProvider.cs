
namespace lection4_hw.Options;

public interface IOptionsProvider
{
    void GetOptions(Action<AppOptions> successAction, Action<AppOptions> errorAction);
}