using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Input = UnityEngine.Windows.Input;

public  class Tile : MonoBehaviour
{
    [SerializeField]
    private Color _allowingColor;
    [SerializeField]
    private Color _forbiddingColor;
    [SerializeField]
    private Color _highlightingColor;
    
    private List<Material> _materials = new();

    private void Awake()
    {
        var renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var meshRenderer in renderers)
        {
            _materials.Add(meshRenderer.material);
        }
    }
    public void SetColor(bool available)
    {
        if (available)
        {
            foreach (var material in _materials)
            {
                material.color = _allowingColor;
            }
        }
        else
        {
            foreach (var material in _materials)
            {
                material.color = _forbiddingColor;
            }
        }
    }

    public void SetHighlight( )
    {
        foreach (var material in _materials)
        {
            material.color = _highlightingColor;
        }
    }
    

    public void ResetColor()
    {
        foreach (var material in _materials)
        {
            material.color = Color.white;
        }
    }

    
}