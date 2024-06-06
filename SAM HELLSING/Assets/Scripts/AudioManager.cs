using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]// single 

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Awake()
    {
        if (Instance == null)// comprobar si esta vacio 
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Aviso! Hay más de un AudioManager en escena.");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReproducirSonido(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }
}

