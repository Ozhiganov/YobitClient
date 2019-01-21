using System.IO;

namespace YobitClient.Nonce
{
    public class NonceFileWriter
    {
        private string BuildNonceFileAbsolutePath(string exchangeKey, string nonceFolderPath)
        {
            return Path.Combine(nonceFolderPath, exchangeKey + ".txt");
        }

        public FileInfo CreateNonceFile(string exchangeKey, string nonceFolderPath)
        {
            var fileAbsolutePath = BuildNonceFileAbsolutePath(exchangeKey, nonceFolderPath);
            var nonceFile = new FileInfo(fileAbsolutePath);
            if (!nonceFile.Exists)
                nonceFile.Create();
            return nonceFile;
        }

        public void UpdateNonce(FileInfo nonceFile, long nonce)
        {
            File.WriteAllText(nonceFile.FullName, nonce.ToString());
        }
    }
}