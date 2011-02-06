using System;
using System.Collections;
using Microsoft.SPOT;

namespace AnalysisUK.LightSwitch.Utilities
{
    /// <summary>
    /// One heck of a HACK of an IoC Container
    /// </summary>
    public static class IoCContainer
    {
        private static ArrayList _arrayList = new ArrayList();
        
        public static void Store (object toStore)
        {
            _arrayList.Add(toStore);
        }

        /// <summary>
        /// Finds the first instance of the matching type in the stored collection!
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Retrieve(Type type)
        {
            foreach (object stored in _arrayList)
            {
                if (stored.GetType().IsInstanceOfType(type))
                {
                    return stored;
                }
            }
            return null;
        }
    }
}
