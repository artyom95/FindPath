using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoloringMousePathBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;


    [SerializeField] private Map _map;

    [SerializeField] private MapIndexProvider _mapIndexProvider;

    [SerializeField] private ButtonController _buttonController;

    [SerializeField] private PathHighLightingAfterClick _pathHighLightingAfterClick;

    private GameObject _highlightableObject;

    private GameObject _currentObject;

    private bool _isObjectHighlight;

    // Update is called once per frame
    void Update()
    {
        /*  if (_buttonController.HasButtonStartPressed() && !_pathHighLightingAfterClick.HasHighLightingPath())
          {
              MouseTracking();
          }
  
         */
    }

    public void MouseTracking()
    {
        _currentObject = GetHighlightObject();
        if (_currentObject != _highlightableObject)
        {
            if (_highlightableObject != null)
            {
                _highlightableObject.GetComponent<Tile>().ResetColor();
                _isObjectHighlight = false;
            }

            if (_currentObject != null)
            {
                _currentObject.GetComponent<Tile>().SetHighlight();
                _isObjectHighlight = true;
            }

            _highlightableObject = _currentObject;
        }
    }


    private GameObject GetHighlightObject()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo))
        {
            var highlightableObject = hitInfo.collider.gameObject;
            if (highlightableObject.CompareTag("SimpleTile"))
            {
                return highlightableObject;
            }
        }

        return null;
    }


    public bool GetStatusHighlightObject()
    {
        if (_isObjectHighlight)
        {
            return true;
        }

        return false;
    }
}
/*
 
   private void ShowIndexTile()
    {
        Tile tile = new Tile();
        for (int i = 0; i < _map.GetTiles().GetLength(0); i++)
        {
            for (int i1 = 0; i < _map.GetTiles().GetLength(1); i++)
            {
                if (_map.GetTiles()[i, i1].Equals( _currentObject))
                {
                    Debug.Log("row:" + i + "column:" + i1);
                }
            }

        }
    }
 
 var tileIndex = _mapIndexProvider.GetIndex(hitInfo.point);
          //  var tilePosition = _mapIndexProvider.GetTilePosition(tileIndex);
           // Debug.Log("Position X :" + tilePosition.x + "Z: "  + tilePosition.z);

           // Debug.Log(" Index X :" + tileIndex.x + "Y: "  + tileIndex.y);
            if (tileIndex.x > 0 && tileIndex.x < 10 && tileIndex.y > 0 && tileIndex.y < 10)
            {
                Debug.Log("row :" + tileIndex.y + "column: "  + tileIndex.x);

                if ( _map.GetTiles()[tileIndex.x, tileIndex.y].CompareTag("SimpleTile"))
                {
                    _map.GetTiles()[tileIndex.x, tileIndex.y].SetColor(true);
                }
            }*/