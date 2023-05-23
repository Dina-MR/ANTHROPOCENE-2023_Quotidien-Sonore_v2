using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    private void Start()
    {
        
    }

    // Affichage du sprite renderer de la zone atteinte
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Main_Target"))
        {
            Debug.Log("Arrivé dans une zone d'intérêt");
            other.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
