using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace metroidvania
{
    public class savegame : manager
    {

        
        [SerializeField] protected int reference;
        Animator anim;
        protected override void initialization()
        {
            base.initialization();
            anim = GetComponent<Animator>();
        }

        // Start is called before the first frame update
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == player)
            {
                save();
                collision.gameObject.GetComponent<playersound>().letterrrakadakuthu.Play();
                gameObject.GetComponentInChildren<ParticleSystem>().Play();
            }


        }
        protected virtual void save()
        {
            PlayerPrefs.SetString(" " + character.gamefile + "loadgame",SceneManager.GetActiveScene().name);
            PlayerPrefs.SetInt(" " + character.gamefile + "savespawnreference", reference);
            PlayerPrefs.SetInt(" " + character.gamefile + "facingleft",character.isfaceleft?1:0);
            PlayerPrefsX.SetIntArray(" " + character.gamefile + "tilestoremove",levelmanager.tileid);
            player.GetComponent<Health>().healthpoints = player.GetComponent<Health>().maxhealthpoints;

        }
    }
}
