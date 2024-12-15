using System;
using System.Collections.Generic;
using System.IO;
using CODE_OF_STORY.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
namespace CODE_OF_STORY.Scenes.Gateway;

internal class MiddleAge : Component
{
    
    TiledMap _tilemap;
    TiledMapRenderer _tileMapRenderer;
   
    public MiddleAge()
    {
       
    }
    internal override void LoadContent(ContentManager Content)
    {
        _tilemap = Content.Load<TiledMap>("Maps/Castle_map");
        _tileMapRenderer = new TiledMapRenderer(Game1._graphics.GraphicsDevice, _tilemap);
      
    }


    internal override void Update(GameTime gameTime)
    {
        _tileMapRenderer.Update(gameTime);
    }
    internal override void Draw(SpriteBatch spriteBatch)
    {
        _tileMapRenderer.Draw();
    }
}
