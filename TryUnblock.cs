using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --- AUTHOR ---
// Luke Kabat, 2021
//
// --- DESCRIPTION ---
// This script will try to unstuck object if it is spawned/moved inside another 2D collider.
//
// --- PUBLIC PARAMETERS TO SET IN EDITOR ---
//      GameObject goToMove         - which transform should be moved when stuck (in most cases same object as this script is attached to, sometimes parent if collider is a child)
//      bool isManuallyActivated    - false by default (will activate on Awake), set to true and manually call Unblock() whenever needed (for example after teleporting object)
//      float distanceToMove        - 5f by default, distance to move the object when stuck

[RequireComponent(typeof(Collider2D))]
public class TryUnblock : MonoBehaviour
{

    public GameObject goToMove;
    public bool isManuallyActivated = false;
    public float distanceToMove = 5.0f;

    private const float RAYCAST_DIST = 0.1f;
    private Vector3 directionToMove;
    private Collider2D ownCollider;

    private void Awake() {
        TryGetComponent(out ownCollider);
        if (!isManuallyActivated) {
            Unblock();
        }
    }

    public void Unblock() {
        if (ownCollider) {
            directionToMove = Random.insideUnitSphere.normalized * distanceToMove;
            directionToMove.z = 0.0f;

            if (IsColliding()) {
                MoveOutOfCollision();
            }
        }
    }

    private bool IsColliding() {
        ContactFilter2D cf = new ContactFilter2D();
        cf.useTriggers = false;

        // Additional filter option can be specified on cf object
        // ...

        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        Physics2D.Raycast(transform.position, -directionToMove, cf, hits, RAYCAST_DIST);
        foreach(var hit in hits) {
            if (hit && hit.collider && hit.collider != ownCollider) {
                print("Intersecting " + hit.collider.name);
                return true;
            }
        }
        return false;
    }

    private void MoveOutOfCollision() {
        goToMove.transform.position += directionToMove;
        if (IsColliding()) {
            MoveOutOfCollision();
        }
    }

}
