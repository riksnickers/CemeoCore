using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Cryptography;

namespace CeMeOCore.Logic.Account
{
    public class Account
    {
        public String EncrypPassword(String password, String username)
        {
            StringBuilder returnVal = new StringBuilder();
            String salt = CreateSalt(username);
            String passwordToHash = MixSaltWithPass(salt, password); 
            byte[] tempSource = ASCIIEncoding.ASCII.GetBytes(passwordToHash) ;
            byte[] tempHash = new SHA384CryptoServiceProvider().ComputeHash( tempSource );

            returnVal.Append(ByteArrayToString(tempHash));

            return returnVal.ToString();
        }

        private String CreateSalt( String usernameToHash )
        {
            StringBuilder returnVal = new StringBuilder();
            string secret = "<3|v|030";
            usernameToHash += secret;
            byte[] tempSource = ASCIIEncoding.ASCII.GetBytes(usernameToHash);
            byte[] tempHash = new SHA256CryptoServiceProvider().ComputeHash(tempSource);

            returnVal.Append( ByteArrayToString(tempHash) );
            
            return returnVal.ToString();
        }

        private string ByteArrayToString(byte[] tempHash)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(tempHash.Length);
            for (i = 0; i < tempHash.Length; i++)
            {
                sOutput.Append(tempHash[i].ToString("x2"));
            }
            return sOutput.ToString();
        }

        private String MixSaltWithPass(String hash, String pass)
        {
            String[] mixed = new String[8];

            for (int i = 0; i < 4; i++)
            {
                int j = pass.Length / 4;
                mixed[i] = pass.Substring(i * j, ((i == 3 && j % 2 != 0) ? j + 1 : j));
                mixed[i+4] = hash.Substring(i * 16, 16);
            }

            StringBuilder returnVal = new StringBuilder();
            returnVal.Append(mixed[6]); //hash part 3
            returnVal.Append(mixed[0]); //pass part 1
            returnVal.Append(mixed[7]); //hash part 4
            returnVal.Append(mixed[2]); //pass part 3
            returnVal.Append(mixed[4]); //hash part 1
            returnVal.Append(mixed[3]); //pass part 4
            returnVal.Append(mixed[5]); //hash part 2
            returnVal.Append(mixed[1]); //pass part 2
            //System.Diagnostics.Debug.WriteLine(returnVal.ToString());
            return returnVal.ToString();
        }
    }
}