using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WorldManager : MonoBehaviour
{
    public GameObject worldManagerSandPrefab;
    public GameObject worldManagerWaterPrefab;
    public GameObject Player;
    public List<ChunkData> allChunks = new List<ChunkData>();
    public Vector3 lastPlayerPosition;
    

    void Start()
    {
        lastPlayerPosition = Player.transform.position;
        SpawnChunksAroundPlayer();
    }


    void Update()
    {
        if(lastPlayerPosition != Player.transform.position){
            SpawnChunksAroundPlayer();
            DestroyOldChunks(); 
            lastPlayerPosition = Player.transform.position;
            }
    }

    public void SpawnBlocks(List<Block> chunkblockslist){
        foreach(Block b in chunkblockslist){
            GameObject newBlock = GameObject.Instantiate(b.blockPrefab, b.position, Quaternion.identity);
        }
    }

    public bool ValidateChunk(Vector2Int myChunkOrigin){
        if (!allChunks.Any(chunk => chunk.chunkOrigin == myChunkOrigin))
        {
            return true;
        }
        else{
            return false;
        }
    }

    public void SpawnChunk(Vector2Int chunkOffset){
        ChunkData myChunk = ScriptableObject.CreateInstance<ChunkData>();
        myChunk.sandPrefab = worldManagerSandPrefab;
        myChunk.waterPrefab = worldManagerWaterPrefab;
        Vector2Int myChunkOrigin = new Vector2Int(chunkOffset.x, chunkOffset.y);
        myChunk.InitializeChunk(myChunkOrigin);
        List<Block> myChunkBlocksList = myChunk.PopulateBlocks();
        SpawnBlocks(myChunkBlocksList);
        allChunks.Add(myChunk);
    }
    
    public int PlayerXPosRoundTo5(){
        int x = (int)Player.transform.position.x;
        for(int i=0;i<=5;i++){
             if(x%5==0){
                return x;
            }
            else{
                x++;
            }
        }
       return 0;
    }
    
    public int PlayerZPosRoundTo5(){
        int z = (int)Player.transform.position.z;
        for(int i=0;i<=5;i++){
             if(z%5==0){
                return z;
            }
            else{
                z++;
            }
        }
        return 0;
    }

    public void DestroyOldChunks(){
        List<ChunkData> ChunksToRemove = new List<ChunkData>();
        Vector3 playerPosition = Player.transform.position;

        foreach(ChunkData c in allChunks){
            Vector3 myDestroyChunkOrigin = new Vector3(c.chunkOrigin.x, 0, c.chunkOrigin.y);
            float distance = Vector3.Distance(playerPosition, myDestroyChunkOrigin);
            if(distance>= 50){
                ChunksToRemove.Add(c);
            }
        }

        foreach(ChunkData c2 in ChunksToRemove){
            foreach(Block b in c2.chunkBlocks){
                GameObject nearestObject = FindNearestBlock(b.position);
                Destroy(nearestObject);
                allChunks.Remove(c2);
            }
        }
    }
    public GameObject FindNearestBlock(Vector3 position){
        GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("BlockTag");
        GameObject nearestBlock = null;
        float minDistance = Mathf.Infinity;

        foreach(GameObject g in allBlocks){
            float distance = Vector3.Distance(g.transform.position, position);
            if(distance <= minDistance)
            {
                minDistance = distance;
                nearestBlock = g;
            }
        }
        return nearestBlock;
    }

    public void SpawnChunksAroundPlayer(){
        Vector2Int[] blockOffsets = new Vector2Int[]{
        new Vector2Int(-5, -5), new Vector2Int(0, -5), new Vector2Int(5, -5),
        new Vector2Int(-5, 0), new Vector2Int(0, 0), new Vector2Int(5, 0),
        new Vector2Int(-5, 5), new Vector2Int(0, 5), new Vector2Int(5, 5),
        };
        Vector2Int currentChunkOrigin = new Vector2Int(PlayerXPosRoundTo5(), PlayerZPosRoundTo5());
            
        foreach(Vector2Int v in blockOffsets){   
            Vector2Int offsetChunkOrigin = new Vector2Int((currentChunkOrigin.x+v.x),(currentChunkOrigin.y+v.y));
            if(ValidateChunk(offsetChunkOrigin)) {
            SpawnChunk(offsetChunkOrigin);
            }
        }
    }
}
