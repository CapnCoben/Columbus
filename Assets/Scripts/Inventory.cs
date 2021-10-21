using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public static int currency;
    [SerializeField] private Text currencyText;
    [SerializeField] public TMP_Text goldNum;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {

    }
    
    public void CoinCoollect()
    {


        if (GoldBeacon.Digged)
        {
            int amount = Random.Range(50, 500);
            currency += amount;
            goldNum.text = currency + "Gold";
            currencyText.text = "Gold Coins:" + currency;

            PlayerPrefs.SetInt("Gold Coins", currency);

        }

        Debug.Log("Coins Collected:" + currency);
    }

    

}
