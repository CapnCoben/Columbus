using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform playerPos;
    private void LateUpdate()
    {
        Vector3 newPos = playerPos.position;
        newPos.y = transform.position.y;
        transform.position = newPos;

        transform.rotation = Quaternion.Euler(90f, playerPos.eulerAngles.y, 0f);

    }




}


   