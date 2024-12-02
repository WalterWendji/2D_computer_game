using System;
using Microsoft.Xna.Framework.Graphics;

namespace CODE_OF_STORY.Core;

public class ShopItem
{
    public string Name {get;}
    public Texture2D Image {get;}
    public int Price {get;}
    
    public ShopItem(string name, Texture2D image, int price)
    {
        Name = name;
        Image = image;
        Price = price;
    }
}
