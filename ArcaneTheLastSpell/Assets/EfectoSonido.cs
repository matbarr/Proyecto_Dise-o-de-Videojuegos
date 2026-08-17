using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoSonido : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && audioSource.clip != null)
        {
            AudioSource.PlayClipAtPoint(audioSource.clip, transform.position, audioSource.volume);
        }

    }
    

}
