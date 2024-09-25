using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using WiimoteApi;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private RectTransform pointer;
    [SerializeField] private float fireLength;
    private float fireStart;
    private Image sprite;
    private Wiimote wiimote;
    private bool fired;

    // Start is called before the first frame update
    void Awake()
    {
        sprite = pointer.GetComponent<Image>();
    }
    void Start()
    {
        if (WiimoteManager.Wiimotes.Count > 0)
        {
            wiimote = WiimoteManager.Wiimotes[0];
        }
    }
    void Update()
    {
        if (fired && Time.time - fireStart >= fireLength)
        {
            fired = false;
            sprite.color = Color.green;
            if (wiimote != null)
            {
                wiimote.RumbleOn = false;
                wiimote.SendPlayerLED(true, false, false, false);
            }
        }

        if (wiimote != null)
        {
            wiimote.ReadWiimoteData();
            float[] pointerCoo = wiimote.Ir.GetPointingPosition();
            SetPointerPosition(new Vector2(pointerCoo[0], pointerCoo[1]));

            if (wiimote.Button.b && !fired)
            {
                Fire();
            }
        }


    }

    void OnPointer(InputValue inputValue)
    {
        if (wiimote != null) return;

        Vector2 vec = inputValue.Get<Vector2>();
        vec.x /= Screen.width;
        vec.y /= Screen.height;

        SetPointerPosition(vec);
    }

    void SetPointerPosition(Vector2 position)
    {
        pointer.anchorMin = new Vector2(position.x, position.y);
        pointer.anchorMax = new Vector2(position.x, position.y);
    }

    void OnFire(InputValue inputValue)
    {
        if (wiimote != null || fired) return;

        Fire();
    }

    void Fire()
    {
        if (wiimote != null)
        {
            wiimote.RumbleOn = true;
            wiimote.SendPlayerLED(true, false, false, false);
        }

        sprite.color = Color.red;
        fireStart = Time.time;
        fired = true;
    }
}
