using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int hits;
    public float chargeDuration;
    private float chargeTime;
    private bool charging = false;
    private bool jumping;
    private Vector2 jumpPosition;
    private float jumpTime;
    public float jumpDistance;
    public float jumpSpeed;

    private Vector2 playerPosition;

	void FixedUpdate () {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        if(!charging && !jumping) {
            charging = true;
            chargeTime = Time.time;
        }

        if(charging) {
            Charge();
        }

        if(jumping) {
            UpdateAttack();
        }
        else {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

	}

    public void Damage() {
        hits -= 1;
        if(hits <= 0) {
            GameObject.Destroy(gameObject);
        }
        else if(hits == 1) {
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            sr.color = new Color(255, 155, 155);
        }
    }

    void Attack() {
        Vector2 position = transform.position;
        jumping = true;
        jumpPosition = position + (playerPosition - position).normalized * jumpDistance;
        jumpTime = Time.time;
    }

    void UpdateAttack() {
        if(Time.time - jumpTime > jumpDistance / jumpSpeed) {
            jumping = false;
            return;
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = (jumpPosition - new Vector2(transform.position.x, transform.position.y)).normalized * jumpSpeed;
    }
        
    void Charge() {
        if(Time.time - chargeTime > chargeDuration) {
            Attack();
            charging = false;
        }
    }
}
