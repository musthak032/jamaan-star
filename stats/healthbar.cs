using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace metroidvania
{
    public class healthbar : manager
    {
        protected Slider slider;
        protected playerhealth playerhealth;
        protected override void initialization()
        {
            base.initialization();
            slider = GetComponent<Slider>();
            playerhealth = player.GetComponent<playerhealth>();
            slider.maxValue = playerhealth.maxhealthpoints;
            slider.value = playerhealth.healthpoints;
            slider.value = PlayerPrefs.GetInt(" " + character.gamefile + " currenthealth");
        }
        private void LateUpdate()
        {
            slider.value = playerhealth.healthpoints;

        }
    }
}
