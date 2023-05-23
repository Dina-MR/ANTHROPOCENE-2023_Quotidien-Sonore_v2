using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsInput : MonoBehaviour
{
    public GameObject[] targets= new GameObject[4];
    public KeyCode[] inputs = new KeyCode[4]; // clavier numérique
    public AudioListener cameraListener;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Gestion de tous les inputs
        for (int i = 0; i < targets.Length; i++)
            if (targets[i] != null)
                HandleTargetInput(i);
        // On interrompt tout en appuyant sur espace
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (cameraListener.enabled)
                cameraListener.enabled = false;
        }
    }

    // Gestion de l'input d'une cible
    public void HandleTargetInput(int index)
    {
        if (Input.GetKeyDown(inputs[index]))
        {
            // On active l'écouteur (s'il a été désactivé par la touche espace)
            if (!cameraListener.enabled)
                cameraListener.enabled = true;
            ActivateTarget(ref targets[index]);
            // Désactivation des autres sons et halos
            for (int i = 0; i < targets.Length; i++)
            {
                if (i == index)
                    continue;
                if (targets[i] != null && targets[i].GetComponent<AudioSource>().isPlaying == true)
                    targets[i].GetComponent<AudioSource>().Stop();
                if (targets[i] != null && targets[i].GetComponent<SpriteRenderer>().enabled == true)
                    targets[i].GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    // Activation des components après input
    public void ActivateTarget(ref GameObject target)
    {
        // Activation du halo lumineux
        if (target.GetComponent<SpriteRenderer>().enabled == false)
            target.GetComponent<SpriteRenderer>().enabled = true;
        // Activation de l'écoute par la caméra (on désactive celle des personnages)
        if (cameraListener.enabled == false)
            cameraListener.enabled = true;
        // Activation du son en 2D
        if (target.GetComponent<AudioSource>().isPlaying == false)
            target.GetComponent<AudioSource>().Play();
        if(target.GetComponent<AudioSource>().spatialBlend != 0)
            target.GetComponent<AudioSource>().spatialBlend = 0;
    }
}
