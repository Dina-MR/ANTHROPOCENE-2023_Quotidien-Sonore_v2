using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererDisplayers : Displayer
{
    public override void DrawOrHide(bool displayStatut)
    {
        foreach (var child in children)
            child.GetComponent<LineRenderer>().enabled = displayStatut;
    }
}
