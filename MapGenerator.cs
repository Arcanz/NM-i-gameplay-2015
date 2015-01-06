using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using System.Collections;

class BlockCounter
{
	public GameObject prefab;
	public int count;

	public BlockCounter(GameObject p)
	{
		prefab = p;
	}
}

public class MapGenerator : MonoBehaviour
{
	public float distTrigger = 5f;

	private int lines;
	private float blockWidth;
	private GameManager manager;

	public List<GameObject> 
		BBlocks,
		GBlocks,
		NBlocks;

	private List<BlockCounter> gBlocks;
	private List<BlockCounter> bBlocks;
	private List<BlockCounter> nBlocks;

	private float lastBoxPos, lastBoxDir;


	void Awake()
	{
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
		var frontPos = manager.GetLeadingPlayer().transform.position.z;

		if (manager.Direction > 0)
		{
			if (frontPos > lastBoxPos + distTrigger)
				SpawnBlocks(manager.Direction);
		}
		else
		{
			if(frontPos<lastBoxPos-distTrigger)
				SpawnBlocks((manager.Direction));
		}
	
	}

	private void SpawnBlocks(int dir)
	{
		
	}
}
