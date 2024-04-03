using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

public class TileMB : MonoBehaviour
{
    [SerializeField] public MeshRenderer Mesh;
    [SerializeField] public BoxCollider BoxCollider;
    public Vector2 Pos => new Vector2(transform.localPosition.x, transform.localPosition.z);
    public GameObject GameObject => this.gameObject;
    public Transform Transform => this.transform;
    public int Entity;

    private EcsWorld _world;
    EcsPool<Client.ClickTileEvent> _clickTileEventPool;

    public void SetWorld(EcsWorld world)
    {
        _world = world;
        _clickTileEventPool = _world.GetPool<Client.ClickTileEvent>();
    }

    private void OnMouseDown()
    {
        if (!_clickTileEventPool.Has(Entity))
            _world.GetPool<Client.ClickTileEvent>().Add(Entity);

        Debug.Log($"Кликнули по TileMB {this.gameObject.name}");
    }
}