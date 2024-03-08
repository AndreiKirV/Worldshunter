using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMB : MonoBehaviour
{
    [SerializeField] public MeshRenderer Mesh;
    public GameObject GameObject => this.gameObject;
    public Transform Transform => this.transform;

    private void OnMouseDown()
    {
        Debug.Log(this.name);
    }
}