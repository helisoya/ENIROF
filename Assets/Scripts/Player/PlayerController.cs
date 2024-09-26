using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using WiimoteApi;
using Color = UnityEngine.Color;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private RectTransform pointer;
    [SerializeField] private float fireLength;
    [SerializeField] private float fireCoolDown;
    [SerializeField] private EnemiesManager enemiesManager;
    private float fireStart;
    private float fireStop;
    private Image sprite;
    private Wiimote wiimote;
    private bool isFiring;
    // Start is called before the first frame update
    void Awake()
    {
        sprite = pointer.GetComponent<Image>();
    }
    void Start()
    {
        WiimoteManager.FindWiimotes();
        if (WiimoteManager.Wiimotes.Count > 0)
        {
            wiimote = WiimoteManager.Wiimotes[0];
            wiimote.SetupIRCamera(IRDataType.BASIC);
        }
    }
    void Update()
    {
        if (isFiring && Time.time - fireStart >= fireLength)
        {
            isFiring = false;
            fireStop = Time.time;
            sprite.color = Color.green;
            if (wiimote != null)
                SetRumble(false);
        }

        if (wiimote != null)
        {
            wiimote.ReadWiimoteData();
            float[] pointerCoo = wiimote.Ir.GetPointingPosition();
            SetPointerPosition(new Vector2(pointerCoo[0], pointerCoo[1]));

            if (wiimote.Button.b && !isFiring && Time.time - fireStop >= fireCoolDown)
                Fire();
        }
    }

    void SetPointerPosition(Vector2 position)
    {
        pointer.anchorMin = position;   
        pointer.anchorMax = position;
    }

    void SetRumble(bool rumble)
    {
        if (rumble == wiimote.RumbleOn) return;

        wiimote.RumbleOn = rumble;
        wiimote.SendPlayerLED(true, false, false, false);
    }

    void OnPointer(InputValue inputValue)
    {
        if (wiimote != null) return;

        Vector2 vec = inputValue.Get<Vector2>();
        vec.x /= Screen.width;
        vec.y /= Screen.height;

        SetPointerPosition(vec);
    }

    void OnFire(InputValue inputValue)
    {
        if (wiimote != null || isFiring) return;

        Fire();
    }

    void Fire()
    {
        if (wiimote != null)
            SetRumble(true);

        enemiesManager.ProcessFire(pointer.position);

        sprite.color = Color.red;
        fireStart = Time.time;
        isFiring = true;
    }
}
