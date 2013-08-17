#if !CF
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace AcceptanceServer
{
    public class SessionId
    {
        private static int m_lenght = 4;
        
        public static string CreateNewId()
        {
            RandomNumberGenerator RNG = RandomNumberGenerator.Create();
            byte[] buf = new byte[m_lenght];
            RNG.GetBytes(buf);

            return agsXMPP.util.Hash.HexToString(buf);
        }


    }
}
#endif
