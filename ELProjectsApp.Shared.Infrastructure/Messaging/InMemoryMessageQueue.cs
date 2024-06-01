using ELProjectsApp.Shared.Abstractions.Events;
using System.Threading.Channels;

namespace ELProjectsApp.Shared.Infrastructure.Messaging;

internal sealed class InMemoryMessageQueue
{
    private readonly Channel<IEvent> _channel =
        Channel.CreateUnbounded<IEvent>();

    public ChannelReader<IEvent> Reader => _channel.Reader;

    public ChannelWriter<IEvent> Writer => _channel.Writer;
}
