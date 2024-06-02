using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barraVida : MonoBehaviour
{
    [SerializeField] public Image vida;
    public void UpdateHealth(float maxHealth, float healt)
    {
        // va de 0 a 1 y lo debbemos dividir para meter el valor concreto
        vida.fillAmount = healt/ maxHealth;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
