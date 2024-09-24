using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WiimoteApi;

public class TestWiimote : MonoBehaviour
{
    private Wiimote wiimote;
    [SerializeField] private RectTransform ir_pointer;


    void Start()
    {
        WiimoteManager.FindWiimotes();
        wiimote = WiimoteManager.Wiimotes[0];
        wiimote.SendPlayerLED(true, false, false, false);
    }


    void Update()
    {
        float[] pointer = wiimote.Ir.GetPointingPosition();
        ir_pointer.anchorMin = new Vector2(pointer[0], pointer[1]);
        ir_pointer.anchorMax = new Vector2(pointer[0], pointer[1]);
    }
}
