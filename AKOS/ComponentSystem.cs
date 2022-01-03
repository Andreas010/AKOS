using System.Collections.Generic;

namespace Andy.AKOS.Component
{
    public class ComponentSystem
    {
        private List<EngineComponent> components;

        public void Initialise()
        {
            components = new List<EngineComponent>();
        }

        public void AddComponent(string componentName, IComponentBase component, bool initialise = true)
        {
            if (!HasComponent(componentName))
            {
                components.Add(new EngineComponent()
                {
                    name = componentName,
                    component = component
                });

                if (initialise)
                    components[^1].component.Initialise();
            }
            else
                AKOS.Current.logger.Log($"COMPONENT_SYSTEM: ALREADY EXISTS; ADD -> {componentName}", Logger.LogLevel.Warning);
        }

        public void InitialiseComponent(string componentName)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].name == componentName)
                {
                    components[i].component.Initialise();
                    return;
                }
            }

            AKOS.Current.logger.Log($"COMPONENT_SYSTEM: DOES NOT EXISTS; INIT -> {componentName}", Logger.LogLevel.Warning);
        }

        public void RunComponent(string componentName, object args)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].name == componentName)
                {
                    components[i].component.Run(args);
                    return;
                }
            }

            AKOS.Current.logger.Log($"COMPONENT_SYSTEM: DOES NOT EXISTS; RUN -> {componentName}", Logger.LogLevel.Warning);
        }

        public void RemoveComponent(string componentName)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].name == componentName)
                {
                    components.RemoveAt(i);
                    return;
                }
            }

            AKOS.Current.logger.Log($"COMPONENT_SYSTEM: DOES NOT EXISTS; REMOVE -> {componentName}", Logger.LogLevel.Warning);
        }

        public bool HasComponent(string componentName)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].name == componentName)
                    return true;
            }

            return false;
        }

        public bool HasComponent(IComponentBase component)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].component == component)
                    return true;
            }

            return false;
        }

        private struct EngineComponent
        {
            public string name;
            public IComponentBase component;
        }
    }

    public interface IComponentBase
    {
        public void Initialise();
        public void Run(object args);
    }
}
