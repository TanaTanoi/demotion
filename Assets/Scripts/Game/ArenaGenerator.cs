using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArenaGenerator : MonoBehaviour {

	public GameObject floor;
	public GameObject[] wallPrefabs;
	public GameObject[] tilePrefabs;
	public GameObject[] tileClutteredPrefabs;
	public GameObject[] tileCrackedPrefabs;

	List<Vector3> tileLocations = new List<Vector3>();
	GameObject arenaOutline;

	GameObject SpawnPoints;

	// Use this for initialization
	void Start () {
		SpawnPoints = GameObject.Find ("SpawnPoints");
		PopulateLists ();

		Debug.Log (wallPrefabs.Length);
		Debug.Log (tilePrefabs.Length);
		Debug.Log (tileClutteredPrefabs.Length);
		Debug.Log (tileCrackedPrefabs.Length);

		GenerateArena ();
		GenerateStartPoints ();
	}

	void PopulateLists()
	{
		wallPrefabs = Resources.LoadAll<GameObject>("Walls");
		tilePrefabs = Resources.LoadAll<GameObject>("FloorTiles/TilesTest");
		tileClutteredPrefabs = Resources.LoadAll<GameObject>("FloorTiles/ClutteredTilesTest");
		tileCrackedPrefabs = Resources.LoadAll<GameObject>("FloorTiles/CrackedTilesTest");
	}

	void GenerateStartPoints(){

		List<Vector3> spawnLocations = new List<Vector3>();
		GameObject spawnPoint = Resources.Load ("SpawnPoint") as GameObject;

		string childTag = "startSpawnLocation";
		foreach (Transform child in arenaOutline.transform) {
			if (child.tag == childTag) {
				spawnLocations.Add (child.position);
			}
		}
			
		Debug.Log (spawnLocations.Count);

		foreach (Vector3 spawnLocation in spawnLocations) {
			GameObject spawn = Instantiate (spawnPoint) as GameObject;
			spawn.transform.position = spawnLocation;
			spawn.transform.position += new Vector3(0,2,0);
			spawn.transform.parent = SpawnPoints.transform;
	
		}
	}

	public void GenerateArena(){
		GenerateOutline ();
		GenerateTiles ();
	}

	void GenerateOutline(){
		List<GameObject> walls = new List<GameObject> ();

//		foreach (GameObject prefab in wallPrefabs) {
//			GameObject prefabObject = Instantiate(prefab) as GameObject; 
//			walls.Add (prefabObject);
//		}

		int randomIndex = Random.Range (0, wallPrefabs.Length);
		GameObject prefabObject = Instantiate(wallPrefabs[randomIndex]) as GameObject; 
		walls.Add (prefabObject);

		int randomIndex2 = Random.Range (0, walls.Count);
		arenaOutline = walls [randomIndex2];
	
		string tag = "tileLocation";
		foreach (Transform child in arenaOutline.transform) {
			if (child.tag == tag) {
				tileLocations.Add (child.position);
			}

		}
			
	}

	void GenerateTiles(){
		List<GameObject> tiles = new List<GameObject>();

		int count = tileLocations.Count;
		int tileCount = (tileLocations.Count)/2;
		count -= tileCount;

		for (int i = 0; i < tileCount; i++) {
			int randomIndex = Random.Range (0, tilePrefabs.Length);
			GameObject prefabObject = Instantiate(tilePrefabs[randomIndex]) as GameObject; 
			tiles.Add (prefabObject);
		}
			
		int crackedTileCount = count / 4;

		count -= crackedTileCount;

		for (int i = 0; i < count; i++) {
			int randomIndex = Random.Range (0, tileClutteredPrefabs.Length);
			GameObject prefabObject = Instantiate(tileClutteredPrefabs[randomIndex]) as GameObject; 
			tiles.Add (prefabObject);
		}

		for (int i = 0; i < crackedTileCount; i++) {
			int randomIndex = Random.Range (0, tileCrackedPrefabs.Length);
			GameObject prefabObject = Instantiate(tileCrackedPrefabs[randomIndex]) as GameObject; 
			tiles.Add (prefabObject);
		}


		int tilesNum = tiles.Count;

		for (int i = 0; i < tilesNum; i++) {
			int randomIndex = Random.Range (0, tiles.Count);
			GameObject tile = tiles [randomIndex];
			PlaceTile (tile);
			//tile.transform.parent = floor.transform;
			tiles.RemoveAt (randomIndex);
		}
	}

	void PlaceTile(GameObject tile){
		List<int> possibleRotations = new List<int>(){0, 90, 180, 270};
		int rotateBy = possibleRotations[Random.Range(0,possibleRotations.Count)];
		tile.transform.Rotate (0, rotateBy, 0);

		tile.transform.position = TilePointReference ();
	}

	Vector3 TilePointReference(){
		int randomIndex = Random.Range (0, tileLocations.Count);
		Vector3 toReturn = tileLocations [randomIndex];
		tileLocations.RemoveAt (randomIndex);
		return toReturn;
	}

}
