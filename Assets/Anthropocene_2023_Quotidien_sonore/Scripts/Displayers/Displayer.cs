using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Displayer : MonoBehaviour
{
    protected List<GameObject> children;
    public KeyCode input;
    public bool isDisplaying;

    void Start()
    {
        children = new List<GameObject>();
        GetAllChildren();
    }

    void GetAllChildren()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
            children.Add(gameObject.transform.GetChild(i).gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(input))
        {
            isDisplaying = !isDisplaying;
            DrawOrHide(isDisplaying);
        }
    }

    public virtual void DrawOrHide(bool displayStatut)
    {
    }
}
