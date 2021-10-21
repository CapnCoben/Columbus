using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gold Object", menuName = "Inventory System/Items/Gold", order = 2)]
public class GoldItem : ItemObjects
{
    public void Awake()
    {
        type = ItemType.Gold;
    }

}
