using Andy.AKOS;

namespace AKS
{
    internal class InstanceField
    {
        public static string programPath = System.IO.Directory.GetCurrentDirectory();
    }

    [System.Serializable]
    public class DllAkosPackage
    {
        public string name;
        public string filePath;
        public string version;
    }
}
