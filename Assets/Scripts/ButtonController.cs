using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
  [SerializeField]
  private List<GameObject> _buttonList = new List<GameObject>();

  [SerializeField] 
  private Map _map;

  
  private bool _isStartButtonPressed;
  
  [UsedImplicitly]
  public void DeactivateAllButtons()
  {
    var tiles = _map.GetTiles();
    foreach (var tile in tiles)
    {
      if (tile == null)
      {
        return;
      }
    }

    foreach (var button in _buttonList)
    {
      button.SetActive(false);
    }

    _isStartButtonPressed = true;
  }

  public bool HasButtonStartPressed()
  {
    return _isStartButtonPressed;
  }
}
