using System.Threading.Channels;
using Sync.BL.Models;

namespace Sync.BL.HostedServiceQueue
{
    public interface IThreadSafeWriter
    {
        Task WriteAsync(Value valueObject);
    }

    public interface IThreadSafeReader
    {
        Task<Value> ReadAsync();
    }

    public class DbWriterQueue: IThreadSafeWriter, IThreadSafeReader
    {
        private readonly ChannelReader<Value> _channelReader;
        private readonly ChannelWriter<Value> _channelWriter;
    
        private readonly Channel<Value> _channel = Channel.CreateBounded<Value>(new BoundedChannelOptions(50)
        {
            SingleReader = true,
            SingleWriter = true
        });

        public DbWriterQueue()
        {
            _channelReader = _channel.Reader;
            _channelWriter = _channel.Writer;
        }
    
        public async Task WriteAsync(Value valueObject)
        {
            await _channelWriter.WriteAsync(valueObject);
        }
        public async Task<Value> ReadAsync()
        {
            return  await _channelReader.ReadAsync();
        }
    }
}