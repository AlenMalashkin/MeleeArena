using Code.Data;
using Code.Services;
using Code.Services.KillCountService;
using Code.Services.WaveService;
using TMPro;
using UnityEngine;

namespace Code.UI.Elements
{
	public class DisplayTargetKillCount : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI killsRemainText;
		
		private IKillCountService _killCountService;
		private IWaveService _waveService;
		private WaveSettings _waveSettings;
		
		private void Awake()
		{
			_killCountService = ServiceLocator.Container.Resolve<IKillCountService>();
			_waveService = ServiceLocator.Container.Resolve<IWaveService>();
			_waveSettings = WaveSettingsByWaveType.GetWaveSettingsByWaveType(_waveService.Wave);
			
			UpdateText(_killCountService.KillCount);
		}

		private void OnEnable()
		{
			_killCountService.KillCountChanged += OnKillCountChanged;
		}

		private void OnDisable()
		{
			_killCountService.KillCountChanged += OnKillCountChanged;
		}

		private void OnKillCountChanged(int killCount)
		{
			UpdateText(killCount);
		}

		private void UpdateText(int killCount)
		{
			killsRemainText.text = "Осталось устранить: " + (_waveSettings.KillCount - killCount);
		}
	}
}