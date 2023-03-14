using System.Collections.Generic;

namespace Mirzipan.Framed.Configurations
{
    public class ConfigurationComparer: IComparer<IConfiguration>
    {
        public static readonly ConfigurationComparer Instance = new();
        
        public int Compare(IConfiguration x, IConfiguration y)
        {
            if (ReferenceEquals(x, y))
            {
                return 0;
            }

            if (ReferenceEquals(null, y))
            {
                return 1;
            }

            if (ReferenceEquals(null, x))
            {
                return -1;
            }

            return -x.Priority.CompareTo(y.Priority);
        }
    }
}