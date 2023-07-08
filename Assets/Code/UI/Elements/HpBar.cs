using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Elements
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image filler;

        public void SetValue(float currentHp, float maxHp)
        {
            filler.fillAmount = currentHp / maxHp;
        }
    }
}
