using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

class BlockCounter
{
	public GameObject prefab;
	public int count;

	public BlockCounter(GameObject p)
	{
		prefab = p;
	}
}

class BlockCollection
{
	public List<BlockCounter> collection = new List<BlockCounter>();
	public float frequency;
	public int SpawnCount;

	public void Add(BlockCounter b)
	{
		collection.Add(b);
	}
}

public class MapGenerator : MonoBehaviour
{
	public float frontPos = 0f;
	public Vector3 origin;
	public bool UseGameobjectOrigin = true;
	public bool BlockSpawnAbove = true;

	public float
		BlockWidth = 1f,
		BlockSpawnTime = 0.5f,
		BlockSpawnHeight = 50f,
		BlockOffset = 0.2f;
	public Int32 Seed;
	public float DistTrigger = 5f;
	public int LineWidth = 5;

	private int lines;
	private GameManager manager;

	public List<GameObject> 
		BBlocks,
		GBlocks,
		NBlocks;

	private BlockCollection 
		gBlocks = new BlockCollection()	{ frequency = 0.2f},
		bBlocks = new BlockCollection() { frequency = 0.2f },
		nBlocks = new BlockCollection() { frequency = 1.2f };

	private float lastLinePos, lastLineDir;

	void Awake()
	{
		Random.seed = Seed;

		if(UseGameobjectOrigin)
			origin = transform.position;

		foreach (var go in BBlocks)
		{
			bBlocks.Add(new BlockCounter(go));
		}
		foreach (var go in NBlocks)
		{
			nBlocks.Add(new BlockCounter(go));
		}
		foreach (var go in GBlocks)
		{
			gBlocks.Add(new BlockCounter(go));
		}
	}

	
	// Update is called once per frame
	void Update ()
	{
		//var frontPos = 10;//manager.GetLeadingPlayer().transform.position.z;

		var direction = 1;//manager.Direction;
		if (direction > 0)
		{
			if (frontPos > lastLinePos - DistTrigger)
				SpawnBlocks(direction);
		}
		else
		{
			if(frontPos<lastLinePos+DistTrigger)
				SpawnBlocks(direction);
		}
	
	}

	private void SpawnBlocks(int dir)
	{
		float prevWidth = 0;
		for (var i = 0; i < LineWidth; i++)
		{

			var blockPosition = new Vector3(origin.x + prevWidth, origin.y, lastLinePos + (BlockWidth*dir));

			int bWeight = (gBlocks.SpawnCount + nBlocks.SpawnCount) - bBlocks.SpawnCount,
				gWeight = (bBlocks.SpawnCount + nBlocks.SpawnCount) - gBlocks.SpawnCount,
				nWeight = (bBlocks.SpawnCount + gBlocks.SpawnCount) - nBlocks.SpawnCount;

			if (bBlocks.frequency*bWeight > (nBlocks.SpawnCount + gBlocks.SpawnCount))
			{
				var ob = SpawnBlock(blockPosition, bBlocks);
				bBlocks.SpawnCount++;
				prevWidth += ob.renderer.bounds.size.x;
				//Can spawn
			}
			else if (gBlocks.frequency*gWeight > (bBlocks.SpawnCount + nBlocks.SpawnCount))
			{
				var ob = SpawnBlock(blockPosition, gBlocks);
				bBlocks.SpawnCount++;
				prevWidth += ob.renderer.bounds.size.x;
			}
			else
			{
				var ob = SpawnBlock(blockPosition, nBlocks);
				bBlocks.SpawnCount++;
				prevWidth += ob.renderer.bounds.size.x;
			}
		}

		lastLinePos += BlockWidth * lastLineDir;
		
	}

	private GameObject SpawnBlock(Vector3 postion, BlockCollection b)
	{
		var ht = new Hashtable
		{
			{"postion", postion},
			{"time", BlockSpawnTime}
		};
		var ob = (GameObject)Instantiate(b.collection[Random.Range(0, b.collection.Count)].prefab, new Vector3(postion.x, postion.y + (BlockSpawnAbove?BlockSpawnHeight:-BlockSpawnHeight) ,postion.z), Quaternion.identity );
		iTween.MoveTo(ob, ht);
		return ob;
	}
}
