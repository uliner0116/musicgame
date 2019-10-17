using UnityEngine;
using System.Collections.Generic;

public class GetTiming : MonoBehaviour
{

    const string nodeName = "Node(Clone)";
    public GameObject tapPosition;
    public CheckTiming checktiming;


    private void Start()
    {
        checktiming = tapPosition.GetComponent<CheckTiming>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == nodeName)
        {
            checktiming.Timing[this.gameObject.name].Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == nodeName)
        {
            checktiming.Timing[this.gameObject.name].Remove(other.gameObject);
        }
    }
}
