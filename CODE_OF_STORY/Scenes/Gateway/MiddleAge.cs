using System;
//using System.Collections.Generic;
//using System.IO;
using CODE_OF_STORY.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
//using MonoGame.Extended.ViewportAdapters;


namespace CODE_OF_STORY.Scenes.Gateway;

internal class MiddleAge : Component
{

    TiledMap _tilemap;
    TiledMapRenderer _tileMapRenderer;

    GraphicsDevice graphicsDevice;
    private float xPosition;
    private float yPosition;
    private Vector2 transformedPlayerPosition;
    private Vector2 transformedMapPosition;

    private Matrix _translation;

    SoundEffect backgroundSoundScenario1;
    SoundEffectInstance backgroundSoundScenario1Instance;

    public MiddleAge()
    {

        graphicsDevice = Game1._graphics.GraphicsDevice;
        xPosition = Game1._graphics.PreferredBackBufferWidth;
        yPosition = Game1._graphics.PreferredBackBufferHeight;

    }

    internal override void LoadContent(ContentManager Content)
    {
        _tilemap = Content.Load<TiledMap>("Maps/Castle_map");
        _tileMapRenderer = new TiledMapRenderer(graphicsDevice, _tilemap);

        backgroundSoundScenario1 = Content.Load<SoundEffect>("Audio/10-Fight");
        backgroundSoundScenario1Instance = backgroundSoundScenario1.CreateInstance();
    }


    internal override void Update(GameTime gameTime)
    {
        _tileMapRenderer.Update(gameTime);
        /* CalculateTranslation();

        transformedPlayerPosition = Vector2.Transform(player.Position, _translation);
        transformedMapPosition = Vector2.Transform(Vector2.Zero, _translation); */

        backgroundSoundScenario1Instance.Play();
        backgroundSoundScenario1Instance.Volume = 0.03f;
    }

    /*  public void CalculateTranslation()
     {
         var dx = (xPosition / 2) - player.Position.X;
         dx = MathHelper.Clamp(dx, _tilemap.Width + xPosition / 2, _tilemap.Width / 2);
         var dy = (yPosition / 2) - player.Position.Y;
         dy = MathHelper.Clamp(dy, -_tilemap.Width + yPosition, _tilemap.Height / 2);
         _translation = Matrix.CreateTranslation(dx, dy, 0f);
     } */

    internal override void Draw(SpriteBatch spriteBatch)
    {

        _tileMapRenderer.Draw(/* _translation */);
        //player.Draw(spriteBatch, transformedPlayerPosition);
    }
}
