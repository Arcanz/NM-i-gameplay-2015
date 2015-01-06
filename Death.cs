using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    public float DeathTime = 2;

    void OnTriggerEnter(Collider collider)
    {
        iTween.MoveBy(collider.gameObject, new Vector3(0, 80, 0), 5);
        StartCoroutine(WaitAndRespawn());
    }

    IEnumerator WaitAndRespawn()
    {
        yield return new WaitForSeconds(DeathTime);

    }
}
