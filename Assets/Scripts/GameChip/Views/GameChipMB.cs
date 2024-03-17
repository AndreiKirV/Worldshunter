using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;


public class GameChipMB : MonoBehaviour
{
    [SerializeField] public MeshRenderer Mesh;
    [SerializeField] public BoxCollider BoxCollider;
    
    public Vector2 Pos => new Vector2(transform.localPosition.x, transform.localPosition.z);
    public GameObject GameObject => this.gameObject;
    public Transform Transform => this.transform;
    public int Entity;

    private EcsWorld _world;

    public void SetWorld(EcsWorld world)
    {
        _world = world;
    }

    private void OnMouseDown()
    {
        Debug.Log($"Кликнули по GameChipMB {this.gameObject.name}");
    }
}
