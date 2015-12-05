using UnityEngine;
using System.Collections;

public class RandomAnimation : MonoBehaviour {

    public float animationLength;

    private AnimationController2D _animator;
    private float animTimer = 0;
    private string anim;
    private bool done = false;

    void Start ()
    {
        _animator = gameObject.GetComponent<AnimationController2D>();
        animTimer = Random.Range(0, animationLength);
    }

    void Update ()
    {
        if(!done)
        {
            if (animTimer > animationLength)
            {
                _animator.setAnimation("Idle");
                done = true;
            }
            animTimer += Time.deltaTime;
        }
    }
}
