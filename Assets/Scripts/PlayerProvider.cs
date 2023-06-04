using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerProvider : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;

    [SerializeField] private Map _map;

    [SerializeField] private GameObject _obstacle; // убрать

    [SerializeField] private GameObject _simplePrefab; //убрать

    [SerializeField] private float _speed;

    private Vector2 _startCoordinatesPlayer;
    public GameObject Player { get; private set; }
    private bool _isPrefabInstantiate = false;
    private bool _isbuttonPressd = false;
    private bool _isPlayerOnFinish;
    private Vector3 _finishPosition;
    private const float _tilePosition = 4.5f;

// автоматическое заполнение сетки тайлами, после решения задачи убрать
    private void Start()
    {
        Tile[,] array = new Tile[10, 10];
        GameObject gam;
        GameObject instant;
        for (var i = 0; i < array.GetLength(0); i++)
        {
            for (var i1 = 0; i1 < array.GetLength(1); i1++)
            {
                var a = GetRandomNumber();
                if (a % 2 == 0)
                {
                    gam = _obstacle;
                }
                else
                {
                    gam = _simplePrefab;
                }

                instant = Instantiate(gam);
                instant.transform.localPosition = new Vector3(i1 - _tilePosition, 0f, -i + _tilePosition);
                _map.SetTile(new Vector2Int(i1, i), instant.GetComponent<Tile>());
            }
        }
    }

// этот код выше 

    [UsedImplicitly]
    public void OnClick()
    {
        _isbuttonPressd = true;
    }

    private void Update()
    {
        if (!_isPrefabInstantiate && _isbuttonPressd)
        {
            var row = GetRandomNumber();
            var column = GetRandomNumber();
            if (CheckPositionInArray(row, column))
            {
                InstantiatePrefab(row, column);
            }
        }
    }

    public void StartMove(Vector2 startCoordinates, LinkedList<Vector2> pathh)
    {
        StartCoroutine(Moving(startCoordinates, pathh));
    }

    private IEnumerator Moving(Vector2 startCoordinates, LinkedList<Vector2> pathh) // изменить коррутину;
    {
        _isPlayerOnFinish = false;
        var path = new LinkedList<Vector2>(pathh);
        float time = 0f;
        Vector3 currentPosition = default;
        Vector3 startPosition =
            new Vector3(startCoordinates.y - _tilePosition, 0.85f,
                -startCoordinates.x + _tilePosition); // вопрос в координатах
        _finishPosition = new Vector3(path.Last.Value.y - _tilePosition, 0.85f, -path.Last.Value.x + _tilePosition);
        foreach (var coordinates in path)
        {
            currentPosition = new Vector3(coordinates.y - _tilePosition, 0.85f, -coordinates.x + _tilePosition);

            while (Player.transform.position != currentPosition)
            {
                time += Time.deltaTime;
                var nextPosition = new Vector3(coordinates.y - _tilePosition, 0.85f, -coordinates.x + _tilePosition);
                Player.transform.LookAt(nextPosition);
                Player.transform.position = Vector3.MoveTowards(startPosition, nextPosition, _speed);
                startPosition = Player.transform.position;
                yield return null;
                if (Player.transform.position == nextPosition)
                {
                    startPosition = Player.transform.position;
                    if (Player.transform.position == _finishPosition)
                    {
                        ChangeCoordinates();
                    }
                }
            }
        }
    }

    private void ChangeCoordinates()
    {
        _startCoordinatesPlayer = new Vector2(_finishPosition.x, _finishPosition.z);
        _isPlayerOnFinish = true;
    }

    public bool HasPlayerArrivedAtFinish()
    {
        return _isPlayerOnFinish;
    }

    public void InstantiatePrefab(int row, int column)
    {
        Player = Instantiate(_playerPrefab);
        Player.transform.localPosition = new Vector3(column - _tilePosition, 0.85f, -row + _tilePosition);

        //  Debug.Log("player coordinates");
        // Debug.Log("row " + row + " columns " + column);
        _startCoordinatesPlayer = new Vector2(row, column);
        //  _map.GetTiles()[column,row].gameObject.GetComponent<Tile>().SetColor(true); // убрать
        _isPrefabInstantiate = true;
        _isbuttonPressd = false;
    }

    public Vector2 GetStartPosition()
    {
        return _startCoordinatesPlayer;
    }

    private bool CheckPositionInArray(int row, int column)
    {
        //   Debug.Log("row" + row);                 // убрать
        // Debug.Log("column" + column);           // убрать
        //Debug.Log( "repeat");                    // убрать

        if (_map.GetTiles()[column, row].gameObject.CompareTag("ObstacleTile"))
        {
            //    _map.GetTiles()[column,row].gameObject.GetComponent<Tile>().SetColor(false);  // убрать

            return false;
        }

        return true;
    }

    private int GetRandomNumber()
    {
        var number = Random.Range(0, 9);
        return number;
    }

    public bool HasPrefabInstantiated()
    {
        return _isPrefabInstantiate;
    }
}