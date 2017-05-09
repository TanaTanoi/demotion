using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoweurpSpawner : MonoBehaviour {
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
        return (Powerup.Type) Random.Range(0, (int)Powerup.Type.Count);
    }


    private GameObject CreatePowerup(Powerup.Type type, Vector3 location) {
        GameObject powerup = GameObject.CreatePrimitive(PrimitiveType.Cube); // replace with model / prefab once decided
        powerup.transform.position = location + transform.position;
        powerup.AddComponent<Rigidbody>();
        powerup.AddComponent<PowerupController>().type = type;
        return powerup;
    }

    Vector3 RandomLocaitonInBounds() {
        Vector3 randomExtension = new Vector3(Random.Range(-1.0f, 1.0f) * bounds.extents.x, Random.Range(-1.0f, 1.0f) * bounds.extents.y, Random.Range(-1.0f, 1.0f) * bounds.extents.z);
        return bounds.center + randomExtension;
    }
}
