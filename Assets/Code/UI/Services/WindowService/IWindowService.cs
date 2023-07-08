using Code.Services;
using Code.UI.Windows;

namespace Code.UI.Services.WindowService
{
	public interface IWindowService : IService
	{
		void Open(WindowType type);
	}
}