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
    private bool first = true;
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
            //if (first) return;
            //ebug.Log("asd");
            //lerping = false;
            //transform.SetPositionAndRotation(finishPoint.position, finishPoint.rotation);
            
        }
    }

    private void Awake()
    {
        first = true;
        Play();
    }


    void Play() {
        lerping = true;
        transform.SetPositionAndRotation(startPoint.position, startPoint.rotation);
		startTime = Time.time;
		journeyLength = Vector3.Distance(startPoint.position, finishPoint.position);
        first = false;
	}

	void Update() {
        if (!lerping) return;
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp(startPoint.position, finishPoint.position, fracJourney);
		transform.rotation = Quaternion.Lerp(startPoint.rotation, finishPoint.rotation, fracJourney);
        // Stop lerping when we get to the end
        if(transform.position == finishPoint.position) lerping = false;
	}

    public void QuitGame()
    {
        Application.Quit();
    }
}
