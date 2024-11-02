using System;
using CODE_OF_STORY.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CODE_OF_STORY.Scenes;

internal class MenuScene : Component
{

    private string[] buttonNames = { "Play_Button", "Continue_Button", "Load_Button", "Settings_Button", "Exit_Button" };
    private Texture2D[] btns;
    private Rectangle[] btnRects;

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

    }

    internal override void Draw(SpriteBatch spriteBatch)
    {
        for (int i=0; i<btns.Length; i++)
        {
            spriteBatch.Draw(btns[i], btnRects[i], Color.White);
        }
    }
}
