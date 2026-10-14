using System;
using UnityEngine;

public class OutlineManager : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public Texture2D[] _spriteSheet;

    public Color[] outlineColor;
    public Color[] newLookColor;

    public bool changeColor;

    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        changeColor = true;
    }

    private void Update()
    {
        if (changeColor)
        {
            changeColor = false;
            UpdatePlayerTexture();
        }
    }

    public Texture2D CopyTexture2D(Texture2D copiedTexture, Color lookColor, Color setColor)
    {
        //Texture2D texture = new Texture2D(copiedTexture.width, copiedTexture.height);
        Texture2D texture = copiedTexture;
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                if (copiedTexture.GetPixel(x, y) == lookColor)
                {
                    texture.SetPixel(x, y, setColor);
                }
                else
                {
                    texture.SetPixel(x, y, copiedTexture.GetPixel(x, y));
                }
            }
        }

        texture.Apply();

        return texture;
    }

    public void UpdatePlayerTexture()
    {
        for (int i = 0; i < _spriteSheet.Length; i++)
        {
            Texture2D characterTexture2D = CopyTexture2D(_spriteSheet[i], newLookColor[i], outlineColor[i]);
            _spriteSheet[i] = characterTexture2D;
            newLookColor[i] = outlineColor[i];

            _spriteRenderer.sprite = Sprite.Create(characterTexture2D, _spriteRenderer.sprite.rect, new Vector2(0.5f, 0.5f));

            _spriteRenderer.material.mainTexture = characterTexture2D;
            _spriteRenderer.material.shader = Shader.Find("Universal Render Pipeline/2D/Sprite-Unlit-Default");
        }
    }
}
