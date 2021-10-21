using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoldCounter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject inventory;
    public bool isEnabled;

    [SerializeField] public int currency;
    [SerializeField] public static int maxGold = 2000;
    [SerializeField] public static GoldCounter instance;
    public InventoryItemm inventoryItemm;
    private TMP_Text goldnum;
    private TMP_Text currencyText;
    private void Awake()
    {
       instance = this;
       goldnum = FindObjectOfType<Spawn>().goldNum;
       currencyText = FindObjectOfType<Spawn>().currencyText;
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    
    public void CoinCoollect()
    {
      var item = GetComponent<Item>();
        if (GoldBeacon.Digged)
        {
            int amount = Random.Range(50, 500);
            currency += amount;
            goldnum.text = amount + "Gold".ToString();
            currencyText.text = "GOLD: " +currency.ToString();
            Debug.Log(goldnum.text);       
            PlayerPrefs.SetInt("Gold Coins", currency);     
        } 
        inventoryItemm.AddItem(item.item, currency);
        Debug.Log("Coins Collected:" + currency);
    }

    

}
