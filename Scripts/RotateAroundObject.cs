using UnityEngine;

public class RotateAroundObject : MonoBehaviour
{
    public GameObject Obj;
    public GameObject RotateAroundThis;
    public float Time = 2;

	// Update is called once per frame
	void Start ()
	{
	    Obj.transform.parent = RotateAroundThis.transform;

        iTween.RotateTo(RotateAroundThis, new Vector3(0, 180, 0), Time);
	}
}
