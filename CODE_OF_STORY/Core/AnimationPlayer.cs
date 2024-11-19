using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CODE_OF_STORY.Core;

/* Implements playback of 
the animation stored by Animation.
*/
public class AnimationPlayer
{
    private readonly Texture2D spriteSheet;
    private readonly int frameWidth;
    private readonly int frameHeight;
    private int currentFrame;
    private readonly int totalFrames;
    private float frameTime;
    private float timer;
    private readonly bool playOnce;
    public bool IsFinished {get; private set; } = false;

    public AnimationPlayer(Texture2D texture, int frameCount, float animationSpeed, bool playOnce)
    {
        spriteSheet = texture;
        frameWidth = texture.Width / frameCount;
        frameHeight = texture.Height;
        totalFrames = frameCount;
        frameTime = animationSpeed;
        currentFrame = 0;
        timer = 0f;
        IsFinished = false;
        this.playOnce = playOnce;
    }

    public void Reset()
    {
        currentFrame = 0;
        timer = 0f;
        IsFinished = false;
    }
    public void Update(GameTime gameTime)
    {
        if(playOnce && IsFinished) return;

        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if(timer >= frameTime)
        {
            currentFrame++;
            
            if(currentFrame >= totalFrames)
            {
                if(playOnce)
                {
                    currentFrame = totalFrames - 1;
                    IsFinished = true;
                }
            
                else
                {
                    currentFrame = 0;
                }
            }
            timer = 0f;
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects effects)
    {
        Rectangle sourceRectangle = new Rectangle(frameWidth * currentFrame, 0, frameWidth, frameHeight);
        spriteBatch.Draw(spriteSheet, position, sourceRectangle, Color.White, 0f, Vector2.Zero, 1f, effects, 0f);
    }
}
