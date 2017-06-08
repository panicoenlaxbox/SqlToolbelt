using System;
using System.Reflection;

namespace SqlToolbelt
{
    public interface ISqlEmbeddedResourceExecutor
    {
        void ExecuteEmbeddedResource(Assembly assembly, string resourceName);
        void ExecuteEmbeddedResources(Assembly assembly, Predicate<string> filter);
    }
}