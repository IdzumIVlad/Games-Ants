using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        if (player.position.y < 0)
        {
            newPosition.y = transform.position.y;
            transform.position = newPosition;
        }
        else
        {
            newPosition.y = transform.position.y;
            newPosition.y = 60;
            transform.position = newPosition;
        }

        
        
        

        //transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);

        
    }
}
