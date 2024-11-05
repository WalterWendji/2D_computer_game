using System;
using CODE_OF_STORY.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CODE_OF_STORY.Scenes.Gateway;

internal class StoneAge : Component
{
    private Player player;
    private Enemy enemy;
    private Gem gem;

    internal override void LoadContent(ContentManager Content)
    {
        Texture2D runTexture = Content.Load<Texture2D>("Player_Level1/Warrior_1/Run");
        Texture2D idleTexture = Content.Load<Texture2D>("Player_Level1/Warrior_1/Idle");
        Texture2D enRunTexture = Content.Load<Texture2D>("Player_Level1/Warrior_2/Run");
        Texture2D gemTexture = Content.Load<Texture2D>("Items/Gems/plate32x8");

        player = new Player(runTexture, idleTexture, new Vector2(100, 600));
        enemy = new EnemyCharge(enRunTexture, new Vector2(400, 600), new Vector2(700, 600), 100f, 100f, 100, 300f);
        gem = new Gem(gemTexture, new Vector2(300, 600));

    }

    internal override void Update(GameTime gameTime)
    {
        if (player != null)
        {
            player.Update(gameTime);
            gem.Update(gameTime);
            enemy.Update(gameTime, player.Position);
        }



    }
    internal override void Draw(SpriteBatch spriteBatch)
    {
        if (player != null)
        {
            player.Draw(spriteBatch);
            gem.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
        }

    }
}
