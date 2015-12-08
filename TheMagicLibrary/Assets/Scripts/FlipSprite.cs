using UnityEngine;
using System.Collections;

public class FlipSprite : MonoBehaviour
{
    void Start()
    {
        GetComponent<AnimationController2D>().setFacing("Left");
    }
}
