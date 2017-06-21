using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour {
    Bounds bounds;
    public AnimationCurve powerupFrequency;
    [Tooltip ("Time before max spawn frequency reached")]
    public float softTimeLimit = 60f;
    [Tooltip ("Minimum Time between Powerup spawns")]
    public float minmumDelay = 1f;
    [Tooltip ("Initial delay between powerup spawns")]
    public float startingDelay = 10.1f;
    [Tooltip ("Delay before the first powerup")]
    public float delayBeforeFirstPowerup = 0f;

	public GameObject boostModel;
	public GameObject powerModel;
	public GameObject shieldModel;

	public ParticleSystem spawnEffect;

    private Powerup.Type[] buffs = new Powerup.Type[] {
        Powerup.Type.POWER,
		Powerup.Type.SHIELD
    };

    private float startTime; // The time the spawning started
    void Start () {
        bounds = GetComponent<MeshFilter>().mesh.bounds;
        StartSpawning();
	}

    void StartSpawning() {
        startTime = Time.time;
        new WaitForSeconds(delayBeforeFirstPowerup);
        StartCoroutine(ContinuouslySpawn());
    }

    IEnumerator ContinuouslySpawn() {
        while(true) {
            SpawnPowerup();
            float time = (Time.time - startTime) / (softTimeLimit);
            float delay = Mathf.Max(startingDelay - (powerupFrequency.Evaluate(time) * startingDelay), minmumDelay);
            yield return new WaitForSeconds(delay);
        }
    }

    void SpawnPowerup() {
        Powerup.Type type = RandomPowerupType();
        Vector3 location = RandomLocaitonInBounds();
        CreatePowerup(type, location);
    }

    private Powerup.Type RandomPowerupType() {
        return buffs[Random.Range(0, buffs.Length)];
    }


    private GameObject CreatePowerup(Powerup.Type type, Vector3 location) {
		GameObject powerup = PowerupModel (type);
		powerup.transform.localScale = Vector3.one * 2;
        powerup.transform.position = location + transform.position;
		powerup.transform.rotation = Quaternion.Euler (new Vector3 (Random.Range (0, 360), Random.Range (0, 360), Random.Range (0, 360)));


        powerup.AddComponent<Rigidbody>();
		powerup.AddComponent<SphereCollider> ();
        powerup.AddComponent<PowerupController>().type = type;

		ParticleSystem ps = Instantiate (spawnEffect);
		ps.transform.parent = powerup.transform;
		ps.transform.localPosition = powerup.GetComponent<SphereCollider> ().center;
		ParticleSystem.MinMaxGradient x = ps.main.startColor;
		x.color = Color.red;

        return powerup;
    }

	private GameObject PowerupModel(Powerup.Type type){
		GameObject powerup;
		switch(type){
		case Powerup.Type.BOOST:
			powerup = boostModel;
			break;
		case Powerup.Type.POWER:
			powerup = powerModel;
			break;
		case Powerup.Type.SHIELD:
			powerup = shieldModel;
			break;
		default:
			powerup = null;
			break;
		};
		if (powerup == null) {
			powerup = GameObject.CreatePrimitive (PrimitiveType.Cube); // replace with model / prefab once decided
		} else {
			powerup = Instantiate (powerup);
		}
		return powerup;
	}

    Vector3 RandomLocaitonInBounds() {
        Vector3 randomExtension = new Vector3(Random.Range(-1.0f, 1.0f) * bounds.extents.x, Random.Range(-1.0f, 1.0f) * bounds.extents.y, Random.Range(-1.0f, 1.0f) * bounds.extents.z);
        return bounds.center + randomExtension;
    }
}
