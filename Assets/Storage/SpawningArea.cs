using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningArea : MonoBehaviour
{
    public float width;
    public float lenght;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        //Gizmos.DrawCube(transform.position, new Vector3(width, 1, lenght));
    }

    public Vector3 SpawnPosition(ResTypes resTypes)
    {
        Vector3 newSpawnPosition;
        float x = transform.position.x + (Random.Range(-width / 2, width / 2));
        float y = transform.position.y + 2.789f;
        float z = transform.position.z + (Random.Range(-lenght / 2, lenght / 2));

        if (resTypes == ResTypes.Apple)
            y = transform.position.y + 0.955f;

        newSpawnPosition = new Vector3(x, y, z);
        return newSpawnPosition;
    }
}
