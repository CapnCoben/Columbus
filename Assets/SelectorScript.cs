using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorScript : MonoBehaviour
{
    private GameObject[] characters;
    private int index;
    private void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected");

        characters = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            characters[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject go in characters)
        {
            go.SetActive(false);
        }
        if (characters[index])
        {
            characters[index].SetActive(true);
        }
    }
    public void LeftArrow()
    {

        characters[index].SetActive(false);
        index--;

        if (index < 0)
        {
            index = characters.Length - 1;
        }
        characters[index].SetActive(true);
    }
    public void RightArrow()
    {

        characters[index].SetActive(false);

        index++;
        if (index == characters.Length)
        {
            index = 0;
        }
        characters[index].SetActive(true);
    }

    public void Confirm()
    {
        Debug.Log(characters[index].ToString() + "selected");
        PlayerPrefs.SetInt("CharacterSelected", index);
    }

}
