using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WiimoteApi;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private RectTransform pointer;
    private Image sprite;
    private Wiimote wiimote;
    // Start is called before the first frame update
    void Awake()
    {
        sprite = pointer.GetComponent<Image>();
    }
    void Start()
    {
        WiimoteManager.FindWiimotes();
        wiimote = WiimoteManager.Wiimotes[0];
        wiimote.SetupIRCamera(IRDataType.BASIC);
    }
    void Update()
    {
        if (!WiimoteManager.HasWiimote()) { return; }
        if (wiimote.Button.b)
        {
            SetRumble(true);
            sprite.color = Color.red;
        }
        else
        {
            SetRumble(false);
            sprite.color = Color.green;
        }

        wiimote.ReadWiimoteData();

        float[] pointerCoo = wiimote.Ir.GetPointingPosition();
        pointer.anchorMin = new Vector2(pointerCoo[0], pointerCoo[1]);
        pointer.anchorMax = new Vector2(pointerCoo[0], pointerCoo[1]);
    }

    void SetRumble(bool rumble)
    {
        if (rumble != wiimote.RumbleOn)
        {
            wiimote.RumbleOn = rumble;
            wiimote.SendPlayerLED(true, false, false, false);
        }
    }
}
