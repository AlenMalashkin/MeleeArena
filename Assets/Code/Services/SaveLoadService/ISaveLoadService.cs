using Code.Data;

namespace Code.Services.SaveLoadService
{
	public interface ISaveLoadService : IService
	{
		void Save();
		PlayerProgress Load();
	}
}