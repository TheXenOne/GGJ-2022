using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguEmotionEngine : MonoBehaviour
{
    SpriteRenderer greenBola;
    Rigidbody2D nyoomComponent;
    public Sprite penguIsDown;
    public Sprite penguIsHappy;
    public Sprite penguIsAlwaysRight;

    // Start is called before the first frame update
    void Start()
    {
        greenBola = gameObject.GetComponent<SpriteRenderer>();
        nyoomComponent = gameObject.GetComponent<Rigidbody2D>();
    }

    float GetOrientationFromVelocity(Vector2 veloo)
    {
        if (veloo.magnitude == 0)
            return 0.0f;
        return -Mathf.Atan2(veloo.x, veloo.y) * Mathf.Rad2Deg;
    }

    // Update is called once per frame
    void Update()
    {
        //Reset state
        greenBola.flipX = false;
        greenBola.flipY = false;

        if (greenBola != null && nyoomComponent != null)
        {
            float thetas = GetOrientationFromVelocity(nyoomComponent.velocity);
            float absThetas = Mathf.Abs(thetas);
            if (absThetas < 45) //up
            {
                greenBola.sprite = penguIsDown;
            }
            else if (thetas > 45 && thetas < 135) //left
            {
                greenBola.sprite = penguIsAlwaysRight;
                greenBola.flipX = true;
            }
            else if (absThetas > 135) //down
            {
                greenBola.sprite = penguIsDown;
                greenBola.flipY = true;
            }
            else if (thetas < -45 &&  thetas > -135) //right
            {
                greenBola.sprite = penguIsAlwaysRight;
            }

            //180 down
            //-180 down left
            //75 left up
            //90 left
            //0 up
        }
    }
}
