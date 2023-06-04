using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    
    [SerializeField] 
    private MapBuilder _mapBuilder;

    [SerializeField] 
    private PlayerProvider _playerProvider;

    [SerializeField] 
    private RecoloringMousePathBehaviour _recoloringMousePathBehaviour;

    [SerializeField] 
    private ChoosePlaceToFinish _choosePlaceToFinish;
    [SerializeField] 
    private ShortestPathProvider _shortestPathProvider;
    [SerializeField] 
    private PathHighLightingAfterClick _pathHighLightingAfterClick;
    
    private UnityEngine.Vector2 _finishCoordinates = new UnityEngine.Vector2();
    private UnityEngine.Vector2 _startCoordinates = new UnityEngine.Vector2();
    private bool _isPrefabMove;
    private bool _isStartGame = true;
    private LinkedList<UnityEngine.Vector2> _path = new LinkedList<UnityEngine.Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (_mapBuilder.HasMapBuilded() && _playerProvider.HasPrefabInstantiated() && !_isPrefabMove)
        {
            _recoloringMousePathBehaviour.MouseTracking();
            if (_isStartGame)
            {
                _startCoordinates = _playerProvider.GetStartPosition();
                _isStartGame = false;
            }
               
            if (Input.GetMouseButtonDown(0) && _recoloringMousePathBehaviour.GetStatusHighlightObject())
            {
                 _finishCoordinates = _choosePlaceToFinish.ChoosePlaceFinish();
                _shortestPathProvider.FillArray();
                _shortestPathProvider.FillPoints(_startCoordinates, _finishCoordinates);
                _shortestPathProvider.FindPath();
                _path = _shortestPathProvider.FindPathForHighLighting(_finishCoordinates);
                if (_path.Count > 1)
               {
                   _pathHighLightingAfterClick.HighLightingPath(_path);
                   _isPrefabMove = true;                  
                   _playerController.Moving();
                   _playerProvider.StartMove(_startCoordinates, _path);
                  
              }
                else
                {
                    _finishCoordinates = default;
                    return;
                }
            }
        }

        if (_playerProvider.HasPlayerArrivedAtFinish() && _isPrefabMove)
        {
                    _pathHighLightingAfterClick.ResetHighLightingPath(_path);
                    _startCoordinates = _finishCoordinates;
                    _path.Clear();
                    _finishCoordinates = default;
                       _playerController.Staying();
                    _isPrefabMove = false;
                    _shortestPathProvider.ClearData(); 
        }
    }
}
