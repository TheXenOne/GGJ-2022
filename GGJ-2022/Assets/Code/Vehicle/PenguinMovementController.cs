using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinMovementController : MonoBehaviour
{
    public class AgentInfo
    {
        public float speed = 5.0f;
        public Vector2 position;
        public Vector2 velocity;
    }

    SteeringOutput steer;
    SeekBehaviour seek;
    FleeBehaviour flee;
    WanderBehaviour wander;
    PlayerSensingComponent psRef;
    AgentInfo agentInfo;
    Rigidbody2D _rigidbody;
    GameObject ply;
    Chonkfactory chimkyPly;
    Chonkfactory chimkyPinky;

    public float _wanderSoundRandomRange = 5f;
    public float _aggroSoundRandomRange = 5f;

    float _currentTimer = 0f;

    public class SteeringOutput
    {
        public Vector2 velocity = new Vector2();
    }

    public abstract class SteeringBehaviour
    {
        public abstract SteeringOutput CalcSteering(AgentInfo info);
    }

    public class SeekBehaviour : SteeringBehaviour
    {
        public Vector2 movementTarget;

        public override SteeringOutput CalcSteering(AgentInfo info)
        {
            SteeringOutput steering = new SteeringOutput();
            steering.velocity = movementTarget - info.position; //Desired Velocity
            steering.velocity.Normalize(); //Normalize Desired Velocity
            steering.velocity *= info.speed; //Rescale to Max Speed
            return steering;
        }
    }

    public class WanderBehaviour : SeekBehaviour
    {
        public override SteeringOutput CalcSteering(AgentInfo info)
        {
            SteeringOutput steering = new SteeringOutput();

            //Calculate WanderOffset
            Vector2 offset = info.velocity;
            offset.Normalize();
            offset *= Offset;

            //WanderCircle Offset (Polar to Cartesian Coordinates)
            Vector2 circleOffset = new Vector2( Mathf.Cos(WanderAngle) * Radius, Mathf.Sin(WanderAngle) * Radius);

            //Change the WanderAngle slightly for next frame
            WanderAngle += Random.value * AngleChange - (AngleChange * .5f); //RAND[-angleChange/2,angleChange/2]

            //Set target as Seek::Target
            movementTarget = info.position + offset + circleOffset;

            //Return Seek Output (with our wander target)
            return base.CalcSteering(info);
        }
        public float Offset = 6.0f; //Offset (Agent Direction)
        public float Radius = 4.0f; //WanderRadius
        public float AngleChange = 30 * Mathf.Deg2Rad; //Max WanderAngle change per frame
        public float WanderAngle = 0.0f; //Internal
    }

    public class FleeBehaviour : SeekBehaviour
    {
        public override SteeringOutput CalcSteering(AgentInfo info)
        {
            SteeringOutput steering = base.CalcSteering(info);
            steering.velocity = steering.velocity * -1;
            return steering;
        }
    }

    float GetOrientationFromVelocity(Vector2 veloo)
	{
		if (veloo.magnitude == 0)
			return 0.0f;
		return -Mathf.Atan2(veloo.x, veloo.y);
    }

    // Start is called before the first frame update
    void Start()
    {
        psRef = GetComponent<PlayerSensingComponent>();
        seek = new SeekBehaviour();
        flee = new FleeBehaviour();
        agentInfo = new AgentInfo();
        _rigidbody = GetComponent<Rigidbody2D>();
        steer = new SteeringOutput();
        wander = new WanderBehaviour();
        ply = psRef.playerRef;
    }

    // Update is called once per frame
    void Update()
    {
        //Device movementbehaviour
        //TODO(stijn): if fleeing too far stop and start wandering again 
        if (psRef.canSensePlayer)
        {
            if (chimkyPly == null) chimkyPly = psRef.playerRef.GetComponent<Chonkfactory>();
            if (chimkyPinky == null) chimkyPinky = gameObject.GetComponent<Chonkfactory>();
            if (psRef.canSeePlayer)
            {
                if (chimkyPly.Chonk >= chimkyPinky.Chonk)
                {
                    flee.movementTarget = psRef.playerRef.transform.position;
                    steer = flee.CalcSteering(agentInfo);
                }
                else if (ply)
                {
                    seek.movementTarget = ply.transform.position;
                    steer = seek.CalcSteering(agentInfo);
                }
            }
        }
        else
        {
            steer = wander.CalcSteering(agentInfo);
        }

        //Linear movement
        Vector2 linVel = _rigidbody.velocity;
        Vector2 steeringForce = steer.velocity - linVel;
        Vector2 acceleration = steeringForce / _rigidbody.mass;

        _rigidbody.velocity = linVel + (acceleration * Time.deltaTime);

        //Angular movement
        _rigidbody.SetRotation(GetOrientationFromVelocity(_rigidbody.velocity)*Mathf.Rad2Deg);

        agentInfo.position = transform.position;
        agentInfo.velocity = _rigidbody.velocity;
    }
}
