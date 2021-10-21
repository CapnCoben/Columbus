using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Shells Object", menuName = "Inventory System/Items/Shells", order = 1)]
public class DefaultItem : ItemObjects
{
    public void Awake()
    {
        type = ItemType.Shells;
    }

}