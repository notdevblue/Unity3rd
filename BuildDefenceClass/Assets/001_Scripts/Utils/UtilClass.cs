using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class UtilClass
{
    static private Camera mainCam;

    static public Vector3 GetMouseWorldPos()
    {
        if(mainCam == null)
        {
            mainCam = Camera.main;
        }

        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        return mouseWorldPos;
    }
}
