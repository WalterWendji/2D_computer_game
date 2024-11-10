using System;
using CODE_OF_STORY.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CODE_OF_STORY.Core;

internal class PausePopupMenu: Component
{
    private string[] buttonNames = {"Continue_Button", "Menu_Button", "Settings_Button", "Exit_Button"};
    private string[] buttonNameColored = {"Continue_col_Button", "Menu_col_Button", "Settings_col_Button", "Exit_col_Button"};
    
    private Texture2D[] buttons;
    private Texture2D[] btnColored;

    private Rectangle[] btnRects;

    private int buttonHeight;
    private int totalHeight;
    private int xPosition;
    private int yPosition;

    private MouseState currentMouseState, oldMouseState;
    private Rectangle currentMouseStateRectangle;
    public PausePopupMenu()
    {
        buttons = new Texture2D[buttonNames.Length];
        btnColored = new Texture2D[buttonNameColored.Length];
        btnRects = new Rectangle[buttonNames.Length];
    }

    internal override void LoadContent(ContentManager Content)
    {
        Viewport viewport = Game1._graphics.GraphicsDevice.Viewport;
        int screenWidth = viewport.Width;
        int screenHeight = viewport.Height;

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = Content.Load<Texture2D>($"Buttons/{buttonNames[i]}");
            buttonHeight = buttons[0].Height/4;
            totalHeight = buttonHeight * buttons.Length;
            xPosition = (screenWidth - (buttons[i].Width/4)) / 2;
            yPosition = (screenHeight - totalHeight) / 2 + i * (buttonHeight + 60);
            btnRects[i] = new Rectangle(xPosition, yPosition, buttons[i].Width / 4, buttons[i].Height / 4);
        }
        for (int i = 0; i < btnColored.Length; i++)
        {
            btnColored[i] = Content.Load<Texture2D>($"Buttons/ColoredButtons/{buttonNameColored[i]}");
            buttonHeight = btnColored[0].Height/4;
            totalHeight = buttonHeight * btnColored.Length*2;
            xPosition = (screenWidth - (btnColored[i].Width / 4)) / 2;
            yPosition = (screenHeight - totalHeight) / 2 + i * (buttonHeight + 60);
            btnRects[i] = new Rectangle(xPosition, yPosition, btnColored[i].Width / 4, btnColored[i].Height / 4);
        }
    }

    internal override void Update(GameTime gameTime)
    {
        oldMouseState = currentMouseState;
        currentMouseState = Mouse.GetState();
        currentMouseStateRectangle = new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1);
        
        if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseStateRectangle.Intersects(btnRects[0]))
            Data.currentGameState = Data.GameState.Playing;
        else if(currentMouseState.LeftButton == ButtonState.Pressed && currentMouseStateRectangle.Intersects(btnRects[1]))
            Data.currentState = Data.Scenes.Menu;
        else if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseStateRectangle.Intersects(btnRects[2]))
            Data.currentState = Data.Scenes.Settings;
        else if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseStateRectangle.Intersects(btnRects[3]))
            Data.Exit = true;
    }
    internal override void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i<buttons.Length; i++)
        {
            spriteBatch.Draw(buttons[i], btnRects[i], Color.White);
            
            if (currentMouseStateRectangle.Intersects(btnRects[i]))
            {
                spriteBatch.Draw(btnColored[i], btnRects[i], Color.White);
            }
        }
    }
}
