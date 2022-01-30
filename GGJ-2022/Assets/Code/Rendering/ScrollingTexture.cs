using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ScrollingTexture : MonoBehaviour
{
    public float _scrollSpeed = 0.5f;
    float offset = 0f;

    SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        offset += (Time.deltaTime * _scrollSpeed) / 10f;
        // SpriteRenderer doesn't support texture offset lmao
        _renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
