using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float distance = 8;
    public Vector3 angle = new Vector3(30, -135, 0);
    public Transform targetObject;
    public bool VisibleWalls;

    [Tooltip("Front left from default camera angles perspective")]
    public GameObject frontLeft;
    [Tooltip("Front right from default camera angles perspective")]
    public GameObject frontRight;
    [Tooltip("Back left from default camera angles perspective")]
    public GameObject backLeft;
    [Tooltip("Back right from default camera angles perspective")]
    public GameObject backRight;

    private GameObject[,] wallPairs;
    private Vector3[] viewDirs;

    void Start()
    {
        if (VisibleWalls)
        {
            wallPairs = new GameObject[4, 2]
            {{frontLeft, frontRight}, {frontLeft, backLeft}, {backLeft, backRight}, {backRight, frontRight}};
            viewDirs = new Vector3[]
            {
                new Vector3(0.5F, 0, 0.5F), new Vector3(0.5F, 0, -0.5F), new Vector3(-0.5F, 0, -0.5F),
                new Vector3(-0.5F, 0, 0.5F)
            };
        }
    }
	void Update ()
	{
	    angle.y += Input.GetKey(KeyCode.A) ? 1 : Input.GetKey(KeyCode.D) ? -1 : 0;
	    transform.eulerAngles = angle;
        transform.position = targetObject.position + (transform.rotation * Vector3.back * distance);
	    if (VisibleWalls)
	    {
	        SetWalls(true);
	        int dir = CheckForWalls();
	        wallPairs[dir, 0].SetActive(false);
	        wallPairs[dir, 1].SetActive(false);
	    }
	}

    int CheckForWalls()
    {
        float bestDot = 0;
        int bestDotI = -1;
        for(int i = 0; i < viewDirs.Length; i++)
        {
            float dot = Vector3.Dot(-transform.forward.normalized, viewDirs[i]);
            if (dot > bestDot)
            {
                bestDot = dot;
                bestDotI = i;
            }
        }
        return bestDotI;
    }

    void SetWalls(bool state)
    {
        frontLeft.SetActive(state);
        frontRight.SetActive(state);
        backLeft.SetActive(state);
        backRight.SetActive(state);
    }
}
