using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace CODE_OF_STORY.Core;

public class AnimationItem
{
    private readonly Texture2D spriteSheet;
    private readonly int frameWidth;
    private readonly int frameHeight;
    private int currentFrame;
    private readonly int totalFrames;
    private readonly float frameTime;
    private float timer;

    public AnimationItem(Texture2D texture, int frameCount, float animationSpeed)
    {
        spriteSheet = texture;
        frameWidth = texture.Width / frameCount;
        frameHeight = texture.Height;
        totalFrames = frameCount;
        frameTime = animationSpeed;
        currentFrame = 0;
        timer = 0f;
    }

    public void Update(GameTime gameTime)
    {
        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if(timer >= frameTime)
        {
            currentFrame = (currentFrame + 1) % totalFrames;
            timer = 0f;
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        Rectangle sourceRectangle = new Rectangle(frameWidth * currentFrame, 0, frameWidth, frameHeight);
        spriteBatch.Draw(spriteSheet, position, sourceRectangle, Color.White);
    }
}

