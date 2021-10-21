
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GoldBeacon : MonoBehaviour
{
    [Header("Variables")]
    public float sphereRadius = 2f;
    public LayerMask layerMask;
    public Vector3 collision;
    [SerializeField] ParticleSystem emitParticle = null;

    [Header("Gold UI")]
    public GameObject goldUI;
    public CanvasGroup coinScreen;
    public static bool Digged;

    [Header("Audio Elements")]
    public AudioSource coin;

    public GoldCounter count;

    private PlayerController Player;
    private List<CharacterController> trackedPersons = new List<CharacterController>();


    private void Awake()
    {
       
        count = FindObjectOfType<GoldCounter>(); 
        Player = FindObjectOfType<PlayerController>();
        coinScreen = FindObjectOfType<Spawn>().VictoryScreen;
    }

    private void Start()
    {
            
        
    

    }

    public enum Behavior
    {
      Signaling,
      Idling
    }

    private Behavior behavior;

    


    void Update()
    {
        switch (behavior)
        {
            case Behavior.Signaling:
                Signal();
                break;
            case Behavior.Idling:
                Idle();
                break;
            default:
                break;
        }
      
       
    }
    public  void Digging()
    {
       StartCoroutine(Fade());
      
    }
    void Idle()
    {

      
    }

    private void Signal()
    {    
        RaycastHit hit;
         if (Physics.SphereCast(this.transform.position + this.GetComponent<SphereCollider>().center, sphereRadius, Vector3.down, out hit, 100f, layerMask))
        {
            collision = hit.point;      
            
        }
    }
    public IEnumerator Fade()
    {
        //Initialize Our State
        float currentTime = 0;
        coin = GetComponent<AudioSource>();
        Animator anim = coinScreen.GetComponent<Animator>();
        //Start Loop and Loop until time is up
        while (currentTime < 1)
        {
            Digged = true;
            GoldCounter.instance.CoinCoollect(); 
            currentTime++;
            //fade screen
            anim.Play("Panel");
        }
        //Wait one frame
        yield return new WaitForSeconds(.5f * Time.deltaTime);

            //increase current time
        //Make sure screen is fully black
        //wait a while to play dig sound
        yield return new WaitForEndOfFrame();
        goldUI.SetActive(false);
        coin.Play();
        // before fadein count gold



        //fade in 

        //Initialize Our State
        
        currentTime = 0;
        //Start Loop and Loop until time is up
        while (currentTime < 1)
        {
            //Wait one frame
            yield return new WaitForEndOfFrame();
            //increase current time
            currentTime += Time.deltaTime;
        }
        //Make sure screen is fully clear
        yield return new WaitForSeconds(0.1f * Time.deltaTime);
        Digged = false;
        gameObject.SetActive(false);


    }

    private void Emit()
    {
        emitParticle.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
          goldUI.SetActive (true);
        }
        ManageTrackedPersons(other,true);
        Emit();
    }

    private void OnTriggerExit(Collider other)
    {
        goldUI.SetActive(false);
       ManageTrackedPersons(other, false);
       emitParticle.Stop();  
    }


    public void ManageTrackedPersons(Collider other, bool entered)
    {
       CharacterController Person = other.GetComponent<CharacterController>();
       if(other.GetComponent<CharacterController>() != null)
        {
          
          if (entered == true)
            {
                trackedPersons.Add(Person);
            }
            else
            {
                trackedPersons.Remove(Person);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position + this.GetComponent<SphereCollider>().center + Vector3.down * 1.0f, sphereRadius);
    }
}
