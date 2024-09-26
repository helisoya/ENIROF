using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollerManager : MonoBehaviour
{
    public static ScrollerManager instance;

    [Header("Components")]
    [SerializeField] private Transform prefabBlock;
    private Transform[] blocks;

    [Header("Infos")]
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float blockLength;
    [SerializeField] private int maxBlocks;
    [SerializeField] private float accelSpeed = 20f;
    private float currentScrollSpeed;
    private Vector3 direction = new Vector3(-1, 0, 0);

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        blocks = new Transform[maxBlocks];

        float position = -blockLength;
        for (int i = 0; i < maxBlocks; i++)
        {
            blocks[i] = Instantiate(prefabBlock, new Vector3(position, 0, 0), Quaternion.identity, transform);
            blocks[i].gameObject.SetActive(true);
            position += blockLength;
        }
    }

    public void SetCurrentSpeed(float percentage)
    {
        currentScrollSpeed = scrollSpeed * percentage;
    }

    public float GetCurrentScrollSpeed()
    {
        return currentScrollSpeed;
    }

    void Update()
    {
        if (currentScrollSpeed <= scrollSpeed)
        {
            currentScrollSpeed += accelSpeed * Time.deltaTime;
        }

        Transform child;
        for (int i = 0; i < blocks.Length; i++)
        {
            child = blocks[i];
            child.transform.position += direction * currentScrollSpeed * Time.deltaTime;
            if (child.transform.position.x < -blockLength * 3)
            {
                child.transform.position += new Vector3(maxBlocks * blockLength, 0, 0);
            }
        }
    }
}

