using System;
using System.Reflection.Metadata;
using CODE_OF_STORY.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CODE_OF_STORY.Scenes;

internal class SettingsScene : Component
{                                           //settings= steuerung| load= audio
    private readonly string[] buttonNames = { "Settings_Button", "Load_Button", "Exit_Button" };
    private readonly string[] buttonNamesColored = { "Settings_col_Button", "Ld_col_Button", "Exit_col_Button" };
    private Texture2D[] btns;
    private Texture2D[] btnsColored;
    private Rectangle[] btnRects;
    private Texture2D menuBackground;
    private Rectangle BgRectangle;


    private MouseState currentMouseState, oldMouseState;
    private Rectangle currentMouseStateRectangle;

    private double timer;
    private const double Delay = 0.3;

    Viewport viewport;

    public SettingsScene()
    {
        btns = new Texture2D[buttonNames.Length];
        btnsColored = new Texture2D[buttonNamesColored.Length];
        btnRects = new Rectangle[buttonNames.Length];
        BgRectangle = new Rectangle();
        timer = 0;
        viewport = Game1._graphics.GraphicsDevice.Viewport;
    } 
    
    private void HandleButtonClick(int buttonIndex)
    {
        switch(buttonIndex)
        {
            case 0:
                Data.currentState = Data.Scenes.Controls;
                Console.WriteLine("Steuerung ausgewählt");
                break;
            case 1:
                Console.WriteLine("Audio ausgewählt");
                break;
            case 2:
                Data.currentState = Data.Scenes.Menu;
                break;      

        }
    }

    internal override void LoadContent(ContentManager Content)
    {
        menuBackground = Content.Load<Texture2D>("Items/bg_desertM");
        BgRectangle = new Rectangle(0, 0, menuBackground.Width * 6, menuBackground.Height * 3);

        int screenWidth = viewport.Width;
        int screenHeight = viewport.Height;

        int buttonHeight = Content.Load<Texture2D>($"Buttons/{buttonNames[0]}").Height / 4;

        for (int i = 0; i < btns.Length; i++)
        {

            btns[i] = Content.Load<Texture2D>($"Buttons/{buttonNames[i]}");
            btnsColored[i] = Content.Load<Texture2D>($"Buttons/ColoredButtons/{buttonNamesColored[i]}");
            
            int xPosition = (screenWidth - btns[i].Width / 4) / 2;
            int yPosition = (screenHeight - (buttonHeight * btns.Length)) / 2 + i * buttonHeight;
            btnRects[i] = new Rectangle(xPosition, yPosition, btns[i].Width / 4, btns[i].Height / 4);

        }
    }

    internal override void Update(GameTime gameTime)
    {
        timer += gameTime.ElapsedGameTime.TotalSeconds;

        if (timer < Delay) 
        return;
        Console.WriteLine("timer: " + timer);
        oldMouseState = currentMouseState;
        currentMouseState = Mouse.GetState();            
        currentMouseStateRectangle = new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1);

        if (currentMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
        {
            for(int i = 0; i < btnRects.Length; i++)
            {
                if(currentMouseStateRectangle.Intersects(btnRects[i]))
                {
                    HandleButtonClick(i);
                    timer = 0;
                    break;
                }
            }
        }
    }
    
    internal override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(menuBackground, BgRectangle, Color.White);
        for(int i = 0; i < btns.Length; i++)
        {
            spriteBatch.Draw(currentMouseStateRectangle.Intersects(btnRects[i]) ? btnsColored[i] : btns[i], btnRects[i], Color.White);
        }
    }
}
