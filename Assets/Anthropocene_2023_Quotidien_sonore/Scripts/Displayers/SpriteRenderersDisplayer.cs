using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRenderersDisplayer : Displayer
{
    public override void DrawOrHide(bool displayStatut)
    {
        foreach (var child in children)
            child.GetComponent<SpriteRenderer>().enabled = isDisplaying;
    }
}
