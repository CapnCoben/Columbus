using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSounds 
{
    public string soundName;
    public AudioClip soundAudio;

    //[Serializable]
    //public class FSounds

    public FSounds (string newstring, AudioClip newAudio)
    {
        soundName = newstring;
        soundAudio = newAudio;
    }
}
