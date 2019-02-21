using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace TestContainers.Containers
{
    public interface IContainer
    {
        /// <summary>
        /// Gets the image name
        /// </summary>
        [NotNull]
        string DockerImageName { get; }

        /// <summary>
        /// Port bindings to create for the container. The port must also be exposed by Exposed ports.
        /// Dictionary&lt;int ExposedPort, int PortBinding&gt;
        /// </summary>
        [NotNull]
        Dictionary<int, int> PortBindings { get; }

        // todo: implement extra hosts
//        /// <summary>
//        /// Dictionary&lt;int Hostname, int IpAddress&gt;
//        /// </summary>
//        Dictionary<int, int> ExtraHosts { get; }

        /// <summary>
        /// Environment variables to be injected into the container
        /// Dictionary&lt;int key, int value&gt;
        /// </summary>
        [NotNull]
        Dictionary<string, string> Env { get; }

        // todo: implement labels
        /// <summary>
        /// Dictionary&lt;int key, int value&gt;
        /// </summary>
//        Dictionary<string, string> Labels { get; }

        /// <summary>
        /// List of ports to be exposed on the container
        /// These ports will be automatically mapped to a higher port upon container start
        /// Use <see cref="GetMappedPort"/> to retrieve the automatically mapped port 
        /// </summary>
        [NotNull]
        IList<int> ExposedPorts { get; }

        // todo: implement mounts
        //List<Mount> Mounts { get; }

        // todo: implement commands
        //List<string> Commands { get; }

        /// <summary>
        /// Starts the container
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>A task that completes when the container fully started</returns>
        Task StartAsync(CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Stops the container
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>A task that completes when the container fully stops</returns>
        Task StopAsync(CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Gets a network host address for this docker instance
        /// </summary>
        /// <returns>The network host for this docker instance</returns>
        string GetDockerHostIpAddress();

        /// <summary>
        /// Gets an mapped port from an exposed port
        /// </summary>
        /// <param name="exposedPort">Exposed port to map</param>
        /// <returns>The mapped port</returns>
        int GetMappedPort(int exposedPort);

        /// <summary>
        /// Executes a command against the container
        /// </summary>
        /// <param name="command">The command and its parameters to run</param>
        /// <returns>Tuple containing the response of the command</returns>
        Task<(string stdout, string stderr)> ExecuteCommand(params string[] command);
    }
}