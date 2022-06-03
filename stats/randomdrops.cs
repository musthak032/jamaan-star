using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomdrops : MonoBehaviour
{
    [SerializeField] protected List<GameObject> Randomdrops = new List<GameObject>();
    [SerializeField] protected int[] droppercentage;


    public virtual void roll()
    {

        int randomnumber = 0; 
        int total = 0;
        foreach(int item in droppercentage)
        {

            total += item;

        }
        randomnumber = Random.Range(1, total);
        for(int i = 0; i < droppercentage.Length; i++)
        {

            if (randomnumber <= droppercentage[i])
            {

                Instantiate(Randomdrops[i], transform.position, Quaternion.identity);
                return;
            }
            else
            {
                randomnumber -= droppercentage[i];
            }
        }
    }
}
