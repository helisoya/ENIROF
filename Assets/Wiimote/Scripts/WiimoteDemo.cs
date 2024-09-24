using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using System;
using WiimoteApi;

public class WiimoteDemo : MonoBehaviour
{
    public RectTransform ir_pointer;
    public Image dot;

    private Quaternion initial_rotation;

    private Wiimote wiimote;

    private Vector2 scrollPosition;

    private Vector3 wmpOffset = Vector3.zero;

    void Start()
    {
        WiimoteManager.FindWiimotes();
        wiimote = WiimoteManager.Wiimotes[0];
        wiimote.SetupIRCamera(IRDataType.BASIC);
    }

    IEnumerator setup()
    {
        yield return new WaitForSeconds(0.1f);
    }

    void Update()
    {
        if (!WiimoteManager.HasWiimote()) { return; }
        if (wiimote.Button.b)
        {
            SetRumble(true);
            dot.color = Color.red;
        }
        else
        {
            SetRumble(false);
            dot.color = Color.green;
        }

        wiimote.ReadWiimoteData();

        float[] pointer = wiimote.Ir.GetPointingPosition();
        ir_pointer.anchorMin = new Vector2(pointer[0], pointer[1]);
        ir_pointer.anchorMax = new Vector2(pointer[0], pointer[1]);
    }

    void SetRumble(bool rumble)
    {
        if(rumble != wiimote.RumbleOn)
        {
            wiimote.RumbleOn = rumble;
            wiimote.SendPlayerLED(true, false, false, false);
        }
    }
}
