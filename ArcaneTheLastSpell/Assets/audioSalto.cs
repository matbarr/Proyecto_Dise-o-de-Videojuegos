using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioSalto : MonoBehaviour
{
    [SerializeField] private AudioClip clipSalto;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ReproducirSalto()
    {
        if (clipSalto != null)
        {
            audioSource.PlayOneShot(clipSalto);
        }
    }
}
