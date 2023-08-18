using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ContactCheck
public class ContactCheck : MonoBehaviour
{
    public bool groundContact  { get; private set;}
    public bool wallContact {get; private set;}
    public Vector2 contactNormal { get; private set;}

    public float friction { get; private set;}
    [SerializeField] float maxGroundSlope = 0.5f;
    [SerializeField] float maxWallSlope = 0.9f;

    PhysicsMaterial2D material;



    void OnCollisionEnter2D(Collision2D collision) {
        EvaluateCollision(collision);
        RetrieveFriction(collision);
    }

    void OnCollisionStay2D(Collision2D collision) {
        EvaluateCollision(collision);
        RetrieveFriction(collision);
    }

    void OnCollisionExit2D(Collision2D collision) {
        groundContact = false;
        wallContact = false;
        friction = 0;
    }


    public void EvaluateCollision(Collision2D collision) {
        for (int i = 0; i < collision.contactCount; i++) {
            contactNormal = collision.GetContact(i).normal;
            groundContact |= contactNormal.y > maxGroundSlope;
            wallContact |= Mathf.Abs(contactNormal.x) > maxWallSlope;
        }
    }

    public float GetWallDirection() {
        if (!wallContact) return 0;
        return (contactNormal.x > 0) ? 1f : -1f;
    }

    void RetrieveFriction(Collision2D collision) {
        material = collision.rigidbody.sharedMaterial;
        friction = (material != null) ? material.friction : 0;

    }
}
