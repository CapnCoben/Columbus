using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Items/Weapon", order = 6)]
public class WeaponsItem : ItemObjects
{

    public void Awake()
    {
        type = ItemType.Weapons; 
    }

}
