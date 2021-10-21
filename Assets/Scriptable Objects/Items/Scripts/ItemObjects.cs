using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Gold,
    Slaves,
    Plants, 
    Shells,
    Weapons,
    Food
}
public abstract class ItemObjects : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    public int tradingPrice;
    [TextArea(15, 20)]
    public string description;
    




}
