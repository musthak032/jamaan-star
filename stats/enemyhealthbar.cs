using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace metroidvania
{
    public class enemyhealthbar : manager
    {
        protected Slider slider;
        protected enemyhealth enemyhealth;
        protected override void initialization()
        {
            base.initialization();
            slider = GetComponent<Slider>();
            enemyhealth =GetComponentInParent<enemyhealth>();
            slider.maxValue = enemyhealth.maxhealthpoints;
            slider.value = enemyhealth.healthpoints;
            
        }
        private void LateUpdate()
        {
            slider.value = enemyhealth.healthpoints;

        }
    }
}
