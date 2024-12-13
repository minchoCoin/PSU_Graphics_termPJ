using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    { Debug.Log("enter collision"); }
    private void OnCollisionStay(Collision collision)
    { Debug.Log("stay collision"); }
    private void OnCollisionExit(Collision collision)
    { Debug.Log("exit collision"); }

}
