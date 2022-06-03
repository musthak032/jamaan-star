using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class dialoguetrigger : MonoBehaviour
    {
        public dialogue Dialogue;

        public void triggerdialogue()
        {

            FindObjectOfType<dialogmanager>().Startdialogue(Dialogue);

        }
        //new
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                GameObject dialoguecanvas = FindObjectOfType<dialogueuifinder>().gameObject;
                dialoguecanvas.SetActive(false);
                dialoguecanvas.SetActive(true);

                FindObjectOfType<dialogmanager>().Startdialogue(Dialogue);
   
            }
        }
    }
}
