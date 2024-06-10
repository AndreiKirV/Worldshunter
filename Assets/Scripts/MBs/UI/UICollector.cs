using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UICollector
{
    public GameObject Canvas;

    public Transform CardTemporaryParentTransform;
    public Transform CardsContentTransform;

    public bool IsUiFree = true;
}