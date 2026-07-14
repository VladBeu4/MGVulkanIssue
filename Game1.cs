using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace Project3;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _whiteSquareTexture;
    private RenderTarget2D _accumulationRenderTarget;
    private RenderTarget2D _auxRenderTarget;
    private List<Rectangle> _rectangles;
    private BlendState _maxBlendState;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        Window.AllowUserResizing = true;
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _accumulationRenderTarget = new RenderTarget2D(
            GraphicsDevice,
            400,
            400,
            false,
            SurfaceFormat.Color,
            DepthFormat.None,
            0,
            RenderTargetUsage.PreserveContents);

        _auxRenderTarget = new RenderTarget2D(GraphicsDevice, 600, 600);

        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _whiteSquareTexture = GetWhiteRectangleTexture(GraphicsDevice, 1, 1);

        _rectangles = new List<Rectangle>
        {
            new(0, 0, 200, 200),
            new(150, 150, 200, 200)
        };

        var aaa = new BlendState
        {
            ColorBlendFunction = BlendFunction.Max,
            AlphaBlendFunction = BlendFunction.Max,
        };

        _maxBlendState = new BlendState
        {
            ColorSourceBlend = Blend.One,
            ColorDestinationBlend = Blend.One,
            ColorBlendFunction = BlendFunction.Max,
            AlphaSourceBlend = Blend.One,
            AlphaDestinationBlend = Blend.One,
            AlphaBlendFunction = BlendFunction.Max,
        };
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(_accumulationRenderTarget);
        GraphicsDevice.Clear(Color.Transparent);

        for (int i = 0; i < _rectangles.Count; i++)
        {
            GraphicsDevice.SetRenderTarget(_auxRenderTarget);
            GraphicsDevice.Clear(Color.Transparent);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_whiteSquareTexture, _rectangles[i], Color.White * 0.75f);
            _spriteBatch.End();

            // If this is called, the bug goes away
            /*var fs = new FileStream("test." + i + ".png", FileMode.Create);
            _auxRenderTarget.SaveAsPng(fs, _auxRenderTarget.Width, _auxRenderTarget.Height);
            fs.Close();*/

            GraphicsDevice.SetRenderTarget(_accumulationRenderTarget);

            _spriteBatch.Begin(blendState: _maxBlendState);
            _spriteBatch.Draw(_auxRenderTarget, Vector2.Zero, Color.White * 0.75f);
            _spriteBatch.End();
        }

        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_accumulationRenderTarget, Vector2.Zero, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private static Texture2D GetWhiteRectangleTexture(GraphicsDevice graphicsDevice, int width, int height)
    {
        var texture = new Texture2D(graphicsDevice, width, height);
        var data = new Color[width * height];
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = Color.White;
        }
        texture.SetData(data);
        return texture;
    }
}
