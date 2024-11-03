using CODE_OF_STORY.Managers;
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
    private static GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameStateManager gameStateManager;
    private Player player;

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
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        gameStateManager.LoadContent(Content);

        Texture2D runTexture = Content.Load<Texture2D>("Player_Level1/Warrior_1/Run");
        Texture2D idleTexture = Content.Load<Texture2D>("Player_Level1/Warrior_1/Idle");

        player = new Player(runTexture, idleTexture, new Vector2(100, 600));

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
  
        player.Update(gameTime);

        if  (Data.Exit)
            Exit();
        
        gameStateManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        //gameStateManager.Draw(_spriteBatch);
        player.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
