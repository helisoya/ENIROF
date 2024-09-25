using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollerManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RoadBlock prefabBlock;
    private RoadBlock[] blocks;

    [Header("Infos")]
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float blockLength;
    [SerializeField] private int maxBlocks;
    [SerializeField] private float curveStrength;
    private Vector3 direction = new Vector3(-1, 0, 0);
    private int lastIdx = 0;

    void Start()
    {
        blocks = new RoadBlock[maxBlocks];

        float position = -blockLength;
        for (int i = 0; i < maxBlocks; i++)
        {
            blocks[i] = Instantiate(prefabBlock, new Vector3(position, 0, 0), Quaternion.identity, transform);
            blocks[i].gameObject.SetActive(true);
            position += blockLength;
        }
    }

    void Update()
    {
        RoadBlock child;
        for (int i = 0; i < blocks.Length; i++)
        {
            child = blocks[i];
            child.transform.position += direction * scrollSpeed * Time.deltaTime;
            if (child.transform.position.x < -blockLength * 2)
            {
                child.transform.position += new Vector3(maxBlocks * blockLength, 0, 0);
            }

            int number = (int)(child.transform.position.x / 200);

            if (number > 0)
            {
                child.SetCurveValue(number / (float)maxBlocks * curveStrength);
            }

        }
    }

}

