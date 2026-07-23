using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project3;

public class Game1 : Game
{
    private SpriteBatch _spriteBatch;
    private RenderTarget2D _renderTarget;
    private Texture2D _blackTexture;

    private BlendState _eraseBlendState;

    public Game1()
    {
        _ = new GraphicsDeviceManager(this);
    }

    protected override void Initialize()
    {
        _renderTarget = new RenderTarget2D(
            graphicsDevice: GraphicsDevice,
            width: 200,
            height: 200,
            mipMap: false,
            preferredFormat: SurfaceFormat.Color,
            preferredDepthFormat: DepthFormat.None,
            preferredMultiSampleCount: 0,
            usage: RenderTargetUsage.PreserveContents
        );

        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _blackTexture = CreateBlackTexture();

        _eraseBlendState = new BlendState
        {
            ColorBlendFunction = BlendFunction.Add,
            ColorSourceBlend = Blend.Zero,
            ColorDestinationBlend = Blend.Zero,

            AlphaBlendFunction = BlendFunction.Add,
            AlphaSourceBlend = Blend.Zero,
            AlphaDestinationBlend = Blend.Zero
        };

        GraphicsDevice.SetRenderTarget(_renderTarget);
        GraphicsDevice.Clear(Color.Red);
    }

    int frame = 0;

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(_renderTarget);

        var destRectangle = new Rectangle(frame, frame, 20, 20);

        frame++;

        _spriteBatch.Begin(blendState: _eraseBlendState);
        _spriteBatch.Draw(_blackTexture, destRectangle, Color.White);
        _spriteBatch.End();

        var data = new Color[200 * 200];
        _renderTarget.GetData(data);

        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_renderTarget, Vector2.Zero, Color.White);
        _spriteBatch.End();

        if (frame == 200)
        {
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.Red);
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            frame = 0;
        }

        base.Draw(gameTime);
    }

    private Texture2D CreateBlackTexture()
    {
        var texture = new Texture2D(GraphicsDevice, 1, 1);
        texture.SetData(new[] { Color.Black });
        return texture;
    }
}
