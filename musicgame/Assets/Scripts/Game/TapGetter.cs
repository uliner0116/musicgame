using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapGetter : MonoBehaviour
{
   /* private bool OnTouchDown()
    {
        bool ret = false;
        if (0 < Input.touchCount)
        {
            for(int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if(touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit raycast_hit = new RaycastHit();
                    if(Physics.Raycast(ray,out raycast_hit))
                    {
                        if(raycast_hit.collider.gameObject == this.gameObject)
                        {
                            ret = true;
                        }
                    }
                }
            }
        }
        return ret;
    }*/

    public Dictionary<GameObject, TouchPhase> OnTouchPhase()
    {
        Dictionary<GameObject, TouchPhase> ret = new Dictionary<GameObject, TouchPhase>();
        if (0 < Input.touchCount)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Debug.Log("touch");
                Touch touch = Input.GetTouch(i);
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit raycast_hit = new RaycastHit();
                if (Physics.Raycast(ray, out raycast_hit))
                {
                    ret.Add(raycast_hit.collider.gameObject, touch.phase);
                }
            }
        }
        return ret;
    }
}
