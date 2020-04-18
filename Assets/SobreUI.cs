using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine .EventSystems ;

[RequireComponent (typeof (GraphicRaycaster )) ]
public class SobreUI : MonoBehaviour 
{
	//Sobre UI
	public bool EnUI()
	{
		if (!this.enabled)
			return false;

		bool OverUI= false;
        //touches
        if (Input.touchSupported)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (OverUI)
                    continue;

                Touch t = Input.GetTouch(i);

                GraphicRaycaster GR = this.GetComponent<GraphicRaycaster>();
                PointerEventData ped = new PointerEventData(null);
                ped.position = t.position;
                List<RaycastResult> result = new List<RaycastResult>();

                GR.Raycast(ped, result);
                if (result.Count == 0 || result == null)
                {
                    OverUI = false;
                }
                else
                {
                    OverUI = true;
                }
            }
        }
        else
        {
            GraphicRaycaster GR = this.GetComponent<GraphicRaycaster>();
            PointerEventData ped = new PointerEventData(null);
            ped.position = Input.mousePosition;
            List<RaycastResult> result = new List<RaycastResult>();

            GR.Raycast(ped, result);
            if (result.Count == 0 || result == null)
            {
                OverUI = false;
            }
            else
            {
                OverUI = true;
            }
        }

		return OverUI;

	}
}
