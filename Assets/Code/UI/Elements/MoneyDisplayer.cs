using System;
using Code.Services;
using Code.Services.Bank;
using TMPro;
using UnityEngine;

namespace Code.UI.Elements
{
	public class MoneyDisplayer : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI displayMoneyCount;
		
		private IBank _bank;
		
		private void Awake()
		{
			_bank = ServiceLocator.Container.Resolve<IBank>();
			displayMoneyCount.text = _bank.Money + "";
		}

		private void OnEnable()
		{
			_bank.MoneyChanged += OnMoneyChanged;
		}

		private void OnDisable()
		{
			_bank.MoneyChanged -= OnMoneyChanged;
		}

		private void OnMoneyChanged(int money)
		{
			displayMoneyCount.text = _bank.Money + "";
		}
	}
}