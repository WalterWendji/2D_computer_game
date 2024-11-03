using System;
using CODE_OF_STORY.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CODE_OF_STORY.Scenes;

internal class NewGameScene : Component
{
    private Player player;
    internal override void LoadContent(ContentManager Content)
    {
        Texture2D runTexture = Content.Load<Texture2D>("Player_Level1/Warrior_1/Run");
        Texture2D idleTexture = Content.Load<Texture2D>("Player_Level1/Warrior_1/Idle");

        player = new Player(runTexture, idleTexture, new Vector2(100, 600));
    }

    internal override void Update(GameTime gameTime)
    {
        if (player != null)
            player.Update(gameTime);
    }
    internal override void Draw(SpriteBatch spriteBatch)
    {
        player.Draw(spriteBatch);
    }
}
