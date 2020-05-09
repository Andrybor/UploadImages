using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UploadImageAPI.Extensions
{
    public static class ListAsync
    {
        public static async Task<bool> ForEachAsync<T>(this List<T> list, Func<T, Task<bool>> func)
        {
            foreach (var value in list)
            {
                var res = await func(value);
                if (!res)
                    return false;
            }

            return true;
        }
    }
}