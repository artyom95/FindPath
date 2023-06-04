using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortestPathProvider : MonoBehaviour
{
    [SerializeField] private Map _map;

    [SerializeField] private ButtonController _buttonController;

    [SerializeField] private ChoosePlaceToFinish _choosePlaceToFinish;

    [SerializeField] private PlayerProvider _playerProvider;

    private int[,] _arrayMap;

    private Vector2[,] _firstArrayPath;

    private LinkedList<Vector2> _path;
    private Vector2 _startPoint;

   
    
    private Vector2 _currentFinishCoordinates;
    private Vector2 _finishCoordinates;
    
    // Start is called before the first frame update
    void Start()
    {
        _path = new LinkedList<Vector2>();
    }

    public void ClearData()
    {
        _arrayMap = default;
        _firstArrayPath = default;
       
    }


    public void FillPoints(Vector2 startCoordinates, Vector2 finishCoordinates)
    {
        _startPoint = startCoordinates;
       

        _arrayMap[(int)_startPoint.x + 1, (int)_startPoint.y + 1] = 1;
    }

    public void FillArray()
    {
        var arrayMap = _map.GetTiles();

        _arrayMap = new int[arrayMap.GetLength(0) + 2, arrayMap.GetLength(1) + 2];
//заполняем массив -1 (делаем защитную полосу)
        for (var i = 0; i < _arrayMap.GetLength(0); i++)
        {
            for (int j = 0; j < _arrayMap.GetLength(1); j++)
            {
                _arrayMap[i, j] = -1;
            }
        }

        // заполняем массив согласно карты тайлов
        for (var j = 0; j < arrayMap.GetLength(0); j++)
        {
            for (int i = 0; i < arrayMap.GetLength(1); i++)
            {
                if (arrayMap[i, j].gameObject.CompareTag("SimpleTile"))
                {
                    _arrayMap[j + 1, i + 1] = 0;
                }
                else
                {
                    _arrayMap[j + 1, i + 1] = -1;
                }
            }

            CreateArrayForPath();
          
        }
    }

    private void CreateArrayForPath()
    {
        var arrayMap = _map.GetTiles();
        _firstArrayPath = new Vector2[arrayMap.GetLength(0), arrayMap.GetLength(1)];
    }

    public void FindPath()
    {
        //поиск кратчайшего пути по массиву 
        Queue<Coordinates> points = new Queue<Coordinates>();
        points.Enqueue(new Coordinates() { X = (int)_startPoint.x + 1, Y = (int)_startPoint.y + 1 });
        int step = 1;
        int start = 1;
        int end = 1;
        int count = 1;

        while (points.Count > 0)
        {
            for (int i = start; i <= end; i++)
            {
                Coordinates currentCoordinates = points.Dequeue();
                int column = currentCoordinates.Y;
                int row = currentCoordinates.X;
                if (_arrayMap[row - 1, column] == 0)
                {
                    _arrayMap[row - 1, column] = step;
                    _firstArrayPath[row - 2, column - 1] = new Vector2(row - 1, column - 1);

                    points.Enqueue(new Coordinates() { X = row - 1, Y = column });
                    count++;
                }

                if (_arrayMap[row + 1, column] == 0)
                {
                    _arrayMap[row + 1, column] = step;
                    _firstArrayPath[row, column - 1] = new Vector2(row - 1, column - 1);
                    points.Enqueue(new Coordinates() { X = row + 1, Y = column });
                    count++;
                }

                if (_arrayMap[row, column - 1] == 0)
                {
                    _arrayMap[row, column - 1] = step;
                    _firstArrayPath[row - 1, column - 2] = new Vector2(row - 1, column - 1);

                    points.Enqueue(new Coordinates() { X = row, Y = column - 1 });
                    count++;
                }

                if (_arrayMap[row, column + 1] == 0)
                {
                    _arrayMap[row, column + 1] = step;
                    _firstArrayPath[row - 1, column] = new Vector2(row - 1, column - 1);

                    points.Enqueue(new Coordinates() { X = row, Y = column + 1 });
                    count++;
                }
            }

            if (points.Count == 0)
            {
             break;
            }

            start = end + 1;
            end = count;
            step++;
        }
    }

    public LinkedList<Vector2> FindPathForHighLighting(Vector2 coordinatesForHighlight)
    {
        var finishCoordinates = coordinatesForHighlight;
        Vector2[,] arrayPath = _firstArrayPath;
        Vector2 currentCoordinates = default;
        Vector2 coordinates = default;
        Queue<Vector2> queue = new Queue<Vector2>();
        _path = new LinkedList<Vector2>();
        if (arrayPath[(int)finishCoordinates.x, (int)finishCoordinates.y] != new Vector2(0, 0))
        {
            queue.Enqueue(finishCoordinates);
            _path.AddFirst(finishCoordinates);
            while (queue.Count > 0)
            {
                coordinates = queue.Dequeue();
                currentCoordinates = arrayPath[(int)coordinates.x, (int)coordinates.y];
                queue.Enqueue(currentCoordinates);
                if (currentCoordinates == Vector2.zero)
                {
                    break;
                }

                _path.AddFirst(currentCoordinates);
            }
        }

        return _path;
    }

    class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}