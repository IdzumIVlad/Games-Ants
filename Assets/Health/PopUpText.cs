using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    public float destroyTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, destroyTime);
    }

    public void DESTROY()
    {
        Destroy(gameObject);
    }

}
