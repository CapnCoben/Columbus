using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CharSelect : MonoBehaviour
{
    private int selectedCharacterIndex;
    private int index;
    private Color desiredColor;

    [Header("List of Characters")]
    [SerializeField] private List<CharacterSelectObject> characterList = new List<CharacterSelectObject>();

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Image characterImage;
    [SerializeField] private Image backgroundImage;


    [Header("Tweaks")]
    [SerializeField] private float backgroundColorTransitionSpeed = 10f;

    private void Start()
    {
        selectedCharacterIndex = PlayerPrefs.GetInt("CharacterSelected");
        UpdateCharacterSelectionUI();
    }

    void Update()
    {
        backgroundImage.color = Color.Lerp(backgroundImage.color, desiredColor, Time.deltaTime * backgroundColorTransitionSpeed);
    }

    public void Confirm()
    {
        PlayerPrefs.SetInt("CharacterSelected", selectedCharacterIndex);
        Debug.Log(string.Format("Character {0}:{1} has been chosen", selectedCharacterIndex, characterList[selectedCharacterIndex].characterName));
    }
    public void LeftArrow()
    {
        selectedCharacterIndex--;

        if (selectedCharacterIndex < 0)
            selectedCharacterIndex = characterList.Count - 1;
        UpdateCharacterSelectionUI();
    }
    public void RightArrow()
    {
        selectedCharacterIndex++;

        if(selectedCharacterIndex == characterList.Count)
        {
            selectedCharacterIndex = 0;
           
        }

        UpdateCharacterSelectionUI();
    }
    private void UpdateCharacterSelectionUI()
    {
        //splash, name, desired color, current player
       characterImage.sprite = characterList[selectedCharacterIndex].splash;
       characterName.text = characterList[selectedCharacterIndex].characterName;
        desiredColor = characterList[selectedCharacterIndex].characterColor;
        


    }
    [System.Serializable]
    public class CharacterSelectObject
    {
        public Sprite splash;
        public string characterName;
        public Color characterColor;
    }
 
}
