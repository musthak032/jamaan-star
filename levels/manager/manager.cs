using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class manager : gamemanager
    {

        protected gamemanager gamemanager;
        protected override void initialization()
        {
            base.initialization();
            gamemanager = FindObjectOfType<gamemanager>();
        }
      
    }
}
