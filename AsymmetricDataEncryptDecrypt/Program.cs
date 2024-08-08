// See https://aka.ms/new-console-template for more information


using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048)) {
            try
            {
                string publicKey = rsa.ToXmlString(false);
                string privateKey = rsa.ToXmlString(true);

                Console.WriteLine("the public key is" + publicKey);
                Console.WriteLine("the private key is"+ privateKey);

                string dataToEncrypt = "Hello Encrypt Data";

                byte[] dataToEncryptBytes = Encoding.UTF8.GetBytes(dataToEncrypt);
                byte[] encryptedData = EncryptData(dataToEncryptBytes, publicKey);
                Console.WriteLine("The encrypted data is" + encryptedData);

                byte[] datatoDectyptBytes = DecryptData(encryptedData,privateKey);
                string decrypteData = Encoding.UTF8.GetString(datatoDectyptBytes);

                Console.WriteLine("The decrypted data is" + decrypteData);

            }
            catch (Exception ex) { 
                string msg = ex.Message;
            }
        }
    }

    public static byte[] EncryptData(byte[] dataToEncrypt,string publicKey)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider()) 
        { 
            rsa.FromXmlString(publicKey);
            return rsa.Encrypt(dataToEncrypt, false);
        }
    }

    public static byte[] DecryptData(byte[] dataToDecrypt, string privateKey) 
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider()) { 
            rsa.FromXmlString(privateKey);
            return rsa.Decrypt(dataToDecrypt, false);
        }
    }
}