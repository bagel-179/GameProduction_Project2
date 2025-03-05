using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyAudioScript : MonoBehaviour
{
    public AudioClip SoundToPlay;
    public float Volume;
    AudioSource audio;
    public bool alreadyPlayed;
    Collider Collider;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        Collider = gameObject.GetComponent<Collider>();
        alreadyPlayed = false;
    }

    IEnumerator Respawn (Collider collision, int time)
    {
        audio.PlayOneShot(SoundToPlay, Volume);
        alreadyPlayed = true;
        collision.enabled = false;
        yield return new WaitForSeconds(time);
        collision.enabled = true;
        alreadyPlayed = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!alreadyPlayed)
        {
            StartCoroutine(Respawn(Collider, 6));
        }
        
    }
}
