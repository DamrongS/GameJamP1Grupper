using System;
using UnityEngine;

public class OutlineManager : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    
    public Color outlineColor;
    public Color newLookColor;

    public bool changeColor;

    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        newLookColor = Color.white;

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
        Texture2D texture = new Texture2D(copiedTexture.width, copiedTexture.height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Debug.Log(x);
                Debug.Log(y);
                
                Debug.Log(texture);
                
                if (copiedTexture.GetPixel(x, y) == lookColor)
                {
                    texture.SetPixel(x, y, setColor);
                } else
                {
                    texture.SetPixel(x, y, copiedTexture.GetPixel(x, y));
                }
            }
        }
        
        texture.Apply();
        newLookColor = setColor;

        return texture;
    }

    public void UpdatePlayerTexture()
    {
        Texture2D characterTexture2D = CopyTexture2D(gameObject.GetComponent<SpriteRenderer>().sprite.texture, newLookColor, outlineColor);

        _spriteRenderer.sprite = Sprite.Create(characterTexture2D, _spriteRenderer.sprite.rect, new Vector2(0.5f, 0.5f));

        _spriteRenderer.material.mainTexture = characterTexture2D;
        _spriteRenderer.material.shader = Shader.Find("Universal Render Pipeline/2D/Sprite-Unlit-Default");
    }
}
