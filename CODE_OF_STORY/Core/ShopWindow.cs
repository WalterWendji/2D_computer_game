using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CODE_OF_STORY.Core;

public class ShopWindow
{
    private Texture2D backgroundTexture;
    private SpriteFont font;
    private Vector2 position;
    private List<string> items;
    private List<Rectangle> itemBounds;
    private int selectedItemIndex = -1;
    public bool isVisible {get; private set;}

    public ShopWindow(Texture2D backgroundTexture, SpriteFont font, Vector2 position, List<string> items)
    {
        this.backgroundTexture = backgroundTexture;
        this.font = font;
        this.position = position;
        this.items = items;
        isVisible = false;

        itemBounds = new List<Rectangle>();
        for(int i = 0; i < items.Count; i++)
        {
            Vector2 itemPosition = position + new Vector2(20, 50 + i * 30);
            Vector2 textSize = font.MeasureString(items[i]);
            itemBounds.Add(new Rectangle((int)itemPosition.X, (int)itemPosition.Y, (int)textSize.X, (int)textSize.Y));
        }
    }

    public void Show()
    {
        isVisible = true;
    }

    public void Hide()
    {
        isVisible = false;
    }

    private void BuyItem(string item)
    {
        System.Console.WriteLine($"Bought: {items}");
    }
    public void Update(GameTime gameTime)
    {
        if(!isVisible) return;
        MouseState mouseState = Mouse.GetState();
        selectedItemIndex = -1;

        for(int i = 0; i < itemBounds.Count; i++)
        {
            if(itemBounds[i].Contains(mouseState.Position))
            {
                selectedItemIndex = i;
                break;
            }
            if(mouseState.LeftButton == ButtonState.Pressed && selectedItemIndex != -1)
            {
                BuyItem(items[selectedItemIndex]);
            }
            if(mouseState.RightButton == ButtonState.Pressed)
            {
                Hide();
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if(!isVisible) return;
        spriteBatch.Draw(backgroundTexture, position, Color.White);
        for(int i = 0; i < items.Count; i++)
        {
            Color color = (i == selectedItemIndex) ? Color.Yellow : Color.White;
            Vector2 itemPosition = position + new Vector2(20, 50 + i * 30);
            spriteBatch.DrawString(font, items[i], itemPosition, color);
        }
    }
}
