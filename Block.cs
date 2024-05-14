using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : ScriptableObject
{
    public Vector3 position;
    public GameObject blockPrefab;

    public void InitializeBlock(GameObject prefab, Vector3 pos)
       {
            position = pos;
            blockPrefab = prefab;
       }
    
}
