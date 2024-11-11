using System;
using CODE_OF_STORY.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CODE_OF_STORY.Scenes;

internal class SettingsScene : Component
{

SpriteFont font1;

    private Vector2 fontpos;
    private SpriteBatch _spriteBatch;
    internal override void LoadContent(ContentManager Content)
    {
         _spriteBatch = new SpriteBatch(Game1._graphics.GraphicsDevice);
        font1 = Content.Load<SpriteFont>("Arial");
        Viewport viewport = Game1._graphics.GraphicsDevice.Viewport;
        fontpos = new Vector2(viewport.Width / 2, viewport.Height / 2);
    }

    internal override void Update(GameTime gameTime)
    {
        
    }
    internal override void Draw(SpriteBatch spriteBatch)
    {
         Game1._graphics.GraphicsDevice.Clear(Color.SpringGreen);

        _spriteBatch.Begin();

        string output = "placeholder for the settings";

        Vector2 FontOrigin = font1.MeasureString(output)/2;

        _spriteBatch.DrawString(font1, output, fontpos, Color.Black, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);

        _spriteBatch.End();
    }
}