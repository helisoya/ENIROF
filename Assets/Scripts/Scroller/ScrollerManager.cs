using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollerManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform prefabBlock;
    private Transform[] blocks;

    [Header("Infos")]
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float blockLength;
    [SerializeField] private int maxBlocks;
    private Vector3 direction = new Vector3(-1, 0, 0);

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

    void Update()
    {
        foreach (Transform child in blocks)
        {
            child.position += direction * scrollSpeed * Time.deltaTime;
            if (child.position.x < -blockLength * 2)
            {
                child.position += new Vector3(maxBlocks * blockLength, 0, 0);
            }
        }
    }
}
