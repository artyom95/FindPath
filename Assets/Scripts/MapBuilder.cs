using System;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    [SerializeField] 
    private Map _map;
    [SerializeField] 
    private MapIndexProvider _mapIndexProvider;

    private Camera _camera;
    private Tile _currentTile;

    private bool _isMapBuilded ;
    private void Awake()
    {
        _camera = Camera.main;
    }

    public void StartPlacingTile(GameObject tilePrefab)
    {
        var tileObject = Instantiate(tilePrefab);
        _currentTile = tileObject.GetComponent<Tile>();
        _currentTile.transform.SetParent(_map.transform);
    }

    private void Update()
    {
        if (!_isMapBuilded)
        {
            var mousePosition = Input.mousePosition;
            var ray = _camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out var hitInfo) && _currentTile != null)
            {
                // Получаем индекс и позицию тайла по позиции курсора
                var tileIndex = _mapIndexProvider.GetIndex(hitInfo.point);
                var tilePosition = _mapIndexProvider.GetTilePosition(tileIndex);
                _currentTile.transform.localPosition = tilePosition;

                // Проверяем, доступно ли место для постройки тайла
                var isAvailable = _map.IsCellAvailable(tileIndex);
                // Задаем тайлу соответствующий цвет
                _currentTile.SetColor(isAvailable);

                // Если место недоступно для постройки - выходим из метода
                if (!isAvailable)
                {
                    return;
                }

                // Если нажата ЛКМ - устанавливаем тайл 
                if (Input.GetMouseButtonDown(0) && isAvailable)
                {
                    _map.SetTile(tileIndex, _currentTile);
                    _currentTile.ResetColor();
                    _currentTile = null;
                }
            }

            _isMapBuilded = HasMapBuilded();
        }
        else
        {
            return;
        }
    }

    public bool HasMapBuilded()
    {
        var countTiles = _map.GetAmountTilesInMap();
        if (countTiles >=100)
        {
            return true;
        }
        
        return false;
    }
    
}