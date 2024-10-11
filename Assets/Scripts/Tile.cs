using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum TileType
{
    Normal,
    Obstacle,
    Breakable,
}

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    public int xIndex;
    public int yIndex;
    public TileType tileType = TileType.Normal;
    Board m_board;
    
    SpriteRenderer m_spriteRenderer;
    
    public int breakableValue = 0;
    public Sprite[] breakableSprites;
    public Color normalColor;

    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(int x, int y, Board board)
    {
        xIndex = x;
        yIndex = y;
        m_board = board;
        
        if(tileType != TileType.Breakable) return;
        
        if(breakableSprites[breakableValue] != null)
        {
            m_spriteRenderer.sprite = breakableSprites[breakableValue];
        }
    }

    private void OnMouseDown()
    {
        if (m_board != null)
        {
            m_board.ClickTile(this);
        }
    }
    
    public void OnMouseEnter()
    {
        if(m_board != null)
        {
            m_board.DragToTile(this);
        }
    }
    
    
    public void OnMouseUp()
    {
        if (m_board != null)
        {
            m_board.ReleaseTile();
        }
    }

    public void BreakTile()
    {
        if (tileType != TileType.Breakable) return;

        StartCoroutine(BreakTileRoutine());
    }

    IEnumerator BreakTileRoutine()
    {
        breakableValue = Mathf.Clamp(--breakableValue, 0, breakableValue);
 
        yield return new WaitForSeconds(0.25f);
        if (breakableSprites[breakableValue] != null)
        {
            m_spriteRenderer.sprite = breakableSprites[breakableValue];
        }
 
        if (breakableValue <= 0)
        {
            tileType = TileType.Normal;
            m_spriteRenderer.color = normalColor;
        }
    }
}
