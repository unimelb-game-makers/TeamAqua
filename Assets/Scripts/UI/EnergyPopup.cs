using Kuroneko.UIDelivery;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EnergyPopup : Popup
    {
        [SerializeField] private Slider energyBar;
        [SerializeField] private Image energyFill;
        [SerializeField] private Gradient energyGradient;
        [SerializeField] private TMP_Text energyText;
    
        protected override void InitPopup()
        {
            EnergyManager.OnEnergyChanged += OnEnergyChanged;
        }

        private void OnEnergyChanged(float energyAmount)
        {
            float percentage = energyAmount / EnergyManager.MAX_ENERGY;
            energyBar.value = percentage;
            energyFill.color = energyGradient.Evaluate(percentage);
            energyText.text = $"{energyAmount} / {EnergyManager.MAX_ENERGY}";
        }

        private void OnDestroy()
        {
            EnergyManager.OnEnergyChanged -= OnEnergyChanged;
        }
    }
}
