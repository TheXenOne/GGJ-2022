using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensingComponent : MonoBehaviour
{
    public GameObject playerRef;
    public LayerMask obstructionMask;
    public bool canSeePlayer;
    public bool canSensePlayer;
    [Range(0, 360)]
    public float angle;
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        canSeePlayer = false;
    }

    void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    bool FOV_CanSeePlayer()
    {
        Transform target = playerRef.transform;
        Vector3 toTargetDir = (target.position - transform.position);
        float distToTarget = toTargetDir.magnitude;
        canSensePlayer = false;
        if (distToTarget < radius)
        {
            canSensePlayer = true;
            if (Vector3.Angle(transform.up, toTargetDir) < angle / 2)
            {
                if (!Physics2D.Raycast(transform.position, toTargetDir, distToTarget, obstructionMask))
                    return true;
            }
        }
        return false;
    }


    // Update is called once per frame
    void Update()
    {
        canSeePlayer = FOV_CanSeePlayer();
        Color color = new Color(0, 0, 1.0f);
        Debug.DrawLine(transform.position, transform.position + (transform.up * radius), color);
    }
}
