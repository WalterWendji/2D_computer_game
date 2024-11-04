using System;
//using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CODE_OF_STORY.Core;

/* Implements a floating gem in the game.
 Gems are collectable by the player, 
 and are worth a set amount of points.
Gems are used to load, draw, and update a gem. */
public class Gem
{
    private Vector2 position;
    private AnimationItem gemAnimation;

public Gem(Texture2D gemTexture, Vector2 postion)
{
    gemAnimation = new AnimationItem(gemTexture, frameCount: 4, animationSpeed: 0.1f);
    this.position = postion;
}

public void Update(GameTime gameTime)
{
    gemAnimation.Update(gameTime);
}

public void Draw(SpriteBatch spriteBatch)
{
    gemAnimation.Draw(spriteBatch, position);
}
}
