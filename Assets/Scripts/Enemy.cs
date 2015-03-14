using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int hits;
    public float moveSpeed;

    //Jumping
    public float jumpDistance; //The distance of the jump
    public float jumpSpeed; //The speed of the jump
    private bool jumping;
    private float jumpTime; //Time of jump start
    private Vector2 jumpPosition; //Position to jump to
   
    //Charging
    public float chargeDistance; //The min distance between player and enemy for enemy to start charging
    public float chargeDuration; //The amount of time the enemy has to charge
    private float chargeTime; //Time of charge start
    private bool charging;

    private Vector2 playerPosition;
    private Vector2 position;

    void Start() {
        charging = false;
        jumping = false;
    }

	void FixedUpdate () {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        position = new Vector2(transform.position.x, transform.position.y);

        if(!charging && !jumping && (playerPosition - position).magnitude <= chargeDistance) {
            charging = true;
            chargeTime = Time.time;
        }
        else if(!charging && !jumping && (playerPosition - position).magnitude > chargeDistance) {
            Move();
        }
        
        if(charging) {
            Charge();
        }
        
        if(jumping) {
            UpdateAttack();
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

    void Move() {
        gameObject.GetComponent<Rigidbody2D>().velocity = (playerPosition - position).normalized * moveSpeed;
    }

    void Charge() {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if(Time.time - chargeTime > chargeDuration) {
            Attack();
            charging = false;
        }
    }
}
