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
    private string[] buttonNamesColored ={"Play_col_Button", "Continue_col_Button", "Ld_col_Button", "Settings_col_Button", "Exit_col_Button"};
    private Texture2D[] btns;
    private Texture2D[] btnsColored;
    private Rectangle[] btnRects;
    private Texture2D menuBackground;
    private Rectangle BgRectangle;
    private MouseState currentMouseState, oldMouseState;
    private Rectangle currentMouseStateRectangle;

    public MenuScene()
    {
        btns = new Texture2D[buttonNames.Length];
        btnsColored = new Texture2D[buttonNamesColored.Length];
        btnRects = new Rectangle[buttonNames.Length];
        BgRectangle = new Rectangle();
    }

    internal override void LoadContent(ContentManager Content)
    {
       menuBackground = Content.Load<Texture2D>("Items/bg_desertM");
       BgRectangle = new Rectangle (0, 0, menuBackground.Width*6, menuBackground.Height*3);
        const int INCREMENT_VALUE = 100;
        for (int i = 0; i < btns.Length; i++)
        {
            btns[i] = Content.Load<Texture2D>($"Buttons/{buttonNames[i]}");
            btnRects[i] = new Rectangle(700, 300 + (INCREMENT_VALUE * i), btns[i].Width / 4, btns[i].Height / 4);
            
        }
        for (int i = 0; i<btnsColored.Length; i++)
        {
            btnsColored[i] = Content.Load<Texture2D>($"Buttons/ColoredButtons/{buttonNamesColored[i]}");
            btnRects[i] = new Rectangle(700, 300 + (INCREMENT_VALUE * i), btnsColored[i].Width / 4, btnsColored[i].Height / 4);
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

    public void CalculateButtonPositons ()
    {
        int buttonHeight = btns[0]?.Height?? 0;
        int totalHeight = buttonHeight * btns.Length;
       // int screenWidth = GraphicsDevice.Viewport.Width;
        //int screenHeight = GraphicsDevice.Viewport.Height;

        /* for (int i = 0; i < btns.Length; i++)
        {
            int x = (screenWidth - btns[i].Width) / 2;
            int y = (screenHeight - totalHeight) / 2 + i * buttonHeight;
            btnPositions[i] = new Vector2(x, y);
        } */
    }

    internal override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(menuBackground, BgRectangle, Color.White);
        for (int i=0; i<btns.Length; i++)
        {
            spriteBatch.Draw(btns[i], btnRects[i], Color.White);
            
            //Hovering effect to the button.
            if(currentMouseStateRectangle.Intersects(btnRects[i]))
            {
                spriteBatch.Draw(btnsColored[i], btnRects[i], Color.White);
            }
        }
    }
}
