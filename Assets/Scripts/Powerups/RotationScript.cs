using UnityEngine;
using System.Collections;

public class RotationScript : MonoBehaviour 
{
    void OnEnable()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0);
    }

    // Update is called once per frame
	void Update () {
	    transform.Rotate(new Vector3(0, 0, -2));
	}
}
