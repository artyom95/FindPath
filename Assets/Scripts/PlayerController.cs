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
  [SerializeField]
    private Animator _animator;
    private bool _isPrefabMoving;

    private void Start()
    {
        
         }

    private void Update()
    {
        _animator.SetBool(IsMoving, _isPrefabMoving);

    }

    public void Moving()
    {
        _isPrefabMoving = true;
    }
    public void Staying()
    {
        _isPrefabMoving = false;
    }
    // По умолчанию у персонажа проигрывается анимация покоя.
    // Для того, чтобы запустить анимацию ходьбы - передавайте в параметр аниматора IsMoving значение true:
    // _animator.SetBool(IsMoving, true);
    // Для того, чтобы запустить анимацию покоя - передавайте в параметр аниматора IsMoving значение false:
    // _animator.SetBool(IsMoving, false);
   
}