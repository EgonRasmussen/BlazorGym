using BlazorGymServerWASM.Client.Services;

namespace BlazorGymServerWASM.Services;

public class SRScopedService : IScopedService, IDisposable
{
    public Guid Guid { get; set; } = Guid.NewGuid();

    public void Dispose() => Console.WriteLine("SRScopedService Disposed");
}
