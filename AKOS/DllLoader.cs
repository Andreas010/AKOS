using System;
using System.Collections.Generic;
using System.Reflection;

namespace Andy.AKOS
{
    public class DllLoader
    {
        private List<DllLoadedItem> loadedDlls;

        public void Initialise()
        {
            loadedDlls = new List<DllLoadedItem>();
        }

        public void LoadDll(string path, string name)
        {
            if (HasDll(name))
            {
                AKOS.Current.logger.Log($"DLL_LOADER: DOES ALREADY EXISTS; LOAD -> {name}", Logger.LogLevel.Warning);
                return;
            }

            Assembly DLL = null;

            try
            {
                DLL = Assembly.LoadFile(path);
            } catch
            {
                AKOS.Current.logger.Log($"DLL_LOADER: LOAD ERROR; LOAD -> {name}", Logger.LogLevel.Error);
                return;
            }

            foreach (Type type in DLL.GetExportedTypes())
            {
                ILoadableAkosClass newClass = Activator.CreateInstance(type) as ILoadableAkosClass;

                if (newClass != null)
                {
                    loadedDlls.Add(new DllLoadedItem()
                    {
                        name = name,
                        akosDll = newClass
                    });
                    return;
                }
            }

            AKOS.Current.logger.Log($"DLL_LOADER: DOES NOT CONTAIN INTERFACE; LOAD -> {name}", Logger.LogLevel.Warning);
        }

        public void UnloadDll(string name)
        {
            if (!HasDll(name))
            {
                AKOS.Current.logger.Log($"DLL_LOADER: DOES NOT EXISTS; UNLOAD -> {name}", Logger.LogLevel.Warning);
                return;
            }

            for (int i = 0; i < loadedDlls.Count; i++)
            {
                if (loadedDlls[i].name == name)
                {
                    loadedDlls.RemoveAt(i);
                    return;
                }
            }
        }

        public void InitialiseDll(string name)
        {
            if (!HasDll(name))
            {
                AKOS.Current.logger.Log($"DLL_LOADER: DOES NOT EXISTS; INIT -> {name}", Logger.LogLevel.Warning);
                return;
            }

            for (int i = 0; i < loadedDlls.Count; i++)
            {
                if (loadedDlls[i].name == name)
                {
                    loadedDlls[i].akosDll.Initialise(AKOS.Current);
                }
            }
        }

        public void RunDll(string name, object[] args)
        {
            if (!HasDll(name))
            {
                AKOS.Current.logger.Log($"DLL_LOADER: DOES NOT EXISTS; RUN -> {name}", Logger.LogLevel.Warning);
                return;
            }

            for (int i = 0; i < loadedDlls.Count; i++)
            {
                if (loadedDlls[i].name == name)
                {
                    loadedDlls[i].akosDll.Run(AKOS.Current, args);
                }
            }
        }

        public bool HasDll(string name)
        {
            for (int i = 0; i < loadedDlls.Count; i++)
                if (loadedDlls[i].name == name)
                    return true;
            return false;
        }

        public struct DllLoadedItem
        {
            public string name;
            public ILoadableAkosClass akosDll;
        }
    }

    public interface ILoadableAkosClass
    {
        public void Initialise(AKOS akos);
        public void Run(AKOS akos, object[] args);
    }
}
