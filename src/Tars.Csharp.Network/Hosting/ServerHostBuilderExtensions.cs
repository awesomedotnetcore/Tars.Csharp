﻿using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Tars.Csharp.Network.Hosting
{
    public static class ServerHostBuilderExtensions
    {
        public static ServerHostBuilder UseLibuvTcp(this ServerHostBuilder builder, Action<IServiceProvider, IChannelPipeline> configureDelegate = null)
        {
            return builder.ConfigureServices(x => x.AddSingleton<IServerHost, LibuvTcpServerHost>()
                .AddSingleton<IChannelHandler>(i => new ActionChannelInitializer<ISocketChannel>(j => configureDelegate?.Invoke(i, j.Pipeline))));
        }

        public static ServerHostBuilder UseTcp(this ServerHostBuilder builder, Action<IServiceProvider, IChannelPipeline> configureDelegate = null)
        {
            return builder.ConfigureServices(x => x.AddSingleton<IServerHost, TcpServerHost>()
                .AddSingleton<IChannelHandler>(i => new ActionChannelInitializer<ISocketChannel>(j => configureDelegate?.Invoke(i, j.Pipeline))));
        }

        public static ServerHostBuilder UseUdp(this ServerHostBuilder builder, Action<IServiceProvider, IChannelPipeline> configureDelegate = null)
        {
            return builder.ConfigureServices(x => x.AddSingleton<IServerHost, UdpServerHost>()
                .AddSingleton<IChannelHandler>(i => new ActionChannelInitializer<IChannel>(j => configureDelegate?.Invoke(i, j.Pipeline))));
        }
    }
}