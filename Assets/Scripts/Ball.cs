using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    private GameManager _gm;
    
    private Rigidbody m_Rigidbody;

    private void Awake() {
        _gm = GameManager.Instance;
    }

    void Start() {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionExit(Collision other) {
        var velocity = m_Rigidbody.velocity;

        //after a collision we accelerate a bit
        velocity += velocity.normalized * _gm.data.settings.ballAccelerate;

        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f) {
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }

        //max velocity
        if (velocity.magnitude > _gm.data.settings.ballMaxVelocity) {
            velocity = velocity.normalized * _gm.data.settings.ballMaxVelocity;
        }

        m_Rigidbody.velocity = velocity;
    }
}
