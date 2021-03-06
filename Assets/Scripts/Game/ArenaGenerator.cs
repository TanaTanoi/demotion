﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArenaGenerator : MonoBehaviour {

	public GameObject floor;
	public GameObject[] wallPrefabs;
	public GameObject[] tileCarpetPrefabs;
	public GameObject[] tileCarpetClutteredPrefabs;
	public GameObject[] tileCarpetCrackedPrefabs;

	public GameObject[] tileWoodPrefabs;
	public GameObject[] tileWoodClutteredPrefabs;
	public GameObject[] tileWoodCrackedPrefabs;

	List<Vector3> tileCarpetLocations = new List<Vector3>();
	List<Vector3> tileWoodLocations = new List<Vector3>();

	GameObject arenaOutline;

	GameObject DestoryPlane;
	GameObject SpawnPoints;
	GameObject Arena;

	void Start() {
		
	}

	public void Generate () {
		SpawnPoints = GameObject.Find ("SpawnPoints");
		Arena = GameObject.Find ("Arena");

		SpawnPoints.transform.parent = Arena.transform;
		PopulateLists ();
		GenerateArena ();
		GenerateStartPoints ();
		gameObject.AddComponent<ItemSpawnController> ();
	}

	/**
     * populates the lists of tiles. gets tiles from the resources folder.
     */
	void PopulateLists()
	{
		wallPrefabs = Resources.LoadAll<GameObject>("Walls");

		tileCarpetPrefabs = Resources.LoadAll<GameObject>("FloorTiles/CarpetClear");
		tileCarpetClutteredPrefabs = Resources.LoadAll<GameObject>("FloorTiles/CarpetCluttered");
		tileCarpetCrackedPrefabs = Resources.LoadAll<GameObject>("FloorTiles/CarpetCracked");
	
		tileWoodPrefabs = Resources.LoadAll<GameObject>("FloorTiles/WoodClear");
		tileWoodClutteredPrefabs = Resources.LoadAll<GameObject>("FloorTiles/WoodCluttered");
		tileWoodCrackedPrefabs = Resources.LoadAll<GameObject>("FloorTiles/WoodCracked");
	}

	/**
     * finds and sets spawn points in the game.
     */
	void GenerateStartPoints()
	{
		List<Vector3> spawnLocations = new List<Vector3>();
		GameObject spawnPoint = Resources.Load ("SpawnPoint") as GameObject;

		string childTag = "startSpawnLocation";
		foreach (Transform child in arenaOutline.transform) {
			if (child.tag == childTag) {
				spawnLocations.Add (child.position);
			}
		}

		foreach (Vector3 spawnLocation in spawnLocations) {
			GameObject spawn = Instantiate (spawnPoint) as GameObject;
			spawn.transform.position = spawnLocation;
			spawn.transform.position += new Vector3(0,2,0);
			spawn.transform.parent = SpawnPoints.transform;
		}
	}

	/**
     * calls all methods to create the arena
     */
	void GenerateArena(){
		GenerateOutline ();
		GenerateCarpetTiles ();
		GenerateWoodTiles ();
	}

	/**
     * instantiates the wall for the arena and gets all the locations for the tiles.
     */
	void GenerateOutline(){
		List<GameObject> walls = new List<GameObject> ();

//		foreach (GameObject prefab in wallPrefabs) {
//			GameObject prefabObject = Instantiate(prefab) as GameObject; 
//			walls.Add (prefabObject);
//		}

		int randomIndex = Random.Range (0, wallPrefabs.Length);
		GameObject prefabObject = Instantiate(wallPrefabs[1], Vector3.zero, Quaternion.identity) as GameObject; 
		walls.Add (prefabObject);

		int randomIndex2 = Random.Range (0, walls.Count);
		arenaOutline = walls [randomIndex2];
		arenaOutline.transform.parent = Arena.transform;

		string carpetLocationTag = "tileLocation";
		string woodLocationTag = "woodTileLocation";

		foreach (Transform child in arenaOutline.transform) {
			if (child.tag == carpetLocationTag) {
				tileCarpetLocations.Add (child.position);
			} else if (child.tag == woodLocationTag) {
				tileWoodLocations.Add (child.position);
			}
		}
	}

	/**
     * randomly instantiates the correct amount of carpet tiles for the arena and
     * adds them to a list.
     */
	void GenerateCarpetTiles(){
		List<GameObject> tiles = new List<GameObject>();

		int tileClearCount = (int)(tileCarpetLocations.Count * 0.5);
		int tileClutteredCount = (int)(tileCarpetLocations.Count * 0.3);
		int crackedTileCount = tileCarpetLocations.Count - (tileClearCount + tileClutteredCount);

//		// adding "clear" carpet objects
//		for (int i = 0; i < count; i++) {
//			int randomIndex = Random.Range (0, tileCarpetPrefabs.Length);
//			GameObject prefabObject = Instantiate(tileCarpetPrefabs[randomIndex]) as GameObject; 
//			tiles.Add (prefabObject);
//		}
//
//		// adding "cracked" carpet objects
//		for (int i = 0; i < count; i++) {
//			int randomIndex = Random.Range (0, tileCarpetCrackedPrefabs.Length);
//			GameObject prefabObject = Instantiate(tileCarpetCrackedPrefabs[randomIndex]) as GameObject; 
//			tiles.Add (prefabObject);
//		}


		// adding "clear" carpet objects
		for (int i = 0; i < tileClearCount; i++) {
			int randomIndex = Random.Range (0, tileCarpetPrefabs.Length);
			GameObject prefabObject = Instantiate(tileCarpetPrefabs[randomIndex]) as GameObject; 
			tiles.Add (prefabObject);
			prefabObject.transform.parent = Arena.transform;
		}

		// adding "cluttered" carpet objects
		for (int i = 0; i < tileClutteredCount; i++) { 
			int randomIndex = Random.Range (0, tileCarpetClutteredPrefabs.Length);
			GameObject prefabObject = Instantiate (tileCarpetClutteredPrefabs [randomIndex]) as GameObject;
			tiles.Add (prefabObject);
			prefabObject.transform.parent = Arena.transform;
		}
			
		//int crackedTileCount = count / 4;

		//count -= crackedTileCount;

		// adding "cluttered" carpet objects
//		for (int i = 0; i < count; i++) {
//			int randomIndex = Random.Range (0, tileCarpetClutteredPrefabs.Length);
//			GameObject prefabObject = Instantiate(tileCarpetClutteredPrefabs[randomIndex]) as GameObject; 
//			tiles.Add (prefabObject);
//		}

		// adding "cracked" carpet objects
		for (int i = 0; i < crackedTileCount; i++) {
			int randomIndex = Random.Range (0, tileCarpetCrackedPrefabs.Length);
			GameObject prefabObject = Instantiate(tileCarpetCrackedPrefabs[randomIndex]) as GameObject; 
			tiles.Add (prefabObject);
			prefabObject.transform.parent = Arena.transform;
		}
			
		int tilesNum = tiles.Count;

		for (int i = 0; i < tilesNum; i++) {
			int randomIndex = Random.Range (0, tiles.Count);
			GameObject tile = tiles [randomIndex];
			PlaceTile (tile, true);
			//tile.transform.parent = floor.transform;
			tiles.RemoveAt (randomIndex);
		}
	}

	/**
     * randomly instantiates the correct amount of wood tiles for the arena and
     * adds them to a list.
     */
	void GenerateWoodTiles(){
		List<GameObject> tiles = new List<GameObject>();

		int count = tileWoodLocations.Count;
		int tileClearCount = (tileWoodLocations.Count)/2;
		count -= tileClearCount;

		// adding "clear" wood objects
		for (int i = 0; i < tileClearCount; i++) {
			int randomIndex = Random.Range (0, tileWoodPrefabs.Length);
			GameObject prefabObject = Instantiate(tileWoodPrefabs[randomIndex]) as GameObject; 
			tiles.Add (prefabObject);
			prefabObject.transform.transform.parent = Arena.transform;
		}

		int crackedTileCount = count / 4;

		count -= crackedTileCount;

		// adding "cluttered" wood objects
		for (int i = 0; i < count; i++) {
			int randomIndex = Random.Range (0, tileWoodClutteredPrefabs.Length);
			GameObject prefabObject = Instantiate(tileWoodClutteredPrefabs[randomIndex]) as GameObject; 
			tiles.Add (prefabObject);
			prefabObject.transform.transform.parent = Arena.transform;
		}

		// adding "cracked" wood objects
		for (int i = 0; i < crackedTileCount; i++) {
			int randomIndex = Random.Range (0, tileWoodCrackedPrefabs.Length);
			GameObject prefabObject = Instantiate(tileWoodCrackedPrefabs[randomIndex]) as GameObject; 
			tiles.Add (prefabObject);
			prefabObject.transform.transform.parent = Arena.transform;
		}

		int tilesNum = tiles.Count;

		for (int i = 0; i < tilesNum; i++) {
			int randomIndex = Random.Range (0, tiles.Count);
			GameObject tile = tiles [randomIndex];
			PlaceTile (tile, false);
			//tile.transform.parent = floor.transform;
			tiles.RemoveAt (randomIndex);
		}
	}
		
	/**
     * places a tile at a random tile location in the arena
     */
	void PlaceTile(GameObject tile, bool isCarpet){
		bool carpetTile = isCarpet;

		List<int> possibleRotations = new List<int>(){0, 90, 180, 270};
		int rotateBy = possibleRotations[Random.Range(0,possibleRotations.Count)];
		tile.transform.Rotate (0, rotateBy, 0);

		tile.transform.position = TilePointReference (carpetTile);
		SetMusicToObjectsOfTile (tile);
	}

	/**
     * gets the random tile location in the arena
     */
	Vector3 TilePointReference(bool isCarpet){
		Vector3 toReturn;
		if (isCarpet) {
			int randomIndex = Random.Range (0, tileCarpetLocations.Count);
			toReturn = tileCarpetLocations [randomIndex];
			tileCarpetLocations.RemoveAt (randomIndex);
		} else {
			int randomIndex = Random.Range (0, tileWoodLocations.Count);
			toReturn = tileWoodLocations [randomIndex];
			tileWoodLocations.RemoveAt (randomIndex);
		}

		return toReturn;
	}

	public void SetMusicToObjectsOfTile(GameObject tile){
		
		Transform tileTransform = tile.transform;

		Rigidbody[] bodies = tileTransform.GetComponentsInChildren<Rigidbody>();
		foreach (Rigidbody rb in bodies) {
			FMODUnity.StudioEventEmitter x = rb.gameObject.AddComponent<FMODUnity.StudioEventEmitter> ();
			x.PlayEvent = FMODUnity.EmitterGameEvent.CollisionEnter;
			int i = Random.Range (0, 3);
			if (i == 0) {
				x.Event = "event:/FX/player/player_collision_impact";
			} else if (i == 1){
				x.Event = "event:/FX/powerups/powerup_spawn";
			} else {
				//x.Event = "event:/FX/environment/furniture_destruction";
				x.Event = "event:/FX/player/player_collision_impact";
			}
		}

	}
}
