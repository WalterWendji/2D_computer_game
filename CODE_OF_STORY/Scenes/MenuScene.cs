using System;
using CODE_OF_STORY.Core;
using CODE_OF_STORY.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CODE_OF_STORY.Scenes;


internal class MenuScene : Component
{
    private string[] buttonNames = { "Play_Button", "Continue_Button", "Load_Button", "Settings_Button", "Exit_Button" };
    private string[] buttonNamesColored = { "Play_col_Button", "Continue_col_Button", "Ld_col_Button", "Settings_col_Button", "Exit_col_Button" };
    private Texture2D[] btns;
    private Texture2D[] btnsColored;
    private Rectangle[] btnRects;
    private Texture2D menuBackground;
    private Rectangle BgRectangle;


    private MouseState currentMouseState, oldMouseState;
    private Rectangle currentMouseStateRectangle;

    private int buttonHeight;
    private int totalHeight;
    private int xPosition;
    private int yPosition;
    private double timer;
    private double delay;

    public MenuScene()
    {
        btns = new Texture2D[buttonNames.Length];
        btnsColored = new Texture2D[buttonNamesColored.Length];
        btnRects = new Rectangle[buttonNames.Length];
        BgRectangle = new Rectangle();
        timer = 0;
        delay = 0.3;
    }

    internal override void LoadContent(ContentManager Content)
    {
        menuBackground = Content.Load<Texture2D>("Items/bg_desertM");
        BgRectangle = new Rectangle(0, 0, menuBackground.Width * 6, menuBackground.Height * 3);

        Viewport viewport = Game1._graphics.GraphicsDevice.Viewport;
        int screenWidth = viewport.Width;
        int screenHeight = viewport.Height;

        for (int i = 0; i < btns.Length; i++)
        {

            btns[i] = Content.Load<Texture2D>($"Buttons/{buttonNames[i]}");
            buttonHeight = btns[0].Height / 4;
            totalHeight = buttonHeight * btns.Length;
            xPosition = (screenWidth - (btns[i].Width / 4)) / 2;
            yPosition = (screenHeight - totalHeight) / 2 + i * buttonHeight;
            btnRects[i] = new Rectangle(xPosition, yPosition, btns[i].Width / 4, btns[i].Height / 4);

        }
        for (int i = 0; i < btnsColored.Length; i++)
        {
            btnsColored[i] = Content.Load<Texture2D>($"Buttons/ColoredButtons/{buttonNamesColored[i]}");
            buttonHeight = btnsColored[0].Height / 4;
            totalHeight = buttonHeight * btns.Length * 2;
            xPosition = (screenWidth - (btnsColored[i].Width / 4)) / 2;
            yPosition = (screenHeight - totalHeight) / 2 + i * (buttonHeight + 60);
            btnRects[i] = new Rectangle(xPosition, yPosition, btnsColored[i].Width / 4, btnsColored[i].Height / 4);
        }
    }

    internal override void Update(GameTime gameTime)
    {
        timer += gameTime.ElapsedGameTime.TotalSeconds;

        if (timer > delay)
        {
            Console.WriteLine("timer: " + timer);
            oldMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            currentMouseStateRectangle = new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1);


            if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseStateRectangle.Intersects(btnRects[0]))
            {
                Data.currentState = Data.Scenes.Gateways;
                timer = 0;
            }
            else if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseStateRectangle.Intersects(btnRects[2]))
            {
                Data.currentState = Data.Scenes.Load;
                timer = 0;
            }
            else if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseStateRectangle.Intersects(btnRects[3]))
            {
                Data.currentState = Data.Scenes.Settings;
                timer = 0;
            }
            else if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseStateRectangle.Intersects(btnRects[4]))
            {
                Data.Exit = true;
                timer = 0;
            }
        }

    }

    internal override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(menuBackground, BgRectangle, Color.White);
        for (int i = 0; i < btns.Length; i++)
        {
            spriteBatch.Draw(btns[i], btnRects[i], Color.White);

            //Hovering effect to the button.
            if (currentMouseStateRectangle.Intersects(btnRects[i]))
            {
                spriteBatch.Draw(btnsColored[i], btnRects[i], Color.White);
            }
        }
    }
}
