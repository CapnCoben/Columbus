using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class DialogueStart : MonoBehaviour
{
    public NPCConversation myConversation;
    public DirectorControlPlayable director;
    public PlayableDirector playableD;
    public AudioSource s;
    public  void OnEnable()
    {
        ConversationManager.OnConversationStarted += ConversationStart;
        ConversationManager.OnConversationEnded += ConversationEnd;
    }
    public void OnDisable()
    {
        ConversationManager.OnConversationStarted -= ConversationStart;
        ConversationManager.OnConversationEnded -= ConversationEnd;
    }
    private void ConversationStart()
    {
        s.Play();
        Debug.Log("A conversation has began.");
    }
    private void ConversationEnd()
    {
        s.Stop();
        playableD.Resume();
        Debug.Log("A conversation has ended.");
        
    }




}
