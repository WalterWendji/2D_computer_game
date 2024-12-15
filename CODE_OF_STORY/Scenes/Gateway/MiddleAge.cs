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
    /* private Dictionary<Vector2, int> tilemap;
    private List<Rectangle> textureStore; */
    TiledMap _tilemap;
    TiledMapRenderer _tileMapRenderer;
    //SpriteBatch _spriteBatch;
    public MiddleAge()
    {
       // tilemap = LoadMap("CODE_OF_STORY/Castle_tileset_F/Castle_map_ground.csv");
    }
    internal override void LoadContent(ContentManager Content)
    {
        _tilemap = Content.Load<TiledMap>("Maps/Castle_map");
        _tileMapRenderer = new TiledMapRenderer(Game1._graphics.GraphicsDevice, _tilemap);
       // _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

   /*  private Dictionary<Vector2, int> LoadMap(string filepath)
    {
        Dictionary<Vector2, int> result = new();
        StreamReader reader = new(filepath);

        int y = 0;
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] intems = line.Split(',');
            for (int x = 0; x < intems.Length; x++)
            {
                if(int.TryParse(intems[x], out int value))
                {
                    if (value > 0)
                        result[new Vector2(x,y)] = value;
                }
            }
            y++;
        }
        return result;
    } */

    internal override void Update(GameTime gameTime)
    {
        _tileMapRenderer.Update(gameTime);
    }
    internal override void Draw(SpriteBatch spriteBatch)
    {
        _tileMapRenderer.Draw();
    }
}
