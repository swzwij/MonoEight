using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight.Core.Sprite;

/// <summary>
/// A <see cref="Component"/> for rendering a <see cref="Texture2D"/> to the screen.
/// </summary>
public class SpriteRenderer : Component
{
    private Texture2D _texture;
    private bool _hasTexture;

    /// <summary>
    /// Gets or sets the texture.
    /// </summary>
    /// <remarks>
    /// Setting this property automatically sets the <see cref="Origin"/> to the center of the new texture.
    /// </remarks>
    public Texture2D Texture
    {
        get => _texture;
        set
        {
            _texture = value;
            Origin = new Vector2((int)(_texture.Width / 2f), (int)(_texture.Height / 2f));
            _hasTexture = true;
        }
    }

    /// <summary>
    /// Gets or sets the rendering offset.
    /// </summary>
    public Point Offset { get; set; } = Point.Zero;
    
    /// <summary>
    /// Gets or sets the color tint applied to the sprite.
    /// </summary>
    /// <remarks>
    /// Default is <see cref="Color.White"/>.
    /// </remarks>
    public Color Color { get; set; } = Color.White;
    
    /// <summary>
    /// Gets or sets the rotation of the sprite in radians.
    /// </summary>
    public float Rotation { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets the origin of the sprite in texture coordinates.
    /// Rotation and scaling occur around this point.
    /// </summary>
    public Vector2 Origin { get; set; } = Vector2.Zero;
    
    /// <summary>
    /// Gets or sets the scale of the sprite.
    /// </summary>
    public float Scale { get; set; } = 1;
    
    /// <summary>
    /// Gets or sets the <see cref="SpriteEffects"/>.
    /// </summary>
    public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
    
    /// <summary>
    /// Gets or sets the sorting depth of the sprite.
    /// </summary>
    public float Layer { get; set; } = 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="SpriteRenderer"/> class with the given texture.
    /// </summary>
    /// <param name="texture">The <see cref="Texture2D"/> to render.</param>
    public SpriteRenderer(Texture2D? texture)
    {
        _texture = texture;
        Texture = texture;
    }

    /// <summary>
    /// Draws the sprite to the screen if a texture is assigned.
    /// </summary>
    /// <param name="spriteBatch"><see cref="SpriteBatch"/></param>
    protected override void Draw(SpriteBatch spriteBatch)
    {
        if (!_hasTexture)
            return;

        spriteBatch.Draw
        (
            _texture,
            (Position.Int() + Offset).Float(),
            null,
            Color,
            Rotation,
            Origin,
            Scale,
            SpriteEffects,
            Layer
        );
    }
}
