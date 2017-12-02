using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Scr_MapData : MonoBehaviour {
	public int map_size_x = 32;
	public int map_size_y = 24;

	private int[,] mapTiles;
	//private GameObject[,] mapObjects;
	public Tilemap tilemap;
	public enum TileType
	{
		EMPTY,
		START,
		FLOOR1,
		WALL1
	}



	MapTileDefinition[] _mapTileTypes =
	{
		new MapTileDefinition(TileType.EMPTY, false, "Tiles/t_floor1"),
		new MapTileDefinition(TileType.START, false, "Tiles/t_floor1"),
		new MapTileDefinition(TileType.FLOOR1, false, "Tiles/t_floor1"),
		new MapTileDefinition(TileType.WALL1, true, "Tiles/t_wall1")
	};

	// Use this for initialization
	public void Initialise () {	
		mapTiles = new int[map_size_x, map_size_y];
		bool start_placed = false;
		for (int y = 0; y < map_size_y; y++)
		{
			for (int x = 0; x < map_size_x; x++)
			{
				int rnd = (int)Random.Range(0, 4);
				mapTiles[x, y] = rnd;
				if ((TileType)rnd == TileType.START)
					start_placed = true;
			}
		}

		if (!start_placed)
			mapTiles[1, 1] = (int)TileType.START;

		DrawMap();
	}

	public Vector2Int FindTilePosition(int tt)
	{
		int x = 0;
		int y = 0;

		for ( y = 0; y < map_size_y; y++)
		{
			for ( x = 0; x < map_size_x; x++)
			{
				if (mapTiles[x, y] == tt)
				{
					return new Vector2Int(x, y);
				}
			}
		}

		return new Vector2Int(-1, -1);
	}
	
	public void DrawMap()
	{
		int x = 0;
		int x_offset = (int)map_size_x / 2;
		int y = 0;
		int y_offset = (int)map_size_y / 2;

		for (y = 0; y < map_size_y; y++)
		{
			for (x = 0; x < map_size_x; x++)
			{
				int tt = mapTiles[x, y];
				Tile tl = Resources.Load(_mapTileTypes[tt].ResourceName) as Tile;
				tilemap.SetTile(new Vector3Int(x - x_offset, y - y_offset, 0), tl);
			}
		}

		tilemap.RefreshAllTiles();
	}

	public bool CheckMapBlocked(int x_pos, int y_pos)
	{
		if (x_pos < 0 || x_pos > map_size_x)
			return true;

		if (y_pos < 0 || y_pos > map_size_y)
			return true;

		if (_mapTileTypes[mapTiles[x_pos, y_pos]].Blocking)
		{
			return true;
		}
		else
			return false;
	}
}

	public class MapTileDefinition
{
	public Scr_MapData.TileType Tt;
	public bool Blocking;
	public string ResourceName;

	public MapTileDefinition(Scr_MapData.TileType tt, bool bl, string rn)
	{
		Tt = tt;
		Blocking = bl;
		ResourceName = rn;

	}
}
