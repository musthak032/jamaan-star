using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class AiManager : EnemeyCharacter
    {
        protected EnemeyCharacter enemycharacter;
        protected override void Initialization()
        {
            base.Initialization();
            enemycharacter = GetComponent<EnemeyCharacter>();
        }
        
    }
}
