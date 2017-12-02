using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GameManager : MonoBehaviour {
	public GameObject playerRef;
	private Scr_Player playerScr;
	public GameObject mapRef;
	private Scr_MapData mapScr;

	// Use this for initialization
	void Start () {
		playerScr = playerRef.GetComponent<Scr_Player>();
		mapScr = mapRef.GetComponent<Scr_MapData>();

		mapScr.Initialise();
		Vector2Int startpos = new Vector2Int(1, 1);
		startpos = mapScr.FindTilePosition((int)Scr_MapData.TileType.START);
		if (startpos.x == - 1 || startpos.y == -1)
		{
			Debug.Log("No start position on map!");
			startpos = new Vector2Int(1, 1);
		}
		playerScr.Initialise(startpos, mapScr);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
