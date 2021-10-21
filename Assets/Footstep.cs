using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    // Start is called before the first frame update

    public LayerMask mask;
    private GameObject lastHit;

    public string soundName;
    public AudioClip soundAudio;
  

    public Dictionary<int, string> layersounds = new Dictionary<int, string>();
    public Dictionary<string, FSounds> footstepsound = new Dictionary<string, FSounds>();
    public AudioSource Lsource;
    public AudioSource Rsource;
    public AudioClip footstep_r;
    public AudioClip footstep_l;
    public AudioClip sand;
    public AudioClip dirtAudio;
    public AudioClip rock;
    public AudioClip concrete;
    public AudioClip waterAudio;
    void Start()
    {
        layersounds.Add(0, "Default");
        layersounds.Add(4, "Wateb");
        layersounds.Add(6, "Grounf");
        FSounds concrete = new FSounds("Concrete",dirtAudio);
       FSounds sand = new FSounds("Sand", dirtAudio );
       FSounds dirt = new FSounds("Dirt", dirtAudio);
       FSounds rocks = new FSounds("Rocks", dirtAudio);
        FSounds water = new FSounds("Water", waterAudio); 

        footstepsound.Add("concrete", concrete);
        footstepsound.Add("sand", sand);
        footstepsound.Add("Grounf", dirt);
        footstepsound.Add("rocks", rocks);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

  

    public void OnRightFootstepRun()
    {
        FootCast("Run", Rsource);
    }


    public void OnLeftFootstepRun()
    {
        FootCast("Run", Lsource);

    }

    public void OnLeftFootstepWalk()
    {

        FootCast("Walk", Lsource);
    }
    public void OnRightFootstepWalk()
    { 
         FootCast("Walk", Rsource);
    }

    public void OnFootstepJump()
    {
        Lsource.PlayOneShot(footstep_l);
    }

    private void FootCast(string footstep, AudioSource FootSource)
    {
        var ray = new Ray(this.transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast( ray, out hit))
        {

            lastHit = hit.transform.gameObject;
            mask.value = lastHit.layer;
            string l;
            layersounds.TryGetValue(mask.value, out l);
            Debug.Log(l);
            footstepsound.TryGetValue(l, out var freesound );
            Debug.Log(freesound);
            FootSource.PlayOneShot(freesound.soundAudio, footstep == "Walk" ? 0.5f : 1.0f);
            Debug.Log(freesound);
        }
    }

}
