using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] 
    private List<GameObject> _buttonList = new List<GameObject>();

    [SerializeField] 
    private Map _map;


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
    }


    public void DeactivateTileButtons()
    {
        var button = _buttonList.Where(button => button.name.Equals("SimpleTile") || button.name.Equals("ObstacleTile"))
            .ToList();
        foreach (var gameObject in button)
        {
            gameObject.GetComponent<Button>().enabled = false;
        }
    }
}