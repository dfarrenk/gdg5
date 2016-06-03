using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour {


    [SerializeField]
    protected float RotationSpeed = 1f;

	[SerializeField]
	protected Vector2 LowerRotationBound;

	[SerializeField]
	protected Vector2 UpperRotationBound;

    private bool active = false;
    private Vector2 movement = new Vector3(0,0);
    private Transform translation;
	public Limit XLimit, YLimit;
	private Vector2 midpoint, altMidpoint;
    
	// Use this for initialization
	void Start () {
		
		translation = gameObject.transform;

		XLimit = new Limit (LowerRotationBound.x, UpperRotationBound.x);
		YLimit = new Limit (LowerRotationBound.y, UpperRotationBound.y);

		midpoint = new Vector2 ((BoundNeutralize (XLimit.min) + BoundNeutralize (XLimit.max)) / 2, (BoundNeutralize (YLimit.min) + BoundNeutralize (YLimit.max)) / 2);
		altMidpoint = new Vector2(BoundNeutralize(midpoint.x - 180), BoundNeutralize(midpoint.y - 180));
	}

	public class Limit {
		public float min, max;
		public Limit (float min = 0f, float max = 360f)
		{
			this.min = (min);
			this.max = (max);
		}
	}

	static float BoundNeutralize (float bound)
	{
		while (bound < 0 || bound > 360)
		{
			if (bound < 0)
				bound += 360;
			else if (bound > 360)
				bound -= 360;
		}
		return bound;
	}
	Vector3 rotation;
	// Update is called once per frame
	void Update () {
        active = gameObject.GetComponent<Camera>().enabled;
        if (active)
		{
			movement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * -1) * RotationSpeed;
			translation.Rotate(new Vector3(movement.y, movement.x, 0f));
			rotation = transform.eulerAngles;
			// LIMIT X ROTATION
			/*
			if (RotationLimit.x <= MaxX.x && RotationLimit.x > 180)
				RotationLimit.x = MaxX.x;
			else if (RotationLimit.x >= MaxX.y && RotationLimit.x <= 180)
				RotationLimit.x = MaxX.y;
				*/
			/*
			if (RotationLimit.y <= MaxY.x)
				RotationLimit.y = MaxY.x;
			else if (RotationLimit.y > MaxY.y)
				RotationLimit.y = MaxY.y;
			*/
			// LIMIT Y ROTATION
			/*
			if (RotationLimit.y <= MaxY.x && RotationLimit.y > 180)
				RotationLimit.y = MaxY.x;
			else if (RotationLimit.y >= MaxY.y && RotationLimit.y <= 180)
				RotationLimit.y = MaxY.y;

			*/
			//float cz = transform.eulerAngles.z;
			//transform.Rotate(0, 0, -cz);

			if (XLimit.min < 0) {
				if (rotation.x < BoundNeutralize (XLimit.min) && rotation.x > midpoint.x) {
					rotation.x = XLimit.min;
					// COMPLETE
				} else if (rotation.x > XLimit.max && rotation.x < midpoint.x) {
					rotation.x = XLimit.max;
					// COMPLETE
				}
			} else {
					if (altMidpoint.x < XLimit.min) {
						if (rotation.x < XLimit.min && rotation.x > altMidpoint.x) { 
							rotation.x = XLimit.min;
						} else if (rotation.x > XLimit.max || rotation.x < XLimit.min) {
							rotation.x = XLimit.max;
						}
					} else if (altMidpoint.x > XLimit.max) {
						if (rotation.x > XLimit.max && rotation.x < altMidpoint.x) {
							rotation.x = XLimit.max;
						} else if (rotation.x > XLimit.max || rotation.x < XLimit.min) {
							rotation.x = XLimit.min;
						}
					}
					// COMPLETE
			}

			if (YLimit.min < 0) {
				if (rotation.y < BoundNeutralize (YLimit.min) && rotation.y > midpoint.y) {
					rotation.y = YLimit.min;
					// COMPLETE
				} else if (rotation.y > YLimit.max && rotation.y < midpoint.y) {
					rotation.y = YLimit.max;
					// COMPLETE
				}
			} else {
				if (altMidpoint.y < YLimit.min) {
					if (rotation.y < YLimit.min && rotation.y > altMidpoint.y) { 
						rotation.y = YLimit.min;
					} else if (rotation.y > YLimit.max || rotation.y < YLimit.min) {
						rotation.y = YLimit.max;
					}
				} else if (altMidpoint.y > YLimit.max) {
					if (rotation.y > YLimit.max && rotation.y < altMidpoint.y) {
						rotation.y = YLimit.max;
					} else if (rotation.y > YLimit.max || rotation.y < YLimit.min) {
						rotation.y = YLimit.min;
					}
				}
				// COMPLETE
			}
			/*
			if (rotation.x < XLimit.min && rotation.x < 180)
				rotation.x = XLimit.min;
			else if (rotation.x > XLimit.max && rotation.x >= 180)
				rotation.x = XLimit.max;
			
			if (rotation.y < YLimit.min && rotation.y < 180)
				rotation.y = YLimit.min;
			else if (rotation.y > YLimit.max && rotation.y >= 180)
				rotation.y = YLimit.max;*/
			transform.rotation = Quaternion.Euler (rotation);

			float cz = transform.eulerAngles.z;
			transform.Rotate(0, 0, -cz);
			//  Cancel out Z value
        } else
        {
            movement = new Vector2(0, 0);
        }
	}
}
