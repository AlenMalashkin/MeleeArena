using System.Threading.Tasks;
using Code.Services;

namespace Code.UI.Services.Factory
{
	public interface IUIFactory : IService
	{
		void CreateLoseScreen();
		void CreateWinScreen();
		void CreateMenu();
		void CreateShop();
		Task CreateUIRoot();
	}
}