using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CODE_OF_STORY.Core;

internal class GameOver : Component
{
    private Texture2D gameOverTexture;
    private Texture2D homeIcon;
    private Texture2D gearIcon;
    private Texture2D levelIcon;
    private Texture2D repeatIcon;

    private Rectangle gameOverRectangle;
    private Rectangle homeRectangle;
    private Rectangle gearRectangle;
    private Rectangle levelIconRectangle;
    private Rectangle repeatIconRectangle;

    private MouseState currentMouseState, oldMouseState;
    private Rectangle currentMouseStateRectangle;

    private Vector2 homeButtonPosition;
    private Vector2 gearButtonPosition;
    private Vector2 gameOverPosition;
    private Vector2 levelIconPosition;
    private Vector2 repeatIconPosition;
    private Vector2 scorePositionInGameOver;
    public static bool isResetButtonClicked;

    private int homeButtonMarginLeft;
    private int gearButtonMarginLeft;
    private int homeAndGearButtonMarginButtom;
    private int levelAndRepeatButtonMarginButtom;
    private int repeatIconMarginLeft;
    private int levelIconMarginLeft;
    private int scoreFontMarginRight;
    private int scoreFontMarginTop;


    SpriteFont scoreFontInGameOver;

    public GameOver()
    {
        isResetButtonClicked = false;

        homeButtonMarginLeft = 90;
        homeAndGearButtonMarginButtom = 20;
        gearButtonMarginLeft = 10;

        levelAndRepeatButtonMarginButtom = 80;
        levelIconMarginLeft = 410;
        repeatIconMarginLeft = 200;

        scoreFontMarginRight = 40;
        scoreFontMarginTop = 28;
    }
    internal override void LoadContent(ContentManager Content)
    {
        Viewport viewport = Game1._graphics.GraphicsDevice.Viewport;

        gameOverTexture = Content.Load<Texture2D>("GameUI/GameOver");
        gearIcon = Content.Load<Texture2D>("Buttons/Gear");
        homeIcon = Content.Load<Texture2D>("Buttons/Home");
        levelIcon = Content.Load<Texture2D>("Buttons/LevelDefault");
        repeatIcon = Content.Load<Texture2D>("Buttons/RepeatDefault");

        scoreFontInGameOver = Content.Load<SpriteFont>("Spritefonts/JosefinSans-Regular");

        homeButtonPosition = new Vector2(viewport.Width - homeButtonMarginLeft, viewport.Height - (viewport.Height - homeAndGearButtonMarginButtom));
        gearButtonPosition = new Vector2(gearButtonMarginLeft, viewport.Height - (viewport.Height - homeAndGearButtonMarginButtom));
        gameOverPosition = new Vector2((viewport.Width - gameOverTexture.Width) / 2, (viewport.Height - gameOverTexture.Height) / 2);
        levelIconPosition = new Vector2((viewport.Width - gameOverTexture.Width + (repeatIcon.Width * 2) + levelIconMarginLeft) / 2, (viewport.Height + levelAndRepeatButtonMarginButtom) / 2);
        repeatIconPosition = new Vector2((viewport.Width - gameOverTexture.Width) / 2 + repeatIconMarginLeft, (viewport.Height + levelAndRepeatButtonMarginButtom) / 2);
        scorePositionInGameOver = new Vector2(viewport.Width/2 + scoreFontMarginRight, viewport.Height/2 - scoreFontMarginTop);

        homeRectangle = new Rectangle((int)homeButtonPosition.X, (int)homeButtonPosition.Y, homeIcon.Width, homeIcon.Height);
        gearRectangle = new Rectangle((int)gearButtonPosition.X, (int)gearButtonPosition.Y, gearIcon.Width, gearIcon.Height);
        gameOverRectangle = new Rectangle((int)gameOverPosition.X, (int)gameOverPosition.Y, gameOverTexture.Width, gameOverTexture.Height);
        levelIconRectangle = new Rectangle((int)levelIconPosition.X, (int)levelIconPosition.Y, levelIcon.Width, levelIcon.Height);
        repeatIconRectangle = new Rectangle((int)repeatIconPosition.X, (int)repeatIconPosition.Y, repeatIcon.Width, repeatIcon.Height);
    }

    internal override void Update(GameTime gameTime)
    {
        oldMouseState = currentMouseState;
        currentMouseState = Mouse.GetState();
        currentMouseStateRectangle = new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1);

        if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseStateRectangle.Intersects(homeRectangle))
            Data.currentState = Data.Scenes.Menu;
        else if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseStateRectangle.Intersects(gearRectangle))
            Data.currentState = Data.Scenes.Settings;
        else if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseStateRectangle.Intersects(levelIconRectangle))
            Data.currentState = Data.Scenes.Gateways;
        else if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseStateRectangle.Intersects(repeatIconRectangle))
        {
            isResetButtonClicked = true;
            Data.currentGameState = Data.GameState.Playing;
            Data.currentState = Data.Scenes.StoneAge;
        }

    }
    internal override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(gameOverTexture, gameOverRectangle, Color.White);
        spriteBatch.Draw(homeIcon, homeRectangle, Color.White);
        spriteBatch.Draw(gearIcon, gearRectangle, Color.White);
        spriteBatch.Draw(levelIcon, levelIconRectangle, Color.White);
        spriteBatch.Draw(repeatIcon, repeatIconRectangle, Color.White);

        //TODO: The score should be set to "TBD"
        spriteBatch.DrawString(scoreFontInGameOver, "TBD", scorePositionInGameOver, Color.White);
    }
}
