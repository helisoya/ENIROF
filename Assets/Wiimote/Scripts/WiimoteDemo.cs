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
        StartCoroutine(setup());
    }

    IEnumerator setup()
    {
        yield return new WaitForSeconds(0.1f);
        wiimote.SetupIRCamera(IRDataType.BASIC);
    }

    void Update()
    {
        if (!WiimoteManager.HasWiimote()) { return; }

        wiimote = WiimoteManager.Wiimotes[0];
        if(wiimote.Button.b) dot.color = Color.red;
        else dot.color = Color.green;

        wiimote.ReadWiimoteData();

        float[] pointer = wiimote.Ir.GetPointingPosition();
        ir_pointer.anchorMin = new Vector2(pointer[0], pointer[1]);
        ir_pointer.anchorMax = new Vector2(pointer[0], pointer[1]);
    }

    private Vector3 GetAccelVector()
    {
        float accel_x;
        float accel_y;
        float accel_z;

        float[] accel = wiimote.Accel.GetCalibratedAccelData();
        accel_x = accel[0];
        accel_y = -accel[2];
        accel_z = -accel[1];

        return new Vector3(accel_x, accel_y, accel_z).normalized;
    }

    [System.Serializable]
    public class WiimoteModel
    {
        public Transform rot;
        public Renderer a;
        public Renderer b;
        public Renderer one;
        public Renderer two;
        public Renderer d_up;
        public Renderer d_down;
        public Renderer d_left;
        public Renderer d_right;
        public Renderer plus;
        public Renderer minus;
        public Renderer home;
    }

    void OnApplicationQuit()
    {
        if (wiimote != null)
        {
            WiimoteManager.Cleanup(wiimote);
            wiimote = null;
        }
    }
}
