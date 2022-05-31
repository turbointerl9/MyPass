using System;
using System.Security.Cryptography;
using System.Text;

namespace TemplateApp.Core.Data.Security
{
    public class Cryption
    {
        public struct Hash
        {
            public byte[] salt { get; private set; }
            public byte[] hashedText { get; private set; }
            public string combined { get; private set; }
            public Hash(byte[] hashedText, byte[] salt)
            {
                this.salt = salt;
                this.hashedText = hashedText;
                combined = hashedText.ToBaseString() + ":" + salt.ToBaseString();
            }
            public Hash(string combined)
            {
                this.salt = combined.Split(':')[1].FromBaseString();
                this.hashedText = combined.Split(':')[0].FromBaseString();
                this.combined = combined;
            }
        }
        public struct CryptedLoginData
        {
            public byte[] IV { get; private set; }
            public byte[] hashedName { get; private set; }
            public byte[] hashedPwd { get; private set; }
            public string combined { get; private set; }
            public byte[] hashedType { get; private set; }
            public byte[] hashedDesc { get; private set; }
            public byte[] hashedWebsite { get; private set; }
            public byte[] hashedImgUri { get; private set; }

            public CryptedLoginData(byte[] hashedName, byte[] hashedPwd, byte[] IV, byte[] type, byte[] desc, byte[] website, byte[] imageuri)
            {
                this.hashedName = hashedName;
                this.hashedPwd = hashedPwd;
                this.IV = IV;
                this.hashedType = type;
                this.hashedDesc = desc;
                this.hashedWebsite = website;
                this.hashedImgUri = imageuri;
                combined = $"{hashedName.ToBaseString()}:{hashedPwd.ToBaseString()}:{IV.ToBaseString()}:{type.ToBaseString()}:{desc.ToBaseString()}:{website.ToBaseString()}:{imageuri.ToBaseString()}";
            }
            public CryptedLoginData(string combined)
            {
                this.hashedName = combined.Split(':')[0].FromBaseString();
                this.hashedPwd = combined.Split(':')[1].FromBaseString();
                this.IV = combined.Split(':')[2].FromBaseString();
                this.hashedType = combined.Split(':')[3].FromBaseString();
                this.hashedDesc = combined.Split(':')[4].FromBaseString();
                this.hashedWebsite = combined.Split(':')[5].FromBaseString();
                this.hashedImgUri = combined.Split (':')[6].FromBaseString();
                this.combined = combined;
            }
        }
        public struct LoginData
        {
            public string username { get; private set; }
            public string password { get; private set; }
            public string type { get; private set; }
            public string description { get; private set; }
            public string website { get; private set; }
            public string imageuri { get; private set; }

            public LoginData(string username, string password, string type, string description, string website, string imageuri)
            {
                this.username = username;
                this.password = password;
                this.type = type;
                this.description = description;
                this.website = website;
                this.imageuri = imageuri;
            }
        }
        private static byte[] newSalt()
        {
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[4];
            rng.GetBytes(salt);
            return salt;
        }
        public static Hash generateNew(string text, bool withSalt)
        {
            SHA256 sha = SHA256.Create();
            byte[] pwd = text.ToByteArray();
            byte[] salt = new byte[] {0}; 
            if (withSalt)
            {
                salt = newSalt();
            }
            byte[] toHash = pwd.CombineArrays(salt);
            byte[] hashValue = sha.ComputeHash(toHash);
            return new Hash(hashValue, salt);
        }
        public static bool validate(Hash hash, string password)
        {
            SHA256 sha = SHA256.Create();
            byte[] pwd = password.ToByteArray();
            byte[] salt = hash.salt;
            byte[] toHash = pwd.CombineArrays(salt);
            byte[] hashValue = sha.ComputeHash(toHash);

            return hashValue.EqualsByteArray(hash.hashedText);
        }
        public static CryptedLoginData AES_256_encrypt_Login_Data(LoginData loginData, string pKey)
        {
            byte[] name = loginData.username.ToByteArray();
            byte[] pwd = loginData.password.ToByteArray();
            byte[] typ = loginData.type.ToByteArray();
            byte[] desc = loginData.description.ToByteArray();
            byte[] website = loginData.website.ToByteArray();
            byte[] imageuri = loginData.imageuri.ToByteArray();

            Aes aes = Aes.Create();
            aes.Key = generateNew(pKey, false).hashedText;
            aes.GenerateIV();
            ICryptoTransform ict = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] hashedName = ict.TransformFinalBlock(name, 0, name.Length);
            ict = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] hashedPwd = ict.TransformFinalBlock(pwd, 0, pwd.Length);
            ict = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] hashedType = ict.TransformFinalBlock(typ, 0, typ.Length);
            ict = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] hashedDesc = ict.TransformFinalBlock(desc, 0, desc.Length);
            ict = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] hashedWeb = ict.TransformFinalBlock(website, 0, desc.Length);
            ict = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] hasehdImg = ict.TransformFinalBlock(imageuri, 0, desc.Length);
            return new CryptedLoginData(hashedName, hashedPwd, aes.IV, hashedType, hashedDesc, hashedWeb, hasehdImg);
        }
        public static LoginData AES_256_decrypt_Login_Data(CryptedLoginData cld, string pKey)
        {
            Aes aes = Aes.Create();
            aes.Key = generateNew(pKey, false).hashedText;
            aes.IV = cld.IV;
            ICryptoTransform ict = aes.CreateDecryptor();
            byte[] Name, Pwd, type, desc, website, imguri;
            try
            {
                Name = ict.TransformFinalBlock(cld.hashedName, 0, cld.hashedName.Length);
                ict = aes.CreateDecryptor();
                Pwd = ict.TransformFinalBlock(cld.hashedPwd, 0, cld.hashedPwd.Length);
                ict = aes.CreateDecryptor();
                type = ict.TransformFinalBlock(cld.hashedType, 0, cld.hashedType.Length);
                ict = aes.CreateDecryptor();
                desc = ict.TransformFinalBlock(cld.hashedDesc, 0, cld.hashedDesc.Length);
                ict = aes.CreateDecryptor();
                website = ict.TransformFinalBlock(cld.hashedWebsite, 0, cld.hashedWebsite.Length);
                ict = aes.CreateDecryptor();
                imguri = ict.TransformFinalBlock(cld.hashedImgUri, 0, cld.hashedImgUri.Length);
            }
            catch
            {
                Name = "hahahahaha".ToByteArray();
                Pwd = "haste gedacht :)".ToByteArray();
                type = "ll".ToByteArray();
                desc = "ajhdjsad".ToByteArray();
                website = "aksjdad".ToByteArray();
                imguri = "asjdsad".ToByteArray();
            }
            return new LoginData(Name.FromByteArray(), Pwd.FromByteArray(), type.FromByteArray(), desc.FromByteArray(), website.FromByteArray(), imguri.FromByteArray()); ;
        }
    }
}
