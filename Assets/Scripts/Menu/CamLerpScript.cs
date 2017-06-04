using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/* Lewis Brewer
Simple script that lerps between two points. Start and Finish.
Made for menu scene
Use two empty game objects to set start and finish.
*/
public class CamLerpScript : MonoBehaviour {
	public Transform startPoint;
	public Transform finishPoint;
	public float speed = 12000.0F;

	private float startTime;
	private float journeyLength;
    private bool lerping = false;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene == SceneManager.GetSceneByBuildIndex(0))
        {
            Time.timeScale = 1;
            Play();
        }
    }


    void Play() {
        lerping = true;
        transform.SetPositionAndRotation(startPoint.position, startPoint.rotation);
		startTime = Time.time;
		journeyLength = Vector3.Distance(startPoint.position, finishPoint.position);
        
    }

	void FixedUpdate() {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startPoint.position, finishPoint.position, fracJourney);
        transform.rotation = Quaternion.Lerp(startPoint.rotation, finishPoint.rotation, fracJourney);
        // Stop lerping when we get to the end
        if (Vector3.Distance(transform.position, finishPoint.position) <= 0.5) lerping = false;
    }
}
