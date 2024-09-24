using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WiimoteApi;

public class TestWiimote : MonoBehaviour
{
    Wiimote wiimote;
    // Start is called before the first frame update
    void Start()
    {
        WiimoteManager.FindWiimotes();
        wiimote = WiimoteManager.Wiimotes[0];
        wiimote.SendPlayerLED(true, false, false, false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
