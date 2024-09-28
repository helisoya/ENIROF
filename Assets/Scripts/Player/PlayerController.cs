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
    private Wiimote wiimote;
    private bool isFiring;

    [Tooltip("Sound effects")]
    [SerializeField] private AudioSource Shoot_audiosource;
    [SerializeField] private AudioClip[] Shoot_Sounds;
    [Range(0.0f,3.0f)] public float volume= 1.0f;    

    private void PlayShootRandomSound(){
        AudioClip Shoot_Sounds = GetRandomClip();
        Shoot_audiosource.PlayOneShot(Shoot_Sounds,volume);
    }

    private AudioClip GetRandomClip(){
        return Shoot_Sounds[UnityEngine.Random.Range(0,Shoot_Sounds.Length)];
        Shoot_audiosource.volume=Random.Range(0.02f,0.05f);
        Shoot_audiosource.pitch=Random.Range(0.9f,1.2f);
        
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
            GameGUI.instance.SetPointerColor(false);
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
        GameGUI.instance.SetPointerPosition(position);
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
            PlayShootRandomSound();

        enemiesManager.ProcessFire(pointer.position);

        GameGUI.instance.SetPointerColor(true);
        fireStart = Time.time;
        isFiring = true;
    }
}
