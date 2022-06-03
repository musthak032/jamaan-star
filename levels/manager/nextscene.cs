using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class nextscene : manager
    {
        [SerializeField] protected SceneReference Nextscene;
        [SerializeField] protected int locationreference;
        protected override void initialization()
        {
            base.initialization();
        }
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == player)
            {
                levelmanager.loadfromsave = false;

                PlayerPrefs.SetInt(" " + character.gamefile + "loadfromsave", levelmanager.loadfromsave ? 1 : 0);

              
                levelmanager.NextScene(Nextscene, locationreference);
            }

        }

    }
}
