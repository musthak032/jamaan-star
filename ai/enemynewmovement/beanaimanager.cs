using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace metroidvania
{
    public class beanaimanager : beanenemy
    {
        protected beanenemy beanenemy;
        protected override void Initialization()
        {
            base.Initialization();
            beanenemy = GetComponent<beanenemy>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
