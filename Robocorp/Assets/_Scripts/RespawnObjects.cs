using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObjects : MonoBehaviour
{
    [SerializeField] GameObject[] respawnableObjects = new GameObject[0];
    [SerializeField] Vector3[] respawnPoints = new Vector3[0];
    [SerializeField] Rigidbody[] respawnableObjectsRB = new Rigidbody[0];
    [SerializeField] DissolveEffect[] dissolveEffects = new DissolveEffect[0];

    float delayTime;

    private void Start()
    {
        GetParameters();
    }

    private void OnTriggerEnter(Collider other)
    {
        DissolveEffect(other);
        StartCoroutine(WaitForDelay(other));
    }

    private void GetParameters()
    {
        for (int i = 0; i < respawnableObjects.Length; ++i)
        {
            respawnPoints[i] = respawnableObjects[i].transform.position;
            respawnableObjectsRB[i] = respawnableObjects[i].GetComponent<Rigidbody>();
            dissolveEffects[i] = respawnableObjects[i].GetComponent<DissolveEffect>();
            if (dissolveEffects[i] != null)
            {
                delayTime = dissolveEffects[i].desiredDuration;
            }
        }
    }

    private void RespawnObject(Collider other)
    {
        for (int i = 0; i < respawnableObjects.Length; ++i)
        {
            if (other.gameObject == respawnableObjects[i])
            {
                respawnableObjects[i].transform.position = respawnPoints[i];
                respawnableObjects[i].transform.rotation = Quaternion.Euler(Vector3.zero);
                respawnableObjectsRB[i].velocity = Vector3.zero;
                respawnableObjectsRB[i].angularVelocity = Vector3.zero;
            }
        }
    }

    private void DissolveEffect(Collider other)
    {
        for (int i = 0; i < respawnableObjects.Length; ++i)
        {
            if (other.gameObject == respawnableObjects[i] && dissolveEffects[i] != null)
            {
                dissolveEffects[i].isDissolving = true;
            }
        }
    }

    IEnumerator WaitForDelay(Collider other)
    {
        yield return new WaitForSeconds(delayTime);
        RespawnObject(other);
    }
}
