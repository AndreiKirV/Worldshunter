using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

public class TileMB : MonoBehaviour
{
    public MeshRenderer Mesh;
    public BoxCollider BoxCollider;
    public Transform Transform;
    public int Entity;
    public Vector2 Pos => new Vector2(transform.localPosition.x, transform.localPosition.z);
    public GameObject GameObject => this.gameObject;

    private EcsWorld _world;
    private EcsPool<Client.ClickTileEvent> _clickTileEventPool;

    private Data _data;

    public void SetWorld(EcsWorld world)
    {
        _world = world;
        _clickTileEventPool = _world.GetPool<Client.ClickTileEvent>();
        _data = Data.GetInstance();
    }

    public void ForceClick()
    {
        if (!_clickTileEventPool.Has(Entity))
            _world.GetPool<Client.ClickTileEvent>().Add(Entity);
    }

    private void OnMouseDown()
    {
        if (!_clickTileEventPool.Has(Entity) && _data.IsUiFree)
            _world.GetPool<Client.ClickTileEvent>().Add(Entity);

        Debug.Log($"Кликнули по TileMB {this.gameObject.name}");
    }
}