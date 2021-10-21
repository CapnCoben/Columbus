using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    public InventoryItemm inventory;

    public int X_Space_Between_Objects;
    public int numberOfColumns;
    public int Y_Space_Between_Objects;
    public CanvasGroup InventoryUI;

    Dictionary<InventorySlot, GameObject> itemDisplayed = new Dictionary<InventorySlot, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
       UpdateDisplay();
       if(Input.GetKeyDown(KeyCode.I))
        {
          if(InventoryUI.alpha == 0)
            {
                InventoryUI.alpha = 1;
                InventoryUI.blocksRaycasts = true;
                InventoryUI.interactable = true;
            }
          else
            {

                InventoryUI.alpha = 0;
                InventoryUI.blocksRaycasts = false;
                InventoryUI.interactable = false;
            }
        }
    }

    public void UpdateDisplay()
    {
        for(int i = 0; i < inventory.container.Count; i++)
        {
            if(itemDisplayed.ContainsKey(inventory.container[i]))
            {
                itemDisplayed[inventory.container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventory.container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
                itemDisplayed.Add(inventory.container[i], obj);
            }
        }
    }
    public void CreateDisplay()
    {
        for(int i = 0; i < inventory.container.Count; i++) 
        {
            var obj = Instantiate(inventory.container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
            itemDisplayed.Add(inventory.container[i], obj);
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_Space_Between_Objects * (i % numberOfColumns),(Y_Space_Between_Objects * (i / numberOfColumns)), 0f);
    }
}
