using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Player : MonoBehaviour {

	private int pos_x = 0, pos_y = 0;
	private Transform playerRef;
	private Scr_MapData mapScr;
	private float positionModifier_x = .5f; // align sprite display into grid
	private float positionModifier_y = .5f;
	public float moveDelay = .1f; // key repeat delay
	private float moveTimer = 0f;

	// Use this for initialization
	public void Initialise(Vector2Int startpos, Scr_MapData mapref) {
		playerRef = GetComponentInParent<Transform>();
		mapScr = mapref;
		positionModifier_x -= (mapScr.map_size_x / 2); // further align sprite display dependent on map size
		positionModifier_y -= (mapScr.map_size_y / 2);
		pos_x = startpos.x;
		pos_y = startpos.y;
		UpdatePlayerPosition();
	}

	// Update is called once per frame
	void Update() {
		if (Input.anyKey)
		{
			int x_d = 0, y_d = 0;
			if (Input.anyKeyDown)
			{
				if (Input.GetKeyDown(KeyCode.LeftArrow))
					x_d = -1;
				if (Input.GetKeyDown(KeyCode.RightArrow))
					x_d = 1;

				if (Input.GetKeyDown(KeyCode.DownArrow))
					y_d = -1;
				if (Input.GetKeyDown(KeyCode.UpArrow))
					y_d = 1;
			}
			else
			{

				if (Input.GetKey(KeyCode.LeftArrow))
				{
					if (moveTimer > moveDelay)
					{
						x_d = -1;
						moveTimer = 0;
					}
					else
						moveTimer += Time.deltaTime;

				}
				if (Input.GetKey(KeyCode.RightArrow))
				{
					if (moveTimer > moveDelay)
					{
						x_d = 1;
						moveTimer = 0;
					}
					else
						moveTimer += Time.deltaTime;
				}
				if (Input.GetKey(KeyCode.DownArrow))
				{
					if (moveTimer > moveDelay)
					{
						y_d = -1;
						moveTimer = 0;
					}
					else
						moveTimer += Time.deltaTime;
				}
				if (Input.GetKey(KeyCode.UpArrow))
				{
					if (moveTimer > moveDelay)
					{
						y_d = 1;
						moveTimer = 0;
					}
					else
						moveTimer += Time.deltaTime;
				}
			}

			if (x_d != 0 || y_d != 0)
			{
				MovePlayer(pos_x + x_d, pos_y + y_d);
				moveTimer = 0;
			}
		}
	}

	void UpdatePlayerPosition()
	{
		playerRef.transform.position = new Vector3(pos_x + positionModifier_x, pos_y + positionModifier_y, 0);
	}

	void MovePlayer(int to_x, int to_y)
	{
		if (!mapScr.CheckMapBlocked(to_x, to_y))
		{
			pos_x = to_x;
			pos_y = to_y;
			UpdatePlayerPosition();
		}
	}
}
