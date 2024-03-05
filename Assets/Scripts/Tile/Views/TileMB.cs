using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMB : MonoBehaviour
{
    [SerializeField] GameObject pref;
    
    private void OnMouseDown() 
    {
        if(pref != null)
        Instantiate(pref, this.transform);
        Debug.Log(this.name);
    }
}