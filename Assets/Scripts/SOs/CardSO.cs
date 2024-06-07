using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AttacType = Data.Glossary.CardElements.AttackType;

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
    [SerializeField] private AttacType _attackType;
    //тип атаки

    public string Name => _name;
    public string Description => _description;
    public Sprite PortraitView => _portraitView;
    public int Speed => _speed;
    public int Armour => _armour;
    public int Health => _health;
    public int Cost => _cost;
    public int Damage => _damage;
    public int AttackDistance => _attackDistance;
    public AttacType AttacType => _attackType;


    //public enum 
}