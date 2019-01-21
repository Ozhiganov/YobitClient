using System.IO;
using System.Reflection;

namespace YobitClient.Nonce
{
    public class NonceService
    {
        private readonly NonceFileReader _nonceFileReader;
        private readonly NonceFileWriter _nonceFileWriter;

        public NonceService(NonceFileWriter nonceFileWriter, NonceFileReader nonceFileReader)
        {
            _nonceFileWriter = nonceFileWriter;
            _nonceFileReader = nonceFileReader;
        }

        public string NonceFolderPath { get; set; } = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        public long GenerateNonce(string exchangeKey)
        {
            var nonceFile = _nonceFileWriter.CreateNonceFile(exchangeKey, NonceFolderPath);
            var nextNonce = _nonceFileReader.NextNonce(nonceFile);
            _nonceFileWriter.UpdateNonce(nonceFile, nextNonce);
            return nextNonce;
        }
    }
}