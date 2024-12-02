namespace Ecomst.Helpers
{
    public static class StaticData
    {
        public static string GetEmptyImagePath()
        {
            return "files" + Path.DirectorySeparatorChar + "images" + Path.DirectorySeparatorChar + "empty.jpg";
        }

        public static string GetProductImageDir()
        {
            return "files" + Path.DirectorySeparatorChar + "images" + Path.DirectorySeparatorChar + "products";
        }
    }
}
