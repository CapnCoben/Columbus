using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Plants", order = 4)]
public class PlantItems : ItemObjects
{
    public void Awake()
    {
        type = ItemType.Plants;
    }

}
