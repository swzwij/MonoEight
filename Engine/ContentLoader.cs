using Microsoft.Xna.Framework.Content;

namespace MonoEight
{
    public class ContentLoader
    {
        private static ContentManager _content;

        public static void Initialize(ContentManager content)
        {
            _content = content;
        }

        public static T Load<T>(string assetName)
        {
            return _content.Load<T>(assetName);
        }
    }
}