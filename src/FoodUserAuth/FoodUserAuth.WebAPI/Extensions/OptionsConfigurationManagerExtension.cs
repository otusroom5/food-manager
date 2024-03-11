namespace FoodUserAuth.WebApi.Extensions
{
    public static class OptionsConfigurationManagerExtension
    {
        /// <summary>
        /// This method returns section of confiuration with name of generic type
        /// </summary>
        /// <returns>IConfigurationSection</returns>
        /// 
        public static IConfigurationSection GetSection<T>(this ConfigurationManager manager)
        {
            return manager.GetSection(nameof(T));
        }

        /// <summary>
        /// This method returns options, if section is not defined then return instace of new created generic class type
        /// </summary>
        /// <returns>T</returns>
        /// 
        public static T GetOptionsOrCreateDefaults<T>(this ConfigurationManager manager)
        {
            return manager.GetSection<T>().Get<T>() ?? Activator.CreateInstance<T>();
        }
    }
}
