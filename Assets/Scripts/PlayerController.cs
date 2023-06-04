using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private Animator _animator;
    private GameObject _prefab;


    private void ReceivePrefab(GameObject gameObject)
    {
        
            if (gameObject != null)
            {
                _prefab = gameObject;
                _animator = _prefab.GetComponent<Animator>();
            }
            else
            {
                Debug.Log("Error: gameObject is null!");
            }
        
    }

    public void Moving(GameObject gameObject)
    {
        
        ReceivePrefab(gameObject);
        _animator.SetBool(IsMoving, true);
    }

    public void Staying()
    {
        _animator.SetBool(IsMoving, false);
        _animator = default;
    }
    // По умолчанию у персонажа проигрывается анимация покоя.
    // Для того, чтобы запустить анимацию ходьбы - передавайте в параметр аниматора IsMoving значение true:
    // _animator.SetBool(IsMoving, true);
    // Для того, чтобы запустить анимацию покоя - передавайте в параметр аниматора IsMoving значение false:
    // _animator.SetBool(IsMoving, false);
}