using UnityEngine;
using System.Collections;

public class FogController : MonoBehaviour {

    // Use this for initialization
    void Start() {

        Texture2D texture = new Texture2D(128, 128);
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = texture;

        Color color = Color.black;
        for(int x = 0; x < 100; x++)
        {
            for (int y = 0; y < 100; y++)
            {
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

}
