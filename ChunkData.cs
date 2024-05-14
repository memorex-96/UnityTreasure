using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkData : ScriptableObject{
    public GameObject sandPrefab;
    public GameObject waterPrefab;
    public Vector2Int chunkOrigin;
    public List<Block> chunkBlocks = new List<Block>();
    public void InitializeChunk(Vector2Int pos){
            chunkOrigin = pos;
       }   
    public List<Block> PopulateBlocks(){ 

        for(int i=-2; i<=2; i++){
            for(int j = -2; j<=2; j++){
                Block blockToBePopulated = ScriptableObject.CreateInstance<Block>();
                Vector3 myBlockPos = new Vector3(chunkOrigin.x + i, 0, chunkOrigin.y + j);
                if(chunkOrigin.x%2==0){
                    blockToBePopulated.InitializeBlock(sandPrefab, myBlockPos);
                }
                else{
                    blockToBePopulated.InitializeBlock(waterPrefab, myBlockPos);
                }
                chunkBlocks.Add(blockToBePopulated);
            }
        }
        return chunkBlocks;
    } 
}
