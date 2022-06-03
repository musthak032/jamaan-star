using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace metroidvania
{
    public class dialogmanager : MonoBehaviour
    {
        public Animator anim;
        public Text nametext;
        public Text dialoguetext;
        private Queue<string> sentences;

        //new
        GameObject showcanvas;
        void Start()
        {
            sentences = new Queue<string>();
            showcanvas = FindObjectOfType<showcanvas>().gameObject; //new
        }
        public void Startdialogue(dialogue dialogue)
        {

            Invoke("offcanvas", .1f); //new
            anim.SetBool("open", true);
            nametext.text = dialogue.name;
            sentences.Clear();
            foreach(string sentence in dialogue.sentences)
            {


                sentences.Enqueue(sentence);
            }
            DisplayNextScentences();
        }
        public void DisplayNextScentences()
        {

            if (sentences.Count == 0)
            {

                enddialogue();
                return;
            }
           string sentence =sentences.Dequeue();
            //dialoguetext.text = sentence;
            StopCoroutine(typesentence(sentence));
            StartCoroutine(typesentence(sentence));

           
        }
        IEnumerator typesentence(string sentence)
        {
            dialoguetext.text = "";
            foreach(char letter in sentence.ToCharArray())
            {

                dialoguetext.text += letter;
                yield return null;
            }

        }
        public void enddialogue()
        {


            anim.SetBool("open", false);
            showcanvas.SetActive(true);

        }
        protected virtual void offcanvas()
        {
           
            showcanvas.SetActive(false); //new
            FindObjectOfType<character>().gameObject.GetComponent<inputmanager>().offallmobileinput();


        }
       



    }
}
