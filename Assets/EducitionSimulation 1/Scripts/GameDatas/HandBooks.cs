using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class HandBooks : MonoBehaviour
{
    public int DealerCardValue { get; set; }
    
    public int[]  PlayerCardValue{ get; set; }

    public List<Sprite> DealerCardImagesSprites;
    
    public List<Sprite> PlayerCardSprites;

    public HandStatus Handvalue { get; set; }
}

[Serializable]
public enum HandStatus
{
    Stand,
    Split,
    Double,
    Hit
}