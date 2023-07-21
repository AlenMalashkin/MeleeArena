using Code.Services;
using Code.Services.Bank;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Code.UI.Elements
{
	public class RewardAdShow : MonoBehaviour
	{
		[SerializeField] private int rewVideoId;
		[SerializeField] private Button button;

		private IBank _bank;
		
		private void Awake()
		{
			_bank = ServiceLocator.Container.Resolve<IBank>();
		}

		private void OnEnable()
		{
			button.onClick.AddListener(() => YandexGame.RewVideoShow(rewVideoId));
			YandexGame.RewardVideoEvent += OnRewardVideoClosed;
		}

		private void OnRewardVideoClosed(int id)
		{
			if (id != rewVideoId)
				return;
			
			_bank.GetMoney(1000);
			YandexGame.RewardVideoEvent -= OnRewardVideoClosed;
		}
	}
}