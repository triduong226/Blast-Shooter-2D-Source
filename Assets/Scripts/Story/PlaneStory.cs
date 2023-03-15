using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneStory : MonoBehaviour
{
    private Rigidbody2D myBody;
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        myBody.velocity = new Vector2(0f, +0.2f);
    }
}
