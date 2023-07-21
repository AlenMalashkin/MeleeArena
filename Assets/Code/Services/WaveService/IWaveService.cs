namespace Code.Services.WaveService
{
	public interface IWaveService : IService
	{
		int Wave { get; }
		void PassWave();
	}
}