using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using CODE_OF_STORY.Managers;

namespace CODE_OF_STORY.Core;

public class Shopkeeper
{
    private float sightRange = 200;
    private float interactionRange = 50;
    private bool isInteracting;
    private bool greeting;
    private Rectangle sightRect;
    private Rectangle interactionRect;
    public Vector2 position;
    private readonly AnimationPlayer shIdleAnimation;
    private readonly AnimationPlayer shDialogueAnimation;
    private readonly AnimationPlayer shApprovalAnimation;
    private readonly AnimationPlayer shGreetingAnimation;
    private ShopWindow shopWindow;

    

    public Shopkeeper(Vector2 idelPosition, Texture2D shopBackground, SpriteFont font, List<ShopItem> items)
    {
        shDialogueAnimation = new AnimationPlayer(ResourceManager.shDialogueTexture, frameCount: 16, animationSpeed: 0.15f, playOnce: true);
        shIdleAnimation = new AnimationPlayer(ResourceManager.shIdleTexture, frameCount: 6, animationSpeed: 0.1f, playOnce: false);
        shGreetingAnimation = new AnimationPlayer(ResourceManager.shGreetingTexture, frameCount: 11, animationSpeed: 0.2f, playOnce: false);
        shApprovalAnimation = new AnimationPlayer(ResourceManager.shApprovalTexture, frameCount: 4, animationSpeed: 0.2f, playOnce: true);
        this.position = idelPosition;

        shopWindow = new ShopWindow(shopBackground, font, new Vector2(600, 300), items);
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
            isInteracting = false;
        }   
    }

    public void CheckInteraction(Player player, KeyboardState keyboardState, KeyboardState prevKeyboardState)
    {
        interactionRect = new Rectangle((int)position.X - (int) interactionRange /2, (int)position.Y, (int)interactionRange, 128);
        if(interactionRect.Contains(player.Position) && keyboardState.IsKeyDown(Keys.F) && prevKeyboardState.IsKeyUp(Keys.F))
        {
            isInteracting = true;
            shDialogueAnimation.PlayOnce();
        }
    }
    public void Update(GameTime gameTime, Player player, KeyboardState keyboardState, KeyboardState prevKeyboardState)
    {
        Greeting(player);
        CheckInteraction(player, keyboardState, prevKeyboardState);
        
        if(greeting && !isInteracting)
        {
            shGreetingAnimation.Update(gameTime);
        }
        if(isInteracting)
        {
            shDialogueAnimation.Update(gameTime);
            shopWindow.Update(gameTime);
            if(shDialogueAnimation.IsFinished && !shopWindow.isVisible)
            {
                shopWindow.Show();
                isInteracting = false;
            }
        }
        if(!greeting && !isInteracting)
        {
            shIdleAnimation.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch, Player player)
    {
        if(!greeting && !isInteracting)
        {
            shIdleAnimation.Draw(spriteBatch, position, SpriteEffects.None);
        }
        if(greeting && !isInteracting)
        {
            shGreetingAnimation.Draw(spriteBatch, position, SpriteEffects.None);
        }
        if(isInteracting)
        {
            shDialogueAnimation.Draw(spriteBatch, position, SpriteEffects.None);
            shopWindow.Draw(spriteBatch);
        }
    }
}
