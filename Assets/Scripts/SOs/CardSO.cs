using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CardSO", menuName = "WorldsHunterSO/CardSO", order = 51)]
public class CardSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _portraitView;
    [SerializeField] private int _speed;
    [SerializeField] private int _armour;
    [SerializeField] private int _health;
    [SerializeField] private int _cost;
    [SerializeField] private int _damage;
    [SerializeField] private int _attackDistance;
    //тип атаки
}