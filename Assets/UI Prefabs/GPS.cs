using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GPS : MonoBehaviour
{
    public Transform targetPoint;
    public TextMeshProUGUI distanceOut;
    void Update()
    {
        var mappedPos = new Vector3(targetPoint.position.x, transform.position.y, targetPoint.position.z);
        var distance = Vector3.Distance(transform.position, mappedPos);
        distanceOut.gameObject.SetActive(distance >= 2);
        distanceOut.text = distance.ToString("f0") + "m";
        transform.GetChild(0).forward = mappedPos - transform.position;
    }
}
