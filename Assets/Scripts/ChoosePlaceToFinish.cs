using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ChoosePlaceToFinish : MonoBehaviour
{
    [SerializeField] 
    private Map _map;
    private Vector2 _finishCoordinates;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      /*  if (Input.GetMouseButtonDown(0)&& _recoloringMousePathBehaviour.GetStatusHighlightObject()  )
        {
            var index = GetIndex();
            var column = index.x+_map.Size.x / 2 ;
            var row = -index.y+Mathf.Ceil(_map.Size.x/2.5f) ;
            if (column < 0 || row < 0 || column > 9 || row > 9)
            {
                return;
            }

            _isPlaceChosen = true;
            _finishCoordinates = new Vector2(row, column);
           // Debug.Log("row:" + row + "column:" + column);
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isPlaceChosen = false;
        }
        */
    }

    public Vector2 ChoosePlaceFinish()
    {
        
        var index = GetIndex();
        var column = index.x+_map.Size.x / 2 ;
        var row = -index.y+Mathf.Ceil(_map.Size.x/2.5f) ;
        if (column < 0 || row < 0 || column > 9 || row > 9)
        {
            return default;
        }

        _finishCoordinates = new Vector2(row, column);
        return _finishCoordinates;
    }
    private Vector2Int GetIndex()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo))
        {
            // Переводим мировую позицию в локальную позицию карты
            var tilePositionInMap = _map.transform.InverseTransformPoint(hitInfo.point);

            // Получаем целочисленные координаты тайла
            var x = Mathf.FloorToInt(tilePositionInMap.x);
            var y = Mathf.FloorToInt(tilePositionInMap.z);

            // Получаем индекс тайла в сетке
            var halfMapSize = _map.Size / 2;
            var mapIndex = new Vector2Int(x, y);

            return mapIndex;
        }

        return default;
    }
}
