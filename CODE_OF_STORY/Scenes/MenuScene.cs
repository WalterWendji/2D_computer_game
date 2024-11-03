using System;
using CODE_OF_STORY.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CODE_OF_STORY.Scenes;

internal class MenuScene : Component
{

    private string[] buttonNames = { "Play_Button", "Continue_Button", "Load_Button", "Settings_Button", "Exit_Button" };
    private Texture2D[] btns;
    private Rectangle[] btnRects;
    private MouseState currentMouseState, oldMouseState;
    private Rectangle currentMouseStateRectangle;

    public MenuScene()
    {
        btns = new Texture2D[buttonNames.Length];
        btnRects = new Rectangle[buttonNames.Length];
    }
    internal override void LoadContent(ContentManager Content)
    {
        const int INCREMENT_VALUE = 100;
        for (int i = 0; i < btns.Length; i++)
        {
            btns[i] = Content.Load<Texture2D>($"Buttons/{buttonNames[i]}");
            btnRects[i] = new Rectangle(500, 80 + (INCREMENT_VALUE * i), btns[i].Width / 4, btns[i].Height / 4);
            
        }
    }

    internal override void Update(GameTime gameTime)
    {
        oldMouseState = currentMouseState;
        currentMouseState = Mouse.GetState();
        currentMouseStateRectangle = new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1);

        if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseStateRectangle.Intersects(btnRects[0]))
            Data.currentState = Data.Scenes.NewGame;
        else if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseStateRectangle.Intersects(btnRects[4]))
            Data.Exit = true;
    }

    internal override void Draw(SpriteBatch spriteBatch)
    {
        for (int i=0; i<btns.Length; i++)
        {
            spriteBatch.Draw(btns[i], btnRects[i], Color.White);
            if(currentMouseStateRectangle.Intersects(btnRects[i]))
            {
                spriteBatch.Draw(btns[i], btnRects[i], Color.Gray);
            }
        }
    }
}
