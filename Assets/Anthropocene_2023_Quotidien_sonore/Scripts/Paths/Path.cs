﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Vector3> nodes; // points d’intérêt (coordonnées seulement)
    public List<GameObject> targets; // points d’intérêt
    private LineRenderer lineRenderer; // tracé

    [Header("Apparence")]
    [SerializeField, Range(0,1)] private float width = 0.2f; // épaisseur du tracé
    public Color color;
    public Material material;

    public bool isInitialized;

    private void Start()
    {
        isInitialized = false;
        InitPath();
    }

    private void Update()
    {
        if(isInitialized)
            DrawStaticPath();
    }

    public void InitPath(List<Vector3> newNodes = null)
    {
        if(newNodes != null)
            nodes = newNodes;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        // Configuration de l’apparence générale
        lineRenderer.material = material;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.positionCount = targets.Count;
        lineRenderer.sortingOrder = 10;
        Debug.Log("NB POS :" + lineRenderer.positionCount);
        // Récupération des positions
        if (targets.Count > 0 || nodes.Count == 0)
            foreach (var target in targets)
                nodes.Add(target.transform.position);
        //On masque le line renderer au début
        lineRenderer.enabled = false;
    }

    public void DrawStaticPath()
    {
        // Ajout des points d’intérêts
        lineRenderer.SetPositions(nodes.ToArray());
    }

    public void DrawAnimatedPath()
    {

    }

}
