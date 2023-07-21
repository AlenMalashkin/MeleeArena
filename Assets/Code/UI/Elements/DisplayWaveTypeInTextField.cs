using System;
using Code.Data;
using Code.Services;
using Code.Services.WaveService;
using TMPro;
using UnityEngine;

namespace Code.UI.Elements
{
	public class DisplayWaveTypeInTextField : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI text;

		private IWaveService _waveService;

		private void Awake()
		{
			_waveService = ServiceLocator.Container.Resolve<IWaveService>();

			WaveType waveType = WaveTypeByWave.GetWaveTypeByWave(_waveService.Wave - 1);

			switch (waveType)
			{
				case WaveType.DefaultWave:
					DisplayDefault();
					break;
				case WaveType.BlueWave:
					DisplayBlue();
					break;
				case WaveType.RedWave:
					DisplayRed();
					break;
				case WaveType.GreenWave:
					DisplayGreen();
					break;
				case WaveType.GoldWave:
					DisplayGold();
					break;
				case WaveType.PurpleWave:
					DisplayPurple();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void DisplayDefault()
		{
			text.text = "Обычная волна";
			text.color = Color.white;
		}
		
		private void DisplayBlue()
		{
			text.text = "Синяя волна";
			text.color = Color.blue;
		}
		
		private void DisplayRed()
		{
			text.text = "Красная волна";
			text.color = Color.red;
		}

		private void DisplayGreen()
		{
			text.text = "Зеленая волна";
			text.color = Color.green;
		}
		
		private void DisplayGold()
		{
			text.text = "Золотая волна";
			text.color = Color.yellow;
		}
		
		private void DisplayPurple()
		{
			text.text = "Бесконечная волна";
			text.color = Color.magenta;
		}
	}
}