using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public void PlayShatterAnim()
    {
        if (transform.parent != null)
        {
            transform.parent = null;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Obstacle>().Shatter();
        }
        StartCoroutine(DeleteObstacleParts());
    }
    IEnumerator DeleteObstacleParts()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
