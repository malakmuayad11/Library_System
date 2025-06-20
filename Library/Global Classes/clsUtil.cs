using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Library
{
    /// <summary>
    /// This class contains multiple random methods for the presentation layer.
    /// </summary>
    public class clsUtil
    {
        
        public static string Key = "1234567890123456";
        public static string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\Library";

        /// <summary>
        /// Methods generate a new GUID.
        /// </summary>
        /// <returns>GUID in string format.</returns>
        /// 
        public static string CreateGUID()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        /// <summary>
        /// The method checks if a specific file exits, if not, it will create a new one.
        /// </summary>
        /// <param name="FolderPath">The folder path to be created if not exists.</param>
        /// <returns>Whether this process succeeded or not.</returns>
        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                }
                catch(IOException ex)
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// This method replaces a specific file with a GUID. 
        /// In this project, this method will be used to store 
        /// the member's image in a folder that contains the members'
        /// images paths replaced with a GUID.
        /// </summary>
        /// <param name="SourceFile">The file path to be replaced with a GUID.</param>
        /// <returns>The file's path replaced with a GUID.</returns>
        public static string ReplaceFileNameWithGUID(string SourceFile)
        {
            string FileName = SourceFile;
            FileInfo fileInfo = new FileInfo(FileName);
            string extension = fileInfo.Extension;
            return CreateGUID() + extension;
        }

        /// <summary>
        /// This method will copy the Image path (after replacing it with a GUID)
        /// to the Project's images folder.
        /// </summary>
        /// <param name="SourceFile">The image path to be saved in the project's folder.</param>
        /// <returns>Wether copying the image to the project's folder succedeed or not.</returns>
        public static bool CopyImageToProjectImagesFolder(ref string SourceFile)
        {
            string DestinationFolder = "C:\\Library-Members-Images\\";
            if (!CreateFolderIfDoesNotExist(DestinationFolder))
                return false;
            string destinationFile = DestinationFolder + ReplaceFileNameWithGUID(SourceFile);
            try
            {
                File.Copy(SourceFile, destinationFile, true);
            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsLogger.Log(iox.Message, EventLogEntryType.Error);
                return false;
            }
            SourceFile = destinationFile;
            return true;
        }

        /// <summary>
        /// This method generates a hexadecimal hash string of the input.
        /// </summary>
        /// <param name="input">The string to be hashed.</param>
        /// <returns>The hash signature of the input.</returns>
        public static string ComputeHash(string input)
        {
            using (SHA256 sHA256 = SHA256.Create())
            {
                StringBuilder sb = new StringBuilder();
                Byte[] hashBytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(input));
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Encrypts the specified input using a key. It uses the AES method of symmetric encryption.
        /// </summary>
        /// <param name="plainText">The text to be encrypted.</param>
        /// <param name="Key">The used key for symmetric encryption.</param>
        /// <returns>The input after encrypting it.</returns>
        public static string Encrypt_AES(string plainText, string Key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = new byte[aes.BlockSize / 8];
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// Decrypts the specified input using a key. It uses the AES method of symmetric decryption.
        /// </summary>
        /// <param name="cipherText">The text to be decrypted.</param>
        /// <param name="Key">The used key for symmetric decryption.</param>
        /// <returns>the input after decrypting it.</returns>
        public static string Decrypt_AES(string cipherText, string Key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = new byte[aes.BlockSize / 8];
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (var msdecrypt = new System.IO.MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (var csdecrypt = new CryptoStream(msdecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srdecrypt = new System.IO.StreamReader(csdecrypt))
                        {
                            return srdecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method save the specified inputs in the windows registry, in the current user.
        /// </summary>
        /// <param name="Username">The username to be stored in the current user.</param>
        /// <param name="Password">The password to be stored in the current user, it will
        /// be symmetrically encrypted using the AES.</param>
        public static void RememberUsernameAndPassword(string Username, string Password)
        {
            try
            {
                Registry.SetValue(KeyPath, "Username", Username);
                Registry.SetValue(KeyPath, "Password", Encrypt_AES(Password, Key));
            }
            catch(Exception ex)
            {
                clsLogger.Log(ex.Message, EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// This methods returns the stored username and password in the current user in the
        /// windows Registry. You should add null checks and decrypt password before using them.
        /// </summary>
        /// <param name="Username">The stored username in the registry, null check must be added.</param>
        /// <param name="Password">The stored password in the registry, null check must be 
        /// added, and decrypt it.</param>
        public static void GetUsernameAndPassword(ref string Username, ref string Password)
        {
            try
            {
                Username = Registry.GetValue(KeyPath, "Username", null) as string;
                Password = Registry.GetValue(KeyPath, "Password", null) as string;
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex.Message, EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// This method writes the specified inputs in the specified location in the windows registry.
        /// </summary>
        /// <param name="KeyPath">The path of the desired registry key.</param>
        /// <param name="KeyName">The name of the sub key.</param>
        /// <param name="KeyValue">The value of the sub key.</param>
        public static void WriteInRegistry(string KeyPath, string KeyName, string KeyValue)
        {
            try
            {
                Registry.SetValue(KeyPath, KeyName, KeyValue);
            }
            catch(Exception ex)
            {
                clsLogger.Log(ex.Message, EventLogEntryType.Error);
            }
        }
    }
}
