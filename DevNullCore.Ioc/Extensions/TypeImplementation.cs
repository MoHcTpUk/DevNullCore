using System.Reflection;

namespace DevNullCore.Ioc.Extensions
{
    /// <summary>
    /// Описывается тип и его реализацию
    /// </summary>
    public class TypeImplementation
    {
        /// <summary>
        /// Определение
        /// </summary>
        public TypeInfo Definition { get; set; }
        /// <summary>
        /// Реализация
        /// </summary>
        public TypeInfo Implementation { get; set; }
    }
}