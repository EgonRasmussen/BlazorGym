using BlazorGymServerWASM.Client.Services;

namespace BlazorGymServerWASM.Services;

public class SRSingletonService : ISingletonService, IDisposable
{
    public Guid Guid { get; set; } = Guid.NewGuid();

    public void Dispose() => Console.WriteLine("SRSingletonService Disposed");
}