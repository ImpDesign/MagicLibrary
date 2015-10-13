using UnityEngine;
using System.Collections;

public class MoveProjectile : MonoBehaviour {

    public Vector2 speed = new Vector2(10, 10);
    public Vector2 direction = new Vector2(-1, 0);
    public float timer = 1;

    private float x = 0;

	// Update is called once per frame
	void Update () {

        if(x < timer)
        {
            Vector3 movement = new Vector3(speed.x * direction.x, speed.y * direction.y, 0);
            movement *= Time.deltaTime;
            transform.Translate(movement);
            x += Time.deltaTime;
        }

	}
}
