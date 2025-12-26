using BlazorGymServerWASM.Client.Services;

namespace BlazorGymServerWASM.Services;

public class SRTransientService : ITransientService, IDisposable
{
    public Guid Guid { get; set; } = Guid.NewGuid();

    public void Dispose() => Console.WriteLine("TransientService Disposed");
}
