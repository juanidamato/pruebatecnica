using HashidsNet;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekusCore.Application.BLL
{
    public static class ReverseHash
    {
        private static string _salt = "";

        public static void SetSalt(string salt)
        {
            _salt = salt;
        }

        public static string Encode(int dataToEncode)
        {
            Hashids encoder = new Hashids(_salt,8);
            return encoder.Encode(dataToEncode);

        }

        public static int[] Decode(string dataToDecode)
        {
            Hashids decoder = new Hashids(_salt,8);
            return decoder.Decode(dataToDecode);
        }
    }
}
