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
    //listen
    private List<ShopItem> items;
    private List<Rectangle> itemBounds;
    private int selectedItemIndex = -1;

    private int itemsPerRow = 4;
    private int itemSpaceingX = 210;
    private int itemSpaceingY = 200;
    private Vector2 startOffset = new Vector2(0, 50);

    public bool isVisible {get; private set;}

    public ShopWindow(Texture2D backgroundTexture, SpriteFont font, Vector2 position, List<ShopItem> items)
    {
        this.backgroundTexture = backgroundTexture;
        this.font = font;
        this.position = position;
        this.items = items;
        isVisible = false;

        itemBounds = new List<Rectangle>();
        for(int i = 0; i < items.Count; i++)
        {
            Vector2 itemPosition = position + new Vector2(20, 50 + i * 100);
            //Vector2 textSize = font.MeasureString(items[i]);
            itemBounds.Add(new Rectangle((int)itemPosition.X, (int)itemPosition.Y, 100, 80) /*(int)textSize.X +100, (int)textSize.Y+80)*/);
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

    private void BuyItem(ShopItem item)
    {
        System.Console.WriteLine($"Bought: {item.Name} für {item.Price} Gold");
    }

    public void Update(GameTime gameTime)
    {
        if(!isVisible) return;
        MouseState mouseState = Mouse.GetState();
        selectedItemIndex = -1;

        for(int i = 0; i < items.Count; i++)
        {
            int row = i / itemsPerRow;
            int column = i % itemsPerRow;
            Vector2 itemPosition = position + startOffset + new Vector2(column * itemSpaceingX, row * itemSpaceingY);
            Rectangle itemRect = new Rectangle((int)itemPosition.X, (int)itemPosition.Y, 200, 170);
            if(itemRect.Contains(mouseState.Position))
            {
                selectedItemIndex = i;
                if(mouseState.LeftButton == ButtonState.Pressed && selectedItemIndex != -1)
                {
                    BuyItem(items[selectedItemIndex]);
                }
                break;
            }
        }
        if(mouseState.RightButton == ButtonState.Pressed)
        {
            Hide();
        }
    }
    

    public void Draw(SpriteBatch spriteBatch)
    {
        if(!isVisible) return;
        spriteBatch.Draw(backgroundTexture, position, Color.White);

        for(int i = 0; i < items.Count; i++)
        {
            int row = i / itemsPerRow;
            int column = i % itemsPerRow;

            Vector2 itemPosition = position + startOffset + new Vector2(column * itemSpaceingX, row * itemSpaceingY);

            if(i == selectedItemIndex)
            {
                //Rectangle highlightRect = new Rectangle((int)itemPosition.X -5, (int)itemPosition.Y -5, items[i].Image.Width + 10, items[i].Image.Height + 10);
                spriteBatch.Draw(items[i].Image, itemPosition, Color.White * (i == selectedItemIndex ? 0.8f : 1f));
            }
            int itemWidth = 200;
            int itemHeight = 170;

            spriteBatch.Draw(items[i].Image, new Rectangle((int)itemPosition.X, (int)itemPosition.Y, itemWidth, itemHeight), Color.White);
            Vector2 namePos = itemPosition + new Vector2(0, itemHeight - 5);
            spriteBatch.DrawString(font, items[i].Name, namePos, Color.Black);
            Vector2 pricePos = namePos + new Vector2(0, 20);
            spriteBatch.DrawString(font, $"{items[i].Price} Gold", pricePos, Color.Gold);
        }
    }
}
