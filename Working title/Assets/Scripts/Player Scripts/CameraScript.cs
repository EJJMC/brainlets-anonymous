using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    //Source for this Code is //https://www.youtube.com/watch?v=blO039OzUZc&t=1247s

    [SerializeField] int Smoothing, Sensitivity, negClamp, posClamp;
    Vector2 MousePos, Smooth;

    GameObject Player;

    private void Start()
    {
        Player = this.transform.parent.gameObject;
    }
    void Update()
    {
        var MouseDirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        MouseDirection = Vector2.Scale(MouseDirection,new Vector2(Smoothing * Sensitivity, Smoothing * Sensitivity));
        Smooth.x = Mathf.Lerp(Smooth.x, MouseDirection.x, 1f / Smoothing);
        Smooth.y = Mathf.Lerp(Smooth.y, MouseDirection.y, 1f / Smoothing);

        MousePos.y = Mathf.Clamp(MousePos.y, -negClamp, posClamp);

        MousePos += Smooth;

       transform.localRotation = Quaternion.AngleAxis(-MousePos.y, Vector3.right);
        Player.transform.localRotation = Quaternion.AngleAxis(MousePos.x, Player.transform.up);
    }
}
