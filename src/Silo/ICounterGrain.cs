using Orleans;

namespace Silo;

public interface ICounterGrain : IGrainWithStringKey
{
    ValueTask<int> Get();
    ValueTask<int> Increment();
}