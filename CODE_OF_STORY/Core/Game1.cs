using CODE_OF_STORY.Managers;
using CODE_OF_STORY.Scenes.Gateway;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CODE_OF_STORY.Core;

/* Implements major game components 
such as content and level loading, 
HUD management and display, and game 
object updating. That is also the "Game1" class. */
public class Game1 : Game
{
    public static GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameStateManager gameStateManager;
    private GatewaysManager gatewaysManager;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = Data.screenW;
        _graphics.PreferredBackBufferHeight = Data.screenH;

        _graphics.ApplyChanges();

        gameStateManager = new GameStateManager();
        gatewaysManager = new GatewaysManager();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        gameStateManager.LoadContent(Content);

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (Data.Exit)
            Exit();

        gameStateManager.Update(gameTime);
        if (Data.currentState == Data.Scenes.StoneAge || Data.currentState == Data.Scenes.MiddleAge || Data.currentState == Data.Scenes.ModernAge || Data.currentState == Data.Scenes.Future)
            gatewaysManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        gameStateManager.Draw(_spriteBatch);
        if (Data.currentState == Data.Scenes.StoneAge || Data.currentState == Data.Scenes.MiddleAge || Data.currentState == Data.Scenes.ModernAge || Data.currentState == Data.Scenes.Future)
            gatewaysManager.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
