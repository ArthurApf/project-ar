using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CameraRail : MonoBehaviour
{
    // Start is called before the first frame update
    public static CameraRail cameraRail = null;

    public List<Transform> points;
    public float speed;

    public Vector3 GetMovementVector()
    {
        Vector3 moveVector = Vector3.zero;
        
        if (points.Count != 0)
            moveVector = Vector3.Normalize(transform.position - points[0].transform.position);
        return GetMovementVector();
    }

    public void ChangePoint()
    {
        points.RemoveAt(0);
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        bool repeat = true;
        while (repeat && points.Count != 0)
        {
            Quaternion originalRotation = transform.rotation;
            Vector3 targetDirection = points[0].position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime / 2, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);

            if (originalRotation != transform.rotation)
                yield return new WaitForEndOfFrame();
            else
                repeat = false;
        }
    }

    void Start()
    {
        cameraRail = this;

        points[0].LookAt(transform.position);
        for (int i = 1; i < points.Count; i++)
        {
            points[i].LookAt(points[i - 1].transform.position);
        }
    }

    void Update()
    {
        if (points.Count != 0)
        {
            float step = speed * Time.deltaTime;
            Vector3 moveVector = Vector3.Normalize(transform.position - points[0].transform.position);
            transform.position = transform.position - moveVector * step;
        }
    }
}
