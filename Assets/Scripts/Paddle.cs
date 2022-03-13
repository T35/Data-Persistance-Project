using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    private GameManager _gm;
    
    public float MaxMovement = 2.0f;

    private void Awake() {
        _gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update() {
        float input = Input.GetAxis("Horizontal");

        Vector3 pos = transform.position;
        pos.x += input * _gm.data.settings.paddleSpeed * Time.deltaTime;

        if (pos.x > MaxMovement)
            pos.x = MaxMovement;
        else if (pos.x < -MaxMovement)
            pos.x = -MaxMovement;

        transform.position = pos;
    }
}
