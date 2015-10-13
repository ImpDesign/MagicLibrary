using UnityEngine;
using System.Collections;

public class ShootBolt : MonoBehaviour {

    public Vector2 speed = new Vector2(10, 10);
    public int direction;
    public float timer = 1f;

    private float x = 0;

    void Update()
    {
        if(x < timer)
        {
            Vector3 movement = new Vector3(speed.x * direction, 0, 0);
            movement *= Time.deltaTime;
            transform.Translate(movement);
            x += Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
