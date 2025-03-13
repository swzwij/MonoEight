using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

public class Main : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private RenderTarget2D _renderTarget;

    private GameStateManager _stateManager;

    public Main()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
    }

    protected override void Initialize()
    {
        ContentLoader.Initialize(Content);
        GameWindow.Initialize(_graphics, true);
        Camera.Initialize();

        _stateManager = GameStateManager.Instance;

        _stateManager.AddState("Loading", new LoadingState());
        _stateManager.AddState("Title", new TitleState());
        _stateManager.AddState("Game", new GameplayState());

        _stateManager.ChangeState("Loading");

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _renderTarget = new RenderTarget2D(GraphicsDevice, GameWindow.Width, GameWindow.Height);
    }

    protected override void Update(GameTime gameTime)
    {
        Input.Update();

        if (Input.IsKeyPressed(Keys.OemPlus))
            GameWindow.Scale++;

        if (Input.IsKeyPressed(Keys.OemMinus))
            GameWindow.Scale--;

        if (Input.IsKeyPressed(Keys.F11))
            GameWindow.ToggleFullscreen();

        if (Input.IsBackPressed && _stateManager.CurrentStateName == "Title")
            Exit();

        _stateManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(_renderTarget);
        GraphicsDevice.Clear(Camera.BackgroundColor);

        _spriteBatch.Begin(transformMatrix: Camera.Transform, samplerState: SamplerState.PointClamp);
        _stateManager.Draw(_spriteBatch);
        _spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        Rectangle targetRect = GameWindow.IsFullscreen
            ? GraphicsHelper.CalculateFullscreenRect(GraphicsDevice)
            : new Rectangle(0, 0, GameWindow.ScaledWidth, GameWindow.ScaledHeight);

        _spriteBatch.Draw(_renderTarget, targetRect, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}