using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BlendMusic : MonoBehaviour
{
    public AudioSource s1;
    public AudioSource s2;


    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(s1.volume >= .5)
            {
                Mathf.Lerp(s1.volume, 0, .5f);
                Mathf.Lerp(s2.volume, 1, .5f);
            }
        }    
    }

}
