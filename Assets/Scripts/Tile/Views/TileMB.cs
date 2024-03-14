using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMB : MonoBehaviour
{
    [SerializeField] public MeshRenderer Mesh;
    public Vector2 Pos => new Vector2(transform.localPosition.x, transform.localPosition.z);
    public GameObject GameObject => this.gameObject;
    public Transform Transform => this.transform;
    public int Entity;

    private void OnMouseDown()
    {
        Debug.Log(Entity);
    }
}