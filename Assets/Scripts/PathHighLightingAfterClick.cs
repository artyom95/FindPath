using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHighLightingAfterClick : MonoBehaviour
{
   
    [SerializeField] 
    private Map _map;
    private LinkedList<Vector2> _path ;

    private bool _isPathHighLighting;
   
    
    public void HighLightingPath(LinkedList<Vector2> path)
    {
        var highlightPath = path;
        var map = _map.GetTiles();
        
        foreach (var step in highlightPath)
            {
               map [(int)step.y, (int)step.x].SetColor(true);

            }

            
    }
    public void ResetHighLightingPath(LinkedList<Vector2> path)
    {
        foreach (var part in path)
        {
            _map.GetTiles()[(int)part.y,(int)part.x].ResetColor();
        }
    }
    
}
