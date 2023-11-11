using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundButton : MonoBehaviour
{
   bool activated = false;
   public GameObject[] activate;



    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player") && activated == false) 
        {
            GetComponent<Animator>().Play("activate");

            for( int i =0; i<activate.Length; i++)
            {
                activate[i].GetComponent<Animator>().Play("activate");
            }
        }
    }
}
