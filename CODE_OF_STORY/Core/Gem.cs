using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CODE_OF_STORY.Core;

/* Implements a floating gem in the game.
 Gems are collectable by the player, 
 and are worth a set amount of points.
Gems are used to load, draw, and update a gem. */
public class Gem
{
    public Vector2 Position{get; private set;} 
    public int PointValue {get; private set;}
     public bool isCollected { get; private set; }
    private readonly AnimationItem gemAnimation;
    public Rectangle Bounds => new Rectangle(
        (int)Position.X,
        (int)Position.Y,
        gemAnimation.frameWidth,
        gemAnimation.frameHeight);
    //private SpriteFont scoreFont;
    

public Gem(Texture2D gemTexture, Vector2 postion,int pointValue=10)
{
    gemAnimation = new AnimationItem(gemTexture, frameCount: 4, animationSpeed: 0.1f);
    Position = postion;
    PointValue = pointValue;
    


}
 public void Collect()
    {
        this.isCollected = true;
    }

public void Reset() 
    {
        this.isCollected = false;
        this.Position = new Vector2(300, 600); 
    }
public void Update(GameTime gameTime)
{
    
    gemAnimation.Update(gameTime);
}

public void Draw(SpriteBatch spriteBatch)
{
    if (!isCollected)
    {
    gemAnimation.Draw(spriteBatch, Position);
    }

    
}
}
