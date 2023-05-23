using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayersInput : MonoBehaviour
{
    [Header("Prerequites")]
    public SpriteRenderersDisplayer iconsDisplay;
    public AudioListener cameraListener;

    [Header("Main")]
    public GameObject[] characters = new GameObject[4];
    public string[] names = new string[4];
    //public Text displayText;
    public TextMeshProUGUI displayText;
    public Path[] paths = new Path[4];
    public Color[] colors = new Color[4];
    public KeyCode[] keypadInputs = new KeyCode[4]; // clavier numérique
    public KeyCode[] alphaInputs = new KeyCode[4]; // clavier alpha numérique
    //public Button[] buttons = new Button[4];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Gestion de tous les inputs
        for (int i = 0; i < characters.Length; i++)
            if (characters[i] != null)
                HandleCharacterInput(i);
        // On cache tout en appuyant sur maj droit
        if(Input.GetKeyDown(KeyCode.RightShift))
            HideEverthing();
    }

    // Gestion de l'input d'un joueur
    public void HandleCharacterInput(int index)
    { 
        if (Input.GetKeyDown(keypadInputs[index]) || Input.GetKeyDown(alphaInputs[index]))
        {
            // Activation du bouton associé
            //buttons[index].Select();
            // On cache les lieux majeurs
            iconsDisplay.DrawOrHide(false);
            // On arrête le listener de la cam
            if (cameraListener.enabled)
                cameraListener.enabled = false;
            // Activation du personnage
            ActivateCharacter(ref characters[index], index);
            // Désactivation des autres persos, de leurs trajets & et des sons
            for(int i = 0; i < characters.Length; i++)
            {
                if (i == index)
                    continue;
                if (characters[i] != null)
                {
                    characters[i].GetComponent<CharacterMove>().enabled = false;
                    if (characters[i].GetComponent<AudioListener>().enabled)
                        characters[i].GetComponent<AudioListener>().enabled = false;
                    if (characters[i].GetComponent<SpriteRenderer>().enabled)
                        characters[i].GetComponent<SpriteRenderer>().enabled = false;
                }
                if (paths[i].GetComponent<LineRenderer>() != null && paths[index].GetComponent<LineRenderer>().enabled)
                    paths[i].GetComponent<LineRenderer>().enabled = false;
            }
        }
    }

    // Activation des components après input
    public void ActivateCharacter(ref GameObject character, int index)
    {
        // Activation du rendu
        //if (paths[index].enabled == false)
        //    paths[index].enabled = true;
        // Activation du trajet
        if (character.GetComponent<CharacterMove>().enabled == false)
        {
            character.GetComponent<CharacterMove>().enabled = true;
            character.GetComponent<CharacterMove>().InitTravel();
            paths[index].isInitialized = true;
            if (paths[index].GetComponent<LineRenderer>() != null && !paths[index].GetComponent<LineRenderer>().enabled)
                paths[index].GetComponent<LineRenderer>().enabled = true;
        }
        // Affichage du sprite
        if (character.GetComponent<SpriteRenderer>().enabled == false)
            character.GetComponent<SpriteRenderer>().enabled = true;
        // Activation de l'écoute relative
        if (character.GetComponent<AudioListener>().enabled == false)
            character.GetComponent<AudioListener>().enabled = true;
        // Affichage du nom
        displayText.text = "Bonjour, je suis " + names[index];
        displayText.color = colors[index];
    }

    public void HideEverthing()
    {
        for(int i = 0; i < characters.Length; i++)
        {
            if (paths[i].GetComponent<LineRenderer>().enabled)
                paths[i].GetComponent<LineRenderer>().enabled = false;
            if (characters[i].GetComponent<SpriteRenderer>().enabled)
                characters[i].GetComponent<SpriteRenderer>().enabled = false;
            if (characters[i].GetComponent<AudioListener>().enabled)
                characters[i].GetComponent<AudioListener>().enabled = false;
            if (characters[i].GetComponent<CharacterMove>().enabled)
                characters[i].GetComponent<CharacterMove>().enabled = false;

        }
    }
}
