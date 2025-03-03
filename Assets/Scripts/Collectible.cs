using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static event Action OnCollected;
    public static int total;
    int keys;
    public GameObject WinTrigger;

    void Awake() => total++;

    void Update()
    {
        transform.localRotation = Quaternion.Euler(90f, Time.time * 100f, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            keys++;
            if (keys == 2)
            {
                WinTrigger.SetActive(true);
            }
            OnCollected?.Invoke();
            Destroy(gameObject);
        }
    }

}
