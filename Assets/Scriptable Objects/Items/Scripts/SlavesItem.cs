using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Slave Object", menuName = "Inventory System/Items/Slave", order = 3)]
public class SlavesItem : ItemObjects
{

    public void Awake()
    {
        type = ItemType.Slaves;
    }

}
