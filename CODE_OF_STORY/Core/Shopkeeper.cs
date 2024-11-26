using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CODE_OF_STORY.Core;

public class Shopkeeper
{
    private float sightRange = 200;
    private bool greeting;
    private Rectangle sightRect;
    public Vector2 position;
    private readonly AnimationPlayer shIdleAnimation;
    private readonly AnimationPlayer shDialogueAnimation;
    private readonly AnimationPlayer shApprovalAnimation;
    private readonly AnimationPlayer shGreetingAnimation;
    private readonly Texture2D shIdleTexture;

    

    public Shopkeeper(Vector2 idelPosition, Texture2D shIdleTexture, Texture2D shDialogueTexture, Texture2D shGreetingTexture, Texture2D shApprovalTexture)
    {
        this.shIdleTexture = shIdleTexture;
        shDialogueAnimation = new AnimationPlayer(shDialogueTexture, frameCount: 6, animationSpeed: 0.1f, playOnce: false);
        shIdleAnimation = new AnimationPlayer(shIdleTexture, frameCount: 6, animationSpeed: 0.1f, playOnce: false);
        shGreetingAnimation = new AnimationPlayer(shGreetingTexture, frameCount: 11, animationSpeed: 0.2f, playOnce: false);
        shApprovalAnimation = new AnimationPlayer(shApprovalTexture, frameCount: 4, animationSpeed: 0.2f, playOnce: true);
        this.position = idelPosition;
    }
    public void Greeting(Player player)
    {
        sightRect = new Rectangle((int)position.X - (int) sightRange /2, (int)position.Y, (int)sightRange, 128);
        if(sightRect.Contains(player.Position))
        {
            greeting = true;
        }
        else
        {
            greeting = false;
        }
        
    }

    public void Update(GameTime gameTime, Player player)
    {
        Greeting(player);
        if(greeting)
        {
            shGreetingAnimation.Update(gameTime);
        }
        else
        {
            shIdleAnimation.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if(greeting)
        {
            shGreetingAnimation.Draw(spriteBatch, position, SpriteEffects.None);
        }
        else
        {
            shIdleAnimation.Draw(spriteBatch, position, SpriteEffects.None);
        }
    }
}
