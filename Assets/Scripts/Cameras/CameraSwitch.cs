using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {

    public Camera[] Cameras = new Camera[7];

	// Use this for initialization
	void Start ()
    {
        //Cameras[0].enabled = true;
        for (int i = 0; i < Cameras.Length; i++)
        {
            Cameras[i].enabled = false;
        }

        Cameras[Cameras.Length - 1].enabled = true;
        
    }

    // Update is called once per frame
    void Update() {
        // SWITCH FUNCTION
        if (Input.anyKeyDown)
        {
            for (int i = 1; i < Cameras.Length + 1; i++)
            {
                if (Input.GetKeyDown(i.ToString()))
                {
                    for (int j = 0; j < Cameras.Length; j++)
                    {
                        Cameras[j].enabled = false;
                    }
                    Cameras[i-1].enabled = true;
                }
            }
        }
    }

    
}
