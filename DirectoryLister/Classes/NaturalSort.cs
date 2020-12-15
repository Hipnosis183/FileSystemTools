using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DirectoryLister.Classes
{
    // Importing from Windows API. You may want to disable it if running from Mono in Linux.
    public static class SafeNativeMethods
    {
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        public static extern int StrCmpLogicalW(string psz1, string psz2);
    }

    public sealed class NaturalStringComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return SafeNativeMethods.StrCmpLogicalW(x, y);
        }
    }
}