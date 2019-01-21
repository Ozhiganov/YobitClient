using System.IO;
using System.Text;

namespace YobitClient.Nonce
{
    public class NonceFileReader
    {
        public long NextNonce(FileInfo nonceFile)
        {
            return CurrentNonce(nonceFile) + 1;
        }

        public long CurrentNonce(FileInfo nonceFile)
        {
            var currentNonce = File.ReadAllText(nonceFile.FullName, Encoding.UTF8);
            return long.TryParse(currentNonce, out var nonce) ? nonce : 0;
        }
    }
}