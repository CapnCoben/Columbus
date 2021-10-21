using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastTravel : MonoBehaviour
{
    public GameObject fastTravelTransitionScreen;
    public GameObject player;
    PlayerController controller;
    Animator animNow;
    public Transform[] travelPos;
    public GameObject otherUI;
    // Start is called before the first frame update
    void Start()
    {
        fastTravelTransitionScreen.SetActive(false);
        player = GameObject.FindWithTag("Player");
        controller = player.GetComponent<PlayerController>();
    }


    public void TravelDestination(int posIndex)
    { 
        fastTravelTransitionScreen.SetActive(true);
        controller.enabled = false;
        player.transform.position = travelPos[posIndex].position;
       
    } 

    public void WaitForAnim(Animator anim)
    {
        StartCoroutine(Loading(anim));
    }
    public IEnumerator Loading(Animator anim)
    {
        animNow = anim;
        yield return new WaitForSeconds(1f);
        otherUI.SetActive(false);
        animNow.Play("Guanahani Canvas");
        yield return new WaitForSeconds(1);
        fastTravelTransitionScreen.SetActive(false);  
        gameObject.SetActive(false);
        controller.enabled = true;
        otherUI.SetActive(true);
     ;

    }
   
}
