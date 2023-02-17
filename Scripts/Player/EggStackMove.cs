using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggStackMove : MonoBehaviour
{
    public void FollowStack(Transform target, bool isFollowStart)
    {
        StartCoroutine(StartFollowingToLastCubePosition(target, isFollowStart));
    }
    public void StopFollow(Transform target, bool isFollowStart)
    {
        StopCoroutine(StartFollowingToLastCubePosition(target,isFollowStart));
    }
    IEnumerator StartFollowingToLastCubePosition(Transform target, bool isFollowStart)
    {
      
        while (isFollowStart)
        {
            yield return new WaitForEndOfFrame();
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.position.x, 20 * Time.deltaTime),
                transform.position.y,
                Mathf.Lerp(transform.position.z, target.position.z, 20 * Time.deltaTime));
        }
      
    }
}
